﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Digiwin.Common;
using Digiwin.Common.Advanced;
using Digiwin.Common.Query2;
using Digiwin.Common.Services;
using Digiwin.Common.Torridity;
using Digiwin.Common.Torridity.Metadata;
using Digiwin.ERP.Common.Business;
using Digiwin.ERP.Common.Utils;

namespace Digiwin.ERP.XTEST.Business.Implement {
    public class Tools {
        private IResourceServiceProvider _resourceServiceProvider;
        private ServiceCallContext _serviceCallContext;

        public IResourceServiceProvider Provider {
            get { return _resourceServiceProvider; }
            set { _resourceServiceProvider = value; }
        }

        public ServiceCallContext CallContext {
            get { return _serviceCallContext; }
            set { _serviceCallContext = value; }
        }

        public Tools(IResourceServiceProvider ResourceServiceProvider, ServiceCallContext ServiceCallContext) {
            this.Provider = ResourceServiceProvider;
            this.CallContext = ServiceCallContext;
        }


        public void creatTmp(string TypeKey, out DataTable dt, out DependencyObjectType targetType) {
            dt = null;
            targetType = null;
            string[] spiltTypeKeys = null;
            bool isColls = false;
            string primaryKey = string.Empty;
            if (TypeKey.Contains(@".")) {
                spiltTypeKeys = TypeKey.Split(new[] {'.'});
                TypeKey = spiltTypeKeys[0];
                primaryKey = spiltTypeKeys[spiltTypeKeys.Length - 2];
                isColls = true;
            }

            ICreateService createSrv = Provider.GetService(typeof (ICreateService), TypeKey) as ICreateService;
            if (createSrv == null) {
                return;
            }
            var entity = createSrv.Create() as DependencyObject;
            DependencyObjectType toType = entity.DependencyObjectType;
            if (isColls && spiltTypeKeys != null) {
                spiltTypeKeys.ToList().ForEach(key => {
                    if (!key.Equals(TypeKey)) {
                        toType =
                            ((ICollectionProperty) (toType.Properties[key])).ItemDataEntityType as DependencyObjectType;
                    }
                });
            }
            IBusinessTypeService businessTypeSrv =
                Provider.GetService(typeof (IBusinessTypeService), CallContext.TypeKey) as IBusinessTypeService;
            targetType = RegiesterType(toType, targetType);
            if (isColls) {
                string primaryKeyName = primaryKey + "_ID";

                targetType.RegisterSimpleProperty(primaryKeyName, businessTypeSrv.SimplePrimaryKeyType,
                    null, false, new Attribute[] {
                        businessTypeSrv.SimplePrimaryKey
                    });
            }

            dt = CreateDt(targetType, dt);
        }


        /// <summary>
        /// 获取下一个单号
        /// </summary>
        /// <param name="documentNumberGenSrv">单号生成服务</param>
        /// <param name="docNo">当前单号,如果传入单号为空，则会调用单号生成服务获取单号</param>
        /// <param name="docID">单据类型</param>
        /// <param name="sequenceDigit">单据类型流水号位数</param>
        /// <param name="date">单据日期</param>
        /// <returns></returns>
        public string NextNumber(IDocumentNumberGenerateService documentNumberGenSrv, string docNo
            , object docID, int sequenceDigit, DateTime date) {
            if (docNo == string.Empty) {
                docNo = documentNumberGenSrv.NextNumber(docID, date);
            }
            else {
                if (docNo.Length > sequenceDigit) {
                    docNo = docNo.Substring(0, docNo.Length - sequenceDigit)
                            +
                            (docNo.Substring(docNo.Length - sequenceDigit, sequenceDigit).ToInt32() + 1)
                                .ToStringExtension().PadLeft(sequenceDigit, '0');
                }
            }

            return docNo;
        }

        /// <summary>
        /// 例如,根据colls 插入到表，colls应与表字段一致
        /// </summary>
        /// <param name="qurService"></param>
        /// <param name="businessTypeSrv"></param>
        /// <param name="entityD"></param>
        /// <param name="TableName"></param>
        private void Insert(IQueryService qurService,
            DependencyObjectCollection colls, string typeKey) {
            var ItemDependencyObjectType = colls.ItemDependencyObjectType;
            //创建临时表
            DependencyObjectType tmpType = RegiesterType(ItemDependencyObjectType, null);
            qurService.CreateTempTable(tmpType);
            //创建DataTable
            var TempDt = new DataTable();
            TempDt = CreateDt(ItemDependencyObjectType, TempDt);
            DOCToDataTable(colls, TempDt);
            //插入临时表
            List<QueryProperty> propies = new List<QueryProperty>();
            InsertTemp(qurService, TempDt, tmpType.Name);

            if (TempDt == null) {
                return;
            }
            foreach (DataColumn row in TempDt.Columns) {
                propies.Add(OOQL.CreateProperty(row.ColumnName, row.ColumnName));
            }

            if (propies != null && propies.Count > 0) {
                QueryNode node = OOQL.Select(propies
                    ).From(tmpType.Name);
                node = OOQL.Insert(typeKey, node, propies.Select(c => c.Alias).ToArray());
                qurService.ExecuteNoQueryWithManageProperties(node);
            }
        }


        /// <summary>
        /// 将DataTable填充到临时表 
        /// </summary>
        /// <param name="qurService"></param>
        /// <param name="dt">DataTable</param>
        /// <param name="tname">表明</param>
        /// <param name="propies">插入字段</param>
        public void InsertTemp(IQueryService qurService, DataTable dt, string tname
            ) {
            List<BulkCopyColumnMapping> mappingList = new List<BulkCopyColumnMapping>();

            foreach (DataColumn dcScan in dt.Columns) {
                var targetName = dcScan.ColumnName; //列名
                ////列名中的下划线大于0，且以[_RTK]或[_ROid]结尾的列名视为多来源字段
                //if ((targetName.IndexOf("_") > 0)
                //    && (targetName.EndsWith("_RTK", StringComparison.CurrentCultureIgnoreCase)
                //        || targetName.EndsWith("_ROid", StringComparison.CurrentCultureIgnoreCase))) {
                //    //列名长度
                //    var nameLength = targetName.Length;
                //    //最后一个下划线后一位位置
                //    var endPos = targetName.LastIndexOf("_") + 1;
                //    //拼接目标字段名
                //    targetName = targetName.Substring(0, endPos - 1) + "." +
                //                 targetName.Substring(endPos, nameLength - endPos);
                //}
                mappingList.Add(new BulkCopyColumnMapping(dcScan.ColumnName, targetName));
            }
            //3.0 不支持直接将dt 插入到实体表中,可借用临时表插入
            qurService.BulkCopy(dt, tname, mappingList.ToArray());
        }

        //参考标准MO
        /// <summary>
        ///  根据DependencyObjectType创建簡單临时表结构
        /// </summary>
        /// <param name="formType">DependencyObject屬性</param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public DependencyObjectType RegiesterType(
            DependencyObjectType formType, DependencyObjectType targetType) {
            IBusinessTypeService businessTypeSrv =
                Provider.GetService(typeof (IBusinessTypeService), CallContext.TypeKey) as IBusinessTypeService;
            SimplePropertyAttribute stringAttr = new SimplePropertyAttribute(GeneralDBType.String);
            SimplePropertyAttribute dtAttr = new SimplePropertyAttribute(GeneralDBType.DateTime);
            SimplePropertyAttribute IntAttr = new SimplePropertyAttribute(GeneralDBType.Int32);
            if (targetType == null) {
                string tempTableName = "tmpYC_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                targetType = new DependencyObjectType(tempTableName, new Attribute[] {});
            }

            if (formType == null) {
                return null;
            }
            foreach (var prop in formType.Properties) {
                //临时表字段名不可大于30个字符，不是处理方式，后续补充
                string prop_name = prop.Name.Length > 30 ? prop.Name.Substring(0, 29) : prop.Name;


                var isExist = targetType.Properties.FirstOrDefault(p => p.Name.Equals(prop_name));
                if (isExist != null) {
                    continue;
                }
                //過濾管理字段 //批量插入有影响
                string[] cnames = {
                    "Version",
                    "CreateDate", "LastModifiedDate", "ModifiedDate", "CreateBy",
                    "LastModifiedBy", "ModifiedBy", "ApproveStatus", "ApproveDate", "ApproveBy"
                };
                if (cnames.Contains(prop_name)) {
                    continue;
                }

                Type prop_type = prop.PropertyType;

                if (prop_type == typeof (DependencyObject) ||
                    prop_type == typeof (DependencyObjectCollection)) {
                    // 本次个案需要
                    try {
                        var flag = prop is IComplexProperty;
                        if (flag) {
                            if (((IComplexProperty) (prop)).DataEntityType.Name == "ReferToEntity") {
                                var cpropies = ((IComplexProperty) (prop)).DataEntityType.SimpleProperties;

                                if (cpropies != null) {
                                    var existRTK =
                                        cpropies.ToList()
                                            .FirstOrDefault(
                                                cprop => cprop.Name.Equals("ROid") || cprop.Name.Equals("RTK"));
                                    if (existRTK != null) {
                                        string rtk_name = prop_name + "_RTK";
                                        string roid_name = prop_name + "_ROid";
                                        targetType.RegisterSimpleProperty(rtk_name, typeof (string),
                                            string.Empty, false, new Attribute[] {
                                                stringAttr
                                            });

                                        targetType.RegisterSimpleProperty(roid_name,
                                            businessTypeSrv.SimplePrimaryKeyType,
                                            null, false, new Attribute[] {
                                                businessTypeSrv.SimplePrimaryKey
                                            });
                                    }
                                }
                            }
                        }
                    }
                    catch {
                    }
                }

                else if (prop_name.StartsWith("UDF")) {
                    continue;
                }

                else {
                    object DEFv = prop.DefaultValue;

                    if (prop_name.EndsWith("_ID")) {
                        targetType.RegisterSimpleProperty(prop_name, businessTypeSrv.SimplePrimaryKeyType,
                            DEFv, false, new Attribute[] {
                                businessTypeSrv.SimplePrimaryKey
                            });
                    }
                    else if (prop_name.Equals("REMARK")) {
                        targetType.RegisterSimpleProperty(prop_name, businessTypeSrv.SimpleRemarkType,
                            DEFv, false, new Attribute[] {
                                businessTypeSrv.SimpleRemark
                            });
                    }
                    else {
                        string typeName = prop_type.ToString();
                        switch (typeName) {
                            case "System.String":
                                targetType.RegisterSimpleProperty(prop_name, prop_type,
                                    DEFv, false, new Attribute[] {stringAttr});
                                break;
                            case "System.DateTime":
                                targetType.RegisterSimpleProperty(prop_name, prop_type,
                                    DEFv, false, new Attribute[] {dtAttr});
                                break;
                            case "System.Int16": //整型
                            case "System.Int32":
                            case "System.Int64":
                                targetType.RegisterSimpleProperty(prop_name, prop_type,
                                    DEFv, false, new Attribute[] {IntAttr});
                                break;
                            case "System.Decimal": //浮点型
                            case "System.Double":
                                targetType.RegisterSimpleProperty(prop_name, businessTypeSrv.SimpleQuantityType,
                                    DEFv, false, new Attribute[] {businessTypeSrv.SimpleQuantity});
                                break;
                            default:
                                targetType.RegisterSimpleProperty(prop_name, prop_type,
                                    DEFv, false, new Attribute[] {});
                                break;
                        }
                    }
                }
            }
            return targetType;
        }


        /// <summary>
        /// DependencyObjectCollection 構建targetTable，单身需独立调用
        /// </summary>
        /// <param name="fromType"></param>
        /// <param name="targetTable"></param>
        public DataTable CreateDt(DependencyObjectType fromType, DataTable targetTable) {
            if (fromType == null) {
                return null;
            }
            DependencyPropertyCollection DPC = fromType.Properties;

            if (targetTable == null) {
                targetTable = new DataTable();
            }

            try {
                foreach (DependencyProperty prop in DPC) {
                    Type prop_type = prop.PropertyType;
                    var targetName = prop.Name; //列名
                    var isExist = false;
                    foreach (DataColumn column in targetTable.Columns) {
                        isExist = column.ColumnName.Equals(targetName);
                        continue;
                    }

                    if (isExist) {
                        continue;
                    }


                    //過濾管理字段 //批量插入有影响
                    string[] cnames = {
                        "Version",
                        "CreateDate", "LastModifiedDate", "ModifiedDate", "CreateBy",
                        "LastModifiedBy", "ModifiedBy", "ApproveStatus", "ApproveDate", "ApproveBy"
                    };
                    if (cnames.Contains(prop.Name)) {
                        continue;
                    }

                    //处理集合字段
                    if (prop_type == typeof (DependencyObjectCollection)
                        || prop_type == typeof (DependencyObject)
                        ) {
                        if (((IComplexProperty) (prop)).DataEntityType.Name == "ReferToEntity") {
                            var cpropies = ((IComplexProperty) (prop)).DataEntityType.SimpleProperties;
                            if (cpropies != null) {
                                var existRTK =
                                    cpropies.ToList()
                                        .FirstOrDefault(cprop => cprop.Name.Equals("ROid") || cprop.Name.Equals("RTK"));
                                if (existRTK != null) {
                                    string rtk_name = targetName + "_RTK";
                                    string roid_name = targetName + "_ROid";
                                    targetTable.Columns.Add(rtk_name, typeof (string));
                                    targetTable.Columns.Add(roid_name, typeof (object));
                                }
                            }
                            else {
                                continue;
                            }
                        }
                    }
                    else if (targetName.StartsWith("UDF")) {
                        continue;
                    }
                    else {
                        targetTable.Columns.Add(prop.Name, prop_type);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return targetTable;
        }

        /// <summary>
        /// 手动指定列名、类型 注册临时表
        /// </summary>
        /// <param name="businessTypeSrv"></param>
        /// <param name="formType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public DependencyObjectType RegiesterType(
            List<RegiesterTypeParameter> formType, DependencyObjectType targetType) {
            if (targetType == null) {
                string tempTableName = "tmpYC_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                targetType = new DependencyObjectType(tempTableName, new Attribute[] {});
            }
            IBusinessTypeService businessTypeSrv =
                Provider.GetService(typeof (IBusinessTypeService), CallContext.TypeKey) as IBusinessTypeService;
            SimplePropertyAttribute stringAttr = new SimplePropertyAttribute(GeneralDBType.String);
            SimplePropertyAttribute dtAttr = new SimplePropertyAttribute(GeneralDBType.DateTime);
            SimplePropertyAttribute IntAttr = new SimplePropertyAttribute(GeneralDBType.Int32);
            if (formType == null) {
                return null;
            }

            formType.ToList().ForEach(prop => {
                Type prop_type = prop.Type;
                var prop_names = prop.Properties;
                foreach (var prop_name in prop_names) {
                    string typeName = prop_type.ToString();
                    var isExist = targetType.Properties.FirstOrDefault(p => p.Name.Equals(prop_name));
                    if (isExist != null) {
                        continue;
                    }
                    if (prop_name.EndsWith("_ID")) {
                        targetType.RegisterSimpleProperty(prop_name, businessTypeSrv.SimplePrimaryKeyType,
                            Guid.Empty, false, new Attribute[] {
                                businessTypeSrv.SimplePrimaryKey
                            });
                    }
                    else if (prop_name.Equals("REMARK")) {
                        targetType.RegisterSimpleProperty(prop_name, businessTypeSrv.SimpleRemarkType,
                            string.Empty, false, new Attribute[] {
                                businessTypeSrv.SimpleRemark
                            });
                    }
                    else {
                        switch (typeName) {
                            case "System.String":
                                targetType.RegisterSimpleProperty(prop_name, prop_type,
                                    string.Empty, false, new Attribute[] {stringAttr});
                                break;
                            case "System.DateTime":
                                targetType.RegisterSimpleProperty(prop_name, prop_type,
                                    OrmDataOption.EmptyDateTime, false, new Attribute[] {dtAttr});
                                break;
                            case "System.Int16": //整型
                            case "System.Int32":
                            case "System.Int64":
                                targetType.RegisterSimpleProperty(prop_name, prop_type,
                                    0, false, new Attribute[] {IntAttr});
                                break;
                            case "System.Decimal": //浮点型
                            case "System.Double":
                                targetType.RegisterSimpleProperty(prop_name, businessTypeSrv.SimpleQuantityType,
                                    0m, false, new Attribute[] {businessTypeSrv.SimpleQuantity});
                                break;
                            default:
                                targetType.RegisterSimpleProperty(prop_name, prop_type,
                                    null, false, new Attribute[] {});
                                break;
                        }
                    }
                }
                ;
            });

            return targetType;
        }

        /// <summary>
        /// 指定列名、类型 構建targetTable，单身需独立调用
        /// </summary>
        /// <param name="fromType"></param>
        /// <param name="targetTable"></param>
        public DataTable CreateDt(List<RegiesterTypeParameter> fromType, DataTable targetTable) {
            if (targetTable == null) {
                targetTable = new DataTable();
            }
            if (fromType == null) {
                return null;
            }

            fromType.ForEach(prop => {
                Type prop_type = prop.Type;
                var prop_names = prop.Properties;
                foreach (var prop_name in prop_names) {
                    var isExist = false;
                    foreach (DataColumn column in targetTable.Columns) {
                        isExist = column.ColumnName.Equals(prop_name);
                        continue;
                    }

                    if (isExist) {
                        continue;
                    }
                    targetTable.Columns.Add(prop_name, prop_type);
                }
                ;
            });
            return targetTable;
        }

        /// <summary>
        /// 单身转datatable,根据dataTable列名填充
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public DataTable DOCToDataTable(DependencyObjectCollection formCollection, DataTable targetTable) {
            foreach (DependencyObject item in formCollection) {
                DataRow dr = targetTable.NewRow();

                foreach (DataColumn col in targetTable.Columns) {
                    try {
                        var fristObject =
                            item.DependencyObjectType.Properties.FirstOrDefault(p => p.Name.Equals(col.ColumnName));
                        if (fristObject != null) {
                            dr[col.ColumnName] = item[col.ColumnName];
                        }
                        else {
                            // 转化复杂类型
                            var targetName = col.ColumnName; //列名
                            //列名中的下划线大于0，且以[_RTK]或[_ROid]结尾的列名视为多来源字段
                            if ((targetName.IndexOf("_") > 0)
                                && (targetName.EndsWith("_RTK", StringComparison.CurrentCultureIgnoreCase)
                                    || targetName.EndsWith("_ROid", StringComparison.CurrentCultureIgnoreCase))) {
                                //列名长度
                                var nameLength = targetName.Length;
                                //最后一个下划线后一位位置
                                var endPos = targetName.LastIndexOf("_") + 1;
                                //拼接目标字段名
                                targetName = targetName.Substring(0, endPos - 1);
                                var extName = targetName.Substring(endPos, nameLength - endPos);
                                fristObject =
                                    item.DependencyObjectType.Properties.FirstOrDefault(p => p.Name.Equals(targetName));
                                if (fristObject != null) {
                                    if (fristObject.PropertyType is IComplexProperty &&
                                        (((IComplexProperty) (fristObject)).DataEntityType.Name == "ReferToEntity")) {
                                        dr[col.ColumnName] = (item[targetName] as DependencyObject)[extName];
                                    }
                                }
                            }
                        }
                    }
                    catch {
                    }
                }
                targetTable.Rows.Add(dr);
            }
            return targetTable;
        }

    }

    public class RegiesterTypeParameter {
        private List<string> propertiesFiled = new List<string>();
        private Dictionary<string, Type> nameTypeFiled = new Dictionary<string, Type>();

        /// <summary>
        /// 指定参数
        /// </summary>
        public List<string> Properties {
            get {
                return
                    propertiesFiled;
            }
            set { propertiesFiled = value; }
        }

        public Dictionary<string, Type> NameAndType {
            get {
                return
                    nameTypeFiled;
            }
            set { nameTypeFiled = value; }
        }

        private Type type = typeof (string);

        /// <summary>
        /// 指定类型
        /// </summary>
        public Type Type {
            get {
                return
                    type;
            }
            set { type = value; }
        }

        public RegiesterTypeParameter(List<string> propertiesFiled, Type type) {
            Properties = propertiesFiled;
            this.Type = type;
        }

        public RegiesterTypeParameter(Dictionary<string, Type> NameAndType) {
            this.NameAndType = NameAndType;
        }
    }
}
﻿/*--------------------------------------------------------
 * createDate 20180409
 * createBy 08628
 * version 0.0.0.1
 * remark 注意 如果遇到错误,请自行修正并反馈给负责人员
 *--------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Digiwin.Common;
using Digiwin.Common.Advanced;
using Digiwin.Common.Query2;
using Digiwin.Common.Services;
using Digiwin.Common.Torridity;
using Digiwin.Common.Torridity.Metadata;
using Digiwin.Common.UI;
using Digiwin.ERP.Common.Business;
using Digiwin.ERP.Common.Utils;

// ReSharper disable once CheckNamespace

namespace Digiwin.ERP.XTEST.Business.Implement {
    public class MyTools {
        #region 属性

        private ServiceTools _myService;


        public MyTools(IResourceServiceProvider resourceServiceProvider, ServiceCallContext serviceCallContext) {
            _myService = new ServiceTools(resourceServiceProvider, serviceCallContext);
        }

        public MyTools(IServiceComponentEvents component) {
            _myService = new ServiceTools(component);
        }

        public ServiceTools MyService {
            get { return _myService; }
        }

        public T GetService<T>(string typeKey) where T : class {
            var ser = MyService.GetService<T>(typeKey);
            return ser;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="typeKey"></param>
        /// <param name="insertNode"></param>
        /// <param name="propies"></param>
        /// <param name="qurSer"></param>
        public void CreateInsert(IQueryService qurSer, string typeKey, QueryNode insertNode,
            IEnumerable<QueryProperty> propies) {
            QueryNode node = OOQL.Insert(typeKey, insertNode, propies.Select(c => c.Alias).ToArray());
            qurSer.ExecuteNoQueryWithManageProperties(node);
        }

        public T CreateNode<T>(QueryNode node) where T : QueryNode {
            return (node as T);
        }


        /// <summary>
        ///     返回实体对应的dt与doc的结构 不包含数据
        /// </summary>
        /// <param name="typeKey">作业MO.XX.XX</param>
        /// <param name="dt"></param>
        /// <param name="targetType"></param>
        public void CreatTmp(string typeKey, out DataTable dt, out DependencyObjectType targetType) {
            dt = null;
            targetType = null;
            string[] spiltTypeKeys = null;
            //单身
            bool isColls = false;
            string primaryKey = string.Empty;
            if (typeKey.Contains(@".")) {
                spiltTypeKeys = typeKey.Split(new[] {'.'});
                typeKey = spiltTypeKeys[0];
                primaryKey = spiltTypeKeys[spiltTypeKeys.Length - 2];
                isColls = true;
            }

            var createSrv = GetService<ICreateService>(typeKey);
            if (createSrv == null) {
                return;
            }
            var entity = createSrv.Create() as DependencyObject;
            if (entity != null) {
                DependencyObjectType toType = entity.DependencyObjectType;
                //去单身实体结构
                if (isColls) {
                    spiltTypeKeys.ToList().ForEach(key => {
                        if (!key.Equals(typeKey)) {
                            toType =
                                ((ICollectionProperty) (toType.Properties[key])).ItemDataEntityType as DependencyObjectType;
                        }
                    });
                }
                IBusinessTypeService businessTypeSrv = MyService.BusinessTypeSrv;
                targetType = RegiesterType(toType, null);
                if (isColls) {
                    //附加父主键
                    string primaryKeyName = primaryKey + "_ID";

                    targetType.RegisterSimpleProperty(primaryKeyName, businessTypeSrv.SimplePrimaryKeyType,
                        null, false, new Attribute[] {
                            businessTypeSrv.SimplePrimaryKey
                        });
                }
            }

            dt = CreateDt(targetType, null);
        }


        /// <summary>
        ///     获取下一个单号
        /// </summary>
        /// <param name="documentNumberGenSrv">单号生成服务</param>
        /// <param name="docNo">当前单号,如果传入单号为空，则会调用单号生成服务获取单号</param>
        /// <param name="docId">单据类型</param>
        /// <param name="sequenceDigit">单据类型流水号位数</param>
        /// <param name="date">单据日期</param>
        /// <returns></returns>
        public string NextNumber(IDocumentNumberGenerateService documentNumberGenSrv, string docNo
            , object docId, int sequenceDigit, DateTime date) {
            if (docNo == string.Empty) {
                docNo = documentNumberGenSrv.NextNumber(docId, date);
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
        ///     根據DOC 創建並返回 臨時表 及dt 包含数据
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="tmpType"></param>
        /// <param name="tempDt"></param>
        // ReSharper disable once UnusedMember.Local
        public void CreateTmp(DependencyObjectCollection datas, out DependencyObjectType tmpType, out DataTable tempDt) {
            try {
                DependencyObjectType itemDependencyObjectType = datas.ItemDependencyObjectType;
                //创建临时表
                tmpType = RegiesterType(itemDependencyObjectType, null);
                tempDt = new DataTable();
                if (tmpType == null) {
                    return;
                }
                IQueryService qurService = MyService.QuerySrv;
                qurService.CreateTempTable(tmpType);
                //创建DataTable
                tempDt = CreateDt(itemDependencyObjectType, tempDt);
                DOCToDataTable(datas, tempDt);
                //插入临时表
                InsertTemp(qurService, tempDt, tmpType.Name);
            }
            catch (Exception ex) {
                throw new BusinessRuleException("創建臨時表出錯" + ex.Message);
            }
        }


        /// <summary>
        ///     将DataTable填充到临时表
        /// </summary>
        /// <param name="qurService"></param>
        /// <param name="dt">DataTable</param>
        /// <param name="tname">表明</param>
        public void InsertTemp(IQueryService qurService, DataTable dt, string tname
            ) {
            #region bak

            //var mappingList = new List<BulkCopyColumnMapping>();

            //foreach (DataColumn dcScan in dt.Columns)
            //{
            //    var targetName = dcScan.ColumnName; //列名
            //    ////列名中的下划线大于0，且以[_RTK]或[_ROid]结尾的列名视为多来源字段
            //    //if ((targetName.IndexOf("_") > 0)
            //    //    && (targetName.EndsWith("_RTK", StringComparison.CurrentCultureIgnoreCase)
            //    //        || targetName.EndsWith("_ROid", StringComparison.CurrentCultureIgnoreCase))) {
            //    //    //列名长度
            //    //    var nameLength = targetName.Length;
            //    //    //最后一个下划线后一位位置
            //    //    var endPos = targetName.LastIndexOf("_") + 1;
            //    //    //拼接目标字段名
            //    //    targetName = targetName.Substring(0, endPos - 1) + "." +
            //    //                 targetName.Substring(endPos, nameLength - endPos);
            //    //}
            //    mappingList.Add(new BulkCopyColumnMapping(dcScan.ColumnName, targetName));
            //}
            ////3.0 不支持直接将dt 插入到实体表（例如MO）中,可借用临时表插入（插入临时表,再由临时表插入MO）
            //qurService.BulkCopy(dt, tname, mappingList.ToArray()); 

            #endregion

            //3.0 不支持直接将dt 插入到实体表（例如MO）中，报错 xx不可为集合属性，如父主键
            //可借用临时表插入（插入临时表,再由临时表插入MO）

            qurService.BulkCopy(dt, tname,
                (from DataColumn dcScan in dt.Columns
                    let targetName = dcScan.ColumnName
                    select new BulkCopyColumnMapping(dcScan.ColumnName, targetName)).ToArray());
        }

        //参考标准MO
        /// <summary>
        ///     根据DependencyObjectType创建簡單临时表结构
        /// </summary>
        /// <param name="formType">DependencyObject屬性</param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public DependencyObjectType RegiesterType(
            DependencyObjectType formType, DependencyObjectType targetType) {
            IBusinessTypeService businessTypeSrv = MyService.BusinessTypeSrv;
            var stringAttr = new SimplePropertyAttribute(GeneralDBType.String);
            var dtAttr = new SimplePropertyAttribute(GeneralDBType.DateTime);
            var intAttr = new SimplePropertyAttribute(GeneralDBType.Int32);
            if (targetType == null) {
                string tempTableName = "tmpYC_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                targetType = new DependencyObjectType(tempTableName, new Attribute[] {});
            }

            if (formType == null) {
                return null;
            }
            foreach (DependencyProperty prop in formType.Properties) {
                //临时表字段名不可大于30个字符，不是处理方式，后续补充
                string propName = prop.Name.Length > 30 ? prop.Name.Substring(0, 29) : prop.Name;


                DependencyProperty isExist = targetType.Properties.FirstOrDefault(p => p.Name.Equals(propName));
                if (isExist != null) {
                    continue;
                }
                //過濾管理字段 //批量插入有影响
                string[] cnames = {
                    "Version",
                    "CreateDate", "LastModifiedDate", "ModifiedDate", "CreateBy",
                    "LastModifiedBy", "ModifiedBy", "ApproveStatus", "ApproveDate", "ApproveBy"
                };
                if (cnames.Contains(propName)) {
                    continue;
                }

                Type propType = prop.PropertyType;

                if (propType == typeof (DependencyObject)
                    ||
                    propType == typeof (DependencyObjectCollection)) {
                    // 本次个案需要
                    try {
                        bool flag = prop is IComplexProperty;
                        if (flag) {
                            if (((IComplexProperty) (prop)).DataEntityType.Name == "ReferToEntity") {
                                ISimplePropertyCollection cpropies =
                                    ((IComplexProperty) (prop)).DataEntityType.SimpleProperties;

                                if (cpropies != null) {
                                    ISimpleProperty existRtk =
                                        cpropies.ToList()
                                            .FirstOrDefault(
                                                cprop => cprop.Name.Equals("ROid") || cprop.Name.Equals("RTK"));
                                    if (existRtk != null) {
                                        string rtkName = propName + "_RTK";
                                        string roidName = propName + "_ROid";
                                        targetType.RegisterSimpleProperty(rtkName, typeof (string),
                                            string.Empty, false, new Attribute[] {
                                                stringAttr
                                            });

                                        targetType.RegisterSimpleProperty(roidName,
                                            businessTypeSrv.SimplePrimaryKeyType,
                                            null, false, new Attribute[] {
                                                businessTypeSrv.SimplePrimaryKey
                                            });
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception) {
                    }
                }
                //忽略UDF字段
                //else if (propName.StartsWith("UDF")) {
                //}

                else {
                    object defValue = prop.DefaultValue;

                    if (propName.EndsWith("_ID")) {
                        targetType.RegisterSimpleProperty(propName, businessTypeSrv.SimplePrimaryKeyType,
                            defValue, false, new Attribute[] {
                                businessTypeSrv.SimplePrimaryKey
                            });
                    }
                    else if (propName.Equals("REMARK")) {
                        targetType.RegisterSimpleProperty(propName, businessTypeSrv.SimpleRemarkType,
                            defValue, false, new Attribute[] {
                                businessTypeSrv.SimpleRemark
                            });
                    }
                    else {
                        string typeName = propType.ToString();
                        switch (typeName) {
                            case "System.String":
                                targetType.RegisterSimpleProperty(propName, propType,
                                    defValue, false, new Attribute[] {stringAttr});
                                break;
                            case "System.DateTime":
                                targetType.RegisterSimpleProperty(propName, propType,
                                    defValue, false, new Attribute[] {dtAttr});
                                break;
                            case "System.Int16": //整型
                            case "System.Int32":
                            case "System.Int64":
                                targetType.RegisterSimpleProperty(propName, propType,
                                    defValue, false, new Attribute[] {intAttr});
                                break;
                            case "System.Decimal": //浮点型
                            case "System.Double":
                                targetType.RegisterSimpleProperty(propName, businessTypeSrv.SimpleQuantityType,
                                    defValue, false, new Attribute[] {businessTypeSrv.SimpleQuantity});
                                break;
                            default:
                                targetType.RegisterSimpleProperty(propName, propType,
                                    defValue, false, new Attribute[] {});
                                break;
                        }
                    }
                }
            }
            return targetType;
        }


        /// <summary>
        ///     DependencyObjectCollection 構建targetTable，单身需独立调用
        /// </summary>
        /// <param name="fromType"></param>
        /// <param name="targetTable"></param>
        public DataTable CreateDt(DependencyObjectType fromType, DataTable targetTable) {
            if (fromType == null) {
                return null;
            }
            DependencyPropertyCollection dpc = fromType.Properties;

            if (targetTable == null) {
                targetTable = new DataTable();
            }

            try {
                foreach (DependencyProperty prop in dpc) {
                    Type propType = prop.PropertyType;
                    string targetName = prop.Name; //列名
                    bool isExist = false;
                    foreach (DataColumn column in targetTable.Columns) {
                        isExist = column.ColumnName.Equals(targetName);
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
                    if (propType == typeof (DependencyObjectCollection)
                        || propType == typeof (DependencyObject)
                        ) {
                        if (prop is IComplexProperty
                            && ((IComplexProperty) (prop)).DataEntityType.Name == "ReferToEntity") {
                            ISimplePropertyCollection cpropies =
                                ((IComplexProperty) (prop)).DataEntityType.SimpleProperties;
                            if (cpropies != null) {
                                ISimpleProperty existRtk =
                                    cpropies.ToList()
                                        .FirstOrDefault(cprop => cprop.Name.Equals("ROid") || cprop.Name.Equals("RTK"));
                                if (existRtk != null) {
                                    string rtkName = targetName + "_RTK";
                                    string roidName = targetName + "_ROid";
                                    targetTable.Columns.Add(rtkName, typeof (string));
                                    targetTable.Columns.Add(roidName, typeof (object));
                                }
                            }
                        }
                    }
                     //忽略UDF字段
                    //else if (targetName.StartsWith("UDF")) {
                    //}
                    else {
                        targetTable.Columns.Add(prop.Name, propType);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return targetTable;
        }

        /// <summary>
        ///     手动指定列名、类型 注册临时表
        /// </summary>
        /// <param name="formType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public DependencyObjectType RegiesterType(
            IEnumerable<RegiesterTypeParameter> formType, DependencyObjectType targetType) {
            if (targetType == null) {
                string tempTableName = "tmpYC_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                targetType = new DependencyObjectType(tempTableName, new Attribute[] {});
            }
            var businessTypeSrv = MyService.BusinessTypeSrv;
            var stringAttr = new SimplePropertyAttribute(GeneralDBType.String);
            var dtAttr = new SimplePropertyAttribute(GeneralDBType.DateTime);
            var intAttr = new SimplePropertyAttribute(GeneralDBType.Int32);
            if (formType == null) {
                return null;
            }

            formType.ToList().ForEach(prop => {
                Type propType = prop.Type;
                List<string> propNames = prop.Properties;
                foreach (string propName in propNames) {
                    string typeName = propType.ToString();
                    string name = propName;
                    DependencyProperty isExist = targetType.Properties.FirstOrDefault(p => p.Name.Equals(name));
                    if (isExist != null) {
                        continue;
                    }
                    if (propName.EndsWith("_ID")) {
                        targetType.RegisterSimpleProperty(propName, businessTypeSrv.SimplePrimaryKeyType,
                            Guid.Empty, false, new Attribute[] {
                                businessTypeSrv.SimplePrimaryKey
                            });
                    }
                    else if (propName.Equals("REMARK")) {
                        targetType.RegisterSimpleProperty(propName, businessTypeSrv.SimpleRemarkType,
                            string.Empty, false, new Attribute[] {
                                businessTypeSrv.SimpleRemark
                            });
                    }
                    else {
                        switch (typeName) {
                            case "System.String":
                                targetType.RegisterSimpleProperty(propName, propType,
                                    string.Empty, false, new Attribute[] {stringAttr});
                                break;
                            case "System.DateTime":
                                targetType.RegisterSimpleProperty(propName, propType,
                                    OrmDataOption.EmptyDateTime, false, new Attribute[] {dtAttr});
                                break;
                            case "System.Int16": //整型
                            case "System.Int32":
                            case "System.Int64":
                                targetType.RegisterSimpleProperty(propName, propType,
                                    0, false, new Attribute[] {intAttr});
                                break;
                            case "System.Decimal": //浮点型
                            case "System.Double":
                                targetType.RegisterSimpleProperty(propName, businessTypeSrv.SimpleQuantityType,
                                    0m, false, new Attribute[] {businessTypeSrv.SimpleQuantity});
                                break;
                            default:
                                targetType.RegisterSimpleProperty(propName, propType,
                                    null, false, new Attribute[] {});
                                break;
                        }
                    }
                }
                
            });

            return targetType;
        }

        /// <summary>
        ///     指定列名、类型 構建targetTable，单身需独立调用
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
                Type propType = prop.Type;
                List<string> propNames = prop.Properties;
                foreach (string propName in propNames) {
                    bool isExist = false;
                    foreach (DataColumn column in targetTable.Columns) {
                        isExist = column.ColumnName.Equals(propName);
                    }

                    if (isExist) {
                        continue;
                    }
                    targetTable.Columns.Add(propName, propType);
                }
                
            });
            return targetTable;
        }

        /// <summary>
        ///     单身转datatable,根据dataTable列名转换,如果 formCollection不存在 则忽略
        /// </summary>
        /// <param name="formCollection"></param>
        /// <param name="targetTable"></param>
        /// <returns></returns>
        public DataTable DOCToDataTable(IEnumerable<DependencyObject> formCollection, DataTable targetTable) {
            foreach (DependencyObject item in formCollection) {
                DataRow dr = targetTable.NewRow();

                foreach (DataColumn col in targetTable.Columns) {
                    try {
                        DependencyProperty fristObject =
                            item.DependencyObjectType.Properties.FirstOrDefault(p => p.Name.Equals(col.ColumnName));
                        if (fristObject != null) {
                            dr[col.ColumnName] = item[col.ColumnName];
                        }
                        else {
                            // 转化复杂类型
                            string targetName = col.ColumnName; //列名
                            //列名中的下划线大于0，且以[_RTK]或[_ROid]结尾的列名视为多来源字段
                            if ((targetName.IndexOf("_", StringComparison.CurrentCultureIgnoreCase) > 0)
                                && (targetName.EndsWith("_RTK", StringComparison.CurrentCultureIgnoreCase)
                                    || targetName.EndsWith("_ROid", StringComparison.CurrentCultureIgnoreCase))) {
                                //列名长度
                                int nameLength = targetName.Length;
                                //最后一个下划线后一位位置
                                int endPos = targetName.LastIndexOf("_", StringComparison.Ordinal) + 1;
                                //拼接目标字段名
                                string newTargetName = targetName.Substring(0, endPos - 1);
                                string extName = targetName.Substring(endPos, nameLength - endPos);
                                fristObject =
                                    item.DependencyObjectType.Properties.FirstOrDefault(
                                        p => p.Name.Equals(newTargetName));
                                if (fristObject != null) {
                                    if ((fristObject is IComplexProperty)
                                        &&
                                        (((IComplexProperty) (fristObject)).DataEntityType.Name == "ReferToEntity")) {
                                        var dependencyObject = item[newTargetName] as DependencyObject;
                                        if (dependencyObject != null) {
                                            dr[col.ColumnName] = dependencyObject[extName];
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception) {
                    }
                }
                targetTable.Rows.Add(dr);
            }
            return targetTable;
        }

        #endregion

        /// <summary>
        ///     不指定单据日期时,调保存自动审核
        /// </summary>
        /// <param name="typekey"></param>
        /// <param name="id"></param>
        /// <param name="dt"></param>
        public void AutoApprove(string typekey, object id, DateTime dt) {
            //保存单据,自動審核

            SetIgnoreWarningTag();
            try {
                if (dt == null) {
                    var readSrv = GetService<IReadService>(typekey) as IReadService;
                    var saveSrv = GetService<ISaveService>(typekey) as ISaveService;
                    object entity = readSrv.Read(id);
                    if (entity != null
                        ) {
                        saveSrv.Save(entity);
                    }
                }
                else {
                    var confirmService = GetService<IConfirmService>(typekey);
                    ILogOnService logOnSer = MyService.LogOnSrv;
                    var context = new ConfirmContext(id, logOnSer.CurrentUserId, dt) {UseTransaction = false};
                    confirmService.Execute(context);
                }
            }
            catch (Exception ex) {
                throw new BusinessRuleException(typekey + "审核时出错：" + ex.Message);
            }
            finally {
                ResetIgnoreWarningTag();
            }
        }

        #region 忽略校驗 警告

        public void SetIgnoreWarningTag() {
            var deliver = CallContext.GetData(DeliverContext.Name) as DeliverContext;
            if (deliver == null) {
                deliver = new DeliverContext();
                CallContext.SetData(DeliverContext.Name, deliver);
            }
            if (deliver.ContainsKey("IgnoreWarning")) {
                deliver["IgnoreWarning"] = true;
            }
            else {
                deliver.Add("IgnoreWarning", true);
            }
        }

        public void ResetIgnoreWarningTag() {
            var deliver = CallContext.GetData(DeliverContext.Name) as DeliverContext;
            if (deliver != null
                && deliver.ContainsKey("IgnoreWarning")) {
                deliver["IgnoreWarning"] = false;
            }
        }

        /// <summary>
        ///     服务端保存去校验，记得生效校验
        /// </summary>
        /// <param name="typeKey"></param>
        /// <param name="points"></param>
        /// <param name="enabled"></param>
        public void SetValidateEnable(string typeKey, IEnumerable<ValidateActivePoint> points, bool enabled) {
            var validateSrv = GetService<IValidatorContainer>(typeKey);
            ValidatorCollection validateColl = validateSrv.Validators;
            validateColl.ToList().ForEach(validate => {
                ValidateActivePointCollection activePointColl = validate.ActivePoints;
                activePointColl.ToList().ForEach(activePoint => {
                    bool exist =
                        points.Any(point => activePoint.ActivePoint != null && activePoint.ActivePoint.Equals(point));
                    if (exist) {
                        activePoint.Enabled = enabled;
                    }
                })
                    ;
            });
        }

        /// <summary>
        ///     设置公式生失效
        /// </summary>
        /// <param name="typeKey"></param>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        public void SetFormula(string typeKey, string path, string id, bool isEnable) {
            var formulas = GetService<IFormulaContainer>(typeKey);
            foreach (FormulaBase item in formulas.Formulas) {
                if (item.Path == path & item.Id == id) {
                    item.Enabled = isEnable;
                }
            }
        }

        /// <summary>
        ///     设置带值生失效
        /// </summary>
        /// <param name="typeKey"></param>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        public void SetRelatedData(string typeKey, string path, string id, bool isEnable) {
            var relate = GetService<IDocumentRelatedDataService>(typeKey);
            foreach (RelatedData item in relate.RelatedDatas) {
                item.Enabled = isEnable;
            }
        }

        #endregion

        #region 批次获取存在于介于

        /// <summary>
        /// 根据范围类的条件实体的Code属性值来生成条件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetProperyName"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IExpression GetQueryConditionByCode(object[] parameter, ParameterStyle type, string targetProperyName)
        {
            //"存在于"
            if (type == ParameterStyle.List)
            {
                var list = new IExpression[parameter.Length];
                for (int i = 0; i < parameter.Length; i++)
                {
                    list[i] = new ConstantsQueryProperty(parameter[i], GeneralDBType.String);
                }
                var condition = new QueryCondition(new SimpleQueryProperty(targetProperyName), list, CompareOperator.In);
                return condition;
            }
            else
            {   //"介于"
                if (!Maths.IsEmpty(parameter[0])) {
                    var condition1 = new QueryCondition(new SimpleQueryProperty(targetProperyName),
                        OOQL.CreateConstants(parameter[0]), CompareOperator.GreaterEqual);
                    if (!Maths.IsEmpty(parameter[1]))
                    {
                        return new QueryConditionGroup(condition1, new QueryCondition(new SimpleQueryProperty(targetProperyName),
                                                OOQL.CreateConstants(parameter[1]), CompareOperator.LessEqual), LogicalOperator.And);
                    }
                    else
                    {
                        return condition1;
                    }
                }
                else
                {
                    if (!Maths.IsEmpty(parameter[1]))
                    {
                        return new QueryCondition(new SimpleQueryProperty(targetProperyName), OOQL.CreateConstants(parameter[1]), CompareOperator.LessEqual);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        } 
        #endregion
    }

    public class RegiesterTypeParameter {
        private Dictionary<string, Type> _nameTypeFiled = new Dictionary<string, Type>();
        private List<string> _propertiesFiled = new List<string>();

        private Type _type = typeof (string);

        public RegiesterTypeParameter(List<string> propertiesFiled, Type type) {
            Properties = propertiesFiled;
            Type = type;
        }

        public RegiesterTypeParameter(Dictionary<string, Type> nameAndType) {
            NameAndType = nameAndType;
        }

        /// <summary>
        ///     指定参数
        /// </summary>
        public List<string> Properties {
            get {
                return
                    _propertiesFiled;
            }
            set { _propertiesFiled = value; }
        }

        public Dictionary<string, Type> NameAndType {
            get {
                return
                    _nameTypeFiled;
            }
            set { _nameTypeFiled = value; }
        }

        /// <summary>
        ///     指定类型
        /// </summary>
        public Type Type {
            get {
                return
                    _type;
            }
            set { _type = value; }
        }
    }
}
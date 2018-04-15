/*--------------------------------------------------------
 * createDate 20180409
 * createBy 08628
 * version 0.0.0.1
 * remark 审核/撤审/保存/删除/作废生失效
 * ----ActionPoint----
 * 
 * 
 * -------------------
 *--------------------------------------------------------
 */
using System;
using System.ComponentModel;
using System.Linq;
using Digiwin.Common;
using Digiwin.Common.Core;
using Digiwin.Common.Query2;
using Digiwin.Common.Services;
using Digiwin.Common.Torridity;
using Digiwin.ERP.Common.Business;
using Digiwin.ERP.CommonSupplyChain.Business;
using System.Globalization;
using Digiwin.ERP.Common.Utils;

namespace Digiwin.ERP.XTEST.Business.Implement {
    [EventInterceptorClass]
    internal sealed class _Interceptor_: ServiceComponent
    {
		#region MyRegion
        private MyTools _myTool;
        private ServiceTools _myServiceTool;

        /// <summary>
        /// 这里提供一些辅助方法
        /// </summary>
        public MyTools MyTool
        {
            get { return _myTool ?? (_myTool = new MyTools(this)); }
        }

        /// <summary>
        /// 常用的服务在这里
        /// </summary>
        public ServiceTools MyServiceTool
        {
            get { return _myServiceTool ?? (_myServiceTool = MyTool.MyService); }
        } 
        #endregion
		
        #region __InsertHere__
        #region  ApproveInterceptor

        /// <summary>
        /// 审核逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Description("审核逻辑")]
        [EventInterceptor(typeof (IConfirmServiceEvents), "BusinessObjectExecute")]
        public void ApproveInterceptor(object sender, ConfirmDetailEventArgs e) {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            //单头审核
            if (Maths.IsEmpty(e.Path))
            {
                // 单头
                DependencyObject entity = e.ActiveObject as DependencyObject;
                

                // 单身
                // var entityD = entity[""] as DependencyObjectCollection;
                // 工厂
                // var plant_id = ((DependencyObject)entity["Owner_Org"])["ROid"];
                // 审核日期
                // DateTime confirmDate = e.Context.ConfirmInfo.ConfirmDate;
                // 获取服务
                // 个案服务
                // var iser = MyTool.GetService<ITESTService>(this.TypeKey);
                // 系统服务
                // var sysParameterSrv =   MyServiceTool.SysParameterSrv;
                // 调自动审核
                // MyTool.AutoApprove(this.TypeKey,entity.Oid,null);
                // 使用事务
                //using (ITransactionService trans = this.GetService<ITransactionService>()) {
                //trans.Complete();
                //}
            }
        }
        #endregion ApproveInterceptor

        #region IDisconfirmInterceptor
        /// <summary>
        /// 撤销审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Description("工单撤销审核")]
        [EventInterceptor(typeof(IDisconfirmServiceEvents), "BusinessObjectExecute")]
        public void DisApproveInterceptor(object sender, ConfirmDetailEventArgs e)
        {
            //单头审核
            if (string.IsNullOrEmpty(e.Path))
            {// 单头
                DependencyObject entity = e.ActiveObject as DependencyObject;
                // 单身
                // var entityD = entity[""] as DependencyObjectCollection;
                // 工厂
                // var plant_id = ((DependencyObject)entity["Owner_Org"])["ROid"];
                // 审核日期
                // DateTime confirmDate = e.Context.ConfirmInfo.ConfirmDate;
                // 获取服务
                // 个案服务
                // var iser = MyTool.GetService<ITESTService>(this.TypeKey);
                // 系统服务
                // var sysParameterSrv =   MyServiceTool.SysParameterSrv;
                // 调自动审核
                // MyTool.AutoApprove(this.TypeKey,entity.Oid,null);
                // 使用事务
                //using (ITransactionService trans = this.GetService<ITransactionService>()) {
                //trans.Complete();
                //}

            }
        }
        #endregion IDisconfirmInterceptor


        #region SaveInterceptor
        /// <summary>
        /// 保存后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventInterceptor(typeof(ISaveServiceEvents), "Saved")]
        private void SaveInterCeptor(object sender, SaveEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            IQueryService qurSer = MyServiceTool.QuerySrv;
            //DependencyObject activeObject = e.Entities[0] as DependencyObject;//当前实体
            // 使用事务
            //using (ITransactionService trans = this.GetService<ITransactionService>()) {
            //trans.Complete();
            //}
            foreach (var activeEntity in e.Entities)
            {
                DependencyObject activeObject = activeEntity as DependencyObject;//当前实体
                if (activeObject == null)
                {
                    IInfoEncodeContainer infoEncode = MyServiceTool.InfoEncodeSrv;//信息编码服务
                    throw new BusinessRuleException(infoEncode.GetMessage("A100273"));//空校验
                }
            }
        }
        #endregion SaveInterceptor

        #region ApproveStatusInterceptor

        [EventInterceptor(typeof(IApproveStatusServiceEvents), "Starting"), Description("作废生失效切片")]
        private void VoidStarting(object sender, ApproveStatusEventArgs e)
        {
            if (e.TargetStatus != ManageStatusEnum.Void) { return; }
          
            DependencyObject entity = e.Entity as DependencyObject; //单头实体
            IApproveStatusService RSer = GetServiceForThisTypeKey<IApproveStatusService>();

            //例如 点击生失效 设置某单据审核状态
            //object id = Guid.Empty;
            //object ApproveStatus = entity["ApproveStatus"];
            //    if (e.TargetStatus == ManageStatusEnum.Available) //生效
            //    {
            //        if (!ApproveStatus.Equals("Y"))
            //        {
            //            RSer.SetApproveStatus(id, ManageStatusEnum.Available);
            //        }
            //    }
            //    else if (e.TargetStatus == ManageStatusEnum.Unavailable) //点击【失效】
            //    {
            //        if (!ApproveStatus.Equals("V"))
            //        {
            //            RSer.SetApproveStatus(id, ManageStatusEnum.Unavailable);
            //        }
            //    }
            //    else if (e.TargetStatus == ManageStatusEnum.Unenforced) //
            //    {
            //        if (!ApproveStatus.Equals("N"))
            //        {
            //            RSer.SetApproveStatus(id, ManageStatusEnum.Unenforced);
            //        }
            //    }
            

        }


        #endregion ApproveStatusInterceptor

        #region DeleteInterceptor
        [EventInterceptor(typeof(IDeleteServiceEvents), "Completed"), Description("删除切片")]
        public void DeleteReservation(object sender, DeleteEventArgs e)
        {
            
            foreach (DependencyObject entity in e.Entities)
            {
                if (entity.DependencyObjectType.Properties.Contains(this.TypeKey + "_ID"))
                {//单头才处理
                   // salesOrderService.DeleteTransactionLine(entity[this.TypeKey + "_ID"]);
                }
            }
        }
        #endregion DeleteInterceptor

        
        #endregion __InsertHere__
    }
}
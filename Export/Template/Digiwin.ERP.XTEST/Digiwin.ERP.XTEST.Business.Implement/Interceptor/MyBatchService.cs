using System;
using Digiwin.Common;
using Digiwin.Common.Services;
using System.ComponentModel;
using Digiwin.Common.Query2;
using Digiwin.Common.Core;
using System.Data;
using Digiwin.Common.Torridity;
using Digiwin.ERP.Common.Utils;
using Digiwin.ERP.Common.Business;

namespace Digiwin.ERP.XTEST.Business.Implement
{
    [SingleGetCreator]
    [ServiceClass(typeof(IBatchService))]    
    sealed class _MyBatchService_ : FreeBatchService<FreeBatchEventsArgs> {

        #region MyRegion
        private MyTools _myTool;
        private ServiceTools _myServiceTool;

        /// <summary>
        /// 这里提供一些辅助方法
        /// </summary>
        private MyTools MyTool
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

        /// <summary>
        /// 是否可预览
        /// </summary>
        /// <returns></returns>
        public override bool CanPreview()
        {
            return false;
        }

        /// <summary>
        /// CreateFreeBatchEventsArgs
        /// </summary>
        /// <param name="args"></param>
        protected override FreeBatchEventsArgs CreateFreeBatchEventsArgs(BatchContext context) {
            return new FreeBatchEventsArgs(context);
        }

        /// <summary>
        /// 返回预览数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override DataTable GetPreviewData(BatchContext context)
        {
            return null;
        }


        /// <summary>
        /// 核心逻辑处理
        /// </summary>
        /// <param name="args"></param>
        protected override void DoProcess(FreeBatchEventsArgs args) {
            //var sysSer = MyServiceTool.SysParameterSrv;
            //object doc_id= args.Context.Parameters["DOC_ID"].Value;
            //进度条
            this.RefreshProcess(10, "Begin", args.Context);
            this.RefreshProcess(100, "END", args.Context);
            this.CompleteProcess();
        }

        protected override void OnStarting(FreeBatchEventsArgs e)
        {
            base.OnStarting(e);
        }

        public override void EndExecute(object task)
        {
            base.EndExecute(task);
        }
        public override string GetCanPreviewQueryId()
        {
            return base.GetCanPreviewQueryId();
        }
        protected override void OnValidating(FreeBatchEventsArgs e)
        {
            base.OnValidating(e);
        }
        protected override void OnCompleted(FreeBatchEventsArgs e)
        {
            base.OnCompleted(e);
        }
    }
}
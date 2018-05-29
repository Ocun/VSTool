using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common;
using Digiwin.Common.Core;
using Digiwin.Common.Query2;
using Digiwin.Common.Torridity;
using System.ComponentModel;
using Digiwin.Common.Services;
using Digiwin.ERP.Common.Utils;
using Digiwin.ERP.CommonSupplyChain.Business;
using Digiwin.ERP.Common.Business;
using Digiwin.Common.Torridity.Metadata;

namespace Digiwin.ERP.XTEST.Business.Implement
{
    [ServiceClass(typeof(_ITestsService_))]
	[SingleGetCreator]
    sealed class _TestsService_ : ServiceComponent, _ITestsService_
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
        #region Service
        public DependencyObjectCollection MyFunc()
        {
            // 使用事务
            //using (ITransactionService trans = this.GetService<ITransactionService>()) {
            //trans.Complete();
            //}
        }
        
         #endregion Service
        #endregion __InsertHere__
    }
}

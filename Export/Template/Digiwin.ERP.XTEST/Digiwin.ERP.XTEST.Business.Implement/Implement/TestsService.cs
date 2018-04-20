using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common;

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

        DependencyObjectCollection myFunc()
        {
            // 使用事务
            //using (ITransactionService trans = this.GetService<ITransactionService>()) {
            //trans.Complete();
            //}
        }
    }
}

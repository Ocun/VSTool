using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common.UI;
using Digiwin.Common.Torridity;
using System.Runtime.Remoting.Messaging;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    /// <summary>
    /// 查询界面
    /// </summary>
    class MyQueryConditionAction : GuideBatchConditionAction
    {
        #region 自定义属性

        private MyUITools _myUITool ;

        public MyUITools MyUITool {
            get {
                if (_myUITool == null) {
                    _myUITool = new MyUITools(this);
                }
                return _myUITool;
            }
        }

        /// <summary>
        /// 获取根实体
        /// </summary>
        private DependencyObject RootData
        {
            get { return (DependencyObject)base.EditorView.DataSource; }
        }

        /// <summary>
        /// 注册数据回调
        /// </summary>
        protected override void RegisterDataResponse()
        {
            ServiceControl traceSrv = this.EditorView as ServiceControl;
            traceSrv.ca
                
                
                .GetServiceForThisTypeKey<IDataEntityTraceService>();
        }

        /// <summary>
        /// 设置导航按钮
        /// </summary>
        /// <param name="context"></param>
        protected override void SetNavigatorButton(StepActionContext context)
        {
            context.MainWindow.NavigateContol.Control.Parent.Visible = false;
            context.MainWindow.NavigateContol.FallBackButton.Visible = false;
            context.MainWindow.NavigateContol.CancelButton.Visible = false;
        }

        private IFindControlService FindControlSrv { get; set; }

        #endregion

        #region 重写方法


        public override void Rollback(StepActionContext context)
        {
            
        }

        public override void REExcute(StepActionContext context)
        {
           
        }

        public override void Complete(StepActionContext context)
        {
            
            
        }

        #endregion
     }
}

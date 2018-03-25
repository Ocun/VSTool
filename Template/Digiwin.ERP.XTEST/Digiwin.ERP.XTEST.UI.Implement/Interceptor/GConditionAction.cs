using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common.UI;
using Digiwin.Common.Torridity;
using System.Runtime.Remoting.Messaging;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    class GConditionAction : GuideBatchConditionAction
    {
        #region 自定义属性

        /// <summary>
        /// 根实体
        /// </summary>
        private DependencyObject RootData
        {
            get { return (DependencyObject)base.EditorView.DataSource; }
        }

        private IFindControlService FindControlSrv { get; set; }

        #endregion

        #region 重写方法

        protected override void RegisterDataResponse()
        {
         
        }

        protected override void SetNavigatorButton(StepActionContext context)
        {
            
        }

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

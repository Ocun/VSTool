using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Digiwin.Common.UI;
using Digiwin.Common.Torridity;
using System.Collections.Generic;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    class QueryDeleteAction : GuideBatchEditorViewAction {
        public QueryDeleteAction() {

        }
        /// <summary>
        /// 注册数据回调
        /// </summary>
        protected override void RegisterDataResponse() {
        }

        /// <summary>
        /// 设置导航按钮
        /// </summary>
        /// <param name="context"></param>
        protected override void SetNavigatorButton(StepActionContext context) {
        }

        /// <summary>
        /// 在执行完成时，查询数据
        /// </summary>
        /// <param name="context"></param>
        public override void Complete(StepActionContext context) {
           
        }

        /// <summary>
        /// 在回退的时候，清空数据
        /// </summary>
        /// <param name="context"></param>
        public override void REExcute(StepActionContext context) {
            
         
        }

        public override void Excute(StepActionContext context) {
            //base.Excute(context);         
        }
        public override void Rollback(StepActionContext context) {
            //base.Rollback(context);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common.UI;
using System.Windows.Forms;
using Digiwin.Common.Torridity;
using System.Data;
using System.Collections;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    /// <summary>
    /// 查询结果界面的Action
    /// </summary>
    public class QueryResultAction : GuideBatchEditorViewAction
    {

        /// <summary>
        /// 获取根实体
        /// </summary>
        DependencyObject RootData
        {
            get
            {
                
            }
        }

        /// <summary>
        /// 上一步时，反注册数据回调
        /// </summary>
        /// <param name="context"></param>
        public override void Rollback(StepActionContext context)
        {
            
        }

        /// <summary>
        /// 注册数据回到
        /// </summary>
        protected override void RegisterDataResponse()
        {
            

        }

        /// <summary>
        /// 设置导航按钮
        /// </summary>
        /// <param name="context"></param>
        protected override void SetNavigatorButton(StepActionContext context)
        {
            
        }

    }
}

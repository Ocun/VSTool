using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common;
using System.ComponentModel;
using Digiwin.Common.UI;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    /// <summary>
    /// 向导式批次的切片
    /// </summary>
    [EventInterceptorClass]
    sealed class MyGuideInterceptor:ServiceComponent
    {       
        //ADD
        /// <summary>
        /// 添加向导式批次的action集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"),
        EventInterceptor(typeof(IStepActionControlServiceEvents), "Initialize")]
        void StepActionControlServiceInitialize(object sender, StepActionEventArgs e)
        {
            IStepActionControlService stepActionControlService = sender as IStepActionControlService;
            if (stepActionControlService != null)
            {
                EditorViewAction conditionAction = new MyQueryConditionAction();
                conditionAction.Id = GuidePatternConsonts.ConditionViewSubName;
                conditionAction.Description = "Description";//添加你的description
                stepActionControlService.Actions.Add(conditionAction);
                EditorViewAction queryResultAction = new MyQueryResultAction();
                queryResultAction.Id = GuidePatternConsonts.QueryResultViewSubName;
                queryResultAction.Description = "Description";//添加你的description
                stepActionControlService.Actions.Add(queryResultAction);
            }
        } 
    }
}

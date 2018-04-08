using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Digiwin.Common;
using Digiwin.Common.UI;
using Digiwin.Common.Torridity;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    [EventInterceptorClass]
    [Description("调度单撤销审核按钮灰显切片")]
    public class _DisableMenuInterceptor_ : ServiceComponent
    {
        //#region 编辑界面__禁用撤销审核按钮
        //[EventInterceptor(typeof(IEditorView), "Load")]
        //public void EditDisconfirm(object sender, EventArgs e)
        //{
        //    ICommandsService cmdService = this.GetServiceForThisTypeKey<ICommandsService>();
        //    CommandBase cmd = cmdService.Commands["Disconfirm"] as CommandBase;//禁用撤销审核按钮Command
        //    cmd.Enabled = false;
        //}
        //#endregion

        //#region 浏览界面__禁用撤销审核按钮
        //[EventInterceptor(typeof(IBrowseWindow), "Load")]
        //public void BrowseDisconfirmCommandForBrowseWindow(object sender, EventArgs e)
        //{
        //    ICommandsService cmdService = this.GetServiceForThisTypeKey<ICommandsService>();
        //    CommandBase cmd = cmdService.Commands["DisconfirmCommandForBrowseWindow"] as CommandBase;//禁用撤销审核按钮Command
        //    cmd.Enabled = false;
        //}
        //#endregion

      

            /// <summary>
            /// 编辑界面
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            [EventInterceptor(typeof(IEditorView), "Load")]
            private void EditorLoad(object sender, EventArgs e)
            {
                ICommandsService cmdSer = this.GetServiceForThisTypeKey<ICommandsService>();
                if (cmdSer.Commands.Contains("Disconfirm"))
                {
                    CommandBase cmdConfirm = cmdSer.Commands["Disconfirm"] as CommandBase;//审核按钮
                    cmdConfirm.EnabledDeciders.Add(cmdSer.GetCommandEnabledDecider<ConfirmEditorEnabledDecider>());
                }
                if (cmdSer.Commands.Contains("DocumentDataDeleteCommand"))
                {
                    CommandBase cmdConfirm = cmdSer.Commands["DocumentDataDeleteCommand"] as CommandBase;//删除按钮
                    cmdConfirm.EnabledDeciders.Add(cmdSer.GetCommandEnabledDecider<ConfirmEditorEnabledDecider>());
                }
                if (cmdSer.Commands.Contains("ModifyCommand"))
                {
                    CommandBase cmdConfirm = cmdSer.Commands["ModifyCommand"] as CommandBase;//修改按钮
                    cmdConfirm.EnabledDeciders.Add(cmdSer.GetCommandEnabledDecider<ConfirmEditorEnabledDecider>());
                }
            }

            /// <summary>
            /// 浏览界面
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            [EventInterceptor(typeof(IBrowseWindow), "Load")]
            private void BrowseViewLoad(object sender, EventArgs e)
            {
                ICommandsService cmdSer = this.GetServiceForThisTypeKey<ICommandsService>();
                if (cmdSer.Commands.Contains("DisconfirmCommandForBrowseWindow"))
                {
                    CommandBase cmdConfirm = cmdSer.Commands["DisconfirmCommandForBrowseWindow"] as CommandBase;//审核按钮
                    cmdConfirm.EnabledDeciders.Add(cmdSer.GetCommandEnabledDecider<ConfirmBrowseEnabledDecider>());
                }
                if (cmdSer.Commands.Contains("DocumentDataDeleteCommand"))
                {
                    CommandBase cmdConfirm = cmdSer.Commands["DocumentDataDeleteCommand"] as CommandBase;//删除按钮
                    cmdConfirm.EnabledDeciders.Add(cmdSer.GetCommandEnabledDecider<ConfirmBrowseEnabledDecider>()); //^_^ 20171010 MODI BY HEHF FOR CBBM11709199 OLD:ConfirmEditorEnabledDecider
                }
                if (cmdSer.Commands.Contains("ModifyCommand"))
                {
                    CommandBase cmdConfirm = cmdSer.Commands["ModifyCommand"] as CommandBase;//修改按钮
                    cmdConfirm.EnabledDeciders.Add(cmdSer.GetCommandEnabledDecider<ConfirmBrowseEnabledDecider>()); //^_^ 20171010 MODI BY HEHF FOR CBBM11709199 OLD:ConfirmEditorEnabledDecider
                }
            }

        }
        /// <summary>
        ///  编辑界面的表决器
        /// </summary>  
        internal sealed class ConfirmBrowseEnabledDecider : CommandEnabledDecider
        {
            /// <summary>
            /// 
            /// </summary>
            public ConfirmBrowseEnabledDecider()
                : base(0, true)
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="provider"></param>
            /// <param name="callContext"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            protected override bool QueryEnabled(Digiwin.Common.Advanced.IResourceServiceProvider provider, Digiwin.Common.ServiceCallContext callContext, System.Windows.Forms.IDataObject context)
            {
                ICurrentBrowseWindow win = provider.GetService(typeof(ICurrentBrowseWindow), "SALES_ORDER_DOC") as ICurrentBrowseWindow;
                if (win == null || win.BrowseView.DataSource == null)
                {
                    return false;
                }
                bool flag = true;
                //^_^ 20171010 MODI BY HEHF FOR CBBM11709199  ↓
                //DependencyObjectCollection coll = win.BrowseView.DataSource as DependencyObjectCollection;
                //var selectedColl = coll.Where(a => Convert.ToBoolean(a["IsSelected"], CultureInfo.InvariantCulture)).Select(b => b);
                var selectedColl = win.BrowseView.SelectObjects;
                //^_^ 20171010 MODI BY HEHF FOR CBBM11709199  ↑
                selectedColl.ToList().ForEach(p =>
                {
                    if (3.Equals(((DependencyObject)p)["XSOURCE_TYPE"]))
                    {
                        flag = false;
                    }

                });
              

                return flag;
            }
        }

        /// <summary>
        ///  浏览界面的表决器
        /// </summary>  
        internal sealed class ConfirmEditorEnabledDecider : CommandEnabledDecider
        {
            /// <summary>
            /// 
            /// </summary>
            public ConfirmEditorEnabledDecider()
                : base(0, true)
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="provider"></param>
            /// <param name="callContext"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            protected override bool QueryEnabled(Digiwin.Common.Advanced.IResourceServiceProvider provider, Digiwin.Common.ServiceCallContext callContext, System.Windows.Forms.IDataObject context)
            {
                ICurrentDocumentWindow win = provider.GetService(typeof(ICurrentDocumentWindow), "SALES_ORDER_DOC") as ICurrentDocumentWindow;
                if (win ==null || win.EditController.Document.DataSource == null)
                {
                    return false;
                }
                bool flag = true;
                DependencyObject coll = win.EditController.Document.DataSource as DependencyObject;
                if (3.Equals(coll["XSOURCE_TYPE"]))
                {
                    flag = false;
                }
                return flag;
            }
        }
    
}
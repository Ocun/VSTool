using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraGrid;
using Digiwin.Common.UI;
using Digiwin.Common.Torridity;
using System.Windows.Forms;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    /// <summary>
    /// 导出EXCEL
    /// </summary>
    [Description("导出EXCEL")]
    class ExportExcelCommand : EditControllerCommandBase
    {
        public ExportExcelCommand()
            : base("ExportExcelCommand")
        {
        }

        public override string ActionName
        {
            get
            {
                return "ExportExcel";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void InitializeServiceComponent()
        {
            base.InitializeServiceComponent();
            ICommandsService commansSrv = this.GetServiceForThisTypeKey<ICommandsService>();
            this.EnabledDeciders.Add(commansSrv.GetCommandEnabledDecider<ExportExcelCommandEnabledDecider>());
        }

        public override void Execute()
        {
            ICurrentDocumentWindow win = this.GetServiceForThisTypeKey<ICurrentDocumentWindow>();
            ServiceControl serControl = win.EditController.EditorView as ServiceControl;
            DependencyObject entity = win.EditController.EditorView.DataSource as DependencyObject;
            DependencyObjectCollection XMO_AA_D2 = entity["XMO_AA_D2"] as DependencyObjectCollection;
            IFindControlService findSer = serControl.GetService<IFindControlService>();
            if (XMO_AA_D2.Count == 0)
            {
                DigiwinMessageBox.ShowInfo("无导出数据！");
                return;
            }
            Control control;
            if (findSer.TryGet("XdesignerGrid1XMO_AA_D1", out control))
            {
                DigiwinGrid gridControl = control as DigiwinGrid;
                if (gridControl != null)
                {
                    BindingSource bs = gridControl.DataSource as BindingSource;
                    DependencyObjectCollection entityDColl = ((DependencyObjectCollectionView<DependencyObjectView>)bs.List).DependencyObjectCollection;

                    using (var form = new ExportExcelForm(gridControl.InnerGridView,this.ResourceServiceProvider, this.ServiceCallContext))
                    {
                        DialogResult log = form.ShowDialog();
                        if (log == DialogResult.OK)
                        {
                            DigiwinMessageBox.ShowInfo("资料导出成功！");
                        }
                        form.Dispose();
                    }
                }

            }





           
        
            return;
            using (var form = new ExportExcelForm(entity, this.ResourceServiceProvider, this.ServiceCallContext))
            {
                DialogResult log = form.ShowDialog();
                if (log == DialogResult.OK)
                {
                    DigiwinMessageBox.ShowInfo("资料导出成功！");
                }
                form.Dispose();
            }
        }


    }
    /// <summary>
    ///  表决器
    /// </summary>  
    internal sealed class ExportExcelCommandEnabledDecider : CommandEnabledDecider
    {
        /// <summary>
        /// 
        /// </summary>
        public ExportExcelCommandEnabledDecider()
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
            bool result = base.QueryEnabled(provider, callContext, context);
            bool flag = true;
            if (result)
            {
                ICurrentDocumentWindow window = provider.GetService(typeof(ICurrentDocumentWindow), callContext.TypeKey) as ICurrentDocumentWindow;
                if (window != null)
                {
                    DependencyObject entity = window.EditController.Document.DataSource as DependencyObject;
                    if (entity != null)
                    {
                        if (window.EditController.Document.EditState != EditState.Edit 
                            && window.EditController.Document.EditState != EditState.Create
                            && window.EditController.Document.EditState != EditState.Open
                            )
                            return false;
                    }

                    return flag;
                }
                return flag;
            }
            return flag;

        }
    }
}

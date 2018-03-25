using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common;
using Digiwin.Common.Services;
using Digiwin.Common.Torridity;
using Digiwin.ERP.Common.Utils;
using Digiwin.Common.UI;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using MenuItem = System.Windows.Forms.MenuItem;
using ContextMenu = System.Windows.Forms.ContextMenu;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    [EventInterceptorClass]
    public sealed class DigiwinGridInterceptor : ServiceComponent
    {
        private static DigiwinGrid _dgGrid1 = null;
        private static DigiwinGrid _dgGrid3 = null;
        DependencyObject entity;
        DependencyObjectCollection entityD;
        DependencyObjectCollection entitySD;
        private bool _isLableClick = false;
        [EventInterceptor(typeof(IEditorView), "DataSourceChanged")]
        private void init(object sender, EventArgs e)
        {             
            ICurrentDocumentWindow currentDocumentWin = this.GetServiceForThisTypeKey<ICurrentDocumentWindow>();
            if (currentDocumentWin == null)
            {
                return;
            }
        
                IFindControlService ser = this.GetService<IFindControlService>();
                Control c;
                if (ser.TryGet("XdesignerGrid1XMO_AB_D2", out c))
                {
                    _dgGrid3 = c as DigiwinGrid;
                    _dgGrid3.InnerGridView.MouseDown += new MouseEventHandler(InnerGridView_MouseDown);
                }
                Control d;
                if (ser.TryGet("XdesignerGrid1XMO_AB_D1", out d))
                {
                    _dgGrid1 = d as DigiwinGrid;
                    _dgGrid1.InnerGridView.MouseDown += new MouseEventHandler(_dgGrid1_InnerGridView_MouseDown);
                }
            
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);
        void _dgGrid1_InnerGridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (((MouseEventArgs)e).Button == MouseButtons.Right && ((MouseEventArgs)e).Clicks == 1)
                {
                    GridHitInfo hit = _dgGrid1.InnerGridView.CalcHitInfo(((MouseEventArgs)e).Location);
                    if (hit.InRowCell && hit.RowHandle >= 0)
                    {
                        DependencyObject nowObj = ((Digiwin.Common.Torridity.DependencyObjectView)(((Digiwin.Common.UI.DigiwinGridBase)(_dgGrid1)).CurrenctViewRows[hit.RowHandle])).DependencyObject;
                        if (hit.Column.FieldName == "PLAN_START_DATE" || hit.Column.FieldName == "PLAN_COMPLETE_DATE" || hit.Column.FieldName == "PLAN_ISSUE_DATE")
                        {
                            {
                                Form form = new Form();
                                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                form.Size = new System.Drawing.Size(120, 25);
                                form.ShowInTaskbar = false;
                                form.BackColor = SystemColors.Control;
                                form.LostFocus += new EventHandler(form_LostFocus);
                                form.ControlBox = false;

                                Label lable = new Label();
                                lable.Text = "向下填充";
                                lable.TextAlign = ContentAlignment.MiddleCenter;
                                lable.Dock = DockStyle.Fill;
                                lable.Click += new EventHandler(_dgGrid1_form_Click);
                                lable.BackColor = SystemColors.Control;
                                form.Controls.Add(lable);

                                form.Show();
                                Point p;
                                GetCursorPos(out p);
                                form.Location = p;
                                form.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception e1)
            {
            };
        }
        void InnerGridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (((MouseEventArgs)e).Button == MouseButtons.Right && ((MouseEventArgs)e).Clicks == 1)
                {
                    GridHitInfo hit = _dgGrid3.InnerGridView.CalcHitInfo(((MouseEventArgs)e).Location);
                    if (hit.InRowCell && hit.RowHandle >= 0)
                    {
                        DependencyObject nowObj = ((Digiwin.Common.Torridity.DependencyObjectView)(((Digiwin.Common.UI.DigiwinGridBase)(_dgGrid3)).CurrenctViewRows[hit.RowHandle])).DependencyObject;
                        if (hit.Column.FieldName == "REQUIRED_QTY" || hit.Column.FieldName == "PLAN_ISSUE_DATE")
                        {
                            {
                                Form form = new Form();
                                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                form.Size = new System.Drawing.Size(120, 25);
                                form.ShowInTaskbar = false;
                                form.BackColor = SystemColors.Control;
                                form.LostFocus += new EventHandler(form_LostFocus);
                                form.ControlBox = false;

                                Label lable = new Label();
                                lable.Text = "向下填充";
                                lable.TextAlign = ContentAlignment.MiddleCenter;
                                lable.Dock = DockStyle.Fill;
                                lable.Click += new EventHandler(form_Click);
                                lable.BackColor = SystemColors.Control;
                                form.Controls.Add(lable);

                                form.Show();
                                Point p;
                                GetCursorPos(out p);
                                form.Location = p;
                                form.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception e1)
            {
            };
        }
        void form_LostFocus(object sender, EventArgs e)
        {
            if (!_isLableClick)
            {
                Form form = (Form)sender;
                form.Close();
            }
        }
        void _dgGrid1_form_Click(object sender, EventArgs e)
        {
            //执行逻辑
            _isLableClick = true;
            try
            {
                int focusHander = _dgGrid1.InnerGridView.FocusedRowHandle;
                string columnName = _dgGrid1.InnerGridView.FocusedColumn.FieldName;
                BindingSource bs = _dgGrid1.DataSource as BindingSource;
                DependencyObjectCollection entityDs = ((DependencyObjectCollectionView<DependencyObjectView>)bs.List).DependencyObjectCollection;
                DependencyObjectView selectValue = _dgGrid1.SelectedValue as DependencyObjectView;
                if (focusHander >= 0 && focusHander < entityDs.Count && selectValue != null)
                {
                    DependencyObject selectObj = selectValue.DependencyObject;
                    for (int i = focusHander + 1; i < _dgGrid1.InnerGridView.RowCount; i++)
                    {
                        if (columnName == "PLAN_START_DATE")
                        {
                            entityDs[i]["PLAN_START_DATE"] = selectObj["PLAN_START_DATE"];
                        }
                        if (columnName == "PLAN_COMPLETE_DATE")
                        {
                            entityDs[i]["PLAN_COMPLETE_DATE"] = selectObj["PLAN_COMPLETE_DATE"];
                        }
                        if (columnName == "PLAN_ISSUE_DATE")
                        {
                            entityDs[i]["PLAN_ISSUE_DATE"] = selectObj["PLAN_ISSUE_DATE"];
                        }
                    }

                }
            }
            catch (Exception e1)
            {
            };

            Form form = (Form)(((Label)sender).Parent);
            form.Close();
            _isLableClick = false;
        }
        

        void form_Click(object sender, EventArgs e)
        {
            //执行逻辑
            _isLableClick = true;
            try
            {
                int focusHander = _dgGrid3.InnerGridView.FocusedRowHandle;
                string columnName = _dgGrid3.InnerGridView.FocusedColumn.FieldName;
                BindingSource bs = _dgGrid3.DataSource as BindingSource;
                DependencyObjectCollection entityDs = ((DependencyObjectCollectionView<DependencyObjectView>)bs.List).DependencyObjectCollection;
                DependencyObjectView selectValue = _dgGrid3.SelectedValue as DependencyObjectView;
                if (focusHander >= 0 && focusHander < entityDs.Count && selectValue != null)
                {
                    DependencyObject selectObj = selectValue.DependencyObject;
                    for (int i = focusHander + 1; i < _dgGrid3.InnerGridView.RowCount; i++)
                    {
                        if (columnName == "REQUIRED_QTY")
                        {
                            entityDs[i]["REQUIRED_QTY"] = selectObj["REQUIRED_QTY"];
                        }
                        if (columnName == "PLAN_ISSUE_DATE")
                        {
                            entityDs[i]["PLAN_ISSUE_DATE"] = selectObj["PLAN_ISSUE_DATE"];
                        } 
                    }

                }
            }
            catch (Exception e1)
            {
            };

            Form form = (Form)(((Label)sender).Parent);
            form.Close();
            _isLableClick = false;
        }
        

    }
}

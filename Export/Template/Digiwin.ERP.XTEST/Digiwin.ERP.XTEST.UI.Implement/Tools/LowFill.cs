using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Digiwin.Common;
using Digiwin.Common.Torridity;
using Digiwin.Common.UI;
using Digiwin.ERP.Common.Utils;
using DependencyObject = Digiwin.Common.Torridity.DependencyObject;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    [EventInterceptorClass]
    public sealed class LowFill : ServiceComponent
    {
        private static DigiwinGrid _dgGrid = null;
        private static string _dgGridName = "TEST";
        private bool _isLableClick = false;
        private string[] _field_name = {"TEST1","TEST2"};


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
            if (ser.TryGet(_dgGridName, out c))
            {
                _dgGrid = c as DigiwinGrid;
                _dgGrid.InnerGridView.MouseDown += new MouseEventHandler(InnerGridView_MouseDown);

                
            }
        }


        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);

        private void InnerGridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (((MouseEventArgs)e).Button == MouseButtons.Right && ((MouseEventArgs)e).Clicks == 1)
                {
                    GridHitInfo hit = _dgGrid.InnerGridView.CalcHitInfo(((MouseEventArgs)e).Location);
                    if (hit.InRowCell && hit.RowHandle >= 0)
                    {
                        DependencyObject nowObj =
                            ((Digiwin.Common.Torridity.DependencyObjectView)
                                (((Digiwin.Common.UI.DigiwinGridBase)(_dgGrid)).CurrenctViewRows[hit.RowHandle]))
                                .DependencyObject;
                        if (_field_name.Contains(hit.Column.FieldName))
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
            }
            ;
        }

        private void form_LostFocus(object sender, EventArgs e)
        {
            if (!_isLableClick)
            {
                Form form = (Form)sender;
                form.Close();
            }
        }


        private void form_Click(object sender, EventArgs e)
        {
            //执行逻辑
            _isLableClick = true;
            try
            {
                int focusHander = _dgGrid.InnerGridView.FocusedRowHandle;
                string columnName = _dgGrid.InnerGridView.FocusedColumn.FieldName;
                BindingSource bs = _dgGrid.DataSource as BindingSource;
                DependencyObjectCollection entityDs =
                    ((DependencyObjectCollectionView<DependencyObjectView>)bs.List).DependencyObjectCollection;
                DependencyObjectView selectValue = _dgGrid.SelectedValue as DependencyObjectView;
                if (focusHander >= 0 && focusHander < entityDs.Count && selectValue != null)
                {
                    DependencyObject selectObj = selectValue.DependencyObject;
                    for (int i = focusHander + 1; i < _dgGrid.InnerGridView.RowCount; i++) {
                        _field_name.ToList().ForEach(NAME => {
                            if (columnName == NAME) {
                                entityDs[i][NAME] = selectObj[NAME];
                            }
                        });

                    }
                }
            }
            catch (Exception e1)
            {
            }
            ;

            Form form = (Form)(((Label)sender).Parent);
            form.Close();
            _isLableClick = false;
        }
    }
}

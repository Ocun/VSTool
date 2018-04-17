using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Digiwin.Common;
using Digiwin.Common.Torridity;
using Digiwin.Common.UI;

namespace Digiwin.ERP.XTEST.UI.Implement {
    [EventInterceptorClass]
    public sealed class LowFill : ServiceComponent {
        private static DigiwinGrid _dgGrid;
        private static string _dgGridName = "TEST";
        private string[] _fieldName = {"TEST1", "TEST2"};
        private bool _isLableClick;


        [EventInterceptor(typeof (IEditorView), "DataSourceChanged")]
        private void Init(object sender, EventArgs e) {
            var currentDocumentWin = GetServiceForThisTypeKey<ICurrentDocumentWindow>();
            if (currentDocumentWin == null) {
                return;
            }

            var ser = GetService<IFindControlService>();
            Control c;
            if (ser.TryGet(_dgGridName, out c)) {
                _dgGrid = c as DigiwinGrid;
                if (_dgGrid != null) {
                    _dgGrid.InnerGridView.MouseDown += InnerGridView_MouseDown;
                }
            }
        }


        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);

        private void InnerGridView_MouseDown(object sender, MouseEventArgs e) {
            try {
                if (e.Button == MouseButtons.Right
                    && e.Clicks == 1) {
                    GridHitInfo hit = _dgGrid.InnerGridView.CalcHitInfo(e.Location);
                    if (hit.InRowCell
                        && hit.RowHandle >= 0) {
                        DependencyObject nowObj =
                            ((DependencyObjectView)
                                (_dgGrid.CurrenctViewRows[hit.RowHandle]))
                                .DependencyObject;
                        if (_fieldName.Contains(hit.Column.FieldName)) {
                            {
                                var form = new Form {
                                    FormBorderStyle = FormBorderStyle.FixedToolWindow,
                                    Size = new Size(120, 25),
                                    ShowInTaskbar = false,
                                    BackColor = SystemColors.Control
                                };
                                form.LostFocus += form_LostFocus;
                                form.ControlBox = false;

                                var lable = new Label {
                                    Text = "向下填充",
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Dock = DockStyle.Fill,
                                    BackColor = SystemColors.Control
                                };
                                lable.Click += form_Click;
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
            catch (Exception) {
            }
            
        }

        private void form_LostFocus(object sender, EventArgs e) {
            if (!_isLableClick) {
                var form = (Form) sender;
                form.Close();
            }
        }


        private void form_Click(object sender, EventArgs e) {
            //执行逻辑
            _isLableClick = true;
            try {
                int focusHander = _dgGrid.InnerGridView.FocusedRowHandle;
                string columnName = _dgGrid.InnerGridView.FocusedColumn.FieldName;
                var bs = _dgGrid.DataSource as BindingSource;
                if (bs != null) {
                    DependencyObjectCollection entityDs =
                        ((DependencyObjectCollectionView<DependencyObjectView>) bs.List).DependencyObjectCollection;
                    var selectValue = _dgGrid.SelectedValue as DependencyObjectView;
                    if (focusHander >= 0
                        && focusHander < entityDs.Count
                        && selectValue != null) {
                        DependencyObject selectObj = selectValue.DependencyObject;
                        for (int i = focusHander + 1; i < _dgGrid.InnerGridView.RowCount; i++) {
                            int i1 = i;
                            _fieldName.ToList().ForEach(name => {
                                if (columnName == name) {
                                    entityDs[i1][name] = selectObj[name];
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception) {
            }
            

            var form = (Form) (((Label) sender).Parent);
            form.Close();
            _isLableClick = false;
        }
    }
}
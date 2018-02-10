using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Digiwin.Common.Advanced;
using Digiwin.Common;
using Digiwin.Common.Torridity;
using Digiwin.Common.Torridity.Metadata;
using Digiwin.ERP.Common.Utils;
using System.IO;
using Digiwin.Common.UI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace Digiwin.ERP.XTEST.UI.Implement {
    public partial class ExportExcelForm : FormBase, IServiceComponentEvents {
        private DependencyObject _entity;
        private DependencyObjectCollection _parentDataSource;
        private IResourceServiceProvider _resourceServiceProvider;
        private ServiceCallContext _serviceCallContext;

        #region Implementation of IServiceComponentEvents

        public override IResourceServiceProvider ResourceServiceProvider {
            get { return _resourceServiceProvider; }
        }

        public ServiceCallContext ServiceCallContext {
            get { return _serviceCallContext; }
        }

        public event ConnectionedEventHandler Connectioned;
        public event DisconnectionedEventHandler Disconnectioned;

        #endregion

        private GridView gridControlField;

        public GridView _gridControl {
            get { return gridControlField; }

            set { gridControlField = value; }
        }


        public ExportExcelForm(DependencyObject entity,
            IResourceServiceProvider resourceServiceProvider, ServiceCallContext serviceCallContext) {
            this._entity = entity;
            _resourceServiceProvider = resourceServiceProvider;
            _serviceCallContext = serviceCallContext;
            InitializeComponent();
        }

        public ExportExcelForm(GridView gridControl, IResourceServiceProvider resourceServiceProvider,
            ServiceCallContext serviceCallContext) {
            InitializeComponent();
            _gridControl = gridControl;
            _resourceServiceProvider = resourceServiceProvider;
            _serviceCallContext = serviceCallContext;
        }


        private void ExportByGD(string path) {
            if (File.Exists(path)) {
                if (DigiwinMessageBox.Show(Properties.Resources.String12, MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information) == DialogResult.Cancel) {
                    return;
                }
            }
            string fileExtenstion = new FileInfo(path).Extension;
            //设置自动设置列宽

            _gridControl.OptionsPrint.AutoWidth = false;
            switch (fileExtenstion) {
                case ".xls":
                    _gridControl.ExportToXls(path);
                    break;
                case ".xlsx":
                    _gridControl.ExportToXlsx(path);
                    break;
                case ".rtf":
                    _gridControl.ExportToRtf(path);
                    break;
                case ".pdf":
                    _gridControl.ExportToPdf(path);
                    break;
                case ".html":
                    _gridControl.ExportToHtml(path);
                    break;
                case ".mht":
                    _gridControl.ExportToMht(path);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 单身转datatable
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private DataTable DOCToDataTable(DataTable targetTable) {
            BindingSource source = _gridControl.DataSource as BindingSource;

            var myDataSource2 = source.List as DependencyObjectCollectionView<DependencyObjectView>; // 获得grid控件的绑定的数据源
            var colls = myDataSource2.DependencyObjectCollection;

            for (int i = 0; i < _gridControl.Columns.Count; i++) {
                var Column = _gridControl.Columns[i];
                if (Column.Visible) {
                    try {
                        string colName = Column.FieldName;
                        Type type = Column.ColumnType;
                        DataColumn dc = new DataColumn(colName, type);
                        string caption = Column.Caption;
                        dc.Caption = caption;
                        targetTable.Columns.Add(dc);
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            foreach (DependencyObject item in colls) {
                DataRow dr = targetTable.NewRow();

                foreach (DataColumn prop in targetTable.Columns) {
                    dr[prop.ColumnName] = item[prop.ColumnName];
                }
                targetTable.Rows.Add(dr);
            }
            return targetTable;
        }

        private void digiwinButton2_Click(object sender, EventArgs e) {
            if (Maths.IsEmpty(this.digiwinLabelTextBox1.Text)) {
                DigiwinMessageBox.ShowError(Properties.Resources.String10);
                return;
            }
            string saveFilePath = this.digiwinLabelTextBox1.Text;
            if (Maths.IsEmpty(saveFilePath)) {
                DigiwinMessageBox.ShowError(Properties.Resources.String10);
                return;
            }
            else {
                if (!Directory.Exists(Path.GetDirectoryName(saveFilePath))) {
                    DigiwinMessageBox.ShowError(Properties.Resources.String9);
                    return;
                }
                else {
                    //方式2，以样式grid导出
                    //ExportByGD(saveFilePath);
                    //this.DialogResult = DialogResult.OK;
                    //return;


                    DataTable dt = new DataTable();
                    dt = DOCToDataTable(dt);
                    ExportByNPOI(dt, saveFilePath);
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        ///  将DataTable导出
        /// </summary> 
        /// <param name="dt">DataTable</param>
        /// <param name="saveFilePath">保存位置</param>
        private void ExportByNPOI(DataTable dt, string saveFilePath) {
            //NPOI到处excel
            IWorkbook workbook;
            string fileExt = Path.GetExtension(saveFilePath);
            if (fileExt.Equals(".xls")) {
                workbook = new HSSFWorkbook();
            }
            else if (fileExt.Equals(".xlsx")) {
                workbook = new XSSFWorkbook();
            }
            else {
                return;
            }
            ISheet sheet = workbook.CreateSheet("Sheet1");
            try {
                //设置格式
                ICellStyle HeadercellStyle = workbook.CreateCellStyle();
                HeadercellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                HeadercellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                HeadercellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                HeadercellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                HeadercellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                //字体
                NPOI.SS.UserModel.IFont headerfont = workbook.CreateFont();
                headerfont.Boldweight = (short) FontBoldWeight.Bold;
                HeadercellStyle.SetFont(headerfont);


                //用Caption 作为列名
                int icolIndex = 0;
                IRow headerRow = sheet.CreateRow(0);

                //配置列名
                foreach (DataColumn item in dt.Columns) {
                    ICell cell = headerRow.CreateCell(icolIndex);
                    cell.SetCellValue(item.Caption);
                    cell.CellStyle = HeadercellStyle;
                    icolIndex++;
                }
                //匹配格式
                ICellStyle cellStyle = workbook.CreateCellStyle();

                //为避免日期格式被Excel自动替换，所以设定 format 为 『@』 表示一率当成text來看
                cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

                //字体
                IFont cellfont = workbook.CreateFont();
                cellfont.Boldweight = (short) FontBoldWeight.Normal;
                cellStyle.SetFont(cellfont);

                ICellStyle dateStyle = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

                //建立内容行
                int iRowIndex = 1;
                int iCellIndex = 0;

                foreach (DataRow Rowitem in dt.Rows) {
                    IRow DataRow = sheet.CreateRow(iRowIndex);
                    foreach (DataColumn Colitem in dt.Columns) {
                        ICell newCell = DataRow.CreateCell(iCellIndex);

                        string drValue = Rowitem[Colitem].ToString();

                        switch (Colitem.DataType.ToString()) {
                            case "System.String": //字符串类型   
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime": //日期类型   
                                DateTime dateV;

                                DateTime.TryParse(drValue, out dateV);

                                if (dateV.Year == 1900) {
                                    newCell.SetCellValue("");
                                    break;
                                }
                                newCell.SetCellValue(dateV);
                                newCell.CellStyle = dateStyle; //格式化显示   
                                break;
                            case "System.Boolean": //布尔型   
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16": //整型   
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal": //浮点型   
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull": //空值处理   
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                        //cell.SetCellValue(rowObject[columnIds[columnIndex]].ToString());
                        //cell.CellStyle = cellStyle;
                        iCellIndex++;
                    }
                    iCellIndex = 0;
                    iRowIndex++;
                }

                //自适应列宽度
                for (int i = 0; i < icolIndex; i++) {
                    sheet.AutoSizeColumn(i);
                }
                for (int columnNum = 0; columnNum <= dt.Columns.Count; columnNum++) {
                    int columnWidth = sheet.GetColumnWidth(columnNum)/256;
                    for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++) {
                        IRow currentRow;
                        //当前行未被使用过
                        if (sheet.GetRow(rowNum) == null) {
                            currentRow = sheet.CreateRow(rowNum);
                        }
                        else {
                            currentRow = sheet.GetRow(rowNum);
                        }

                        if (currentRow.GetCell(columnNum) != null) {
                            ICell currentCell = currentRow.GetCell(columnNum);
                            int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                            if (columnWidth < length) {
                                columnWidth = length;
                            }
                        }
                    }
                    sheet.SetColumnWidth(columnNum, columnWidth*256);
                }

                //写Excel
                if (File.Exists(saveFilePath)) {
                    if (
                        DigiwinMessageBox.Show(Properties.Resources.String12, MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information) == DialogResult.Cancel) {
                        return;
                    }
                }

                using (FileStream file = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write)) {
                    workbook.Write(file);
                }

                //导出后打开文件
                //DialogResult dialogResult = DigiwinMessageBox.Show(Properties.Resources.String3,
                //    MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                //if (dialogResult == DialogResult.OK)
                //{
                //    //打开文件
                //    System.Diagnostics.Process process = new System.Diagnostics.Process();
                //    process.StartInfo.FileName = saveFilePath;
                //    try
                //    {
                //        process.Start();
                //    }
                //    catch (System.ComponentModel.Win32Exception we)
                //    {
                //        MessageBox.Show(we.Message);
                //        return;
                //    }
                //}
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                workbook = null;
            }
        }


        private void digiwinButton3_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //选择导出路径
        private void digiwinButton1_Click(object sender, EventArgs e) {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件(*.xls*.xlsx)|*.xls;*.xlsx";
            saveDialog.FilterIndex = 0;
            saveDialog.RestoreDirectory = true;
            saveDialog.OverwritePrompt = false;
            DialogResult dialogResult = saveDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) {
                return;
            }
            this.digiwinLabelTextBox1.Text = saveDialog.FileName;
        }

     
    }
}
// create By 08628 20180411

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Digiwin.Chun.Common.Controller;
using Digiwin.Chun.Common.Views;

namespace Digiwin.Chun.Common.EventHandler {
    /// <summary>
    ///     事件帮助类
    /// </summary>
    public class EventHelper {
        /// <summary>
        /// </summary>
        public static Form VsForm { get; set; }


        /// <summary>
        ///     防止重获焦点时，选择项瞬间跳离的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MyTreeView_Leave(object sender, EventArgs e) {
            var mytreeView = sender as MyTreeView;
            if (mytreeView != null)
                mytreeView.SelectedNode = null;
        }


        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TxtNewTypeKey_TextChanged(object sender, EventArgs e) {
            var toolpars = MyTool.Toolpars;
            if (!toolpars.FormEntity.IsModi)
                return;
            if (!string.Equals(toolpars.FormEntity.TxtToPath, string.Empty, StringComparison.Ordinal)
                && !string.Equals(toolpars.FormEntity.TxtNewTypeKey, string.Empty, StringComparison.Ordinal)
                && !string.Equals(toolpars.FormEntity.PkgTypekey, string.Empty, StringComparison.Ordinal)
            ) {
                var pathInfo = toolpars.PathEntity;
                var pkgDir = pathInfo.PkgTypeKeyFullRootDir;
                if (Directory.Exists(pkgDir))
                    ControlTool.MyPaintTreeView(pkgDir);
                else
                    GetControlByName<MyTreeView>("treeView1").Nodes.Clear();
            }
            else {
                GetControlByName<MyTreeView>("treeView1").Nodes.Clear();
            }
        }

        #region 拖拽到主窗体

        /// <summary>
        ///     拖拽到主窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ScrollPanel_DragEnter(object sender, DragEventArgs e) {
            var myTreeView1 = GetControlByName<MyTreeView>("myTreeView1");
            if (myTreeView1?.SelectedNode == null)
                return;
            var node = myTreeView1.SelectedNode as MyTreeNode;
            var b = node?.BuildeType.Id.Equals("MYTools");
            if (b == null || !(bool) b)
                return;
            var fileList = (Array) e.Data.GetData(DataFormats.FileDrop);
            var f = true;
            foreach (var filePath in fileList) {
                string[] extName = {".lnk", ".exe"};
                var exeExtension = Path.GetExtension(filePath.ToString());
                if (!extName.Contains(exeExtension))
                    f = false;
            }
            if (!f)
                return;

            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetControlByName<T>(string name) where T : class {
            try {
                var controls = VsForm.Controls.Find(name, true);
                T tagertControl = null;
                foreach (var c in controls) {
                    tagertControl = c as T;
                    if (tagertControl != null)
                        break;
                }
                return tagertControl;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
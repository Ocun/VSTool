// create By 08628 20180411

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.UI;

namespace Common.Implement.Tools {
    /// <summary>
    ///     treeView生成辅助
    /// </summary>
    public static class TreeViewTool {
        /// <summary>
        ///     创建右面板
        /// </summary>
        /// <param name="myTreeView5"></param>
        public static void CreateRightView(MyTreeView myTreeView5) {
            myTreeView5.Nodes.Clear();
            CreateRightView(myTreeView5.Nodes);
            myTreeView5.ExpandAll();
        }

        /// <summary>
        /// </summary>
        /// <param name="mytree"></param>
        /// <param name="buildeEntity"></param>
        /// <param name="showCheck"></param>
        public static void CreateTree(MyTreeView mytree, List<BuildeType> buildeEntity,
            bool showCheck) {
            mytree.Nodes.Clear();
            if (buildeEntity == null || buildeEntity.Count <= 0)
                return;
            var item = buildeEntity.ToList();
            item.ForEach(buildeType => {
                var node = CreateTree(buildeType, showCheck, false);
                if (node != null)
                    mytree.Nodes.Add(node);
            });
        }

        /// <summary>
        /// </summary>
        /// <param name="buildeType"></param>
        /// <param name="showCheck"></param>
        /// <param name="readSubView"></param>
        /// <returns></returns>
        public static TreeNode CreateTree(BuildeType buildeType, bool showCheck, bool readSubView) {
            var text = buildeType.Name ?? string.Empty;
            if (buildeType.Visiable != null && buildeType.Visiable.Equals("False") && buildeType.BuildeItems == null)
                return null;
            var newChild = new MyTreeNode(text) {BuildeType = buildeType};
            if (showCheck)
                if (buildeType.ShowCheckedBox != null
                    && buildeType.ShowCheckedBox.Equals("False")) {
                }
                else {
                    if (buildeType.Checked != null &&
                        buildeType.Checked.Equals("True") &&
                        buildeType.ReadOnly != null &&
                        buildeType.ReadOnly.Equals("True")
                    )
                        newChild.BuildeType.FileInfos = MyTool.CreateFileMappingInfo(newChild.BuildeType);
                    newChild.CheckBoxVisible = true;
                }
            //读下层目录
            else if (readSubView && buildeType.BuildeItems != null && buildeType.BuildeItems.Length > 0)
                buildeType.BuildeItems.ToList().ForEach(buildeItem => {
                    newChild.Nodes.Add(CreateTree(buildeItem, false, true));
                });
            return newChild;
        }

        /// <summary>
        ///     根据标准路径生成结点
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static MyTreeNode MyPaintTreeView(string fullPath) {
            var dirName = Path.GetFileName(fullPath);
            var node = new MyTreeNode(dirName) {CheckBoxVisible = false};
            var dirs = new DirectoryInfo(fullPath);
            var dir = dirs.GetDirectories();
            var file = dirs.GetFiles();
            if (!dir.Any()
                && file.Length == 0)
                node.CheckBoxVisible = false;
            var dircount = dir.Count();
            var filecount = file.Count();
            for (var i = 0; i < dircount; i++) {
                var pathNode = $@"{fullPath}\{dir[i].Name}";
                var newChild = MyPaintTreeView(pathNode);

                node.Nodes.Add(newChild);
            }

            for (var j = 0; j < filecount; j++) {
                var fullName = file[j].FullName;
                var extensionName = Path.GetExtension(fullName);
                string[] extensionNames = {".cs", ".resx"};

                if (!extensionNames.Contains(extensionName))
                    continue;
                var newChild = new MyTreeNode(file[j].Name) {CheckBoxVisible = true};
                var bt = new BuildeType();
                var infos = new List<FileInfos>();
                var info = new FileInfos {
                    FromPath = fullName,
                    ToPath = fullName
                };

                //string oldTypeKey = Toolpars.formEntity.txtNewTypeKey.Substring(1);
                //string toPath = info.FromPath.Replace(oldTypeKey, Toolpars.formEntity.txtNewTypeKey);
                //info.ToPath = toPath;

                infos.Add(info);
                bt.FileInfos = infos;
                newChild.BuildeType = bt;
                node.Nodes.Add(newChild);
            }
            return node;
        }


        #region CreateRightView

        /// <summary>
        ///     创建右面板
        /// </summary>
        /// <param name="nodes"></param>
        public static void CreateRightView(TreeNodeCollection nodes) {
            var toolPar = MyTool.Toolpars;
            var buildeEntity = toolPar.BuilderEntity.BuildeTypies;
            if (buildeEntity == null)
                return;
            var item = buildeEntity.ToList();
            item.ForEach(buildeType => {
                var node = CreateTree(buildeType);
                if (node != null)
                    nodes.Add(node);
            });
        }

        /// <summary>
        /// 创建右侧message
        /// </summary>
        /// <param name="buildeType"></param>
        /// <returns></returns>
        public static MyTreeNode CreateTree(BuildeType buildeType) {
            var text = buildeType.Name ?? string.Empty;
            var newChild = new MyTreeNode(text);
            var existedFile = true;
            newChild.BuildeType = buildeType;
            if (newChild.BuildeType.FileInfos == null
                || newChild.BuildeType.FileInfos.Count == 0)
                if (buildeType.BuildeItems != null
                    && buildeType.BuildeItems.Length > 0)
                    buildeType.BuildeItems.ToList().ForEach(buildeItem => {
                        var node = CreateTree(buildeItem);
                        if (node != null)
                            newChild.Nodes.Add(node);
                    });
                else
                    existedFile = false;
            else
                newChild.BuildeType.FileInfos.ToList().ForEach(createfile => {
                    var fileChild = new MyTreeNode(createfile.FileName);
                    newChild.Nodes.Add(fileChild);
                });
            if (existedFile && newChild.Nodes.Count > 0)
                return newChild;
            return null;
        }

        #endregion
    }
}
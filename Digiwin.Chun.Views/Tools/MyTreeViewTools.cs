// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Digiwin.Chun.Common.Tools;
using Digiwin.Chun.Models;

namespace Digiwin.Chun.Views.Tools {
    /// <summary>
    ///     treeView生成辅助
    /// </summary>
    public static class MyTreeViewTools {
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
        /// 创建主视图TreeNode
        /// </summary>
        /// <param name="mytree"></param>
        /// <param name="buildeEntity"></param>
        /// <param name="showCheck"></param>
        public static void CreateMainTreeNode(MyTreeView mytree, List<BuildeType> buildeEntity,
            bool showCheck ) {
            mytree.Nodes.Clear();
            if (buildeEntity == null || buildeEntity.Count <= 0)
                return;
            var item = buildeEntity.ToList();

            item.ForEach(buildeType => {
                var node = SetNodeInfo(buildeType, showCheck, false);
                if (node != null)
                    mytree.Nodes.Add(node);
            });
            //if (!editState)
            //    return;
            //var newBt = new BuildeType
            //{
            //    Id = "MainAddItemID",
            //    Description = "单击添加你的项目",
            //    EditState = "True"
            //};

            //var newChild = new MyTreeNode("添加你的选项") { BuildeType = newBt };
            //mytree.Nodes.Add(newChild);
        }

        /// <summary>
        /// 设置节点信息
        /// </summary>
        /// <param name="buildeType"></param>
        /// <param name="showCheck"></param>
        /// <param name="readSubView"></param>
        /// <returns></returns>
        public static TreeNode SetNodeInfo(BuildeType buildeType, bool showCheck, bool readSubView) {
            var text = buildeType.Name ?? string.Empty;
            if (PathTools.IsFasle(buildeType.Visiable) && buildeType.BuildeItems == null)
                return null;
            var newChild = new MyTreeNode(text) {BuildeType = buildeType};
            if (showCheck)
                if (PathTools.IsFasle(buildeType.ShowCheckedBox)) {
                }
                else {
                    if (PathTools.IsTrue(buildeType.Checked) &&
                        PathTools.IsTrue(buildeType.ReadOnly)
                    )
                        newChild.BuildeType.FileInfos = MyTools.CreateFileMappingInfo(newChild.BuildeType,
                            $"Create{newChild.BuildeType.Id}", $"Create{newChild.BuildeType.Id}");
                    newChild.CheckBoxVisible = true;
                }
            //读下层目录
            else if (readSubView && buildeType.BuildeItems != null && buildeType.BuildeItems.Length > 0)
                buildeType.BuildeItems.ToList().ForEach(buildeItem => {
                    newChild.Nodes.Add(SetNodeInfo(buildeItem, false, true));
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
            var toolPar = MyTools.Toolpars;
            var buildeEntity = toolPar.BuildeEntity.BuildeTypies.Where(builderItem=>!PathTools.IsFasle(builderItem.Visiable)).ToList();
            var item = buildeEntity.ToList();
            item.ForEach(buildeType => {
                var node = CreateMainTreeNode(buildeType);
                if (node != null)
                    nodes.Add(node);
            });
        }

        /// <summary>
        ///     创建右侧message
        /// </summary>
        /// <param name="buildeType"></param>
        /// <returns></returns>
        public static MyTreeNode CreateMainTreeNode(BuildeType buildeType) {
            var text = buildeType.Name ?? string.Empty;
            var newChild = new MyTreeNode(text);
            var existedFile = true;
            newChild.BuildeType = buildeType;
            if (newChild.BuildeType.FileInfos == null
                || newChild.BuildeType.FileInfos.Count == 0)
                if (buildeType.BuildeItems != null
                    && buildeType.BuildeItems.Length > 0)
                    buildeType.BuildeItems.ToList().ForEach(buildeItem => {
                        if (!PathTools.IsFasle(buildeItem.Visiable)) {
                            var node = CreateMainTreeNode(buildeItem);
                            if (node != null)
                                newChild.Nodes.Add(node);
                        }
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

        /// <summary>
        ///     获取全部TreeView
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static List<MyTreeView> GetTreeViews(Control control) {
            if (control == null)
                throw new ArgumentNullException(nameof(control));
            var treeViewList = new List<MyTreeView>();
            var container = control as SplitContainer;
            if (container == null)
                return treeViewList;
            var treeViewControl = container.Panel1.Controls[0];
            if (treeViewControl is MyTreeView)
                treeViewList.Add(treeViewControl as MyTreeView);
            var splitContainerControl = container.Panel2.Controls[0];
            if (splitContainerControl is SplitContainer)
                treeViewList.AddRange(GetTreeViews(splitContainerControl));
            return treeViewList;
        }


        /// <summary>
        ///     获取全部SpiltContainer
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static List<SplitContainer> GetSplitContainerList(IDisposable control) {
            var splitContainerList = new List<SplitContainer>();
            var container = control as SplitContainer;
            if (container == null)
                return splitContainerList;
            splitContainerList.Add(container);
            var panel2Control = container.Panel2.Controls[0];
            if (panel2Control is SplitContainer)
                splitContainerList.AddRange(GetSplitContainerList(panel2Control));
            return splitContainerList;
        }

        #endregion
    }
}
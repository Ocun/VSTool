// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.UI;

namespace Common.Implement.Tools
{
    public static class TreeViewTool
    {
        public static void CreateRightView(MyTreeView myTreeView5,toolpars Toolpars)
        {
            myTreeView5.Nodes.Clear();
            CreateRightView(Toolpars, myTreeView5.Nodes);
            myTreeView5.ExpandAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mytree"></param>
        /// <param name="BuildeEntity"></param>
        /// <param name="showCheck"></param>
        public static void createTree(toolpars Toolpars, MyTreeView mytree, List<BuildeType> BuildeEntity, bool showCheck)
        {
            mytree.Nodes.Clear();
            if (BuildeEntity != null
                && BuildeEntity.Count > 0)
            {
                var item = BuildeEntity.ToList();
                item.ForEach(BuildeType => { mytree.Nodes.Add(CreateTree(Toolpars, BuildeType, showCheck, false)); });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildeType"></param>
        /// <param name="type">是否读取子节点 </param>
        /// <returns></returns>
        public static TreeNode CreateTree(toolpars Toolpars, BuildeType buildeType, bool showCheck, bool readSubView)
        {
            string text = buildeType.Name ?? String.Empty;
            MyTreeNode new_child = new MyTreeNode(text);
            new_child.buildeType = buildeType;
            if (showCheck)
            {
                if (buildeType.ShowCheckedBox != null
                    && buildeType.ShowCheckedBox.Equals("False"))
                {

                }
                else
                {
                    //if (buildeType.Checked != null &&
                    //    buildeType.Checked.Equals("True") &&
                    //    buildeType.ReadOnly != null &&
                    //    buildeType.ReadOnly.Equals("True")
                    //)
                    //{
                    //    new_child.buildeType.FileInfos = MyTool.createFileMappingInfo(Toolpars, new_child.buildeType);
                    //}
                    new_child.CheckBoxVisible = true;
                }
            }
            //读下层目录
            else if (readSubView && buildeType.BuildeItems.Length > 0)
            {
                buildeType.BuildeItems.ToList().ForEach(BuildeItem => {
                    new_child.Nodes.Add(CreateTree(Toolpars, BuildeItem, showCheck, readSubView));
                });
            }
            return new_child;
        }


        #region CreateRightView

        /// <summary>
        /// 创建右面板
        /// </summary>
        /// <param name="ToolPar"></param>
        /// <param name="nodes"></param>
        public static void CreateRightView(toolpars ToolPar, TreeNodeCollection nodes)
        {
            var BuildeEntity = ToolPar.BuilderEntity.BuildeTypies;
            if (BuildeEntity != null)
            {
                var item = BuildeEntity.ToList();
                item.ForEach(buildeType => {
                    var node = createTree(buildeType);
                    if (node != null)
                    {
                        nodes.Add(node);
                    }
                });
            }
        }

        public static MyTreeNode createTree(BuildeType buildeType)
        {
            string text = buildeType.Name ?? String.Empty;
            MyTreeNode new_child = new MyTreeNode(text);
            bool existedFile = true;
            new_child.buildeType = buildeType;
            if (new_child.buildeType.FileInfos == null
                || new_child.buildeType.FileInfos.Count == 0)
            {
                //读下层目录
                if (buildeType.BuildeItems != null
                    && buildeType.BuildeItems.Length > 0)
                {
                    buildeType.BuildeItems.ToList().ForEach(BuildeItem => {
                        var node = createTree(BuildeItem);
                        if (node != null)
                        {
                            new_child.Nodes.Add(node);
                        }
                    });
                }
                else
                {
                    existedFile = false;
                }
            }
            else
            {
                new_child.buildeType.FileInfos.ToList().ForEach(createfile => {
                    MyTreeNode file_child = new MyTreeNode(createfile.FileName);
                    new_child.Nodes.Add(file_child);
                });
            }
            if (existedFile && new_child.Nodes.Count > 0)
                return new_child;
            else
            {
                return null;
            }
        }

        #endregion
        /// <summary>
        /// 根据标准路径生成结点
        /// </summary>
        /// <param name="Toolpars"></param>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static MyTreeNode myPaintTreeView(toolpars Toolpars, string fullPath)
        {

            string DirName = Path.GetFileName(fullPath);
            MyTreeNode Node = new MyTreeNode(DirName) { CheckBoxVisible = false };
            DirectoryInfo dirs = new DirectoryInfo(fullPath);
            DirectoryInfo[] dir = dirs.GetDirectories();
            FileInfo[] file = dirs.GetFiles();
            if (!dir.Any()
                && file.Length == 0)
            {
                Node.CheckBoxVisible = false;
            }
            int dircount = dir.Count();
            int filecount = file.Count();
            for (int i = 0; i < dircount; i++)
            {

                string pathNode = fullPath + @"\" + dir[i].Name;
                MyTreeNode new_child = myPaintTreeView(Toolpars, pathNode);

                Node.Nodes.Add(new_child);
            }

            for (int j = 0; j < filecount; j++)
            {

                string fullName = file[j].FullName;
                string extensionName = Path.GetExtension(fullName);
                string[] extensionNames = { ".cs", ".resx" };

                if (extensionNames.Contains(extensionName))
                {
                    MyTreeNode new_child = new MyTreeNode(file[j].Name);
                    new_child.CheckBoxVisible = true;
                    BuildeType bt = new BuildeType();
                    List<FileInfos> infos = new List<FileInfos>();
                    FileInfos info = new FileInfos();
                    info.FromPath = fullName;

                    //string oldTypeKey = Toolpars.formEntity.txtNewTypeKey.Substring(1);
                    //string toPath = info.FromPath.Replace(oldTypeKey, Toolpars.formEntity.txtNewTypeKey);
                    //info.ToPath = toPath;

                    info.ToPath = fullName;
                    infos.Add(info);
                    bt.FileInfos = infos;
                    new_child.buildeType = bt;
                    Node.Nodes.Add(new_child);
                }
            }
            return Node;
        }
    }
}

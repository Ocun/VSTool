﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Common.Implement.Entity;
using Common.Implement.UI;

namespace Common.Implement {
    public class paloTool {
        /// <summary>
        /// 把文件拷入指定的文件夹
        /// </summary>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        /// <param name="filterName"></param>
        public static void copyTo(toolpars Toolpars, string fromDir, string toDir, List<string> fileNames,
            string fromTypeKey, string ToTypeKey) {
            var formFiles = GetFilePath(fromDir);
            foreach (var file in formFiles) {
                if (!File.Exists(file))
                    continue;
                FileInfo fileinfo = new FileInfo(file);
                string file_name = Path.GetFileNameWithoutExtension(file);
                if (!fileNames.Contains(file_name)) continue;
                string absolutedir = fileinfo.Directory.FullName.Replace(fromDir, String.Empty);
                string absolutePath = file.Replace(fromDir, String.Empty);
                string newFilePath = Path.Combine(toDir, absolutePath);
                string newFileDir = Path.Combine(toDir, absolutedir).Replace(fromTypeKey, ToTypeKey);
                if (!Directory.Exists(newFileDir)) {
                    Directory.CreateDirectory(newFileDir);
                }
                if (File.Exists(newFilePath)) {
                    if (MessageBox.Show("文件已存在，是否覆盖？", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        != DialogResult.OK) {
                        return;
                    }
                }
                fileinfo.CopyTo(newFilePath);
                changeText(newFilePath, fromTypeKey, ToTypeKey);
                FileInfo newfileinfo = new FileInfo(newFilePath);
                var dir = newfileinfo.Directory;

                //dir.Parent
                //Tools.ModiCS(mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                //                            ".Business.Implement.csproj", "Interceptor\\DetailInterceptorS" + ra + ".cs");
            }
            ;
        }

        /// <summary>
        /// 获取项目配置路径
        /// </summary>
        /// <returns></returns>
        private static string getcsproj(DirectoryInfo dir) {
            //if(dir!=null)
            return null;
        }

        /// <summary>
        /// 递归获取指定文件夹内所有文件全路径
        /// </summary>
        /// <param name="dirpath"></param>
        /// <returns></returns>
        private static List<string> GetFilePath(string dirpath) {
            List<string> filepathList = new List<string>();
            if (Directory.Exists(dirpath)) {
                DirectoryInfo dirinfo = new DirectoryInfo(dirpath);
                //递归目录
                DirectoryInfo[] childDirList = dirinfo.GetDirectories();
                if (childDirList.Length > 0) {
                    childDirList.ToList().ForEach(a => {
                            var res = GetFilePath(a.FullName);
                            if (res.Count > 0) {
                                filepathList.AddRange(res);
                            }
                        }
                    );
                }
                //文件
                FileInfo[] filepaths = dirinfo.GetFiles();
                filepaths.ToList().ForEach(a => filepathList.Add(a.FullName));
            }


            return filepathList;
        }

        /// <summary>
        /// 查找指定目录下文件，返回路径
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        private static string SearchFile(string dirpath, string searchFile, bool containerSub) {
            string filePath = String.Empty;
            if (Directory.Exists(dirpath)) {
                DirectoryInfo dirinfo = new DirectoryInfo(dirpath);
                FileInfo[] filepaths = dirinfo.GetFiles();
                var fob = filepaths.ToList().FirstOrDefault(file => file.Name.Equals(searchFile));
                if (fob != null) {
                    filePath = fob.FullName;
                }
                else if (containerSub) {
                    DirectoryInfo[] childDirList = dirinfo.GetDirectories();
                    if (childDirList.Length > 0) {
                        childDirList.ToList().ForEach(a => {
                                var res = SearchFile(a.FullName, searchFile, containerSub);
                                if (!res.Equals(String.Empty)) {
                                    filePath = res;
                                }
                            }
                        );
                    }
                }
            }
            return filePath;
        }

        /// <summary>
        /// 指定文件文本替换
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fromName"></param>
        /// <param name="toName"></param>
        private static void changeText(string path, string fromName, string toName) {
            string con = "";

            string text = File.ReadAllText(path);
            text = text.Replace(fromName, toName);
            File.SetAttributes(path, FileAttributes.Normal);
            File.Delete(path);
            File.WriteAllText(path, text, System.Text.UTF8Encoding.UTF8);


            //using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            //{
            //    using (StreamReader sr = new StreamReader(fs))
            //    {
            //        con = sr.ReadToEnd();
            //        con = con.Replace(fromName, toName);
            //        sr.Close();

            //        FileStream fs2 = new FileStream(path, FileMode.Open, FileAccess.Write);
            //        StreamWriter sw = new StreamWriter(fs2, Encoding.UTF8);
            //        sw.WriteLine(con);
            //        sw.Flush();
            //        sw.Close();
            //        fs2.Close();

            //    }


            //}
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mytree"></param>
        /// <param name="BuildeEntity"></param>
        /// <param name="showCheck"></param>
        public static void createTree(toolpars Toolpars, MyTreeView mytree, List<BuildeType> BuildeEntity, bool showCheck) {
            mytree.Nodes.Clear();
            if (BuildeEntity != null
                && BuildeEntity.Count > 0) {
                var item = BuildeEntity.ToList();
                item.ForEach(BuildeType => { mytree.Nodes.Add(CreateTree(Toolpars,BuildeType, showCheck, false)); });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildeType"></param>
        /// <param name="type">是否读取子节点 </param>
        /// <returns></returns>
        public static TreeNode CreateTree(toolpars Toolpars,BuildeType buildeType, bool showCheck, bool readSubView) {
            string text = buildeType.Name ?? String.Empty;
            MyTreeNode new_child = new MyTreeNode(text);
            new_child.buildeType = buildeType;
            if (showCheck) {
                if (buildeType.ShowCheckedBox != null
                    && buildeType.ShowCheckedBox.Equals("False")) {

                }
                else {
                    if (buildeType.ReadOnly != null && 
                        buildeType.ReadOnly.Equals("True")
                        ) {
                        new_child.buildeType.FileInfos = createFileMappingInfo(Toolpars, new_child.buildeType);
                    }
                        new_child.CheckBoxVisible = true;
                }
            }
            //读下层目录
            else if (readSubView && buildeType.BuildeItems.Length > 0) {
                buildeType.BuildeItems.ToList().ForEach(BuildeItem => {
                    new_child.Nodes.Add(CreateTree(Toolpars,BuildeItem, showCheck, readSubView));
                });
            }
            return new_child;
        }


        /// <summary>
        /// 將dll 考入平臺目錄
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        /// <param name="filterStr"></param>
        public static void FileCopyUIdll(string fromPath, string toPath, string filterStr) {
            try {
                string[] filedir = Directory.GetFiles(fromPath, filterStr,
                    SearchOption.AllDirectories);
                foreach (var mfile in filedir) {
                    File.Copy(mfile, mfile.Replace(fromPath, toPath), true);
                }
                MessageBox.Show("复制成功 !!!");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void copyDll(toolpars Toolpars) {
            var PathEntity = Toolpars.PathEntity;
            if (PathEntity != null) {
                string serverPath = PathEntity.ServerProgramsPath;
                string clientPath = PathEntity.DeployProgramsPath;
                string businessDllFullPath = PathEntity.ExportPath + PathEntity.BusinessDllName;
                string ImplementDllFullPath = PathEntity.ExportPath + PathEntity.ImplementDllName;
                string UIDllFullPath = PathEntity.ExportPath + PathEntity.UIDllName;
                string UIImplementDllFullPath = PathEntity.ExportPath + PathEntity.UIImplementDllName;
                //Path.GetDirectoryName()
                string toPath = String.Empty;
                if (File.Exists(businessDllFullPath)) {
                    if (Directory.Exists(serverPath)) {
                        toPath = serverPath + PathEntity.BusinessDllName;
                        File.Copy(businessDllFullPath, toPath, true);
                    }
                    if (Directory.Exists(clientPath)) {
                        toPath = clientPath + PathEntity.BusinessDllName;
                        File.Copy(businessDllFullPath, toPath, true);
                    }
                }
                if (File.Exists(ImplementDllFullPath)) {
                    if (Directory.Exists(serverPath)) {
                        File.Copy(ImplementDllFullPath,
                            serverPath + PathEntity.ImplementDllName, true);
                    }
                }
                if (File.Exists(UIDllFullPath)) {
                    if (Directory.Exists(clientPath)) {
                        File.Copy(UIDllFullPath,
                            clientPath + PathEntity.UIDllName, true);
                    }
                }
                if (File.Exists(UIImplementDllFullPath)) {
                    if (Directory.Exists(clientPath)) {
                        File.Copy(UIImplementDllFullPath,
                            clientPath + PathEntity.UIImplementDllName, true);
                    }
                }
            }
        }


        #region CreateRightView

        /// <summary>
        /// 创建右面板
        /// </summary>
        /// <param name="ToolPar"></param>
        /// <param name="nodes"></param>
        public static void CreateRightView(toolpars ToolPar, TreeNodeCollection nodes) {
            var BuildeEntity = ToolPar.BuilderEntity.BuildeTypies;
            if (BuildeEntity != null) {
                var item = BuildeEntity.ToList();
                item.ForEach(buildeType => {
                    var node = createTree(buildeType);
                    if (node != null) {
                        nodes.Add(node);
                    }
                });
            }
        }

        public static MyTreeNode createTree(BuildeType buildeType) {
            string text = buildeType.Name ?? String.Empty;
            MyTreeNode new_child = new MyTreeNode(text);
            bool existedFile = true;
            new_child.buildeType = buildeType;
            if (new_child.buildeType.FileInfos == null
                || new_child.buildeType.FileInfos.Count == 0) {
                //读下层目录
                if (buildeType.BuildeItems != null
                    && buildeType.BuildeItems.Length > 0) {
                    buildeType.BuildeItems.ToList().ForEach(BuildeItem => {
                        var node = createTree(BuildeItem);
                        if (node != null) {
                            new_child.Nodes.Add(node);
                        }
                    });
                }
                else {
                    existedFile = false;
                }
            }
            else {
                new_child.buildeType.FileInfos.ToList().ForEach(createfile => {
                    MyTreeNode file_child = new MyTreeNode(createfile.FileName);
                    new_child.Nodes.Add(file_child);
                });
            }
            if (existedFile && new_child.Nodes.Count > 0)
                return new_child;
            else {
                return null;
            }
        }

        #endregion

        #region Copy

        /// <summary>
        /// 找到文件所属第一个CSproj
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static string findCSproj(DirectoryInfo Dir) {
            string csproj = string.Empty;
            if (Dir.Exists) {
                var fileInfo = Dir.GetFiles();
                var fcs = fileInfo.FirstOrDefault(f => f.Extension.Equals(@".csproj"));
                if (fcs == null) {
                    csproj = findCSproj(Dir.Parent);
                }
                else {
                    csproj = fcs.FullName;
                }
            }
            return csproj;
        }

        /// <summary>
        /// 查看文件是否生成
        /// </summary>
        /// <param name="treeView"></param>
        /// <returns></returns>
        public static List<string> getExistedMsg(Dictionary<string, List<FileInfos>> pathDic) {
            List<string> msgList = new List<string>();
            foreach (var kv in pathDic) {
                var msg = kv.Key + ":" + Environment.NewLine;
                var value = kv.Value;
                var f = false;
                value.ForEach(path => {
                    var toPath = path.ToPath;
                    if (File.Exists(toPath)) {
                        ///模板内代码片段是否生成
                        if (path.PartId != null
                            && !path.PartId.Equals(string.Empty)
                            && path.IsMerge != null
                            && !path.IsMerge.Equals(string.Empty)
                        ) {
                        }
                        else {
                            f = true;
                            var filename = Path.GetFileName(toPath);
                            msg += filename + Environment.NewLine;
                        }
                    }
                });
                if (f)
                    msgList.Add(msg);
            }
            return msgList;
        }


        public static void testR(string path) {
            string text = File.ReadAllText(path);
            // string csName = @"(?<=[^\/\:]\s+class\s+)[^\n\:{]+(?=[:,{])";
            string csName = @"(?<=[^\/\:]\s+(class|interface)\s+)[^\n\:{]+(?=[\n\:{])";
            var test = Regex.Match(text, csName).Value;
            //text = text.Replace("TestsService", csname.Substring(1));
            //text = text.Replace("Digiwin.ERP." + Toolpars.OldTypekey,
            //"Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
            //File.SetAttributes(f.FullName, FileAttributes.Normal);
            //File.Delete(f.FullName);
            //File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
        }

        public static bool CreateFile(MyTreeView treeView, toolpars ToolPars) {
            // var test = Regex.Match("http://192.168.1.250:9595/", @"(?<=://)[a-zA-Z\.0-9-_]+(?=[:,\/])").Value;

            var pathDic = GetTreeViewFilePath(treeView.Nodes, ToolPars);
            var msgList = getExistedMsg(pathDic);
            bool f = true;
            bool success = true;

            #region 检查覆盖

            if (msgList.Count > 0) {
                var msg = string.Empty;
                msgList.ForEach(str => { msg += str + Environment.NewLine; });
                if (MessageBox.Show(msg + "是否覆盖？", "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    != DialogResult.Yes) {
                    f = false;
                    success = false;
                }
                ;
            }

            #endregion

            if (f) {
                #region 从配置创建目录 及 基本的文件 

                CreateBaseItem(ToolPars);

                #endregion

                #region Copy 并修改 csproj

                try {
                    foreach (var kv in pathDic) {
                        foreach (var path in kv.Value) {
                            if (!File.Exists(path.FromPath)) {
                                MessageBox.Show(string.Format("{0}/{1}模板不存在，请检查", kv.Key, path.FileName), "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            FileInfo fileinfo = new FileInfo(path.FromPath);

                            var toPath = path.ToPath;
                            var parentDir = Path.GetDirectoryName(toPath);
                            if (!Directory.Exists(parentDir)) {
                                Directory.CreateDirectory(parentDir);
                            }
                            if (File.Exists(toPath)) {
                                if (path.IsMerge != null
                                    && !path.IsMerge.Equals(string.Empty)) {
                                    //將唯讀權限拿掉
                                    File.SetAttributes(toPath, FileAttributes.Normal);
                                    FindPartAndInsert(path);
                                }
                                else {
                                    //將唯讀權限拿掉
                                    File.SetAttributes(toPath, FileAttributes.Normal);
                                    //修改
                                    fileinfo.CopyTo(toPath, true);
                                }
                            }
                            else {
                                if (path.PartId != null
                                    && !path.PartId.Equals(string.Empty)) {
                                    FindPartAndInsert(path);
                                }
                                else {
                                    fileinfo.CopyTo(toPath);
                                }
                            }

                            testR(toPath);

                            #region 修改csproj

                            fileinfo = new FileInfo(toPath);
                            var dirInfo = fileinfo.Directory;
                            string csPath = findCSproj(dirInfo);
                            string csDir = string.Format("{0}\\", Path.GetDirectoryName(csPath));

                            int index = toPath.IndexOf(csDir);

                            if (index > -1) {
                                ModiCS(csPath, toPath.Substring(index + csDir.Length));
                            }

                            #endregion
                        }
                        ;
                    }
                }
                catch (Exception ex) {
                    success = false;
                }

                #endregion
            }
            if (success) {
                WriteLog(ToolPars, treeView);
                InitBuliderEntity(ToolPars.BuilderEntity.BuildeTypies);
                MessageBox.Show("生成成功");
            }
            return success;
        }

        #region __insertPart__

        /// <summary>
        /// 查找代码片段，插入指定文件内
        /// </summary>
        static void FindPartAndInsert(FileInfos fileinfo) {
            if (fileinfo.PartId != null
                && !fileinfo.PartId.Equals(string.Empty)
            ) {
                var fromPath = fileinfo.FromPath;
                var toPath = fileinfo.ToPath;
                string text = File.ReadAllText(fromPath);
                string inserthere = @"__InsertHere__";
                string fromMatch =
                    string.Format(@"(?<=(\#region\s+{0}))[\s\S\w\r\n]*(?=(\#endregion\s+{1}))",
                        fileinfo.Id, fileinfo.Id);
                string toMatch =
                    string.Format(@"(?<=(\#region\s+{0}))[\s\S\w\r\n]*(?=(\#endregion\s+{1}))",
                        inserthere, inserthere);
                string fromTest = Regex.Match(text, fromMatch).Value;
                string target = string.Empty;


                if (!File.Exists(toPath)) {
                    target = text;
                    
                }
                else {
                    target = File.ReadAllText(toPath);
                    File.SetAttributes(toPath, FileAttributes.Normal);
                }
                Regex regex = new Regex(toMatch);
                target = regex.Replace(target, fromTest);

                #region 替换插入位置

                regex = new Regex(string.Format(@"\#region\s+{0}", inserthere));
                target = regex.Replace(target, string.Format(@"#region {0}", fileinfo.Id));
                regex = new Regex(string.Format(@"\#endregion\s+{0}", inserthere));


                string newInsertHere =
                    string.Format(
                        "#endregion {0}\r\n        #region __InsertHere__ \r\n        #endregion __InsertHere__",
                        fileinfo.Id);

                target = regex.Replace(target, newInsertHere);
                #endregion
                
                File.WriteAllText(toPath, target, System.Text.UTF8Encoding.UTF8);
            }
        }

        #endregion __insertPart__

        #region  修改解决方案的csproj文件 添加类文件

        public static void ModiCS(string xmlPath, string CSName) {
            if (CSName.Contains(".designer.cs")) {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                XmlElement xe1 = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI); //创建一个<book>节点
                xe1.SetAttribute("Include", CSName); //设置该节点genre属性 
                var loc = xmlDoc.CreateElement("DependentUpon", xmlDoc.DocumentElement.NamespaceURI);
                String msname = CSName.Substring(CSName.LastIndexOf("\\") + 1);
                msname = msname.Substring(0, msname.IndexOf("."));
                msname = msname + ".cs";
                //loc.InnerText = model.loc;
                loc.InnerText = msname;
                xe1.AppendChild(loc);
                //xe1.InnerText = "WPF";
                var notExisted = checkXmlNode(root, CSName);
                if (notExisted) {
                    root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }
            else if (CSName.Contains(".resx")) {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                XmlElement xe1 = xmlDoc.CreateElement("EmbeddedResource", xmlDoc.DocumentElement.NamespaceURI);
                //创建一个<book>节点
                xe1.SetAttribute("Include", CSName); //设置该节点genre属性 
                var loc = xmlDoc.CreateElement("DependentUpon", xmlDoc.DocumentElement.NamespaceURI);
                String msname = CSName.Substring(CSName.LastIndexOf("\\") + 1);
                msname = msname.Substring(0, msname.IndexOf("."));
                msname = msname + ".cs";
                //loc.InnerText = model.loc;
                loc.InnerText = msname;
                xe1.AppendChild(loc);
                //xe1.InnerText = "WPF";
                var notExisted = checkXmlNode(root, CSName);
                if (notExisted) {
                    root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }

            else {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                XmlElement xe1 = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI); //创建一个<book>节点
                xe1.SetAttribute("Include", CSName); //设置该节点genre属性    
                var notExisted = checkXmlNode(root, CSName);
                if (notExisted) {
                    root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }
        }

        static bool checkXmlNode(XmlNode root, string name) {
            #region 判断是否已经添加

            bool notExisted = true;
            foreach (XmlNode node in root.ChildNodes) {
                if (node.Attributes["Include"] != null) {
                    if (node.Attributes["Include"].Value.Equals(name)) {
                        notExisted = false;
                        break;
                    }
                }
            }

            #endregion

            return notExisted;
        }

        #endregion

        private static void InitBuliderEntity(BuildeType[] BuildeTypies) {
            BuildeTypies.ToList().ForEach(item => {
                item.FileInfos = new List<FileInfos>();
                item.Checked = "False";
                if (item.BuildeItems != null
                    && item.BuildeItems.Length > 0) {
                    InitBuliderEntity(item.BuildeItems);
                }
            });
        }

        /// <summary>
        /// 一些必考项目,模板目录
        /// </summary>
        public static void CreateBaseItem(toolpars ToolPars) {
            var TemplateType = ToolPars.SettingPathEntity.TemplateTypeKey;
            var newTypeKey = ToolPars.formEntity.txtNewTypeKey;
            var file_mapping = ToolPars.FileMappingEntity;
            var fileInfo = file_mapping.MappingItems.ToList().FirstOrDefault(filmap =>
                filmap.Id.Equals("BaseItem")
            );
            fileInfo.Paths.ToList().ForEach(path => {
                var oldFileName = Path.GetFileNameWithoutExtension(path);
                var fromPath = ToolPars.MVSToolpath + @"Template\" + path;

                var newFilePath = path.Replace(TemplateType, newTypeKey);
                newFilePath = ToolPars.GToIni + @"\" + newFilePath;

                if (!File.Exists(newFilePath)) {
                    var fileinfo = new FileInfo(fromPath);
                    var NewDir = Path.GetDirectoryName(newFilePath);
                    if (!Directory.Exists(NewDir)) {
                        Directory.CreateDirectory(NewDir);
                    }
                    fileinfo.CopyTo(newFilePath);
                    string slnPath = Path.GetExtension(newFilePath);

                    changeText(newFilePath, TemplateType, newTypeKey);
                }
            });
        }


        /// <summary>
        /// 获取节点地址与文件地址
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static Dictionary<string, List<FileInfos>> GetTreeViewFilePath(TreeNodeCollection nodes,
            toolpars ToolPars) {
            Dictionary<string, List<FileInfos>> paths = new Dictionary<string, List<FileInfos>>();

            foreach (MyTreeNode node in nodes) {
                var filesInfo = node.buildeType.FileInfos;
                if (filesInfo == null
                    || filesInfo.Count == 0) {
                    if (node.Nodes.Count > 0) {
                        var cpaths = GetTreeViewFilePath(node.Nodes, ToolPars);
                        foreach (var kv in cpaths) {
                            var keys = paths.Keys;
                            var key = kv.Key;
                            var value = kv.Value;
                            if (keys.Contains(key)) {
                                var newV = paths[key];
                                newV.AddRange(value);
                                updatePath(newV, ToolPars);
                                paths[key] = newV;
                            }
                            else {
                                updatePath(value, ToolPars);
                                paths.Add(kv.Key, value);
                            }
                        }
                    }
                }
                else {
                    string nodePath = node.FullPath;
                    var keys = paths.Keys;
                    if (keys.Contains(nodePath)) {
                        var newV = paths[nodePath];
                        newV.AddRange(filesInfo);
                        updatePath(newV, ToolPars);
                        paths[nodePath] = newV;
                    }
                    else {
                        updatePath(filesInfo, ToolPars);
                        paths.Add(nodePath, filesInfo);
                    }
                }
            }
            return paths;
        }

        static void updatePath(List<FileInfos> FileInfos, toolpars ToolPars) {
            var TemplateType = ToolPars.SettingPathEntity.TemplateTypeKey;
            var newTypeKey = ToolPars.formEntity.txtNewTypeKey;
            FileInfos.ForEach(fileinfo => {
                    var BasePath = fileinfo.BasePath;
                    var oldFilePath = Path.GetFileNameWithoutExtension(BasePath);
                    var newFilePath = BasePath.Replace(TemplateType, newTypeKey)
                        .Replace(oldFilePath, fileinfo.FileName);
                    fileinfo.ToPath = ToolPars.GToIni + @"\" + newFilePath;
                }
            );
        }

        public static List<FileInfos> createFileMappingInfo(toolpars _toolpars, BuildeType bt) {
            List<FileInfos> FileInfos = new List<Entity.FileInfos>();
            var fileMapping = _toolpars.FileMappingEntity;
            string Id = bt.Id;
            var fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                filmap.Id.Equals(Id)
            );
            if (fileInfo?.Paths != null) {
                if (fileInfo.Paths.Count() == 1) {
                    FileInfos fileinfo = new FileInfos();
                    fileinfo.actionNameFiled = "";
                    fileinfo.ClassName = string.Format("Create{0}", Id);
                    ;
                    fileinfo.FileName = string.Format("Create{0}", Id);
                    ;
                    fileinfo.FunctionName = string.Format("Create{0}", Id);
                    ;
                    var path = fileInfo.Paths[0];
                    fileinfo.BasePath = fileInfo.Paths[0];
                    var fromPath = _toolpars.MVSToolpath + @"\Template\" + path;
                    fileinfo.FromPath = fromPath;
                    var oldFilePath = Path.GetFileNameWithoutExtension(path);
                    var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);
                    fileinfo.ToPath = _toolpars.GToIni + @"\" + newFilePath;
                    if (bt.PartId != null
                        && !bt.PartId.Equals(string.Empty)) {
                        fileinfo.PartId = bt.PartId;
                        fileinfo.PartId = bt.Id;
                        fileinfo.IsMerge = bt.IsMerge;
                    }
                    FileInfos.Add(fileinfo);
                }
                else {
                    fileInfo.Paths.ToList().ForEach(path => {
                        string ClassNameFiled = Path.GetFileName(path);
                        FileInfos fileinfo = new FileInfos();
                        fileinfo.actionNameFiled = "";
                        fileinfo.ClassName = ClassNameFiled;
                        fileinfo.FileName = ClassNameFiled;
                        fileinfo.FunctionName = "";
                        fileinfo.BasePath = path;
                        var fromPath = _toolpars.MVSToolpath + @"\Template\" + path;
                        fileinfo.FromPath = fromPath;
                        var oldFilePath = Path.GetFileNameWithoutExtension(path);
                        var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);

                        fileinfo.ToPath = _toolpars.GToIni + @"\" + newFilePath;

                        FileInfos.Add(fileinfo);
                    });
                }
            }

            return FileInfos;
        }

        #endregion


        #region 日志

        /// <summary>
        /// 日志
        /// </summary>
        public static void WriteLog(toolpars Toolpars, MyTreeView treeView) {
            var pathDic = GetTreeViewFilePath(treeView.Nodes, Toolpars);
            string txtNewTypeKey = Toolpars.formEntity.txtNewTypeKey;
            string varAppPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log";
            if (!Directory.Exists(varAppPath)) {
                Directory.CreateDirectory(varAppPath);
            }
            string logPath = string.Format(@"{0}\\{1}.log", varAppPath,
                Toolpars.CustomerName == null || Toolpars.CustomerName.Equals(string.Empty)
                    ? DateTime.Now.ToString("yyyyMMddhhmmss")
                    : Toolpars.CustomerName);


            StringBuilder logStr = new StringBuilder();
            string headStr = string.Empty;
            for (int i = 0; i <= 80; i++) {
                headStr += "_";
            }
            logStr.AppendLine(headStr).AppendLine(string.Format("    # CREATEDATE   {0}",
                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")))
                .AppendLine(string.Format("    # CREATEBY  {0}", Environment.MachineName))
                .AppendLine(string.Format("    # TYPEKEY  {0}", txtNewTypeKey)).AppendLine();


            string head = DateTime.Now.ToString("hh:mm:ss:fff");
            foreach (var kv in pathDic) {
                foreach (var fileinfo in kv.Value) {
                    logStr.AppendLine(String.Format("    # {0} {1}", kv.Key, fileinfo.FunctionName));
                }
            }
            logStr.AppendLine(headStr);
            using (StreamWriter SW = new StreamWriter(logPath, true, Encoding.UTF8)) {
                SW.WriteLine(logStr.ToString());
                SW.Flush();
                SW.Close();
            }
            ;
        }

        public static void writeToServer(toolpars Toolpars, Dictionary<string, List<FileInfos>> pathDic) {
            foreach (var kv in pathDic) {
                kv.Value.ForEach(fileinfo => {
                    // sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "Create" + Toolpars.formEntity.txtNewTypeKey);
                    string year = DateTime.Now.ToString("yyyyMMdd");
                    string demandId = string.Format("S01231_{0}_01", year);
                    sqlTools.insertToolInfo(demandId, year,
                        Toolpars.formEntity.txtNewTypeKey + "_" + fileinfo.FunctionName);
                });
            }
        }

        #endregion


        public static void modiXml(string xmlPath,string Id,string value) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList xns = xmlDoc.SelectNodes("//BuildeItem");
            foreach (XmlNode xn in xns) {
                var childNodes = xn.ChildNodes;
                bool f = true;
                foreach (XmlNode node in childNodes) {
                    if (node.Name == "Id"
                        && node.InnerText.Equals(Id)) {
                        f = false;
                        break;
                    }
                    else {
                        break;
                    }
                }
                if (!f) {
                    foreach (XmlNode node in childNodes)
                    {
                        if (node.Name == "Url") {
                            node.InnerText = value;
                            break;
                        }
                     
                    }
                  
                    break;
                }

            }
             xmlDoc.Save(xmlPath);

        }

       public static void OpenTools(string path) {
          
            try
            {
                Process p = new Process();
                var infos = Process.GetProcesses();
                string exeName = Path.GetFileNameWithoutExtension(path);
                bool f = true;
                foreach (var info in infos) {
                    if (info.ProcessName.ToUpper().Contains(exeName.ToUpper())) {
                        f = false;
                        break;
                    }
                }
                if (f) {
                    p.StartInfo.FileName = path;

                    p.Start();
                }
                else {
                    MessageBox.Show("已启动！");
                }
            }
            catch (Exception e0)
            {
                MessageBox.Show("启动应用程序时出错！原因：" + e0.Message);
            }
        }

        #region 修改
        public static MyTreeNode myPaintTreeView(toolpars Toolpars,string fullPath) {

            string DirName = Path.GetFileName(fullPath);
            MyTreeNode Node = new MyTreeNode(DirName) { CheckBoxVisible = false };
            DirectoryInfo dirs = new DirectoryInfo(fullPath);
            DirectoryInfo[] dir = dirs.GetDirectories();
            FileInfo[] file = dirs.GetFiles();
            if (!dir.Any()
                && file.Length == 0) {
                Node.CheckBoxVisible = false;
            }

            int dircount = dir.Count();
            int filecount = file.Count();
            for (int i = 0; i < dircount; i++)
            {
           
                string pathNode = fullPath + @"\" + dir[i].Name;
                MyTreeNode new_child = myPaintTreeView(Toolpars,pathNode);

                Node.Nodes.Add(new_child);
            }

            for (int j = 0; j < filecount; j++) {

                string fullName = file[j].FullName;
                string extensionName =  Path.GetExtension(fullName);
                string[] extensionNames = {".cs",".resx" };

                if (extensionNames.Contains(extensionName)) {
                    MyTreeNode new_child = new MyTreeNode(file[j].Name);
                    new_child.CheckBoxVisible = true;
                    BuildeType bt = new BuildeType();
                    List<FileInfos> infos = new List<FileInfos>();
                    FileInfos info = new FileInfos();
                    info.FromPath = fullName;

                    string oldTypeKey = Toolpars.formEntity.txtNewTypeKey.Substring(1);
                    string toPath = info.FromPath.Replace(oldTypeKey, Toolpars.formEntity.txtNewTypeKey);
                    info.ToPath = toPath;

                    info.ToPath = fullName;
                    infos.Add(info);
                    bt.FileInfos = infos;
                    new_child.buildeType = bt;
                    Node.Nodes.Add(new_child);
                }
            }
            return Node;
        } 

        #endregion

    }
}
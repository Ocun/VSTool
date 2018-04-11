// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.UI;

namespace Common.Implement.Tools
{
    public class MyTool {

        #region 作废
        /// <summary>
        /// 把文件拷入指定的文件夹
        /// </summary>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        /// <param name="filterName"></param>
        public static void copyTo(toolpars Toolpars, string fromDir, string toDir, List<string> fileNames,
            string fromTypeKey, string ToTypeKey)
        {
            var formFiles = GetFilePath(fromDir);
            foreach (var file in formFiles)
            {
                if (!File.Exists(file))
                    continue;
                FileInfo fileinfo = new FileInfo(file);
                string file_name = Path.GetFileNameWithoutExtension(file);
                if (!fileNames.Contains(file_name)) continue;
                string absolutedir = fileinfo.Directory.FullName.Replace(fromDir, String.Empty);
                string absolutePath = file.Replace(fromDir, String.Empty);
                string newFilePath = Path.Combine(toDir, absolutePath);
                string newFileDir = Path.Combine(toDir, absolutedir).Replace(fromTypeKey, ToTypeKey);
                if (!Directory.Exists(newFileDir))
                {
                    Directory.CreateDirectory(newFileDir);
                }
                if (File.Exists(newFilePath))
                {
                    if (MessageBox.Show("文件已存在，是否覆盖？", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        != DialogResult.OK)
                    {
                        return;
                    }
                }
                fileinfo.CopyTo(newFilePath);
                changeText(newFilePath, fromTypeKey, ToTypeKey);
                FileInfo newfileinfo = new FileInfo(newFilePath);
                var dir = newfileinfo.Directory;

                //dir.Parent
                //OldTools.ModiCS(mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                //                            ".Business.Implement.csproj", "Interceptor\\DetailInterceptorS" + ra + ".cs");
            }
            ;
        }

        #endregion

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
        
        #region Copy

        /// <summary>
        /// 找到文件所属第一个CSproj,这有问题！！
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
                if (MessageBox.Show(msg + "已存在是否覆盖？", "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    != DialogResult.Yes) {
                    f = false;
                    success = false;
                }
                ;
            }

            #endregion

            if (f) {
                try {
                    #region 从配置创建目录 及 基本的文件 

                    CreateBaseItem(ToolPars);

                    #endregion

                    #region Copy 并修改 csproj
                    bool checkTemplate = true;
                    foreach (var kv in pathDic) {
                        foreach (var path in kv.Value) {
                            if (!File.Exists(path.FromPath)) {
                                MessageBox.Show(string.Format("{0}/{1}模板不存在，请检查", kv.Key, path.FileName), "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                checkTemplate = false;
                                break;
                            }
                        }
                        if (!checkTemplate) {
                            break;
                        }
                    }
                    if (!checkTemplate) {
                        return false;
                    }
                    foreach (var kv in pathDic) {
                        foreach (var path in kv.Value) {
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
                            
                            #region 修改解决方案

                            fileinfo = new FileInfo(toPath);
                            var dirInfo = fileinfo.Directory;
                            string csPath = findCSproj(dirInfo);
                            string csDir = string.Format("{0}\\", Path.GetDirectoryName(csPath));

                            //找到相对位置
                            int index = toPath.IndexOf(csDir);

                            if (index > -1) {
                                XMLTools.ModiCS(csPath, toPath.Substring(index + csDir.Length));
                            }

                            #endregion
                        }
                        ;
                    }
                    ModiFiles(ToolPars, ToolPars.OldTypekey, ToolPars.formEntity.txtNewTypeKey);
                }
                catch (Exception ex) {
                    success = false;
                }

                #endregion
            }
            if (success) {
                LogTool.WriteLog(ToolPars, treeView);
                InitBuilderEntity(ToolPars);
                MessageBox.Show("生成成功");
            }
            return success;
        }

        public static void InitBuilderEntity(toolpars ToolPars)
        {
            try
            {
                string Path = string.Format(@"{0}Config\BuildeEntity.xml", ToolPars.MVSToolpath);
                ToolPars.BuilderEntity = ReadToEntityTools.ReadToEntity<BuildeEntity>(Path);
                var BuildeTypies = ToolPars.BuilderEntity.BuildeTypies;
                InitBuildeTypies(BuildeTypies, ToolPars);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void InitBuildeTypies(BuildeType[] BuildeTypies, toolpars ToolPars)
        {
            BuildeTypies.ToList().ForEach(item => {
                if (item.Checked != null
                    && item.Checked.Equals("True")
                )
                {
                    item.FileInfos = createFileMappingInfo(ToolPars, item);
                }
                if (item.BuildeItems != null)
                {
                    InitBuildeTypies(item.BuildeItems, ToolPars);
                }
            });
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
                }
            });
        }

        static void ModiFiles(toolpars Toolpars, string oldKey, string newKey) {
          
            //个案路径
            string DirectoryPath = string.Format(@"{0}\Digiwin.ERP.{1}\", Toolpars.GToIni, newKey);
            DirectoryInfo tDes = new DirectoryInfo(DirectoryPath);

            List<string> tSearchPatternList = new List<string>();
            tSearchPatternList.AddRange(new[] {
                "*xml",
                "*.sln",
                "*.repx",
                "*proj",
                "*.complete",
                "*.cs"
            });
            for (int i = 0; i < tSearchPatternList.Count;  i++) {
                foreach (FileInfo f in tDes.GetFiles(tSearchPatternList[i], SearchOption.AllDirectories)) {
                    string filePath = f.FullName;
                    if (File.Exists(filePath)) {
                        string text = File.ReadAllText(filePath);
                        text = text.Replace("Digiwin.ERP." + oldKey,
                            "Digiwin.ERP." + newKey);
                       // text = text.Replace(@"<HintPath>..\..\", @"<HintPath>..\..\..\..\..\WD_PR\SRC\");
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        File.Delete(filePath);
                        File.WriteAllText(filePath, text, System.Text.UTF8Encoding.UTF8);
                    }
                }
            }
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
            List<FileInfos> FileInfos = new List<FileInfos>();
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
        /// <summary>
        /// 目录下的文件copy至另一目录 ,先copy 再替换 typekey
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="pDistFolder"></param>
        public static void CopyPKG(string pFileName, string pDistFolder,List<FileInfos> filesinfo)
        {

            //文件夹替换，递归
            if (Directory.Exists(pFileName))
            {
                // Folder
                var di = new DirectoryInfo(pFileName);
                //子文件夹
                var diries = di.GetDirectories();
                //文件
                var files = di.GetFiles();
                //if (!Directory.Exists(pDistFolder)) {
                //    Directory.CreateDirectory(pDistFolder);
                //}
                foreach (DirectoryInfo d in diries)
                {
                    string tFolderPath = pDistFolder + @"\" + d.Name;
                    //if (!Directory.Exists(tFolderPath))
                    //{
                    //    Directory.CreateDirectory(tFolderPath);
                    //}
                    CopyPKG(d.FullName, tFolderPath, filesinfo);
                }
                foreach (FileInfo f in files) {
                    string[] filter = {
                        ".sln",".csproj"
                    };
                    
                    string frompath = f.FullName;
                    var Selected = filesinfo.FirstOrDefault(file => file.FromPath.Equals(frompath));
                    string extionName = Path.GetExtension(frompath);
                    if (Selected != null 
                        || filter.Contains(extionName)
                        ) {
                            CopyPKG(f.FullName, pDistFolder + @"\" + f.Name, filesinfo);
                    }
                }
            }
            else if (File.Exists(pFileName))
            {
                //文件夹
                if (!Directory.Exists(pDistFolder.Remove(pDistFolder.LastIndexOf("\\"))))
                {
                    Directory.CreateDirectory(pDistFolder.Remove(pDistFolder.LastIndexOf("\\")));
                }
                if (File.Exists(pDistFolder))
                {
                    File.SetAttributes(pDistFolder, FileAttributes.Normal);
                }
                try
                {
                    File.Copy(pFileName, pDistFolder, true);
                }
                catch
                {
                    
                }
               
            }
        }

        public static bool copyModi(TreeNodeCollection nodes,toolpars ToolPars ) {
            bool sucess = true;
            try {
               
                var fileInfos = GetTreeViewPath(nodes);

                string formDir = ToolPars.formEntity.txtPKGpath + "Digiwin.ERP."
                               + ToolPars.formEntity.PkgTypekey;
                string toDir = ToolPars.formEntity.txtToPath + "\\Digiwin.ERP."
                               + ToolPars.formEntity.txtNewTypeKey;

                CopyPKG(formDir, toDir, fileInfos);
                ModiName(ToolPars);
            }
            catch (Exception ex){
                sucess = false;
            }
          
            return sucess;


        }
        
        /// <summary>
        /// 批量修改个案cs文件
        /// </summary>
        /// <param name="Toolpars"></param>
        public static void ModiName(toolpars Toolpars)
        {
            string txtNewTypeKey = Toolpars.formEntity.txtNewTypeKey;
            string PkgTypekey = Toolpars.formEntity.PkgTypekey;
            //个案路径
            string DirectoryPath = string.Format(@"{0}\Digiwin.ERP.{1}\", Toolpars.GToIni, txtNewTypeKey);
            DirectoryInfo tDes = new DirectoryInfo(DirectoryPath);
            
            List<string> tSearchPatternList = new List<string>();
            tSearchPatternList.AddRange(new[] {
                "*xml",
                "*.sln",
                "*.repx",
                "*proj",
                "*.complete",
                "*.cs"
            });
            foreach (DirectoryInfo d in tDes.GetDirectories("*", SearchOption.AllDirectories))
            {
                //查找不包含txtNewTypeKey的文件夹
                if (d.Name.IndexOf(txtNewTypeKey) == -1)
                {
                    //查找txtPKGpath
                    if (d.Name.IndexOf(PkgTypekey) != -1)
                    {
                        if (
                            File.Exists(d.Parent.FullName + "\\" +
                                        d.Name.Replace(PkgTypekey, txtNewTypeKey)))
                        {
                            File.SetAttributes(
                                d.Parent.FullName + "\\" +
                                d.Name.Replace(PkgTypekey, txtNewTypeKey),
                                FileAttributes.Normal);
                            File.Delete(d.Parent.FullName + "\\" +
                                        d.Name.Replace(PkgTypekey, txtNewTypeKey));
                        }
                        if (
                            Directory.Exists(d.Parent.FullName + "\\" +
                                             d.Name.Replace(PkgTypekey, txtNewTypeKey)) ==
                            false)
                        {
                            d.MoveTo(d.Parent.FullName + "\\" +
                                     d.Name.Replace(PkgTypekey, txtNewTypeKey));
                        }
                        Application.DoEvents();
                    }
                }
            }


            foreach (FileInfo f in tDes.GetFiles("*", SearchOption.AllDirectories))
            {
                if (f.Name.IndexOf(txtNewTypeKey) == -1)
                {
                    if (f.Name.IndexOf(PkgTypekey) != -1)
                    {
                        if (File.Exists(f.FullName))
                        {
                            if (
                                File.Exists(f.Directory.FullName + "\\" +
                                            f.Name.Replace("Digiwin.ERP." + PkgTypekey,
                                                "Digiwin.ERP." + txtNewTypeKey)) == false)
                            {
                                f.MoveTo(f.Directory.FullName + "\\" +
                                         f.Name.Replace("Digiwin.ERP." + PkgTypekey,
                                             "Digiwin.ERP." + txtNewTypeKey));
                            }
                            Application.DoEvents();
                        }
                    }
                }
            }
            List<string> patList = GetFilePath(DirectoryPath);
            for (int i = 0; i < tSearchPatternList.Count; i++)
            {
                foreach (System.IO.FileInfo f in tDes.GetFiles(tSearchPatternList[i], SearchOption.AllDirectories)) {
                    string filePath = f.FullName;
                    if (File.Exists(filePath))
                    {
                        string text = File.ReadAllText(filePath);
                        text = text.Replace("Digiwin.ERP." + PkgTypekey,
                            "Digiwin.ERP." + txtNewTypeKey);
                        text = text.Replace(@"<HintPath>..\..\", @"<HintPath>..\..\..\..\..\WD_PR\SRC\");
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        File.Delete(filePath);
                        File.WriteAllText(filePath, text, System.Text.UTF8Encoding.UTF8);

                        string extionName= Path.GetExtension(filePath);
                        if (extionName.Equals(".csproj")) {

                            XMLTools.XmlNodeByXPath(filePath, @"Compile", patList);
                            XMLTools.XmlNodeByXPath(filePath, @"EmbeddedResource", patList);
                        }
                        Application.DoEvents();
                    }
                }
            }
        }
        public static List<FileInfos> GetTreeViewPath(TreeNodeCollection nodes)
        {
             List<FileInfos> paths = new  List<FileInfos>();

            foreach (MyTreeNode node in nodes)
            {
                var filesInfo = node.buildeType.FileInfos;
                if (filesInfo == null
                    || filesInfo.Count == 0)
                {
                    if (node.Nodes.Count > 0)
                    {
                        var cpaths = GetTreeViewPath(node.Nodes);
                        paths.AddRange(cpaths);
                    }
                }
                else
                {
                    if (node.Checked) {
                        var nodePath = node.buildeType.FileInfos;
                        paths.AddRange(nodePath);
                    }
                }
            }
            return paths;
        }
        #endregion

    }
}
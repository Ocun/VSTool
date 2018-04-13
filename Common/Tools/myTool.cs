// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Properties;
using Common.Implement.UI;
using static System.String;

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
        public static void copyTo(Toolpars Toolpars, string fromDir, string toDir, List<string> fileNames,
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
                string absolutedir = fileinfo.Directory.FullName.Replace(fromDir, Empty);
                string absolutePath = file.Replace(fromDir, Empty);
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
                ChangeText(newFilePath, fromTypeKey, ToTypeKey);
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
            string filePath = Empty;
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
                                if (!res.Equals(Empty)) {
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
        private static void ChangeText(string path, string fromName, string toName) {
            var text = File.ReadAllText(path);
            text = text.Replace(fromName, toName);
            File.SetAttributes(path, FileAttributes.Normal);
            File.Delete(path);
            File.WriteAllText(path, text, Encoding.UTF8);

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
                var filedir = Directory.GetFiles(fromPath, filterStr,
                    SearchOption.AllDirectories);
                foreach (var mfile in filedir) {
                    File.Copy(mfile, mfile.Replace(fromPath, toPath), true);
                }
                MessageBox.Show(Resource.CopySucess);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString(), Resource.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CopyDll(Toolpars toolpars) {
            var pathEntity = toolpars.PathEntity;
            if (pathEntity != null) {
                string serverPath = pathEntity.ServerProgramsPath;
                string clientPath = pathEntity.DeployProgramsPath;
                string businessDllFullPath = pathEntity.ExportPath + pathEntity.BusinessDllName;
                string implementDllFullPath = pathEntity.ExportPath + pathEntity.ImplementDllName;
                string UIDllFullPath = pathEntity.ExportPath + pathEntity.UIDllName;
                string UIImplementDllFullPath = pathEntity.ExportPath + pathEntity.UIImplementDllName;
                //Path.GetDirectoryName()
                string toPath = Empty;
                if (File.Exists(businessDllFullPath)) {
                    if (Directory.Exists(serverPath)) {
                        toPath = serverPath + pathEntity.BusinessDllName;
                        File.Copy(businessDllFullPath, toPath, true);
                    }
                    if (Directory.Exists(clientPath)) {
                        toPath = clientPath + pathEntity.BusinessDllName;
                        File.Copy(businessDllFullPath, toPath, true);
                    }
                }
                if (File.Exists(implementDllFullPath)) {
                    if (Directory.Exists(serverPath)) {
                        File.Copy(implementDllFullPath,
                            serverPath + pathEntity.ImplementDllName, true);
                    }
                }
                if (File.Exists(UIDllFullPath)) {
                    if (Directory.Exists(clientPath)) {
                        File.Copy(UIDllFullPath,
                            clientPath + pathEntity.UIDllName, true);
                    }
                }
                if (File.Exists(UIImplementDllFullPath)) {
                    if (Directory.Exists(clientPath)) {
                        File.Copy(UIImplementDllFullPath,
                            clientPath + pathEntity.UIImplementDllName, true);
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
        public static string FindCSproj(DirectoryInfo Dir) {
            var csproj = Empty;
            if (!Dir.Exists) return csproj;
            var fileInfo = Dir.GetFiles();
            var fcs = fileInfo.FirstOrDefault(f => f.Extension.Equals(@".csproj"));
            csproj = fcs?.FullName ?? FindCSproj(Dir.Parent);
            return csproj;
        }

        /// <summary>
        /// 查看文件是否生成
        /// </summary>
        /// <param name="pathDic"></param>
        /// <returns></returns>
        public static List<string> GetExistedMsg(Dictionary<string, List<FileInfos>> pathDic) {
            var msgList = new List<string>();
            foreach (var kv in pathDic) {
                var msg = kv.Key + ":" + Environment.NewLine;
                var value = kv.Value;
                var f = false;
                value.ForEach(path => {
                    var toPath = path.ToPath;
                    if (File.Exists(toPath)) {
                        if (path.PartId != null
                            && !path.PartId.Equals(Empty)
                            && path.IsMerge != null
                            && !path.IsMerge.Equals(Empty)
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

        public static bool CreateFile(MyTreeView treeView, Toolpars toolPars) {
            // var test = Regex.Match("http://192.168.1.250:9595/", @"(?<=://)[a-zA-Z\.0-9-_]+(?=[:,\/])").Value;

            var pathDic = GetTreeViewFilePath(treeView.Nodes, toolPars);
            var msgList = GetExistedMsg(pathDic);
            var f = true;
            var success = true;

            #region 检查覆盖

            if (msgList.Count > 0) {
                var msg = Empty;
                msgList.ForEach(str => { msg += str + Environment.NewLine; });
                if (MessageBox.Show(msg + Resource.FileExisted, Resource.WarningMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
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

                    CreateBaseItem(toolPars);

                    #endregion

                    #region Copy 并修改 csproj
                    bool checkTemplate = true;
                    foreach (var kv in pathDic) {
                        foreach (var path in kv.Value) {
                            if (!File.Exists(path.FromPath)) {
                                MessageBox.Show(Format("{0}/{1}模板不存在，请检查", kv.Key, path.FileName), Resource.ErrorMsg,
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

                            var fileName = path.FileName;
                            var toPath = path.ToPath;
                            var parentDir = Path.GetDirectoryName(toPath);
                            if (!Directory.Exists(parentDir)) {
                                Directory.CreateDirectory(parentDir);
                            }
                            if (File.Exists(toPath)) {
                                if (path.IsMerge != null
                                    && !path.IsMerge.Equals(Empty)) {
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
                                    && !path.PartId.Equals(Empty)) {
                                    FindPartAndInsert(path);
                                }
                                else {
                                    fileinfo.CopyTo(toPath);
                                }
                            }
                            //修改形如‘_ClassName_’类名为文件名，否则不管
                            string csName = @"(?<=[^\/\:]\s+(class|interface)\s+)[^\n\:{]+(?=[\n\:{])";
                            ReplaceByRegex(toPath, csName, fileName);
                            #region 修改解决方案

                            fileinfo = new FileInfo(toPath);
                            var dirInfo = fileinfo.Directory;
                            string csPath = FindCSproj(dirInfo);
                            string csDir = Format("{0}\\", Path.GetDirectoryName(csPath));

                            //找到相对位置
                            int index = toPath.IndexOf(csDir);

                            if (index > -1) {
                                //添加编译项
                                XMLTools.ModiCS(csPath, toPath.Substring(index + csDir.Length));
                            }

                            #endregion
                        }
                        ;
                    }
                    //修改文件内容，替换typekey
                    ModiFiles(toolPars, toolPars.OldTypekey, toolPars.formEntity.txtNewTypeKey);
                }
                catch (Exception ex) {
                    success = false;
                }

                #endregion
            }
            if (success) {
                LogTool.WriteLog(toolPars, treeView);
                InitBuilderEntity(toolPars);
                MessageBox.Show(Resource.GenerateSucess);
            }
            return success;
        }


        /// <summary>
        /// 初始化，创建完成之后 清空按钮 选择项
        /// </summary>
        /// <param name="ToolPars"></param>
        public static void InitBuilderEntity(Toolpars ToolPars)
        {
            try
            {
                string Path = Format(@"{0}Config\BuildeEntity.xml", ToolPars.MVSToolpath);
                ToolPars.BuilderEntity = ReadToEntityTools.ReadToEntity<BuildeEntity>(Path);
                InitBuildeTypies(ToolPars.BuilderEntity.BuildeTypies,ToolPars);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void InitBuildeTypies(BuildeType[] BuildeTypies, Toolpars ToolPars) {
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


        static void ReplaceByRegex(string filePath,string matchStr,string toStr) {
            if (!File.Exists(filePath))
                return;
            string text = File.ReadAllText(filePath);
            Regex regex = new Regex(matchStr);
            text = regex.Replace(text, toStr);
            File.WriteAllText(filePath, text, Encoding.UTF8);
        }

        /// <summary>
        /// 查找代码片段，插入指定文件内
        /// </summary>
        static void FindPartAndInsert(FileInfos fileinfo) {
            if (fileinfo.PartId != null
                && !fileinfo.PartId.Equals(Empty)
            ) {
                var fromPath = fileinfo.FromPath;
                var toPath = fileinfo.ToPath;
                string text = File.ReadAllText(fromPath);
                string inserthere = @"__InsertHere__";
                string fromMatch =
                    Format(@"(?<=(\#region\s+{0}))[\s\S\w\r\n]*(?=(\#endregion\s+{1}))",
                        fileinfo.Id, fileinfo.Id);
                string toMatch =
                    Format(@"(?<=(\#region\s+{0}))[\s\S\w\r\n]*(?=(\#endregion\s+{1}))",
                        inserthere, inserthere);
                string fromTest = Regex.Match(text, fromMatch).Value;
                string target = Empty;


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

                regex = new Regex(Format(@"\#region\s+{0}", inserthere));
                target = regex.Replace(target, Format(@"#region {0}", fileinfo.Id));
                regex = new Regex(Format(@"\#endregion\s+{0}", inserthere));


                string newInsertHere =
                    Format(
                        "#endregion {0}\r\n        #region __InsertHere__ \r\n        #endregion __InsertHere__",
                        fileinfo.Id);

                target = regex.Replace(target, newInsertHere);
                #endregion
                
                File.WriteAllText(toPath, target, Encoding.UTF8);
            }
        }

        #endregion __insertPart__
        
        /// <summary>
        /// 一些必考项目,模板目录
        /// </summary>
        public static void CreateBaseItem(Toolpars ToolPars) {
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

        /// <summary>
        /// 修改个案文件的typeKey
        /// </summary>
        /// <param name="Toolpars"></param>
        /// <param name="oldKey"></param>
        /// <param name="newKey"></param>
        public static void ModiFiles(Toolpars Toolpars, string oldKey, string newKey) {
          
            //个案路径
            string DirectoryPath = Format(@"{0}\Digiwin.ERP.{1}\", Toolpars.GToIni, newKey);
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
            Toolpars ToolPars) {
            Dictionary<string, List<FileInfos>> paths = new Dictionary<string, List<FileInfos>>();

            foreach (MyTreeNode node in nodes) {
               
                    var filesInfo = node.buildeType.FileInfos;
                    if (filesInfo == null
                        || filesInfo.Count == 0)
                    {
                        if (node.Nodes.Count > 0)
                        {
                            var cpaths = GetTreeViewFilePath(node.Nodes, ToolPars);
                            foreach (var kv in cpaths)
                            {
                                var keys = paths.Keys;
                                var key = kv.Key;
                                var value = kv.Value;
                                if (keys.Contains(key))
                                {
                                    var newV = paths[key];
                                    newV.AddRange(value);
                                    updatePath(newV, ToolPars);
                                    paths[key] = newV;
                                }
                                else
                                {
                                    updatePath(value, ToolPars);
                                    paths.Add(kv.Key, value);
                                }
                            }
                        }
                    }
                    else
                    {
                            string nodePath = node.FullPath;
                            var keys = paths.Keys;
                            if (keys.Contains(nodePath))
                            {
                                var newV = paths[nodePath];
                                newV.AddRange(filesInfo);
                                updatePath(newV, ToolPars);
                                paths[nodePath] = newV;
                            }
                            else
                            {
                                updatePath(filesInfo, ToolPars);
                                paths.Add(nodePath, filesInfo);
                            }
                        
                    
                    }
                
            }
            return paths;
        }

       

        static void updatePath(List<FileInfos> FileInfos, Toolpars ToolPars) {
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

        public static List<FileInfos> createFileMappingInfo(Toolpars _toolpars, BuildeType bt) {
            List<FileInfos> FileInfos = new List<FileInfos>();
            var fileMapping = _toolpars.FileMappingEntity;
            string Id = bt.Id;
            var fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                filmap.Id.Equals(Id)
            );
            if (fileInfo?.Paths != null) {
                if (fileInfo.Paths.Count() == 1) {
                    FileInfos fileinfo = new FileInfos {
                        actionNameFiled = "",
                        ClassName = Format("Create{0}", Id),
                        FileName = Format("Create{0}", Id),
                        FunctionName = Format("Create{0}", Id)
                    };

                    var path = fileInfo.Paths[0];
                    fileinfo.BasePath = fileInfo.Paths[0];
                    var fromPath = _toolpars.MVSToolpath + @"\Template\" + path;
                    fileinfo.FromPath = fromPath;
                    var oldFilePath = Path.GetFileNameWithoutExtension(path);
                    var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);
                    fileinfo.ToPath = _toolpars.GToIni + @"\" + newFilePath;
                    if (bt.PartId != null
                        && !bt.PartId.Equals(Empty)) {
                        fileinfo.PartId = bt.PartId;
                        fileinfo.PartId = bt.Id;
                        fileinfo.IsMerge = bt.IsMerge;
                    }
                    FileInfos.Add(fileinfo);
                }
                else {
                    fileInfo.Paths.ToList().ForEach(path => {
                        string ClassNameFiled = Path.GetFileNameWithoutExtension(path);
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
                    MessageBox.Show(Resource.ExeAlreadyExe);
                }
            }
            catch (Exception e0)
            {
                MessageBox.Show(Resource.ExeExeError + e0.Message);
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

        public static bool copyModi(TreeNodeCollection nodes,Toolpars ToolPars ) {
            bool sucess = true;
            try {
               
                var fileInfos = GetTreeViewPath(nodes);

                string formDir = ToolPars.formEntity.txtPKGpath + "Digiwin.ERP."
                               + ToolPars.formEntity.PkgTypekey;
                string toDir = ToolPars.formEntity.TxtToPath + "\\Digiwin.ERP."
                               + ToolPars.formEntity.txtNewTypeKey;

                CopyPKG(formDir, toDir, fileInfos);
                ModiName(ToolPars, ToolPars.formEntity.PkgTypekey);
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
        public static void ModiName(Toolpars Toolpars, string oldTypeKey)
        {
            string txtNewTypeKey = Toolpars.formEntity.txtNewTypeKey;
            string PkgTypekey = oldTypeKey;// Toolpars.formEntity.PkgTypekey;
            //个案路径
            string DirectoryPath = Format(@"{0}\Digiwin.ERP.{1}\", Toolpars.GToIni, txtNewTypeKey);
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

        #region 一键借用

        public static bool CopyAllPkG(Toolpars Toolpars) {
            bool success = true;
            try
            {

                Toolpars.GToIni = Toolpars.formEntity.TxtToPath;
                if (Toolpars.formEntity.TxtToPath != ""
                    && Toolpars.formEntity.txtNewTypeKey != "")
                {
                    if (Directory.Exists(Toolpars.formEntity.TxtToPath))
                    {

                        DirectoryInfo tCusSRC = new DirectoryInfo(Toolpars.GToIni + @"\");
                        string strb1 = Toolpars.formEntity.txtPKGpath + "Digiwin.ERP."
                                       + Toolpars.formEntity.PkgTypekey;
                        if (!Directory.Exists(strb1))
                        {
                            MessageBox.Show("文件夹" + strb1 + "不存在，请查看！！！", Resource.ErrorMsg, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            success =false;
                        }
                        else
                        {
                            if (
                                Directory.Exists(Path.Combine(Toolpars.GToIni + @"\",
                                    "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey)))
                            {
                                DialogResult result =
                                    MessageBox.Show(
                                        Path.Combine(Toolpars.formEntity.TxtToPath, Toolpars.formEntity.txtNewTypeKey)
                                        + Environment.NewLine+Resource.DirExisted,
                                        "Warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (result == DialogResult.Yes)
                                {
                                    object tArgsPath = Path.Combine(Toolpars.GToIni + @"\",
                                        "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                                    OldTools.DeleteAll(tArgsPath);
                                }
                                else
                                {
                                    success = false; 
                                }
                            }
                            if(success)
                                OldTools.CopyAllPKG(strb1, tCusSRC + "Digiwin.ERP." + Toolpars.formEntity.txtNewTypeKey);
                        }
                    }
                    else
                    {
                        MessageBox.Show("文件夹" + Toolpars.formEntity.TxtToPath + "不存在，请查看！！！", Resource.ErrorMsg,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        success = false; 
                    }
                }
                else
                {
                    MessageBox.Show(Resource.TypekeyNotExisted, Resource.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    success = false; 
                }

                #region 修改命名

                if (success) {
                    ModiName(Toolpars, Toolpars.formEntity.PkgTypekey);
                    MessageBox.Show("生成成功 !!!");
                }
                #endregion

            }
            catch (Exception ex)
            {
                success = false;
                MessageBox.Show(ex.Message.ToString(), Resource.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            new Thread(delegate () {
                   sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "COPY PKG SOURCE");
                }
            ).Start();
            return success;
        }
     
        #endregion

        #endregion



    }
}
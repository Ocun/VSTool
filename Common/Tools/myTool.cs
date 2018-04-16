// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Properties;
using Common.Implement.UI;
using static System.String;
using MSWord = Microsoft.Office.Interop.Word;

namespace Common.Implement.Tools {
    public class MyTool 
    {
        #region 作废

        /// <summary>
        ///     把文件拷入指定的文件夹
        /// </summary>
        /// <param name="toolpars"></param>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        /// <param name="fileNames"></param>
        /// <param name="fromTypeKey"></param>
        /// <param name="toTypeKey"></param>
        public static void CopyTo(Toolpars toolpars, string fromDir, string toDir, List<string> fileNames,
            string fromTypeKey, string toTypeKey) {
            var formFiles = GetFilePath(fromDir);
            foreach (var file in formFiles) {
                if (!File.Exists(file))
                    continue;
                var fileinfo = new FileInfo(file);
                var fileName = Path.GetFileNameWithoutExtension(file);
                if (!fileNames.Contains(fileName))
                    continue;
                if (fileinfo.Directory != null) {
                    var absolutedir = fileinfo.Directory.FullName.Replace(fromDir, Empty);
                    var absolutePath = file.Replace(fromDir, Empty);
                    var newFilePath = Path.Combine(toDir, absolutePath);
                    var newFileDir = Path.Combine(toDir, absolutedir).Replace(fromTypeKey, toTypeKey);
                    if (!Directory.Exists(newFileDir))
                        Directory.CreateDirectory(newFileDir);
                    if (File.Exists(newFilePath))
                        if (MessageBox.Show(Resource.FileExisted, Resource.WarningMsg, MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
                            != DialogResult.OK)
                            return;
                    fileinfo.CopyTo(newFilePath);
                    ChangeText(newFilePath, fromTypeKey, toTypeKey);
                    //var fileInfo = new FileInfo(newFilePath);
                }

                //dir.Parent
                //OldTools.ModiCS(mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                //                            ".Business.Implement.csproj", "Interceptor\\DetailInterceptorS" + ra + ".cs");
            }
        }

        #region 获取应用程序图标 （太小）原准备在treeView生成图标，
        [DllImport("shell32.dll")]
        private static extern int ExtractIconEx(string lpszFile, int niconIndex, IntPtr[] phiconLarge,
            IntPtr[] phiconSmall, int nIcons);

        public static void GetExeIcon(string appPath)
        {
            var appExtension = Path.GetExtension(appPath);
            string[] extensions = { ".exe", "dll" };
            if (!extensions.Contains(appExtension))
                return;
            //第一步：获取程序中的图标数
            var iconCount = ExtractIconEx(appPath, -1, null, null, 0);

            //第二步：创建存放大/小图标的空间
            var largeIcons = new IntPtr[iconCount];
            var smallIcons = new IntPtr[iconCount];
            //第三步：抽取所有的大小图标保存到largeIcons和smallIcons中
            ExtractIconEx(appPath, 0, largeIcons, smallIcons, iconCount);

            //第四步：显示抽取的图标(推荐使用imageList和listview搭配显示）
            var images = new List<Image>();
            for (var i = 0; i < iconCount; i++)
            {
                var icon = Icon.FromHandle(largeIcons[i]);
                images.Add(Image.FromHbitmap(icon.ToBitmap().GetHbitmap())); //图标添加进imageList中
            }
            var exeName = Path.GetFileNameWithoutExtension(appPath);
            //第五步：保存图标
            foreach (var image in images)
                using (var fs =
                    new FileStream($"{Application.StartupPath}\\Images\\{exeName}.png", FileMode.OpenOrCreate))
                {
                    image.Save(fs, ImageFormat.Png);
                }
        }

        #endregion

        #endregion

        /// <summary>
        ///     递归获取指定文件夹内所有文件全路径
        /// </summary>
        /// <param name="dirpath"></param>
        /// <returns></returns>
        private static List<string> GetFilePath(string dirpath) {
            var filepathList = new List<string>();
            if (!Directory.Exists(dirpath))
                return filepathList;
            var dirinfo = new DirectoryInfo(dirpath);
            //递归目录
            var childDirList = dirinfo.GetDirectories();
            if (childDirList.Length > 0)
                childDirList.ToList().ForEach(a => {
                        var res = GetFilePath(a.FullName);
                        if (res.Count > 0)
                            filepathList.AddRange(res);
                    }
                );
            //文件
            var filepaths = dirinfo.GetFiles();
            filepaths.ToList().ForEach(a => filepathList.Add(a.FullName));
            return filepathList;
        }

        /// <summary>
        ///     指定文件文本替换
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
        ///     將dll 考入平臺目錄
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        /// <param name="filterStr"></param>
        public static void FileCopyUIdll(string fromPath, string toPath, string filterStr) {
            try {
                var filedir = Directory.GetFiles(fromPath, filterStr,
                    SearchOption.AllDirectories);
                foreach (var mfile in filedir)
                    File.Copy(mfile, mfile.Replace(fromPath, toPath), true);
                MessageBox.Show(Resource.CopySucess);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Resource.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CopyDll(Toolpars toolpars) {
            var pathEntity = toolpars.PathEntity;
            if (pathEntity == null)
                return;
            var serverPath = pathEntity.ServerProgramsPath;
            var clientPath = pathEntity.DeployProgramsPath;
            var businessDllFullPath = pathEntity.ExportPath + pathEntity.BusinessDllName;
            var implementDllFullPath = pathEntity.ExportPath + pathEntity.ImplementDllName;
            var uiDllFullPath = pathEntity.ExportPath + pathEntity.UIDllName;
            var uiImplementDllFullPath = pathEntity.ExportPath + pathEntity.UIImplementDllName;
            //Path.GetDirectoryName()

            //business.dll
            if (File.Exists(businessDllFullPath)) {
                string toPath;
                if (Directory.Exists(serverPath)) {
                    toPath = serverPath + pathEntity.BusinessDllName;
                    File.Copy(businessDllFullPath, toPath, true);
                }
                if (Directory.Exists(clientPath)) {
                    toPath = clientPath + pathEntity.BusinessDllName;
                    File.Copy(businessDllFullPath, toPath, true);
                }
            }
            //business.implement.dll
            if (File.Exists(implementDllFullPath))
                if (Directory.Exists(serverPath))
                    File.Copy(implementDllFullPath,
                        serverPath + pathEntity.ImplementDllName, true);
            //ui.dll
            if (File.Exists(uiDllFullPath))
                if (Directory.Exists(clientPath))
                    File.Copy(uiDllFullPath,
                        clientPath + pathEntity.UIDllName, true);
            //ui.implement.dll
            if (!File.Exists(uiImplementDllFullPath))
                return;
            if (Directory.Exists(clientPath))
                File.Copy(uiImplementDllFullPath,
                    clientPath + pathEntity.UIImplementDllName, true);
        }

        public static void OpenTools(BuildeType bt) {
            try {
                var p = new Process();
                var infos = Process.GetProcesses();
                var path = bt.Url;
                if (!File.Exists(path))
                    SetToolsPath(bt);
                path = bt.Url;
                var exeName = Path.GetFileNameWithoutExtension(path);
                var f = infos.All(info => exeName != null && !info.ProcessName.ToUpper().Contains(exeName.ToUpper()));
                if (f) {
                    p.StartInfo.FileName = path;

                    p.Start();
                }
                else {
                    MessageBox.Show(Resource.ExeAlreadyExe);
                }
            }
            catch (Exception e0) {
                MessageBox.Show(Resource.ExeExeError + e0.Message);
            }
        }

        public static void SetToolsPath(BuildeType bt) {
            var form = new SetToolPath(bt.Url);
            if (form.ShowDialog() != DialogResult.OK)
                return;
            bt.Url = form.Path;
            XmlTools.ModiXml(AppDomain.CurrentDomain.BaseDirectory + @"Config\BuildeEntity.xml",
                bt.Id, bt.Url);
            //获取Exe图标
            //GetExeIcon(bt.Url);
        }

        #region Copy

        /// <summary>
        ///     找到文件所属第一个CSproj,这有问题！！
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static string FindCSproj(DirectoryInfo dir) {
            var csproj = Empty;
            if (!dir.Exists)
                return csproj;
            var fileInfo = dir.GetFiles();
            var fcs = fileInfo.FirstOrDefault(f => f.Extension.Equals(@".csproj"));
            csproj = fcs?.FullName ?? FindCSproj(dir.Parent);
            return csproj;
        }

        /// <summary>
        ///     查看文件是否生成
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
                    if (File.Exists(toPath))
                        if (path.IsMerge != null
                            && path.IsMerge.Equals("True")
                        ) {
                            //合并文件不报错
                        }
                        else {
                            f = true;
                            var filename = Path.GetFileName(toPath);
                            msg += filename + Environment.NewLine;
                        }
                });
                if (f)
                    msgList.Add(msg);
            }
            return msgList;
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
                if (MessageBox.Show(msg + Resource.FileExisted, Resource.WarningMsg, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning)
                    != DialogResult.Yes) {
                    f = false;
                    success = false;
                }
            }

            #endregion

            if (f) {
                try {
                    #region 从配置创建目录 及 基本的文件 

                    CreateBaseItem(toolPars);

                    #endregion

                    #region Copy 并修改 csproj

                    var checkTemplate = true;
                    foreach (var kv in pathDic) {
                        foreach (var path in kv.Value)
                            if (!File.Exists(path.FromPath)) {
                                MessageBox.Show(string.Format(Resource.TemplateNotExisted, kv.Key, path.FileName),
                                    Resource.ErrorMsg,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                checkTemplate = false;
                                break;
                            }
                        if (!checkTemplate)
                            break;
                    }
                    if (!checkTemplate)
                        return false;

                    foreach (var kv in pathDic)
                    foreach (var path in kv.Value) {
                        var fileinfo = new FileInfo(path.FromPath);

                        var fileName = path.FileName;
                        var toPath = path.ToPath;
                        var parentDir = Path.GetDirectoryName(toPath);
                        if (!Directory.Exists(parentDir))
                            if (parentDir != null)
                                Directory.CreateDirectory(parentDir);
                        if (File.Exists(toPath)) {
                            if (path.IsMerge != null
                                && path.IsMerge.Equals("True")) {
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
                                && !path.PartId.Equals(Empty))
                                FindPartAndInsert(path);
                            else
                                fileinfo.CopyTo(toPath);
                        }
                        //修改形如‘_ClassName_’类名为文件名，否则不管
                        var csName = @"(?<=[^\/\:]\s+(class|interface)\s+)[^\n\:{]+(?=[\n\:{])";
                        ReplaceByRegex(toPath, csName, fileName);

                        #region 修改解决方案

                        fileinfo = new FileInfo(toPath);
                        var dirInfo = fileinfo.Directory;
                        var csPath = FindCSproj(dirInfo);
                        var csDir = $"{Path.GetDirectoryName(csPath)}\\";

                        //找到相对位置
                        var index = toPath.IndexOf(csDir, StringComparison.Ordinal);

                        if (index > -1)
                            XmlTools.ModiCs(csPath, toPath.Substring(index + csDir.Length));

                        #endregion
                    }
                    //修改文件内容，替换typekey
                    ModiFiles(toolPars, toolPars.OldTypekey, toolPars.FormEntity.txtNewTypeKey);
                }
                catch (Exception) {
                    success = false;
                }

                #endregion
            }
            if (!success)
                return false;
            LogTool.WriteLogByTreeView(toolPars, treeView);
            InitBuilderEntity(toolPars);
            MessageBox.Show(Resource.GenerateSucess);
            return true;
        }


        /// <summary>
        ///     初始化，创建完成之后 清空按钮 选择项
        /// </summary>
        /// <param name="toolPars"></param>
        public static void InitBuilderEntity(Toolpars toolPars) {
            try {
                var path = $@"{toolPars.MVSToolpath}Config\BuildeEntity.xml";
                toolPars.BuilderEntity = ReadToEntityTools.ReadToEntity<BuildeEntity>(path);
                InitBuildeTypies(toolPars.BuilderEntity.BuildeTypies, toolPars);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static void InitBuildeTypies(BuildeType[] buildeTypies, Toolpars toolPars) {
            buildeTypies?.ToList().ForEach(item => {
                if (item.Checked != null
                    && item.Checked.Equals("True")
                )
                    item.FileInfos = CreateFileMappingInfo(toolPars, item);
                if (item.BuildeItems != null)
                    InitBuildeTypies(item.BuildeItems, toolPars);
            });
        }

        #region __insertPart__

        private static void ReplaceByRegex(string filePath, string matchStr, string toStr) {
            if (!File.Exists(filePath))
                return;
            var text = File.ReadAllText(filePath);
            var regex = new Regex(matchStr);
            text = regex.Replace(text, toStr);
            //修改接口名 b
            var interfaceName = @"(?<=[^\/\:]\s+(class|interface)\s+)[^\n\r\{]+(?=[\r\n\{])";
            var csRow = (Regex.Match(text, interfaceName).Value).Trim();
            interfaceName = @"(?<=\s*,)[^\n\r\{]+";
            var interfaceNameText= (Regex.Match(csRow, interfaceName).Value).Trim();

            if (interfaceNameText.StartsWith(@"_") && interfaceNameText.EndsWith(@"_")) {
               
                text = text.Replace(interfaceNameText, "I"+toStr);
            }
            //修改接口名 e
            File.WriteAllText(filePath, text, Encoding.UTF8);
        }

        /// <summary>
        ///     查找代码片段，插入指定文件内
        /// </summary>
        private static void FindPartAndInsert(FileInfos fileinfo) {
            if (fileinfo.PartId != null
                && !fileinfo.PartId.Equals(Empty)
            ) {
                var fromPath = fileinfo.FromPath;
                var toPath = fileinfo.ToPath;
                var text = File.ReadAllText(fromPath);
                var inserthere = @"__InsertHere__";
                var fromMatch =
                    $@"(?<=(\#region\s+{fileinfo.Id}))[\s\S\w\r\n]*(?=(\#endregion\s+{fileinfo.Id}))";
                var toMatch =
                    $@"(?<=(\#region\s+{inserthere}))[\s\S\w\r\n]*(?=(\#endregion\s+{inserthere}))";
                var fromTest = Regex.Match(text, fromMatch).Value;
                string target;


                if (!File.Exists(toPath)) {
                    target = text;
                }
                else {
                    target = File.ReadAllText(toPath);
                    File.SetAttributes(toPath, FileAttributes.Normal);
                }
                var regex = new Regex(toMatch);
                target = regex.Replace(target, fromTest);

                #region 替换插入位置

                regex = new Regex($@"\#region\s+{inserthere}");
                target = regex.Replace(target, $@"#region {fileinfo.Id}");
                regex = new Regex($@"\#endregion\s+{inserthere}");


                var newInsertHere =
                    $"#endregion {fileinfo.Id}\r\n        #region __InsertHere__ \r\n        #endregion __InsertHere__";

                target = regex.Replace(target, newInsertHere);

                #endregion

                File.WriteAllText(toPath, target, Encoding.UTF8);
            }
        }

        #endregion __insertPart__

        /// <summary>
        ///     一些必考项目,模板目录
        /// </summary>
        public static void CreateBaseItem(Toolpars toolPars) {
            var templateType = toolPars.SettingPathEntity.TemplateTypeKey;
            var newTypeKey = toolPars.FormEntity.txtNewTypeKey;
            var fileMapping = toolPars.FileMappingEntity;
            var fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                filmap.Id.Equals("BaseItem")
            );
            fileInfo?.Paths.ToList().ForEach(path => {
                var fromPath = toolPars.MVSToolpath + @"Template\" + path;

                var newFilePath = path.Replace(templateType, newTypeKey);
                newFilePath = toolPars.GToIni + @"\" + newFilePath;

                if (File.Exists(newFilePath))
                    return;
                var fileinfo = new FileInfo(fromPath);
                var newDir = Path.GetDirectoryName(newFilePath);
                if (newDir != null && !Directory.Exists(newDir))
                    Directory.CreateDirectory(newDir);
                fileinfo.CopyTo(newFilePath);
            });
        }

        /// <summary>
        ///     修改个案文件的typeKey
        /// </summary>
        /// <param name="toolpars"></param>
        /// <param name="oldKey"></param>
        /// <param name="newKey"></param>
        public static void ModiFiles(Toolpars toolpars, string oldKey, string newKey) {
            //个案路径
            var directoryPath = $@"{toolpars.GToIni}\Digiwin.ERP.{newKey}\";
            var tDes = new DirectoryInfo(directoryPath);

            var tSearchPatternList = new List<string>();
            tSearchPatternList.AddRange(new[] {
                "*xml",
                "*.sln",
                "*.repx",
                "*proj",
                "*.complete",
                "*.cs"
            });
            foreach (var t in tSearchPatternList)
            foreach (var f in tDes.GetFiles(t, SearchOption.AllDirectories)) {
                var filePath = f.FullName;
                if (!File.Exists(filePath))
                    continue;
                var text = File.ReadAllText(filePath);
                text = text.Replace("Digiwin.ERP." + oldKey,
                    "Digiwin.ERP." + newKey);
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
                File.WriteAllText(filePath, text, Encoding.UTF8);
            }
        }

        /// <summary>
        ///     获取节点地址与文件地址
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="toolPars"></param>
        /// <returns></returns>
        public static Dictionary<string, List<FileInfos>> GetTreeViewFilePath(TreeNodeCollection nodes,
            Toolpars toolPars) {
            var paths = new Dictionary<string, List<FileInfos>>();

            foreach (MyTreeNode node in nodes) {
                var filesInfo = node.BuildeType.FileInfos;
                if (filesInfo == null
                    || filesInfo.Count == 0) {
                    if (node.Nodes.Count <= 0)
                        continue;
                    var cpaths = GetTreeViewFilePath(node.Nodes, toolPars);
                    foreach (var kv in cpaths) {
                        var keys = paths.Keys;
                        var key = kv.Key;
                        var value = kv.Value;
                        if (keys.Contains(key)) {
                            var newV = paths[key];
                            newV.AddRange(value);
                            UpdatePath(newV, toolPars);
                            paths[key] = newV;
                        }
                        else {
                            UpdatePath(value, toolPars);
                            paths.Add(kv.Key, value);
                        }
                    }
                }
                else {
                    var nodePath = node.FullPath;
                    var keys = paths.Keys;
                    if (keys.Contains(nodePath)) {
                        var newV = paths[nodePath];
                        newV.AddRange(filesInfo);
                        UpdatePath(newV, toolPars);
                        paths[nodePath] = newV;
                    }
                    else {
                        UpdatePath(filesInfo, toolPars);
                        paths.Add(nodePath, filesInfo);
                    }
                }
            }
            return paths;
        }


        /// <summary>
        ///     当中途更改typekey时，更新个案路径
        /// </summary>
        /// <param name="fileInfos"></param>
        /// <param name="toolPars"></param>
        private static void UpdatePath(List<FileInfos> fileInfos, Toolpars toolPars) {
            var templateType = toolPars.SettingPathEntity.TemplateTypeKey;
            var newTypeKey = toolPars.FormEntity.txtNewTypeKey;
            fileInfos.ForEach(fileinfo => {
                    var basePath = fileinfo.BasePath;
                    var oldFilePath = Path.GetFileNameWithoutExtension(basePath);
                    if (oldFilePath == null)
                        return;
                    var newFilePath = basePath.Replace(templateType, newTypeKey)
                        .Replace(oldFilePath, fileinfo.FileName);
                    fileinfo.ToPath = $@"{toolPars.GToIni}\{newFilePath}";
                }
            );
        }

        public static List<FileInfos> CreateFileMappingInfo(Toolpars toolpars, BuildeType bt) {
            var fileInfos = new List<FileInfos>();
            var fileMapping = toolpars.FileMappingEntity;
            var id = bt.Id;
            var fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                filmap.Id.Equals(id)
            );
            if (fileInfo?.Paths == null)
                return fileInfos;
            if (fileInfo.Paths.Count() == 1) {
                var fileinfo = new FileInfos {
                    ActionName = "",
                    ClassName = $"Create{id}",
                    FileName = $"Create{id}",
                    FunctionName = $"Create{id}"
                };

                var path = fileInfo.Paths[0];
                fileinfo.BasePath = fileInfo.Paths[0];
                var fromPath = $@"{toolpars.MVSToolpath}\Template\{path}";
                fileinfo.FromPath = fromPath;
                var oldFilePath = Path.GetFileNameWithoutExtension(path);
                if (oldFilePath != null) {
                    var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);
                    fileinfo.ToPath = $@"{toolpars.GToIni}\{newFilePath}";
                }
                if (bt.PartId != null
                    && !bt.PartId.Equals(Empty)) {
                    fileinfo.PartId = bt.PartId;
                    fileinfo.PartId = bt.Id;
                    fileinfo.IsMerge = bt.IsMerge;
                }
                fileInfos.Add(fileinfo);
            }
            else {
                fileInfo.Paths.ToList().ForEach(path => {
                    var classNameFiled = Path.GetFileNameWithoutExtension(path);
                    var fromPath = $@"{toolpars.MVSToolpath}\Template\{path}";
                    var fileinfo = new FileInfos {
                        ActionName = "",
                        ClassName = classNameFiled,
                        FileName = classNameFiled,
                        FunctionName = "",
                        BasePath = path,
                        FromPath = fromPath
                    };
                    var oldFilePath = Path.GetFileNameWithoutExtension(path);
                    if (oldFilePath != null) {
                        var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);

                        fileinfo.ToPath = $@"{toolpars.GToIni}\{newFilePath}";
                    }

                    fileInfos.Add(fileinfo);
                });
            }

            return fileInfos;
        }

        #endregion

        #region 修改

        /// <summary>
        ///     目录下的文件copy至另一目录 ,先copy 再替换 typekey
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="pDistFolder"></param>
        /// <param name="filesinfo"></param>
        public static void CopyPkg(string pFileName, string pDistFolder, List<FileInfos> filesinfo) {
            //文件夹替换，递归
            if (Directory.Exists(pFileName)) {
                // Folder
                var di = new DirectoryInfo(pFileName);
                //子文件夹
                var diries = di.GetDirectories();
                //文件
                var files = di.GetFiles();
                foreach (var d in diries) {
                    var tFolderPath = $@"{pDistFolder}\{d.Name}";
                    CopyPkg(d.FullName, tFolderPath, filesinfo);
                }
                foreach (var f in files) {
                    string[] filter = {
                        ".sln", ".csproj"
                    };

                    var frompath = f.FullName;
                    var selected = filesinfo.FirstOrDefault(file => file.FromPath.Equals(frompath));
                    var extionName = Path.GetExtension(frompath);
                    if (selected != null
                        || filter.Contains(extionName)
                    )
                        CopyPkg(f.FullName, pDistFolder + @"\" + f.Name, filesinfo);
                }
            }
            else if (File.Exists(pFileName)) {
                //文件夹
                if (!Directory.Exists(pDistFolder.Remove(pDistFolder.LastIndexOf("\\", StringComparison.Ordinal))))
                    Directory.CreateDirectory(
                        pDistFolder.Remove(pDistFolder.LastIndexOf("\\", StringComparison.Ordinal)));
                if (File.Exists(pDistFolder))
                    File.SetAttributes(pDistFolder, FileAttributes.Normal);
                try {
                    File.Copy(pFileName, pDistFolder, true);
                }
                catch {
                    // ignored
                }
            }
        }

        public static bool CopyModi(TreeNodeCollection nodes, Toolpars toolPars) {
            var sucess = true;
            try {
                var fileInfos = GetTreeViewPath(nodes);

                var formDir = $@"{toolPars.FormEntity.txtPKGpath}\Digiwin.ERP.{toolPars.FormEntity.PkgTypekey}";
                var toDir = $@"{toolPars.FormEntity.TxtToPath}\Digiwin.ERP.{toolPars.FormEntity.txtNewTypeKey}";

                CopyPkg(formDir, toDir, fileInfos);
                ModiName(toolPars, toolPars.FormEntity.PkgTypekey);
            }
            catch (Exception) {
                sucess = false;
            }

            return sucess;
        }

        /// <summary>
        ///     批量修改个案cs文件
        /// </summary>
        /// <param name="toolpars"></param>
        /// <param name="oldTypeKey"></param>
        public static void ModiName(Toolpars toolpars, string oldTypeKey) {
            var txtNewTypeKey = toolpars.FormEntity.txtNewTypeKey;
            var pkgTypekey = oldTypeKey; // Toolpars.formEntity.PkgTypekey;
            //个案路径
            var directoryPath = $@"{toolpars.GToIni}\Digiwin.ERP.{txtNewTypeKey}\";
            var tDes = new DirectoryInfo(directoryPath);

            var tSearchPatternList = new List<string>();
            tSearchPatternList.AddRange(new[] {
                "*xml",
                "*.sln",
                "*.repx",
                "*proj",
                "*.complete",
                "*.cs"
            });
            foreach (var d in tDes.GetDirectories("*", SearchOption.AllDirectories)) {
                //查找不包含txtNewTypeKey的文件夹
                if (d.Name.IndexOf(txtNewTypeKey, StringComparison.Ordinal) != -1)
                    continue;
                //查找txtPKGpath
                if (d.Name.IndexOf(pkgTypekey, StringComparison.Ordinal) == -1)
                    continue;
                if (
                    d.Parent != null && File.Exists($@"{d.Parent.FullName}\\{d.Name.Replace(pkgTypekey, txtNewTypeKey)}"
                    )) {
                    File.SetAttributes(
                        d.Parent.FullName + "\\" +
                        d.Name.Replace(pkgTypekey, txtNewTypeKey),
                        FileAttributes.Normal);
                    File.Delete(d.Parent.FullName + "\\" +
                                d.Name.Replace(pkgTypekey, txtNewTypeKey));
                }
                if (
                    d.Parent != null && Directory.Exists(d.Parent.FullName + "\\" +
                                                         d.Name.Replace(pkgTypekey, txtNewTypeKey)) ==
                    false)
                    d.MoveTo(d.Parent.FullName + "\\" +
                             d.Name.Replace(pkgTypekey, txtNewTypeKey));
                Application.DoEvents();
            }


            foreach (var f in tDes.GetFiles("*", SearchOption.AllDirectories)) {
                if (f.Name.IndexOf(txtNewTypeKey, StringComparison.Ordinal) != -1)
                    continue;
                if (f.Name.IndexOf(pkgTypekey, StringComparison.Ordinal) == -1)
                    continue;
                if (!File.Exists(f.FullName))
                    continue;
                if (
                    f.Directory != null && File.Exists(f.Directory.FullName + "\\" +
                                                       f.Name.Replace("Digiwin.ERP." + pkgTypekey,
                                                           "Digiwin.ERP." + txtNewTypeKey)) == false)
                    f.MoveTo(f.Directory.FullName + "\\" +
                             f.Name.Replace("Digiwin.ERP." + pkgTypekey,
                                 "Digiwin.ERP." + txtNewTypeKey));
                Application.DoEvents();
            }
            var patList = GetFilePath(directoryPath);
            foreach (var t in tSearchPatternList)
            foreach (var f in tDes.GetFiles(t, SearchOption.AllDirectories)) {
                var filePath = f.FullName;
                if (!File.Exists(filePath))
                    continue;
                var text = File.ReadAllText(filePath);
                text = text.Replace("Digiwin.ERP." + pkgTypekey,
                    "Digiwin.ERP." + txtNewTypeKey);
                text = text.Replace(@"<HintPath>..\..\", @"<HintPath>..\..\..\..\..\WD_PR\SRC\");
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
                File.WriteAllText(filePath, text, Encoding.UTF8);

                var extionName = Path.GetExtension(filePath);
                if (extionName.Equals(".csproj")) {
                    XmlTools.XmlNodeByXPath(filePath, @"Compile", patList);
                    XmlTools.XmlNodeByXPath(filePath, @"EmbeddedResource", patList);
                }
                Application.DoEvents();
            }
        }

        public static List<FileInfos> GetTreeViewPath(TreeNodeCollection nodes) {
            var paths = new List<FileInfos>();

            foreach (MyTreeNode node in nodes) {
                var filesInfo = node.BuildeType.FileInfos;
                if (filesInfo == null
                    || filesInfo.Count == 0) {
                    if (node.Nodes.Count <= 0)
                        continue;
                    var cpaths = GetTreeViewPath(node.Nodes);
                    paths.AddRange(cpaths);
                }
                else {
                    if (!node.Checked)
                        continue;
                    var nodePath = node.BuildeType.FileInfos;
                    paths.AddRange(nodePath);
                }
            }
            return paths;
        }

        #region 一键借用

        public static bool CopyAllPkG(Toolpars toolpars,string pkgPath) {
            var success = true;
            try {
                toolpars.GToIni = toolpars.FormEntity.TxtToPath;
                if (toolpars.FormEntity.TxtToPath != ""
                    && toolpars.FormEntity.txtNewTypeKey != "") {
                    if (Directory.Exists(toolpars.FormEntity.TxtToPath)) {
                        var tCusSrc = new DirectoryInfo(toolpars.GToIni + @"\");
                        //toolpars.FormEntity.txtPKGpath + "Digiwin.ERP."+ toolpars.FormEntity.PkgTypekey;
                        if (!Directory.Exists(pkgPath)) {
                            MessageBox.Show(string.Format(Resource.DirNotExisted, pkgPath), Resource.ErrorMsg,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            success = false;
                        }
                        else {
                            if (
                                Directory.Exists(Path.Combine(toolpars.GToIni + @"\",
                                    "Digiwin.ERP." + toolpars.FormEntity.txtNewTypeKey))) {
                                var result =
                                    MessageBox.Show(
                                        Path.Combine(toolpars.FormEntity.TxtToPath, toolpars.FormEntity.txtNewTypeKey)
                                        + Environment.NewLine + Resource.DirExisted,
                                        Resource.WarningMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (result == DialogResult.Yes) {
                                    object tArgsPath = Path.Combine(toolpars.GToIni + @"\",
                                        "Digiwin.ERP." + toolpars.FormEntity.txtNewTypeKey);
                                    OldTools.DeleteAll(tArgsPath);
                                }
                                else {
                                    success = false;
                                }
                            }
                            if (success)
                                OldTools.CopyAllPkg(pkgPath,
                                    tCusSrc + "Digiwin.ERP." + toolpars.FormEntity.txtNewTypeKey);
                        }
                    }
                    else {
                        MessageBox.Show(string.Format(Resource.DirNotExisted, toolpars.FormEntity.TxtToPath),
                            Resource.ErrorMsg,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        success = false;
                    }
                }
                else {
                    MessageBox.Show(Resource.TypekeyNotExisted, Resource.ErrorMsg, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    success = false;
                }

                #region 修改命名

                if (success) {
                    ModiName(toolpars, toolpars.FormEntity.PkgTypekey);
                    MessageBox.Show(Resource.GenerateSucess);
                }

                #endregion
            }
            catch (Exception ex) {
                success = false;
                MessageBox.Show(ex.Message, Resource.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            new Thread(delegate() { SqlTools.InsertToolInfo("S01231_20160503_01", "20160503", "COPY PKG SOURCE"); }
            ).Start();
            return success;
        }

        #endregion

        #endregion

        #region 操作实体

        public static List<string> GetPropNameByEntity(MetadataContainer metadataContainer, string typeKey)
        {
            var propies = new List<string>();
            var selectTypeKey =
                metadataContainer.DataEntityTypes.FirstOrDefault(entityType => entityType.Name.Equals(typeKey));
            selectTypeKey?.Properties.Items?.ToList().ForEach(prop =>
            {
                if (prop != null)
                {
                    // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
                    if (prop is SimpleProperty)
                    {
                        var name = ((SimpleProperty)prop).Name ?? string.Empty;
                        if (!name.Equals(string.Empty))
                        {
                            propies.Add(name);
                        }
                    }
                    // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
                    if (prop is ComplexProperty)
                    {
                        var name = ((ComplexProperty)prop).Name ?? string.Empty;
                        if (!name.Equals(string.Empty))
                        {
                            propies.Add(name + @"_ROid");
                            propies.Add(name + @"_RTK");
                        }
                    }
                    // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
                    if (prop is ReferenceProperty)
                    {
                        var name = ((ReferenceProperty)prop).Name ?? string.Empty;
                        if (!name.Equals(string.Empty))
                        {
                            propies.Add(name);
                        }
                    }
                }
            });
            selectTypeKey?.InterfaceReferences?.ToList().ForEach(item =>
            {
                var name = item.Name ?? string.Empty;
                if (!name.Equals(string.Empty))
                {
                    switch (name)
                    {
                        case "IDocumentNumber":
                            propies.Add("DOC_NO");
                            break;
                        case "IOwner":
                            propies.Add("Owner_Org.RTK");
                            propies.Add("Owner_Org.ROid");
                            break;
                        case "ISequenceNumber":
                            propies.Add("SequenceNumber");
                            break;
                    }
                }
            });
            return propies;
        } 
        #endregion

        public static void CallModule(BuildeType bt, Toolpars toolpars) {

            var plugPath = bt.PlugPath;
            var moduleName = bt.ModuleName;
            if (plugPath == null
                || plugPath.Trim().Equals(string.Empty)
                || moduleName == null
                || moduleName.Trim().Equals(string.Empty)
            )
            {
                MessageBox.Show(Resource.ModuleNotExisted);
                return;
            }
            try
            {
                var dirPath = toolpars.MVSToolpath;
                var dirInfo = new DirectoryInfo(dirPath);
                var plugFullPath = dirInfo.GetFiles(plugPath, SearchOption.AllDirectories);
                if (!plugFullPath.Any())
                {
                    MessageBox.Show($@"应用目录内未找到模块文件{plugPath}");
                }
                var dllPath = plugFullPath[0].FullName;

                ////    1.Load(命名空间名称)，GetType(命名空间.类名)  
                var type = Assembly.LoadFile(dllPath).GetType(moduleName);

                ////    3.调用的实例化方法（非静态方法）需要创建类型的一个实例  
                var obj = Activator.CreateInstance(type, new object[] { toolpars });
                (obj as Form)?.ShowDialog();
            }
            catch (Exception ex)
            {
                // ignored
                MessageBox.Show($@"插件{moduleName}激活失败，请检查模块信息,详细信息{Environment.NewLine}{ex.Message}");
            }
            //InsertForm insertForm = new InsertForm(Toolpars);
            //insertForm.ShowDialog();
        }

        public static void OpenWord() {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var dirInfo = new DirectoryInfo(basePath);
            var fileMatch = "Help.docx";
            var matchFile = dirInfo.GetFiles(fileMatch, SearchOption.AllDirectories);
            if (matchFile.Any()) {
                var path = matchFile[0].FullName;

                try
                {
                    var app = new MSWord.Application { Visible = true };
                    app.Documents.Open(path);
                }
                catch (Exception)
                {
                    MessageBox.Show(Resource.OpenDocError);
                }
            }
            else {
                MessageBox.Show(Resource.HelpDocNotExiested);
                return;
            }
      

        }
    }
}
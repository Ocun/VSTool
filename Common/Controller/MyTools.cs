// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Digiwin.Chun.Common.Model;
using Digiwin.Chun.Common.Properties;
using Digiwin.Chun.Common.Views;
using Application = System.Windows.Forms.Application;
using MSWord = Microsoft.Office.Interop.Word;

namespace Digiwin.Chun.Common.Controller {
    /// <summary>
    ///     工具类
    /// </summary>
    public static class MyTools {
        /// <summary>
        /// </summary>
        public static Toolpars Toolpars { get; } = new Toolpars();

        /// <summary>
        ///     初始化窗体参数
        /// </summary>
        /// <param name="pToIni"></param>
        public static void InitToolpars(string[] pToIni) {
            if (pToIni == null) {
                Toolpars.FormEntity.TxtToPath = String.Empty;
            }
            else {
                Toolpars.Mall = pToIni[0];
                var args = Toolpars.Mall.Split('&');
                Toolpars.Mpath = args[0]; //D:\DF_E10_2.0.2\C002152226(达峰机械)\WD_PR_C\SRC
                Toolpars.MInpath = args[1]; //D:\DF_E10_2.0.2\X30001(鼎捷紧固件)\WD_PR_I\SRC
                Toolpars.Mplatform = args[2]; //C:\DF_E10_2.0.2
                Toolpars.MdesignPath = args[3]; //E:\平台\E202
                Toolpars.MVersion = args[4]; //DF_E10_2.0.2
                Toolpars.MIndustry = Convert.ToBoolean(args[5]);
                Toolpars.CustomerName = args[6];

                Toolpars.FormEntity.TxtToPath = Toolpars.Mpath;
                if (Toolpars.MIndustry)
                    Toolpars.FormEntity.TxtToPath = Toolpars.MInpath;

                Toolpars.FormEntity.TxtPkGpath = $@"{Toolpars.MdesignPath}\WD_PR\SRC";
                Toolpars.FormEntity.Industry = Toolpars.MIndustry;
                if (Toolpars.Mpath.Contains("PKG")
                    && !Toolpars.MIndustry)
                    Toolpars.FormEntity.TxtToPath = $@"{Toolpars.MdesignPath}\WD_PR\SRC\";
            }
            //分割宽度
            Toolpars.FormEntity.SpiltWidth = 200;
            //最大分割列
            Toolpars.FormEntity.MaxSplitCount = 6;
            Toolpars.OldTypekey = Toolpars.SettingPathEntity.TemplateTypeKey;
            Toolpars.ModelType = ModelType.Json;
            Toolpars.FormEntity.EditState = false;
            IconTools.InitImageList();
            InitBuilderEntity();
        }

        #region 作废

        /// <summary>
        ///     把文件拷入指定的文件夹
        /// </summary>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        /// <param name="fileNames"></param>
        /// <param name="fromTypeKey"></param>
        /// <param name="toTypeKey"></param>
        // ReSharper disable once UnusedMember.Global
        public static void CopyTo(string fromDir, string toDir, List<string> fileNames,
            string fromTypeKey, string toTypeKey) {
            var formFiles = GetFilePath(fromDir);
            foreach (var file in formFiles) {
                if (!File.Exists(file))
                    continue;
                var fileinfo = new FileInfo(file);
                var fileName = Path.GetFileNameWithoutExtension(file);
                if (!fileNames.Contains(fileName))
                    continue;
                if (fileinfo.Directory == null)
                    continue;
                var absolutedir = fileinfo.Directory.FullName.Replace(fromDir, String.Empty);
                var absolutePath = file.Replace(fromDir, String.Empty);
                var newFilePath = Path.Combine(toDir, absolutePath);
                var newFileDir = Path.Combine(toDir, absolutedir).Replace(fromTypeKey, toTypeKey);
                if (!Directory.Exists(newFileDir))
                    Directory.CreateDirectory(newFileDir);
                if (File.Exists(newFilePath))
                    if (MessageBox.Show(Resources.FileExisted, Resources.WarningMsg, MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
                        != DialogResult.OK)
                        return;
                fileinfo.CopyTo(newFilePath);
                ChangeText(newFilePath, fromTypeKey, toTypeKey);
                //var fileInfo = new FileInfo(newFilePath);

                //dir.Parent
                //OldTools.ModiCS(mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                //                            ".Business.Implement.csproj", "Interceptor\\DetailInterceptorS" + ra + ".cs");
            }
        }

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

        #region 操作实体

        /// <summary>
        ///     获取实体属性名称
        /// </summary>
        /// <param name="metadataContainer"></param>
        /// <param name="typeKey"></param>
        /// <returns></returns>
        public static List<string> GetPropNameByEntity(MetadataContainer metadataContainer, string typeKey) {
            var propies = new List<string>();
            var selectTypeKey =
                metadataContainer.DataEntityTypes.FirstOrDefault(entityType => entityType.Name.Equals(typeKey));
            selectTypeKey?.Properties.Items?.ToList().ForEach(prop => {
                if (prop != null) {
                    // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
                    if (prop is SimpleProperty) {
                        var name = ((SimpleProperty) prop).Name ?? String.Empty;
                        if (!name.Equals(String.Empty))
                            propies.Add(name);
                    }
                    // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
                    if (prop is ComplexProperty) {
                        var name = ((ComplexProperty) prop).Name ?? String.Empty;
                        if (!name.Equals(String.Empty)) {
                            propies.Add(name + @"_ROid");
                            propies.Add(name + @"_RTK");
                        }
                    }
                    // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
                    if (prop is ReferenceProperty) {
                        var name = ((ReferenceProperty) prop).Name ?? String.Empty;
                        if (!name.Equals(String.Empty))
                            propies.Add(name);
                    }
                }
            });
            selectTypeKey?.InterfaceReferences?.ToList().ForEach(item => {
                var name = item.Name ?? String.Empty;
                if (!name.Equals(String.Empty))
                    switch (name) {
                        case "IDocumentNumber":
                            propies.Add("DOC_NO");
                            break;
                        case "IOwner":
                            propies.Add("Owner_Org_RTK");
                            propies.Add("Owner_Org_ROid");
                            break;
                        case "ISequenceNumber":
                            propies.Add("SequenceNumber");
                            break;
                    }
            });
            return propies;
        }

        #endregion

        #region CopyDll

        /// <summary>
        ///     將dll 考入平臺目錄
        /// </summary>
        public static void CopyUIdll() {
            try {
                if (PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtNewTypeKey)) {
                    MessageBox.Show(Resources.TypekeyNotExisted);
                    return;
                }
                var fromPath = Toolpars.PathEntity.ExportFullPath;
                var toPath = $"{Toolpars.Mplatform}\\DeployServer\\Shared\\Customization\\Programs\\";
                var filterStr = $"*{Toolpars.FormEntity.TxtNewTypeKey}.UI.*";
                if (Toolpars.MIndustry)
                    toPath = $"{Toolpars.Mplatform}\\DeployServer\\Shared\\Industry\\Programs\\";
                if (!Directory.Exists(fromPath)) {
                    MessageBox.Show(string.Format(Resources.DirNotExisted, fromPath));
                    return;
                }
                var filedir = Directory.GetFiles(fromPath, filterStr,
                    SearchOption.AllDirectories);
                foreach (var mfile in filedir)
                    File.Copy(mfile, mfile.Replace(fromPath, toPath), true);
                MessageBox.Show(Resources.CopySucess);
            }
            catch (Exception ex) {
                string[] processNames = {
                    "Digiwin.Mars.ClientStart"
                };
                var f = CheckCanCopyDll(processNames);
                if (f)
                    CopyUIdll();
                else
                    MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            new Thread(() => SqlTools.InsertToolInfo("S01231_20160503_01", "20160503", "btncopyUIdll_Click")).Start();
        }

        /// <summary>
        ///     CopyDll
        /// </summary>
        public static void CopyDll() {
            var pathEntity = Toolpars.PathEntity;
            if (pathEntity == null)
                return;
            if (PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtNewTypeKey))
            {
                MessageBox.Show(Resources.TypekeyNotExisted);
                return;
            }
            var serverPath = pathEntity.ServerProgramsFullPath;
            var clientPath = pathEntity.DeployProgramsFullPath;
            var businessDllFullPath = pathEntity.ExportFullPath + pathEntity.BusinessDllName;
            var implementDllFullPath = pathEntity.ExportFullPath + pathEntity.ImplementDllName;
            var uiDllFullPath = pathEntity.ExportFullPath + pathEntity.UiDllName;
            var uiImplementDllFullPath = pathEntity.ExportFullPath + pathEntity.UiImplementDllName;

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
                        clientPath + pathEntity.UiDllName, true);
            //ui.implement.dll
            if (!File.Exists(uiImplementDllFullPath))
                return;
            if (Directory.Exists(clientPath))
                File.Copy(uiImplementDllFullPath,
                    clientPath + pathEntity.UiImplementDllName, true);
        }

        /// <summary>
        ///     检查dll是否被占用
        /// </summary>
        /// <param name="processNames"></param>
        /// <returns></returns>
        public static bool CheckCanCopyDll(string[] processNames) {
            var flag = true;
            var infos = Process.GetProcesses();
            foreach (var info in infos)
                if (processNames.Contains(info.ProcessName))
                    flag = false;
            if (flag)
                return true;
            if (MessageBox.Show(Resources.DllUsedMsg, Resources.WarningMsg, MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) != DialogResult.OK)
                return false;
            KillProcess(processNames);
            return true;
        }

        /// <summary>
        ///     kill the process
        /// </summary>
        public static void KillProcess(string[] processNames) {
            foreach (var p in Process.GetProcesses())
                processNames.ToList().ForEach(processName => {
                    if (p.ProcessName.Contains(processName))
                        p.Kill();
                });
        }

        #endregion

        #region 开启外部程序

        /// <summary>
        ///     设置第三方工具的路径
        /// </summary>
        /// <param name="bt"></param>
        public static void SetToolsPath(BuildeType bt) {
            var form = new SetToolPath(bt.Url);
            if (form.ShowDialog() != DialogResult.OK)
                return;
            bt.Url = (form.Path??string.Empty).Trim();
            var modelType = Toolpars.ModelType;
            var settingPath = PathTools.GetSettingPath("BuildeEntity", modelType);
            switch (modelType) {
                case ModelType.Binary:
                case ModelType.Json:
                    ModiBuilderById(Toolpars.BuilderEntity.BuildeTypies,bt.Id,bt.Url);
                    ReadToEntityTools.SaveSerialize(Toolpars.BuilderEntity, Toolpars.ModelType, settingPath);
                    break;
                case ModelType.Xml:
                    XmlTools.ModiXml(settingPath,
                        bt.Id, bt.Url);
                    break;
            }
           
         
            //获取Exe图标
            IconTools.SetExeIcon(bt.Url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        private static void ModiBuilderById(BuildeType[] items,string id, string value) {
            if (items == null) return;
            foreach (var buildeType in items)
            {
                if (buildeType.Id.Equals(id)) {
                    buildeType.Url = value;
                    break;
                }
                else if(buildeType.BuildeItems!= null &&buildeType.BuildeItems.Length>0) {
                    ModiBuilderById(buildeType.BuildeItems, id, value);
                }
            }
        }

        /// <summary>
        ///     打开第三方工具
        /// </summary>
        /// <param name="bt"></param>
        public static void OpenTools(BuildeType bt) {
            try {
                var p = new Process();
                var infos = Process.GetProcesses();
                var path = bt.Url;
                if (!File.Exists(path))
                    SetToolsPath(bt);
                path = bt.Url;
                var exeName = Path.GetFileName(path);
                var f = infos.All(info => exeName != null && !info.ProcessName.ToUpper().Contains(exeName.ToUpper()));
                if (f) {
                    p.StartInfo.FileName = path;

                    p.Start();
                }
                else {
                    MessageBox.Show(Resources.ExeAlreadyExe);
                }
            }
            catch (Exception e0) {
                MessageBox.Show(Resources.ExeExeError + e0.Message);
            }
        }

        /// <summary>
        ///     呼叫第三方模块
        /// </summary>
        /// <param name="bt"></param>
        public static void CallModule(BuildeType bt) {
            var plugPath = bt.PlugPath;
            var moduleName = bt.ModuleName;
            if (plugPath == null
                || plugPath.Trim().Equals(String.Empty)
                || moduleName == null
                || moduleName.Trim().Equals(String.Empty)
            ) {
                MessageBox.Show(Resources.ModuleNotExisted);
                return;
            }
            try {
                var dirPath = Toolpars.MvsToolpath;
                var dirInfo = new DirectoryInfo(dirPath);
                var plugFullPath = dirInfo.GetFiles(plugPath, SearchOption.AllDirectories);
                if (!plugFullPath.Any())
                    MessageBox.Show($@"应用目录内未找到模块文件{plugPath}");
                var dllPath = plugFullPath[0].FullName;

                ////    1.Load(命名空间名称)，GetType(命名空间.类名)  
                var type = Assembly.LoadFile(dllPath).GetType(moduleName);

                ////    3.调用的实例化方法（非静态方法）需要创建类型的一个实例  
                var obj = Activator.CreateInstance(type, Toolpars);
                // ReSharper disable once UseNullPropagation
                if (obj is Form)
                    (obj as Form).ShowDialog();
            }
            catch (Exception ex) {
                // ignored
                MessageBox.Show($@"插件{moduleName}激活失败，请检查模块信息,详细信息{Environment.NewLine}{ex.Message}");
            }
            //InsertForm insertForm = new InsertForm(Toolpars);
            //insertForm.ShowDialog();
        }

        /// <summary>
        ///     打开Word
        /// </summary>
        public static void OpenWord(string fileName) {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var dirInfo = new DirectoryInfo(basePath);
            var matchFile = dirInfo.GetFiles(fileName, SearchOption.AllDirectories);
            if (matchFile.Any()) {
                var path = matchFile[0].FullName;

                try {
                    var app = new MSWord.Application {Visible = true};
                    app.Documents.Open(path);
                }
                catch (Exception) {
                    MessageBox.Show(Resources.OpenDocError);
                }
            }
            else {
                MessageBox.Show(Resources.HelpDocNotExiested);
            }
        }

        /// <summary>
        ///     打开文件夹
        /// </summary>
        /// <param name="targetDir"></param>
        public static void OpenDir(string targetDir) {
            if (Directory.Exists(targetDir))
                Process.Start(targetDir);
            else
                MessageBox.Show(string.Format(Resources.DirNotExisted, targetDir), Resources.WarningMsg,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region CopyFile

        /// <summary>
        ///     找到文件所属第一个CSproj,这有问题！！
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static string FindCSproj(DirectoryInfo dir) {
            var csproj = String.Empty;
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
        private static List<string> GetExistedMsg(Dictionary<string, List<FileInfos>> pathDic) {
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

        /// <summary>
        ///     根据选择生成项目文件
        /// </summary>
        /// <param name="treeView"></param>
        /// <returns></returns>
        public static bool CreateFile(MyTreeView treeView) {
            var pathDic = GetTreeViewFilePath(treeView.Nodes);
            var msgList = GetExistedMsg(pathDic);
            var f = true;
            var success = true;

            #region 检查覆盖

            if (msgList.Count > 0) {
                var msg = String.Empty;
                msgList.ForEach(str => { msg += str + Environment.NewLine; });
                if (MessageBox.Show(msg + Resources.FileExisted, Resources.WarningMsg, MessageBoxButtons.YesNo,
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

                    CreateBaseItem();

                    #endregion

                    #region Copy 并修改 csproj

                    var checkTemplate = true;
                    foreach (var kv in pathDic) {
                        foreach (var path in kv.Value)
                            if (!File.Exists(path.FromPath)) {
                                MessageBox.Show(string.Format(Resources.TemplateNotExisted, kv.Key, path.FileName),
                                    Resources.ErrorMsg,
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
                                && !path.PartId.Equals(String.Empty))
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
                        var csDir = $@"{Path.GetDirectoryName(csPath)}\";

                        //找到相对位置
                        var index = toPath.IndexOf(csDir, StringComparison.Ordinal);

                        if (index > -1)
                            XmlTools.ModiCs(csPath, toPath.Substring(index + csDir.Length));

                        #endregion
                    }
                    //修改文件内容，替换typekey
                    ModiFiles();
                }
                catch (Exception) {
                    success = false;
                }

                #endregion
            }
            if (!success)
                return false;
            LogTools.WriteLogByTreeView(treeView);
            InitBuilderEntity();
            MessageBox.Show(Resources.GenerateSucess);
            return true;
        }


        /// <summary>
        ///     初始化，创建完成之后 清空按钮 选择项
        /// </summary>
        public static void InitBuilderEntity() {
            try {
                var path = PathTools.GetSettingPath("BuildeEntity", Toolpars.ModelType);
                Toolpars.BuilderEntity = ReadToEntityTools.ReadToEntity<BuildeEntity>(path, Toolpars.ModelType);
                InitBuildeTypies(Toolpars.BuilderEntity.BuildeTypies);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///     初始化主界面
        /// </summary>
        /// <param name="buildeTypies"></param>
        // ReSharper disable once MemberCanBePrivate.Global
        public static void InitBuildeTypies(BuildeType[] buildeTypies) {
            buildeTypies?.ToList().ForEach(item => {
                if (item.Checked != null
                    && item.Checked.Equals("True")
                ) {
                    item.FileInfos = CreateFileMappingInfo(item);
                    if (item.BuildeItems != null)
                        InitBuildeTypies(item.BuildeItems);
                }
                else {
                    item.FileInfos = new List<FileInfos>();
                }
            });
        }

        #region __insertPart__

        /// <summary>
        ///     根据Regex替换文本
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="matchStr"></param>
        /// <param name="toStr"></param>
        private static void ReplaceByRegex(string filePath, string matchStr, string toStr) {
            if (!File.Exists(filePath))
                return;
            var text = File.ReadAllText(filePath);

            var classNameText = Regex.Match(text, matchStr).Value.Trim();
            if (classNameText.StartsWith(@"_") && classNameText.EndsWith(@"_")) {
                var regex = new Regex(matchStr);
                text = regex.Replace(text, toStr);
            }
            //修改接口名 b
            var interfaceName = @"(?<=[^\/\:]\s+(class|interface)\s+)[^\n\r\{]+(?=[\r\n\{])";
            var csRow = Regex.Match(text, interfaceName).Value.Trim();
            interfaceName = @"(?<=\s*,)[^\n\r\{]+";
            var interfaceNameText = Regex.Match(csRow, interfaceName).Value.Trim();

            if (interfaceNameText.StartsWith(@"_") && interfaceNameText.EndsWith(@"_"))
                text = text.Replace(interfaceNameText, "I" + toStr);
            //修改接口名 e
            File.WriteAllText(filePath, text, Encoding.UTF8);
        }

        /// <summary>
        ///     查找代码片段，插入指定文件内
        /// </summary>
        private static void FindPartAndInsert(FileInfos fileinfo) {
            if (fileinfo.PartId != null
                && !fileinfo.PartId.Equals(String.Empty)
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
        private static void CreateBaseItem() {
            var templateType = Toolpars.SettingPathEntity.TemplateTypeKey;
            var newTypeKey = Toolpars.FormEntity.TxtNewTypeKey;
            var fileMapping = Toolpars.FileMappingEntity;
            var fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                filmap.Id.Equals("BaseItem")
            );
            fileInfo?.Paths.ToList().ForEach(path => {
                var fromPath = PathTools.PathCombine(Toolpars.MvsToolpath, "Template", path);

                var newFilePath = path.Replace(templateType, newTypeKey);
                newFilePath = PathTools.PathCombine(Toolpars.FormEntity.TxtToPath, newFilePath);

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
        private static void ModiFiles() {
            //个案路径
            var oldKey = Toolpars.OldTypekey;
            var pathInfo = Toolpars.PathEntity;
            var directoryPath = $@"{pathInfo.TypeKeyFullRootDir}\";
            var typeKeyRootDir = pathInfo.TypeKeyRootDir;
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
                    typeKeyRootDir);
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
                File.WriteAllText(filePath, text, Encoding.UTF8);
            }
        }

        /// <summary>
        ///     获取节点地址与文件地址
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static Dictionary<string, List<FileInfos>> GetTreeViewFilePath(TreeNodeCollection nodes) {
            var paths = new Dictionary<string, List<FileInfos>>();

            foreach (MyTreeNode node in nodes) {
                var filesInfo = node.BuildeType.FileInfos;
                if (filesInfo == null
                    || filesInfo.Count == 0) {
                    if (node.Nodes.Count <= 0)
                        continue;
                    var cpaths = GetTreeViewFilePath(node.Nodes);
                    foreach (var kv in cpaths) {
                        var keys = paths.Keys;
                        var key = kv.Key;
                        var value = kv.Value;
                        if (keys.Contains(key)) {
                            var newV = paths[key];
                            newV.AddRange(value);
                            UpdatePath(newV);
                            paths[key] = newV;
                        }
                        else {
                            UpdatePath(value);
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
                        UpdatePath(newV);
                        paths[nodePath] = newV;
                    }
                    else {
                        UpdatePath(filesInfo);
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
        private static void UpdatePath(List<FileInfos> fileInfos) {
            var templateType = Toolpars.SettingPathEntity.TemplateTypeKey;
            var newTypeKey = Toolpars.FormEntity.TxtNewTypeKey;
            fileInfos.ForEach(fileinfo => {
                    var basePath = fileinfo.BasePath;
                    var oldFilePath = Path.GetFileNameWithoutExtension(basePath);
                    if (oldFilePath == null)
                        return;
                    var newFilePath = basePath.Replace(templateType, newTypeKey)
                        .Replace(oldFilePath, fileinfo.FileName);
                    fileinfo.ToPath = PathTools.PathCombine(Toolpars.FormEntity.TxtToPath, newFilePath);
                }
            );
        }

        /// <summary>
        ///     创建项目的模板文件映射
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static List<FileInfos> CreateFileMappingInfo(BuildeType bt) {
            var fileInfos = new List<FileInfos>();
            var fileMapping = Toolpars.FileMappingEntity;
            var id = bt.Id;
            var fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                filmap.Id.Equals(id)
            );
            if (fileInfo?.Paths == null)
                return fileInfos;
            if (fileInfo.Paths.Length == 1) {
                var fileinfo = new FileInfos {
                    ActionName = "",
                    ClassName = $"Create{id}",
                    FileName = $"Create{id}",
                    FunctionName = $"Create{id}"
                };

                var path = fileInfo.Paths[0];
                fileinfo.BasePath = fileInfo.Paths[0];
                var fromPath = PathTools.PathCombine(Toolpars.MvsToolpath, "Template", path);
                fileinfo.FromPath = fromPath;
                var oldFilePath = Path.GetFileNameWithoutExtension(path);
                if (oldFilePath != null) {
                    var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);
                    fileinfo.ToPath = PathTools.PathCombine(Toolpars.FormEntity.TxtToPath, newFilePath);
                }
                if (bt.PartId != null
                    && !bt.PartId.Equals(String.Empty)) {
                    fileinfo.PartId = bt.PartId;
                    fileinfo.PartId = bt.Id;
                    fileinfo.IsMerge = bt.IsMerge;
                }
                fileInfos.Add(fileinfo);
            }
            else {
                fileInfo.Paths.ToList().ForEach(path => {
                    var classNameFiled = Path.GetFileNameWithoutExtension(path);
                    var fromPath = PathTools.PathCombine(Toolpars.MvsToolpath, "Template", path);
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

                        fileinfo.ToPath = PathTools.PathCombine(Toolpars.FormEntity.TxtToPath, newFilePath);
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
        private static void CopyPkg(string pFileName, string pDistFolder, List<FileInfos> filesinfo) {
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

        /// <summary>
        ///     修改代码
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static bool CopyModi(TreeNodeCollection nodes) {
            var sucess = true;
            try {
                var fileInfos = GetTreeViewPath(nodes);
                var pathInfo = Toolpars.PathEntity;
                var formDir = pathInfo.PkgTypeKeyFullRootDir;
                var toDir = pathInfo.TypeKeyFullRootDir;
                CopyPkg(formDir, toDir, fileInfos);
                ModiName();
            }
            catch (Exception) {
                sucess = false;
            }

            return sucess;
        }

        /// <summary>
        ///     批量修改个案cs文件
        /// </summary>
        private static void ModiName() {
            var pkgTypekey = Toolpars.FormEntity.PkgTypekey;
            var txtNewTypeKey = Toolpars.FormEntity.TxtNewTypeKey;
            var pathInfo = Toolpars.PathEntity;
            var newTypeKeyRootDir = pathInfo.TypeKeyRootDir;
            var tempTypeKeyRootDir = pathInfo.PkgTypeKeyRootDir;

            //个案路径
            var directoryPath = pathInfo.TypeKeyFullRootDir;
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
                var newTypeKeyFile = PathTools.PathCombine(d.Parent?.FullName,
                    d.Name.Replace(pkgTypekey, txtNewTypeKey));
                if (
                    d.Parent != null && File.Exists(newTypeKeyFile)) {
                    File.SetAttributes(newTypeKeyFile,
                        FileAttributes.Normal);
                    File.Delete(newTypeKeyFile);
                }
                if (
                    d.Parent != null && Directory.Exists(newTypeKeyFile) ==
                    false)
                    d.MoveTo(newTypeKeyFile);
                Application.DoEvents();
            }

            foreach (var f in tDes.GetFiles("*", SearchOption.AllDirectories)) {
                if (f.Name.IndexOf(txtNewTypeKey, StringComparison.Ordinal) != -1)
                    continue;
                if (f.Name.IndexOf(pkgTypekey, StringComparison.Ordinal) == -1)
                    continue;
                if (!File.Exists(f.FullName))
                    continue;
                var newTypeKeyFile = PathTools.PathCombine(f.Directory?.FullName, f.Name.Replace(tempTypeKeyRootDir,
                    newTypeKeyRootDir));
                if (
                    f.Directory != null && !File.Exists(newTypeKeyFile))
                    f.MoveTo(newTypeKeyFile);
                Application.DoEvents();
            }
            var patList = GetFilePath(directoryPath);
            foreach (var t in tSearchPatternList)
            foreach (var f in tDes.GetFiles(t, SearchOption.AllDirectories)) {
                var filePath = f.FullName;
                if (!File.Exists(filePath))
                    continue;
                var text = File.ReadAllText(filePath);
                text = text.Replace(tempTypeKeyRootDir,
                    newTypeKeyRootDir);
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

        /// <summary>
        ///     获取生成文件的路径
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
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

        /// <summary>
        /// </summary>
        /// <param name="pkgPath"></param>
        /// <returns></returns>
        public static bool CopyAllPkG(string pkgPath) {
            var success = true;
            try {
                var pathInfo = Toolpars.PathEntity;
                var newTypeKeyFullRootDir = pathInfo.TypeKeyFullRootDir;
                if (!PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtToPath)
                    && !PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtNewTypeKey)) {
                    if (Directory.Exists(Toolpars.FormEntity.TxtToPath)) {
                        if (!Directory.Exists(pkgPath)) {
                            MessageBox.Show(string.Format(Resources.DirNotExisted, pkgPath), Resources.ErrorMsg,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            success = false;
                        }
                        else {
                            if (
                                Directory.Exists(newTypeKeyFullRootDir)) {
                                var result =
                                    MessageBox.Show(
                                        Path.Combine(Toolpars.FormEntity.TxtToPath, Toolpars.FormEntity.TxtNewTypeKey)
                                        + Environment.NewLine + Resources.DirExisted,
                                        Resources.WarningMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (result == DialogResult.Yes) {
                                    object tArgsPath = Path.Combine(newTypeKeyFullRootDir);
                                    OldTools.DeleteAll(tArgsPath);
                                }
                                else {
                                    success = false;
                                }
                            }
                            if (success)
                                OldTools.CopyAllPkg(pkgPath, newTypeKeyFullRootDir);
                        }
                    }
                    else {
                        MessageBox.Show(string.Format(Resources.DirNotExisted, Toolpars.FormEntity.TxtToPath),
                            Resources.ErrorMsg,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        success = false;
                    }
                }
                else {
                    MessageBox.Show(Resources.TypekeyNotExisted, Resources.ErrorMsg, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    success = false;
                }

                #region 修改命名

                if (success) {
                    ModiName();
                    MessageBox.Show(Resources.GenerateSucess);
                }

                #endregion
            }
            catch (Exception ex) {
                success = false;
                MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            new Thread(() => SqlTools.InsertToolInfo("S01231_20160503_01", "20160503", "COPY PKG SOURCE")
            ).Start();
            return success;
        }

        #endregion

        #endregion

        #region 开启客户端，服务端

        /// <summary>
        ///     打开服务器
        /// </summary>
        public static void ServerOn(string args) {
            var isOn = CheckProcessOn("Digiwin.Mars.ServerStart");
            if (isOn) {
                MessageBox.Show(Resources.ServerRunning);
                return;
            }
            var tServerPath = Toolpars.Mplatform + "\\Server\\Control\\Digiwin.Mars.ServerStart.exe";
          

            if (!File.Exists(tServerPath)) {
                MessageBox.Show(string.Format(Resources.NotFindFile, tServerPath));
                return;
            }
            Process.Start(tServerPath, args);
        }

        /// <summary>
        ///     打开服务器
        /// </summary>
        public static void ClientOn(string args) {
            if (!CheckProcessOn("Digiwin.Mars.ServerStart")) {
                MessageBox.Show(Resources.ServerNotRunning);
                return;
            }
            if (CheckProcessOn("Digiwin.Mars.ClientStart")) {
                MessageBox.Show(Resources.ClientRunning);
                return;
            }
            var tClientPath = Toolpars.Mplatform + "\\DeployServer\\Shared\\Digiwin.Mars.ClientStart.exe";
            Process.Start(tClientPath, args);
        }

        #region executeCmd

        /// <summary>
        ///     打开cmd执行bat命令，本项目未用到 mark
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="pArguments"></param>
        // ReSharper disable once UnusedMember.Local
        private static void ExecuteCmd(string pFileName, string pArguments) {
            var process = new Process {
                StartInfo = {
                    FileName = pFileName,
                    Arguments = pArguments,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };
            process.Start();
        }

        #endregion

        /// <summary>
        ///     检查进程是否启动
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool CheckProcessOn(string processName) {
            var tIsOpen = false;
            foreach (var p in Process.GetProcesses())
                if (p.ProcessName.Contains(processName))
                    tIsOpen = true;
            return tIsOpen;
        }

        #endregion
    }
}
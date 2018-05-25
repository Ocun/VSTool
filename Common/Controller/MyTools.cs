// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Digiwin.Chun.Common.Model;
using Digiwin.Chun.Common.Properties;
using Digiwin.Chun.Common.Views;
using MSWord = Microsoft.Office.Interop.Word;

namespace Digiwin.Chun.Common.Controller {
    /// <summary>
    ///     工具类
    /// </summary>
    public static class MyTools {
        static void SetTestdata() {
            Toolpars.Mpath = ""; //D:\DF_E10_2.0.2\C002152226(达峰机械)\WD_PR_C\SRC
            Toolpars.MInpath = ""; //D:\DF_E10_2.0.2\X30001(鼎捷紧固件)\WD_PR_I\SRC
            Toolpars.Mplatform = @""; //C:\DF_E10_2.0.2
            Toolpars.MdesignPath =""; //E:\平台\E202
            Toolpars.MVersion = ""; //DF_E10_2.0.2
            Toolpars.MIndustry = false;
            Toolpars.CustomerName ="";

            Toolpars.FormEntity.TxtToPath =$@"C:\Users\zychu\Desktop\justtest";
            Toolpars.FormEntity.TxtPkGpath = $@"E:\DF_E10_5.0.0\WD_PR\SRC";
        }
        /// <summary>
        /// </summary>
        public static Toolpars Toolpars { get; } = new Toolpars();
        /// <summary>
        /// 硬件信息
        /// </summary>
        public static HardwareEntity HardwareInfo { get; } =  HardwareTools.GetHardwareInfo();

        /// <summary>
        ///     初始化窗体参数
        /// </summary>
        /// <param name="pToIni"></param>
        public static void InitToolpars(string[] pToIni) {
            Toolpars.ModelType = ModelType.Json;
            if (pToIni == null) {
                Toolpars.FormEntity.TxtToPath = string.Empty;
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
                //if (Toolpars.MIndustry)
                //    Toolpars.FormEntity.TxtToPath = Toolpars.MInpath;

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
            Toolpars.FormEntity.EditState = false;
            IconTools.InitImageList();
            InitBuilderEntity();
          //  SetTestdata();


        }
        /// <summary>
        /// 文件copy
        /// </summary>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        public static void CopyTo(string fromDir, string toDir)
        {
            var formFiles = GetFilePath(fromDir);
            foreach (var file in formFiles)
            {
                var fileinfo = new FileInfo(file);
                var frompath = fileinfo.FullName;
                if (fileinfo.Directory == null)
                    continue;
                var absolutedir = fileinfo.Directory.FullName.Replace(fromDir, string.Empty);
                var absolutePath = file.Replace(fromDir, string.Empty);
                var newFilePath = PathTools.PathCombine(toDir, absolutePath);
                var newFileDir = PathTools.PathCombine(toDir, absolutedir);

                if (!Directory.Exists(newFileDir))
                    Directory.CreateDirectory(newFileDir);
                CopyFile(frompath, newFilePath);
            }
        }
        /// <summary>
        ///     把文件拷入指定的文件夹,并修改文件名
        /// </summary>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        /// <param name="fromTypeKey"></param>
        /// <param name="toTypeKey"></param>
        /// <param name="filterfilesinfo"></param>
        /// <param name="copyAll"></param>
        // ReSharper disable once UnusedMember.Global
        public static void CopyTo(string fromDir, string toDir,
            string fromTypeKey, string toTypeKey, IReadOnlyCollection<FileInfos> filterfilesinfo, bool copyAll) {
            var formFiles = GetFilePath(fromDir);
            string[] extensions = {
                ".sln", ".csproj"
            };
            //记录一键Copy的文件
            var logPath = new List<FileInfos>();
            foreach (var file in formFiles) {
                var fileinfo = new FileInfo(file);
                var extensionName = Path.GetExtension(file);
               
                if (!copyAll && filterfilesinfo != null
                    && filterfilesinfo.Count > 0) {
                    var selected = filterfilesinfo.FirstOrDefault(f => f.FromPath.Equals(file));
                    if (selected == null
                        && !extensions.Contains(extensionName)
                    )
                    {
                        continue;
                    }
                }

                if (fileinfo.Directory == null)
                    continue;
                var absolutedir = fileinfo.Directory.FullName.Replace(fromDir, string.Empty).Replace(fromTypeKey, toTypeKey);
                var fileName = fileinfo.Name;
                var absolutePath = PathTools.PathCombine(absolutedir,fileName);

                var newFilePath = PathTools.PathCombine(toDir, absolutePath);
                var newFileDir = PathTools.PathCombine(toDir, absolutedir);

                if (!Directory.Exists(newFileDir))
                    Directory.CreateDirectory(newFileDir);

                if (File.Exists(newFilePath)) {
                    //项目文件已存在则忽略
                    if (extensions.Contains(extensionName) ) {
                        continue;
                    }
                    //否则跳过
                    if (MessageBox.Show(Resources.FileExisted, Resources.WarningMsg, MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning)
                        != DialogResult.Yes)
                        continue;
                }

                CopyFile(file, newFilePath);
                // 无法释放，所以用流形式
                //  fileinfo.CopyTo(newFilePath, true);
                //一键copy先copy文件，再统一改变typekey
                if (copyAll) {
                    logPath.Add(new FileInfos {
                        FileName = fileinfo.Name,
                        FromPath = file,
                        ToPath = newFilePath
                    });
                    continue;
                }
                //若是修改时，则先清空项目文件，再逐一添加
                if (!".csproj".Equals(extensionName)) continue;
                XmlTools.DeleteXmlNodeByXPath(newFilePath, "Compile");
                XmlTools.DeleteXmlNodeByXPath(newFilePath, "EmbeddedResource");
            }
            if (copyAll) {
                Task.Factory.StartNew(() => {
                    Thread.CurrentThread.IsBackground = false;
                    LogTools.WriteToServer(logPath);
                });
            }
            
        }

        /// <summary>
        /// 流形式copyFile，媒体文件亦可
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="tagerPath"></param>
        public static void CopyFile(string fromPath, string tagerPath) {
            //创建一个负责读取的流
            using (var fsRead = new FileStream(fromPath, FileMode.Open, FileAccess.Read))
            {
                //创建一个负责写入的流
                using (var fsWrite = new FileStream(tagerPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var buffer = new byte[1024 * 1024 * 5];

                    //因为文件可能比较大所以在读取的时候应该用循坏去读取
                    while (true)
                    {
                        //返回本次实际读取到的字节数
                        var r = fsRead.Read(buffer, 0, buffer.Length);

                        if (r == 0)
                        {
                            break;
                        }
                        fsWrite.Write(buffer, 0, r); //写入
                    }
                    fsWrite.Flush();
                }
            }
        }


        /// <summary>
        ///     递归获取指定文件夹内所有文件全路径
        /// </summary>
        /// <param name="dirpath"></param>
        /// <returns></returns>
        public static List<string> GetFilePath(string dirpath) {
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
        /// 操作插入数据库
        /// </summary>
        /// <param name="operationName"></param>
        public static void InsertInfo(string operationName) {
            Task.Factory.StartNew(() => {
                Thread.CurrentThread.IsBackground = false;
                SqlTools.InsertToolInfo($"S01231_{DateTime.Now:yyyyMMdd}_01",
                    $"{DateTime.Now:yyyyMMdd}", operationName);
            });
        }
        
             /// <summary>
            ///     CopyDll
            /// </summary>
        public static bool CopyDll(CopyModelType copyModelType) {
            if (copyModelType.Equals(CopyModelType.ALL)) {
                 InsertInfo("CopyALLdll_Click");

            }else if (copyModelType.Equals(CopyModelType.Server)) {
                 InsertInfo("CopyServerdll_Click");

            }else if (copyModelType.Equals(CopyModelType.Client)) {
                 InsertInfo("CopyClientdll_Click");

            }

            var pathEntity = Toolpars.PathEntity;
            if (pathEntity == null)
                return false;
            if (PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtNewTypeKey)) {
                MessageBox.Show(Resources.TypekeyNotExisted);
                return false;
            }
            var serverPath = pathEntity.ServerProgramsFullPath;
            var clientPath = pathEntity.DeployProgramsFullPath;
            var exportFullPath = pathEntity.ExportFullPath;
            var filterStr = $"*{Toolpars.FormEntity.TxtNewTypeKey}.*";
            if (!Directory.Exists(exportFullPath)) {
                MessageBox.Show(string.Format(Resources.DirNotExisted, exportFullPath));
                return false;
            }
            if (!Directory.Exists(serverPath)) {
                MessageBox.Show(string.Format(Resources.DirNotExisted, serverPath));
                return false;
            }
            if (!Directory.Exists(clientPath)) {
                MessageBox.Show(string.Format(Resources.DirNotExisted, clientPath));
                return false;
            }
            var filedir = Directory.GetFiles(exportFullPath, filterStr,
                SearchOption.AllDirectories);
            var clientDll = new []{".UI.dll",
                ".UI.Implement.dll",
                ".Business.dll"};
            var serverDll = new []{
                ".Business.Implement.dll",
                ".Business.dll"};
            foreach (var mfile in filedir) {
                switch (copyModelType) {
                    case CopyModelType.ALL:
                        clientDll.ToList().ForEach(item => {
                            if (!mfile.ToUpper().EndsWith(item.ToUpper())) return;
                            var tagertDir = mfile.Replace(exportFullPath, clientPath);
                            File.Copy(mfile, tagertDir, true);
                        });
                        serverDll.ToList().ForEach(item => {
                            if (!mfile.ToUpper().EndsWith(item.ToUpper())) return;
                            var tagertDir = mfile.Replace(exportFullPath, serverPath);
                            File.Copy(mfile, tagertDir, true);
                        });
                        break;
                    case CopyModelType.Server:
                        serverDll.ToList().ForEach(item => {
                            if (!mfile.ToUpper().EndsWith(item.ToUpper())) return;
                            var tagertDir = mfile.Replace(exportFullPath, serverPath);
                            File.Copy(mfile, tagertDir, true);
                        });
                        break;
                    case CopyModelType.Client:
                        clientDll.ToList().ForEach(item => {
                            if (!mfile.ToUpper().EndsWith(item.ToUpper())) return;
                            var tagertDir = mfile.Replace(exportFullPath, clientPath);
                            File.Copy(mfile, tagertDir, true);
                        });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(copyModelType), copyModelType, null);
                }
              
            }
         
            return true;

        }

        /// <summary>
        ///     检查dll是否被占用
        /// </summary>
        /// <param name="processNames"></param>
        /// <returns></returns>
        public static bool CheckProcessRunning(string[] processNames) {
            var flag = false;
            var infos = Process.GetProcesses();
            foreach (var info in infos)
                if (processNames.Contains(info.ProcessName))
                    flag = true;
            return flag;
        }

        /// <summary>
        ///     kill the process
        /// </summary>
        public static void KillProcess(string[] processNames) {
            InsertInfo("BtnKillProcess");
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
                    var xpath= $@"//BuildeItem[Id='{bt.Id}']/Url";
                    XmlTools.ModiXmlByXpath(settingPath,
                        xpath, bt.Url);
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
                InsertInfo($@"Open{bt.Id}");
                var infos = Process.GetProcesses();
                var path = bt.Url;
                if (!File.Exists(path))
                    SetToolsPath(bt);
                if(!File.Exists(bt.Url))
                    return;
                path = bt.Url;
                var exeName = Path.GetFileName(path);
                var f = infos.All(info => exeName != null && !info.ProcessName.ToUpper().Contains(exeName.ToUpper()));
                if (f) {
                    OpenExe(path);
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
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void OpenExe(string path) {
            try {
                var p = new Process {StartInfo = {FileName = path}};

                p.Start();
            }
            catch (Exception ex) {
                LogTools.LogError($"Open{path} Error! Detail:{ex.Message}");
            }
        }
        /// <summary>
        ///     呼叫第三方模块,反射比直接调用慢数百倍
        /// </summary>
        /// <param name="bt"></param>
        public static void CallModule(BuildeType bt) {
            var plugPath = bt.PlugPath;
            var moduleName = bt.ModuleName;
            InsertInfo($@"Call{moduleName}");
            if (plugPath == null
                || plugPath.Trim().Equals(string.Empty)
                || moduleName == null
                || moduleName.Trim().Equals(string.Empty)
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
            try {
                if (Directory.Exists(targetDir))
                    Process.Start(targetDir);
                else
                    MessageBox.Show(string.Format(Resources.DirNotExisted, targetDir), Resources.WarningMsg,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) {
                LogTools.LogError($"OpenDir{targetDir} Error! Detail:{ex.Message}");
            }
        }

        #endregion

        #region CopyFile
        
        /// <summary>
        ///     找到文件所属第一个CSproj
        /// </summary>
        /// <returns></returns>
        private static string FindCSproj(string filePath) {
            var csPath = string.Empty;
            try {
                var pathInfo = Toolpars.PathEntity;
                var typeKeyFullRootDir = pathInfo.TypeKeyFullRootDir;
                var dirInfo = new DirectoryInfo(typeKeyFullRootDir);
                var csFiles = dirInfo.GetFiles("*.csproj", SearchOption.AllDirectories);
                foreach(var file in csFiles)
                {
                    if (file.Directory == null) continue;
                    var pathDir = file.Directory.FullName;
                    //找到相对位置
                    var index = filePath.IndexOf(pathDir, StringComparison.Ordinal);

                    if (index <= -1) continue;
                    var filterPath =filePath.Substring(index + pathDir.Length);
                    if (filterPath.StartsWith(@".")) continue;
                    csPath = file.FullName;
                    break;
                }
            }
            catch (Exception ex) {
                LogTools.LogError($@"Find csProj Error! Detail:{ex.Message}");
            }


            return csPath;

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
                      
                        CreateFile(path);

                        var fileName = path.FileName;
                        var toPath = path.ToPath;
                        //修改形如‘_ClassName_’类名为文件名，否则不管
                        var csNameMatch = @"(?<=[^\/\:]\s+(class|interface)\s+)[^\n\:{]+(?=[\n\:{])";
                        ReplaceByRegex(toPath, csNameMatch, fileName);

                        #region 修改解决方案

                        var csPath = FindCSproj(toPath); 
                        var csDir = $@"{Path.GetDirectoryName(csPath)}\";

                        //找到相对位置
                        var index = toPath.IndexOf(csDir, StringComparison.Ordinal);

                        if (index > -1)
                            XmlTools.AddCsproj(csPath, toPath.Substring(index + csDir.Length));

                        #endregion
                    }
                    //修改文件内容，替换typekey
                    ModiFiles();
                }
                catch (Exception ex) {
                    LogTools.LogError($@"GenerFile Error ! Detail {ex.Message}");
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
        /// 从模版copy对应的文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static void CreateFile(FileInfos path) {
            var fileinfo = new FileInfo(path.FromPath);
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
        private static void InitBuildeTypies(BuildeType[] buildeTypies) {
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

        #region 借用修改
        
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
                CopyTo(formDir, toDir, Toolpars.FormEntity.PkgTypekey, Toolpars.FormEntity.TxtNewTypeKey, fileInfos,false);
                ModiFile(false);
            }
            catch (Exception) {
                sucess = false;
                //删除失败的文件夹
                //if(Directory.Exists(formDir))
                //      OldTools.DeleteAll(formDir);
            }

            return sucess;
        }
        
        /// <summary>
        /// 修改文件 新的typekey
        /// </summary>
        public static void ModiFile(bool modiAll) {
            var oldTypekey = Toolpars.FormEntity.PkgTypekey;
            var newTypeKey = Toolpars.FormEntity.TxtNewTypeKey;
            var pathInfo = Toolpars.PathEntity;
            var newTypeKeyRootDir = pathInfo.TypeKeyRootDir;
            var tempTypeKeyRootDir = pathInfo.PkgTypeKeyRootDir;

            //个案路径
            var directoryPath = pathInfo.TypeKeyFullRootDir;
            var tDes = new DirectoryInfo(directoryPath);
            //修改解决方案
            var tSearchPatternList = new List<string>();
            tSearchPatternList.AddRange(new[] {
                "*xml",
                "*.sln",
                "*.repx",
                "*.resx",
                "*proj",
                "*.complete",
                "*.cs"
            });
            foreach (var t in tSearchPatternList)
            {
                foreach (var f in tDes.GetFiles(t, SearchOption.AllDirectories))
                {
                    var filePath = f.FullName;
                    if (!File.Exists(filePath))
                        continue;

                    var text = File.ReadAllText(filePath);
                    var oldStr = t.Equals("*.sln")
                        ? oldTypekey
                        : tempTypeKeyRootDir; 
                    var newStr = t.Equals("*.sln")
                        ? newTypeKey
                        : newTypeKeyRootDir;
                    var matchStr = $@"\b{oldStr}\b";
                    var regex = new Regex(matchStr);
                    text = regex.Replace(text, newStr);
                    //text = t.Equals("*.sln")
                    //    ? text.Replace(oldTypekey,
                    //        newTypeKey)
                    //    : text.Replace(tempTypeKeyRootDir,
                    //        newTypeKeyRootDir);


                    const string bpath = @"<HintPath>..\..\";
                    var hintPaths = new[] { "bin", "Export" };
                    text = hintPaths.Aggregate(text, 
                        (current, hitPath) => 
                        current.Replace(bpath + hitPath, bpath + @"..\..\..\WD_PR\SRC\" + hitPath));
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.WriteAllText(filePath, text, Encoding.UTF8);

                    //
                    if (!modiAll) {
                        // 添加编译选项
                        var extionName = Path.GetExtension(filePath);
                        var extionNames = new[] { ".cs", ".resx" };
                        if (!extionNames.Contains(extionName)) continue;
                        
                        var csPath =FindCSproj(filePath);
                        var csDir = $@"{Path.GetDirectoryName(csPath)}\";

                        //找到相对位置
                        var index = filePath.IndexOf(csDir, StringComparison.Ordinal);

                        if (index > -1)
                            XmlTools.AddCsproj(csPath, filePath.Substring(index + csDir.Length));
                    }
                   
                }
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
                var newTypeKey = Toolpars.FormEntity.TxtNewTypeKey;
                var oldTypeKey = Toolpars.FormEntity.PkgTypekey;
                //客户与typekey不可为空
                if (!PathTools.IsNullOrEmpty(Toolpars.FormEntity.TxtToPath)
                    && !PathTools.IsNullOrEmpty(newTypeKey)) {
                    //客户是否存在
                    if (Directory.Exists(Toolpars.FormEntity.TxtToPath)) {
                        //借用是否存在
                        if (!Directory.Exists(pkgPath)) {
                            MessageBox.Show(string.Format(Resources.DirNotExisted, pkgPath), Resources.ErrorMsg,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            success = false;
                        }
                        else {
                            //已借用，是否覆盖,直接删除全部
                            if (Directory.Exists(newTypeKeyFullRootDir)) {
                                if (MessageBox.Show(
                                        newTypeKeyFullRootDir
                                        + Environment.NewLine + Resources.DirExisted,
                                        Resources.WarningMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                                    object tArgsPath = Path.Combine(newTypeKeyFullRootDir);
                                    OldTools.DeleteAll(tArgsPath);
                                }
                                else {
                                    success = false;
                                }
                            }
                            if (success)
                                CopyTo(pkgPath, newTypeKeyFullRootDir, oldTypeKey,newTypeKey,null,true);
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
                    ModiFile(true);
                    MessageBox.Show(Resources.GenerateSucess);
                }

                #endregion
            }
            catch (Exception ex) {
                success = false;
                LogTools.LogError($"CopyAllPkg Error! Detail {ex.Message}");
                MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            InsertInfo("COPY PKG SOURCE");
            return success;
        }

        #endregion

        #endregion

        #region 开启客户端，服务端

        /// <summary>
        ///     打开服务器
        /// </summary>
        public static void ServerOn(string args) {
            InsertInfo("BtnServerOn");
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
            InsertInfo("BtnClientOn");
            if (!CheckProcessOn("Digiwin.Mars.ServerStart")) {
                MessageBox.Show(Resources.ServerNotRunning);
                return;
            }
            if (CheckProcessOn("Digiwin.Mars.ClientStart")) {
                MessageBox.Show(Resources.ClientRunning);
                return;
            }
            var tClientPath = Toolpars.Mplatform + "\\DeployServer\\Shared\\Digiwin.Mars.ClientStart.exe";
            if (!File.Exists(tClientPath))
            {
                MessageBox.Show(string.Format(Resources.NotFindFile, tClientPath));
                return;
            }
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

        #region 删除菜单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bts"></param>
        public static BuildeType[] FindBuilderTypeAndDelete(string id, BuildeType[] bts)
        {
            if (PathTools.IsNullOrEmpty(id) || bts == null) return bts;
            var btsList= bts.ToList();
            var findIndex = btsList.FirstOrDefault(bt => bt.Id.Equals(id));
            if (findIndex != null) {
                btsList.Remove(findIndex);
            }
            else {
                btsList.ForEach(bt=> bt.BuildeItems =FindBuilderTypeAndDelete(id, bt.BuildeItems));
              
            }
            return btsList.ToArray();
        }


        /// <summary>
        ///  根据Id删除配置节点
        /// </summary>
        /// <param name="bt"></param>
        public  static void DeleteById(BuildeType bt)
        {
            try {

                if (bt == null)
                    return;
                Toolpars.BuilderEntity.BuildeTypies =
                    FindBuilderTypeAndDelete(bt.Id, Toolpars.BuilderEntity.BuildeTypies);
                var xmlPath = PathTools.GetSettingPath("BuilderEntity", Toolpars.ModelType);
                ReadToEntityTools.SaveSerialize(Toolpars.BuilderEntity, Toolpars.ModelType, xmlPath);
                ControlTools.InitMainView();
            }
            catch (Exception ex) {
                LogTools.LogError($"Delete Menu Error! Detail:{ex.Message}");
            }

        } 
        #endregion
    }
}
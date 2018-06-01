// create By 08628 20180411

using System;
using System.Collections;
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
using Digiwin.Chun.Common.Tools;
using MSWord = Microsoft.Office.Interop.Word;
using Digiwin.Chun.Models;
using Digiwin.Chun.Views.Properties;
using static Digiwin.Chun.Common.Tools.CommonTools;

namespace Digiwin.Chun.Views.Tools {
    /// <summary>
    ///     工具类
    /// </summary>
    public static class MyTools {
        #region 测试参数
        static void SetTestdata()
        {
            Toolpars.Mpath = ""; //D:\DF_E10_2.0.2\C002152226(达峰机械)\WD_PR_C\SRC
            Toolpars.MInpath = ""; //D:\DF_E10_2.0.2\X30001(鼎捷紧固件)\WD_PR_I\SRC
            Toolpars.Mplatform = @"E:\平台\E50"; //C:\DF_E10_2.0.2
            Toolpars.MdesignPath = @"E:\平台\E50"; //E:\平台\E202
            Toolpars.MVersion = "DF_E10_5.0.0"; //DF_E10_2.0.2
            Toolpars.MIndustry = false;
            Toolpars.CustomerName = "TEST";
            Toolpars.FormEntity.ToPath = $@"E:\DF_E10_5.0.0\TEST";
            Toolpars.FormEntity.PkgPath = $@"E:\DF_E10_5.0.0";
        } 
        #endregion

        #region 属性
        /// <summary>
        /// </summary>
        public static Toolpars Toolpars { get; } = new Toolpars();
        /// <summary>
        ///     存储图标
        /// </summary>

        public static Hashtable ImageList { get; set; } = new Hashtable(); 
        #endregion

        #region 初始化窗体参数

        /// <summary>
        ///     初始化窗体参数
        /// </summary>
        /// <param name="pToIni"></param>
        public static void InitToolpars(string[] pToIni)
        {
            Toolpars.ModelType = ModelType.Json;
            if (pToIni == null)
            {
                Toolpars.FormEntity.ToPath = string.Empty;
            }
            else
            {
                Toolpars.Mall = pToIni[0];
                var args = Toolpars.Mall.Split('&');
                var mpath = args[0];
                Toolpars.Mpath = mpath;//D:\DF_E10_2.0.2\C002152226(达峰机械)\WD_PR_C\SRC
                Toolpars.MInpath = args[1]; //D:\DF_E10_2.0.2\X30001(鼎捷紧固件)\WD_PR_I\SRC
                Toolpars.Mplatform = args[2]; //C:\DF_E10_2.0.2
                Toolpars.MdesignPath = args[3]; //E:\平台\E202
                Toolpars.MVersion = args[4]; //DF_E10_2.0.2
                Toolpars.MIndustry = Convert.ToBoolean(args[5]);
                Toolpars.CustomerName = args[6];
                if (!string.IsNullOrEmpty(mpath))
                {
                    var srcDir = new DirectoryInfo(mpath);
                    Toolpars.FormEntity.ToPath = srcDir.Parent?.Parent?.FullName;
                }
                Toolpars.FormEntity.PkgPath = $@"{Toolpars.MdesignPath}";

                Toolpars.FormEntity.Industry = Toolpars.MIndustry;
                if (Toolpars.Mpath.Contains("PKG")
                    && !Toolpars.MIndustry)
                {
                    Toolpars.FormEntity.IsPkg = true;
                    Toolpars.FormEntity.ToPath = $@"{Toolpars.MdesignPath}";
                }

            }
            //分割宽度
            Toolpars.FormEntity.SpiltWidth = 200;
            //最大分割列
            Toolpars.FormEntity.MaxSplitCount = 6;
            Toolpars.OldTypekey = Toolpars.SettingPathEntity.TemplateTypeKey;
            Toolpars.FormEntity.EditState = false;
            IconTools.InitImageList();
            InitBuilderEntity();
            // SetTestdata();


        }


        #endregion

        #region 辅助类

        /// <summary>
        ///    CopyTypeKey
        /// </summary>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        /// <param name="fromTypeKey"></param>
        /// <param name="toTypeKey"></param>
        // ReSharper disable once UnusedMember.Global
        public static void CopyTo(string fromDir, string toDir,
            string fromTypeKey, string toTypeKey)
        {
            var formFiles = GetFilePath(fromDir);

            //记录一键Copy的文件
            var logPath = new List<FileInfos>();
            foreach (var file in formFiles)
            {
                var fileinfo = new FileInfo(file);
                if (fileinfo.Directory == null)
                    continue;
                var absolutedir = fileinfo.Directory.FullName.Replace(fromDir, string.Empty).Replace(fromTypeKey, toTypeKey);

                var fileName = fileinfo.Name.Replace(fromTypeKey, toTypeKey);
                if (fileName.Contains("ReportLayoutInfo") && (fileName.EndsWith(".repx") || fileName.EndsWith(".xml")))
                {
                    fileName = $@"X{fileName}";
                }
                var absolutePath = PathTools.PathCombine(absolutedir, fileName);

                var newFilePath = PathTools.PathCombine(toDir, absolutePath);
                var newFileDir = PathTools.PathCombine(toDir, absolutedir);

                if (!Directory.Exists(newFileDir))
                    Directory.CreateDirectory(newFileDir);

                CopyFile(file, newFilePath);

            }
            Task.Factory.StartNew(() =>
            {
                Thread.CurrentThread.IsBackground = false;
                WriteToServer(logPath);
            });


        }

        public static void WriteToServer(IEnumerable<FileInfos> fileInfos)
        {
            SqlTools.InsertToolInfo(Toolpars.FormEntity.TxtNewTypeKey, fileInfos);
        }

        /// <summary>
        ///     獲取各種路徑
        /// </summary>
        /// <param name="toolpars"></param>
        /// <returns></returns>
        public static PathEntity GetPathEntity(Toolpars toolpars)
        {
            var settingPathEntity = toolpars.SettingPathEntity;

            var serverFullPath = PathTools.CombineStr(new[] {
                toolpars.Mplatform, settingPathEntity.ServerDir
            }); //平台\\Server\\Application\\Customization
            var clientFullPath = PathTools.CombineStr(new[] {
                toolpars.Mplatform, settingPathEntity.DeployServerDir
            }); //平台\\DeployServer\\Shared\\Customization\\
            var serverProgramsFullPath = PathTools.CombineStr(new[] {
                serverFullPath,
                settingPathEntity.Programs
            }); //平台\\Server\\Application\\Customization\\Programs\\
            var clientProgramsFullPath = PathTools.CombineStr(new[] {
                clientFullPath,
                settingPathEntity.Programs
            }); //平台\\DeployServer\\Shared\\Customization\\Programs\\
            var exportFullPath = PathTools.CombineStr(new[]
                {toolpars.FormEntity.SrcToPath, settingPathEntity.ExportDir}); //个案\\Export\\

            #region typekey路径
            var txtNewTypeKey = toolpars.FormEntity.TxtNewTypeKey;
            var newTypeKeyRootDir = txtNewTypeKey;
            if (!newTypeKeyRootDir.StartsWith(settingPathEntity.PackageBaseName))
            {
                newTypeKeyRootDir = PathTools.CombineStr(new[]
                    {settingPathEntity.PackageBaseName, txtNewTypeKey}); //Digiwin.ERP.typekey
            }
            var newTypeKeyFullRootDir = PathTools.PathCombine(toolpars.FormEntity.SrcToPath, newTypeKeyRootDir); //..\\Digiwin.ERP.typekey 
            #endregion

            #region pkgTypeKey路径
            var pkgTxtNewTypeKey = toolpars.FormEntity.PkgTypekey;
            var pkgTypeKeyRootDir = pkgTxtNewTypeKey;
            if (!pkgTxtNewTypeKey.StartsWith(settingPathEntity.PackageBaseName))
            {
                pkgTypeKeyRootDir = PathTools.CombineStr(new[]
                    {settingPathEntity.PackageBaseName, pkgTxtNewTypeKey}); //Digiwin.ERP.typekey  
            }

            var pkgTypeKeyFullRootDir = PathTools.PathCombine(toolpars.FormEntity.PkgSrcPath, pkgTypeKeyRootDir); //..\\Digiwin.ERP.typekey 
            #endregion

            var businessDir =
                PathTools.CombineStr(new[]
                    {newTypeKeyRootDir, settingPathEntity.BusinessDirExtention}); //Digiwin.ERP.typekey.Business\\
            var implementDir = PathTools.CombineStr(new[]
                {businessDir, settingPathEntity.ImplementDirExtention}); //Digiwin.ERP.typekey.Business.Implement\\
            var uiDir = PathTools.CombineStr(new[] { newTypeKeyRootDir, settingPathEntity.UiDirExtention }); //Digiwin.ERP.typekey.UI\\
            var uiImplementDir =
                PathTools.CombineStr(new[]
                    {uiDir, settingPathEntity.ImplementDirExtention}); //Digiwin.ERP.typekey.UI.Implement\\
            var businessDllName =
                PathTools.CombineStr(new[]
                    {businessDir, settingPathEntity.DllExtention}); //Digiwin.ERP.typekey.Business.dll\\
            var implementDllName =
                PathTools.CombineStr(new[]
                    {implementDir, settingPathEntity.DllExtention}); //Digiwin.ERP.typekey.Business.Implement.dll\\
            var uiDllName = PathTools.CombineStr(new[]
                {uiDir, settingPathEntity.DllExtention}); //Digiwin.ERP.typekey.UI.dll\\
            var uiImplementDllName =
                PathTools.CombineStr(new[]
                    {uiImplementDir, settingPathEntity.DllExtention}); //Digiwin.ERP.typekey.UI.Implement.dll\\


            if (toolpars.MIndustry)
            {
                serverFullPath = PathTools.CombineStr(new[] {
                    toolpars.Mplatform, settingPathEntity.IndustryServerDir
                }); //平台\\Server\\Application\\IndustryServerDir
                clientFullPath = PathTools.CombineStr(new[] {
                    toolpars.Mplatform, settingPathEntity.IndustryDeployDir
                }); //平台\\DeployServer\\Shared\\IndustryDeployDir
                serverProgramsFullPath = PathTools.CombineStr(new[]
                    {serverFullPath, settingPathEntity.Programs});
                clientProgramsFullPath = PathTools.CombineStr(new[]
                    {clientFullPath, settingPathEntity.Programs});
            }
            var pathEntity = new PathEntity
            {
                ServerFullPath = serverFullPath,
                DeployFullPath = clientFullPath,
                ServerProgramsFullPath = serverProgramsFullPath,
                DeployProgramsFullPath = clientProgramsFullPath,
                ExportFullPath = exportFullPath,
                TypeKeySrcRootDir = newTypeKeyRootDir,
                TypeKeySrcFullRootDir = newTypeKeyFullRootDir,
                PkgTypeKeySrcRootDir = pkgTypeKeyRootDir,
                PkgTypeKeySrcFullRootDir = pkgTypeKeyFullRootDir,
                BusinessDir = businessDir,
                ImplementDir = implementDir,
                UiDir = uiDir,
                UiImplementDir = uiImplementDir,
                BusinessDllName = businessDllName,
                ImplementDllName = implementDllName,
                UiDllName = uiDllName,
                UiImplementDllName = uiImplementDllName
            };
            return pathEntity;
        }
        /// <summary>
        ///   Copy源码
        /// </summary>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        /// <param name="fromTypeKey"></param>
        /// <param name="toTypeKey"></param>
        /// <param name="filterfilesinfo"></param>
        /// <param name="copyAll"></param>
        // ReSharper disable once UnusedMember.Global
        public static void CopyTo(string fromDir, string toDir,
            string fromTypeKey, string toTypeKey, IReadOnlyCollection<FileInfos> filterfilesinfo, bool copyAll)
        {
            var formFiles = GetFilePath(fromDir);
            string[] extensions = {
                ".sln", ".csproj"
            };
            //记录一键Copy的文件
            var logPath = new List<FileInfos>();
            foreach (var file in formFiles)
            {
                var fileinfo = new FileInfo(file);
                var extensionName = Path.GetExtension(file);

                if (!copyAll && filterfilesinfo != null
                    && filterfilesinfo.Count > 0)
                {
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
                //不改变文件名，copy代码不用改文件名
                var fileName = fileinfo.Name.Replace(fromTypeKey, toTypeKey);
                var absolutePath = PathTools.PathCombine(absolutedir, fileName);

                var newFilePath = PathTools.PathCombine(toDir, absolutePath);
                var newFileDir = PathTools.PathCombine(toDir, absolutedir);

                if (!Directory.Exists(newFileDir))
                    Directory.CreateDirectory(newFileDir);

                if (File.Exists(newFilePath))
                {
                    //项目文件已存在则忽略
                    if (extensions.Contains(extensionName))
                    {
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
                if (copyAll)
                {
                    logPath.Add(new FileInfos
                    {
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
            if (copyAll)
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.CurrentThread.IsBackground = false;
                    WriteToServer(logPath);
                });
            }
        } 
        #endregion

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
                var typeKeyFullRootDir = pathInfo.TypeKeySrcFullRootDir;
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
                var msg = string.Empty;
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
            WriteLogByTreeView(treeView);
            InitBuilderEntity();
            return true;
        }
        /// <summary>
        ///     日志
        /// </summary>
        public static void WriteLogByTreeView(MyTreeView treeView)
        {
            var toolpars = Toolpars;
            var pathDic = GetTreeViewFilePath(treeView.Nodes);
            var operationLog = (toolpars.CustomerName == null || toolpars.CustomerName.Equals(string.Empty)
                ? DateTime.Now.ToString("yyyyMMddhhmmss")
                : toolpars.CustomerName);
            var logPath = LogTools.GetLogDir(operationLog);

            var logStr = new StringBuilder();
            const string empStr = @"      ";
            foreach (var kv in pathDic)
            {
                foreach (var fileinfo in kv.Value)
                    logStr.AppendLine($"{(logStr.Length > 0 ? empStr : string.Empty)}# {kv.Key} {empStr}{fileinfo.FileName}");
            }
            LogTools.LogMsg(logPath, logStr.ToString());
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
                    &&PathTools.IsTrue("True")
                ) {
                    item.FileInfos = CreateFileMappingInfo(item, $"Create{item.Id}", $"Create{item.Id}");
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
                newFilePath = PathTools.PathCombine(Toolpars.FormEntity.SrcToPath, newFilePath);

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
            var directoryPath = $@"{pathInfo.TypeKeySrcFullRootDir}\";
            var typeKeyRootDir = pathInfo.TypeKeySrcRootDir;
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
                    fileinfo.ToPath = PathTools.PathCombine(Toolpars.FormEntity.SrcToPath, newFilePath);
                }
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="bt"></param>
        /// <param name="fileInfos"></param>
        public static void SetFileInfo(string parentId, BuildeType bt,List<FileInfos> fileInfos)  {
            var parItem = Toolpars.BuilderEntity.BuildeTypies.ToList()
                .Where(et => et.Id.Equals(parentId)).ToList();
            if (parItem.Count <= 0) return;
            {
                var citem = parItem[0].BuildeItems
                    .Where(et => et.Id.Equals(bt.Id)).ToList();
                if (citem.Count > 0)
                    if (PathTools.IsTrue(bt.Checked))
                        citem.ForEach(ee => {
                                ee.Checked = "True";
                                ee.FileInfos = fileInfos;
                            }
                        );
                    else
                        citem.ForEach(ee => {
                            ee.Checked = "False";
                            ee.FileInfos = fileInfos;
                        });
            }
        }

        /// <summary>
        /// 返回项目对应的文件
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static MappingItem QueryMappingItemByBt(BuildeType bt) {
            var fileMapping = Toolpars.FileMappingEntity;
            var id = bt.Id;
            var fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                filmap.Id.Equals(id));
            if (bt.ParentId != null
                && !bt.ParentId.Equals(string.Empty))
                fileInfo = fileMapping.MappingItems.ToList().FirstOrDefault(filmap =>
                    filmap.Id.Equals(bt.ParentId));
            return fileInfo;
        }

        /// <summary>
        ///     创建项目的模板文件映射
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="className"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public static List<FileInfos> CreateFileMappingInfo(BuildeType bt,string className,string functionName) {
            var fileInfos = new List<FileInfos>();
            var fileInfo = QueryMappingItemByBt(bt);

            if (fileInfo?.Paths == null)
                return fileInfos;
            if (fileInfo.Paths.Length == 1) {
               var fileinfo =CreateFileInfos( className, functionName, fileInfo.Paths[0]);
                if (bt.ParentId != null
                    && !bt.ParentId.Equals(string.Empty))
                {
                    fileinfo.PartId = bt.ParentId;
                    fileinfo.Id = bt.Id;
                    fileinfo.IsMerge = bt.IsMerge;
                }
                fileInfos.Add(fileinfo);
             
            }
            else {
                fileInfo.Paths.ToList().ForEach(path => {
                    var classNameFiled = Path.GetFileNameWithoutExtension(path);
                    var fileinfo = CreateFileInfos(classNameFiled,"",path);
                    fileInfos.Add(fileinfo);
                });
            }

            //联动其他项
            if (PathTools.IsNullOrEmpty(bt.UnionId))
                return fileInfos;
            var unionItems = bt.UnionId;
            unionItems.ToList().ForEach(id => {
                if(id.Equals(bt.Id))return;//防联动自身死循环
                var iService = GetBuildeTypeById(id, Toolpars.BuilderEntity.BuildeTypies);
                if (iService == null) return;
                var newClassName = $@"Create{id}";
                var newFunctionName = $@"Create{id}";
                if (id.Equals("IService"))
                {
                    newClassName = className.Substring(1);
                    newFunctionName = functionName;
                }
                var newFileInfo = CreateFileMappingInfo(iService, newClassName, newFunctionName);
                fileInfos.AddRange(newFileInfo);
            });
            return fileInfos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <param name="functionName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileInfos CreateFileInfos(string className,string functionName,string path) {
            var fromPath = PathTools.PathCombine(Toolpars.MvsToolpath, "Template", path);
            var fileinfo = new FileInfos {
                ActionName = "",
                ClassName = className,
                FileName = className,
                FunctionName = functionName,
                BasePath = path,
                FromPath = fromPath,
            };
            var oldFilePath = Path.GetFileNameWithoutExtension(path);
            if (oldFilePath == null) return fileinfo;
            var newFilePath = path.Replace(oldFilePath, fileinfo.FileName);
            fileinfo.ToPath = PathTools.PathCombine(Toolpars.FormEntity.SrcToPath, newFilePath);
            return fileinfo;
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
                var formDir = pathInfo.PkgTypeKeySrcFullRootDir;
                var toDir = pathInfo.TypeKeySrcFullRootDir;
                CopyTo(formDir, toDir, Toolpars.FormEntity.PkgTypekey, Toolpars.FormEntity.TxtNewTypeKey, fileInfos,
                    false);
                ModiFile(toDir, true, false);
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
        public static void ModiFile(string targetDir,bool isSrc,bool modiAll) {
            var oldTypekey = Toolpars.FormEntity.PkgTypekey;
            var newTypeKey = Toolpars.FormEntity.TxtNewTypeKey;
            var pathInfo = Toolpars.PathEntity;
            var newTypeKeyRootDir = pathInfo.TypeKeySrcRootDir;
            var tempTypeKeyRootDir = pathInfo.PkgTypeKeySrcRootDir;

            //个案路径
            var tDes = new DirectoryInfo(targetDir);
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
                    if (isSrc) {
                        //修改与新增，可能引发的问题是，将旧type重复命名，如XMO-->XXMO
                        //一键copy不存在此问题，因为每次都是删除全部文件后添加
                        var oldStr = !t.Equals("*.sln")
                                ? tempTypeKeyRootDir
                                : oldTypekey
                            ;
                        var newStr = !t.Equals("*.sln")
                            ? newTypeKeyRootDir
                            : newTypeKey;
                        var matchStr = modiAll && t.Equals("*proj")?$@"{oldTypekey}": $@"\b{oldStr}\b";
                        var regex = new Regex(matchStr);
                        if (modiAll && t.Equals("*proj")) {
                            text = regex.Replace(text, newTypeKey);
                        }
                        else {
                            text = regex.Replace(text, newStr);

                        }
                        const string bpath = @"<HintPath>..\..\";
                        var hintPaths = new[] {"bin", "Export"};
                        text = hintPaths.Aggregate(text,
                            (current, hitPath) =>
                                current.Replace(bpath + hitPath, bpath + @"..\..\..\WD_PR\SRC\" + hitPath));
                      
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        File.WriteAllText(filePath, text, Encoding.UTF8);
                        //
                        if (!modiAll) {
                            // 添加编译选项
                            var extionName = Path.GetExtension(filePath);
                            var extionNames = new[] {".cs", ".resx"};
                            if (!extionNames.Contains(extionName)) continue;

                            var csPath = FindCSproj(filePath);
                            var csDir = $@"{Path.GetDirectoryName(csPath)}\";

                            //找到相对位置
                            var index = filePath.IndexOf(csDir, StringComparison.Ordinal);

                            if (index > -1)
                                XmlTools.AddCsproj(csPath, filePath.Substring(index + csDir.Length));
                        }
                    }
                    //配置
                    else {
                      
                        text = text.Replace(oldTypekey, newTypeKey);
                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                        File.Delete(f.FullName);
                        if (f.Name.Contains("ReportInfoContainer.RPDdcxml") ||
                            f.Name.Contains("ReportInfoContainer_Ex.xml") ||
                            f.Name.Contains("DataAuthorizationInfoContainer.dcxml")||
                            f.Name.Contains("QueryInfoContainer.dcxml") )
                        {
                            if (text.Contains("_ReportLayoutInfo_")) {
                                if (f.Directory != null) {
                                    var title = f.Directory.Name.Split('_');
                                    text = text.Replace(title[0].Substring(1) + "_ReportLayoutInfo_", title[0] + "_ReportLayoutInfo_");
                                }
                            }
                            else
                            {
                                  text = text.Replace("ReportLayoutInfo_", "XReportLayoutInfo_");
                            }
                        }
                        File.WriteAllText(f.FullName, text);
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
        /// 获取服务端目录
        /// </summary>
        /// <param name="isPkg"></param>
        /// <returns></returns>
        public static string GetServerDirPath(bool isPkg)
        {
            var wd  = isPkg ? "WD" : "WD_C";
            var pkgPath = Toolpars.FormEntity.PkgPath;
            var typeKeyDir = $@"{pkgPath}\{wd}";
            var path = FindTypekeyDir(typeKeyDir, Toolpars.FormEntity.PkgTypekey);
            return path;
        }
        

        /// <summary>
        /// </summary>
        /// <param name="isPkg"></param>
        /// <returns></returns>
        public static bool CopyAllPkG(bool isPkg) {
            var success = false;
            try {
                var customerToPath = Toolpars.FormEntity.ToPath;
                var copySrc = false;
                var copyWd = false;

                var pathInfo = Toolpars.PathEntity;
                //默认取pkgsource为digiwin.erp.typekey
                var pkgPath = pathInfo.PkgTypeKeySrcFullRootDir;
                var newTypeKeyFullRootDir = pathInfo.TypeKeySrcFullRootDir;
                var newTypeKey = Toolpars.FormEntity.TxtNewTypeKey;
                var oldTypeKey = Toolpars.FormEntity.PkgTypekey;
                if (!oldTypeKey.ToUpper().StartsWith(@"X")&&!Directory.Exists(pkgPath)) {
                    pkgPath = pkgPath.Replace(oldTypeKey, $@"X{oldTypeKey}");
                }
                //  var TxtToPath
                //找到typeKey目录
                var formTypeKeyDir = GetServerDirPath(isPkg);
                //typekey目录
                var newTypeKeyDir = string.Empty;
                //formTypeKeyDir一定是形如 WD_C\BUSINESS_OBJECT\MO 的目录
                if (!string.IsNullOrEmpty(formTypeKeyDir) && Directory.Exists(formTypeKeyDir)) {
                  
                    var directoryInfo = new DirectoryInfo(formTypeKeyDir);
                    var absoluteDir = $@"{directoryInfo.Parent?.Parent?.Name}\{directoryInfo.Parent?.Name}";
                    var wd = isPkg ? "WD" : "WD_C";
                    absoluteDir = absoluteDir.Replace(wd, "WD_C");
                    newTypeKeyDir = PathTools.PathCombine(customerToPath, absoluteDir, Toolpars.FormEntity.TxtNewTypeKey);
                }

                //客户与typekey不可为空
                    if (!PathTools.IsNullOrEmpty(customerToPath)
                    && !PathTools.IsNullOrEmpty(newTypeKey)) {
                    //客户需存在
                    if (Directory.Exists(customerToPath)) {
                        //借用不存在（源码/typekey）
                        if (!Directory.Exists(pkgPath)
                            && (string.IsNullOrEmpty(formTypeKeyDir)
                            || !Directory.Exists(formTypeKeyDir))) {
                            MessageBox.Show(Resources.CopyResourceNotExisted, Resources.ErrorMsg,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                        else {
                            //源码已借用，询问是否覆盖,直接删除全部
                            if (Directory.Exists(pkgPath)) {
                                if (Directory.Exists(newTypeKeyFullRootDir)
                                )
                                {
                                    if (MessageBox.Show(newTypeKeyFullRootDir + Environment.NewLine + Resources.DirExisted,
                                            Resources.WarningMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                                        == DialogResult.Yes)
                                    {
                                        copySrc = true;
                                        object tArgsPath = Path.Combine(newTypeKeyFullRootDir);
                                        DeleteAll(tArgsPath);
                                    }
                                }
                                else
                                {
                                    copySrc = true;
                                }
                            }
                            if ((!string.IsNullOrEmpty(formTypeKeyDir)
                                 && Directory.Exists(formTypeKeyDir))) {
                                //配置已借用，询问是否覆盖,直接删除全部
                                if ((!string.IsNullOrEmpty(newTypeKeyDir) && (Directory.Exists(newTypeKeyDir))
                                ))
                                {
                                    if (MessageBox.Show(newTypeKeyDir + Environment.NewLine + Resources.DirExisted,
                                            Resources.WarningMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                                        == DialogResult.Yes)
                                    {
                                        copyWd = true;
                                        object tArgsPath = Path.Combine(newTypeKeyDir);
                                        DeleteAll(tArgsPath);
                                    }
                                }
                                else
                                {
                                    copyWd = true;
                                }
                            }
                        
                            //copyTypeKey
                            if (copyWd) {
                                if (!string.IsNullOrEmpty(formTypeKeyDir)
                                    && Directory.Exists(formTypeKeyDir)) {
                                    CopyTo(formTypeKeyDir, newTypeKeyDir, oldTypeKey, newTypeKey);
                                }
                            }
                            //COPYSRC
                            if (copySrc) {
                                if (!string.IsNullOrEmpty(pkgPath)
                                    && Directory.Exists(pkgPath)) {
                                    //copy源码
                                    CopyTo(pkgPath, newTypeKeyFullRootDir, oldTypeKey, newTypeKey, null, true);
                                }
                            }
                        }
                    }
                    else {
                        MessageBox.Show(string.Format(Resources.DirNotExisted, customerToPath),
                            Resources.ErrorMsg,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else {
                    MessageBox.Show(Resources.TypekeyNotExisted, Resources.ErrorMsg, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                #region 修改命名

              
                if (copyWd)
                {
                    ModiFile(newTypeKeyDir, false, true);
                }
                if (copySrc)
                {
                    ModiFile(newTypeKeyFullRootDir, true, true);
                }
                if (copySrc || copyWd) {
                    success = true;
                    MessageBox.Show(Resources.GenerateSucess);
                }
                else {
                    
                    MessageBox.Show(Resources.NoCopySource);
                }
                #endregion
            }
            catch (Exception ex) {
                LogTools.LogError($"CopyAllPkg Error! Detail {ex.Message}");
                MessageBox.Show(ex.Message, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            InsertInfo("COPY PKG SOURCE");
            return success;
        }
       
        #endregion

        #endregion
        
        #region 删除菜单
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
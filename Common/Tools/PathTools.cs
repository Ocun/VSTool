// create By 08628 20180411

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Implement.Entity;
using Common.Implement.Properties;

namespace Common.Implement.Tools {
    internal class PathTools {
        /// <summary>
        ///     獲取各種路徑
        /// </summary>
        /// <param name="toolpars"></param>
        /// <returns></returns>
        public static PathEntity GetPathEntity(Toolpars toolpars) {
            var settingPathEntity = toolpars.SettingPathEntity;
            var serverProgramsFullPath = CombineStr(new[] {
                toolpars.Mplatform, settingPathEntity.ServerDir,
                settingPathEntity.Programs
            }); //平台\\Server\\Application\\Customization\\Programs\\
            var clientProgramsFullPath = CombineStr(new[] {
                toolpars.Mplatform, settingPathEntity.DeployServerDir,
                settingPathEntity.Programs
            }); //平台\\DeployServer\\Shared\\Customization\\Programs\\
            var exportFullPath = CombineStr(new[]
                {toolpars.FormEntity.TxtToPath, settingPathEntity.ExportDir}); //个案\\Export\\

            #region typekey路径
            var txtNewTypeKey = toolpars.FormEntity.TxtNewTypeKey;
            var newTypeKeyRootDir = txtNewTypeKey;
            if (newTypeKeyRootDir.StartsWith(settingPathEntity.PackageBaseName)) {
                newTypeKeyRootDir= CombineStr(new[]
                    {settingPathEntity.PackageBaseName, txtNewTypeKey}); //Digiwin.ERP.typekey
            }
            var newTypeKeyFullRootDir = PathCombine(toolpars.FormEntity.TxtToPath, newTypeKeyRootDir); //..\\Digiwin.ERP.typekey 
            #endregion

            #region pkgTypeKey路径
            var pkgTxtNewTypeKey = toolpars.FormEntity.PkgTypekey;
            var pkgTypeKeyRootDir = pkgTxtNewTypeKey;
            if (!pkgTxtNewTypeKey.StartsWith(settingPathEntity.PackageBaseName)) {
                pkgTypeKeyRootDir = CombineStr(new[]
                    {settingPathEntity.PackageBaseName, pkgTxtNewTypeKey}); //Digiwin.ERP.typekey  
            }
            
            var pkgTypeKeyFullRootDir = PathCombine(toolpars.FormEntity.TxtPkGpath, pkgTypeKeyRootDir); //..\\Digiwin.ERP.typekey 
            #endregion

            var businessDir =
                CombineStr(new[]
                    {newTypeKeyRootDir, settingPathEntity.BusinessDirExtention}); //Digiwin.ERP.typekey.Business\\
            var implementDir = CombineStr(new[]
                {businessDir, settingPathEntity.ImplementDirExtention}); //Digiwin.ERP.typekey.Business.Implement\\
            var uiDir = CombineStr(new[] {newTypeKeyRootDir, settingPathEntity.UiDirExtention}); //Digiwin.ERP.typekey.UI\\
            var uiImplementDir =
                CombineStr(new[]
                    {uiDir, settingPathEntity.ImplementDirExtention}); //Digiwin.ERP.typekey.UI.Implement\\
            var businessDllName =
                CombineStr(new[]
                    {businessDir, settingPathEntity.DllExtention}); //Digiwin.ERP.typekey.Business.dll\\
            var implementDllName =
                CombineStr(new[]
                    {implementDir, settingPathEntity.DllExtention}); //Digiwin.ERP.typekey.Business.Implement.dll\\
            var uiDllName = CombineStr(new[]
                {uiDir, settingPathEntity.DllExtention}); //Digiwin.ERP.typekey.UI.dll\\
            var uiImplementDllName =
                CombineStr(new[]
                    {uiImplementDir, settingPathEntity.DllExtention}); //Digiwin.ERP.typekey.UI.Implement.dll\\


            if (toolpars.MIndustry) {
                serverProgramsFullPath = CombineStr(new[]
                    {toolpars.Mplatform, settingPathEntity.IndustryServerDir, settingPathEntity.Programs});
                clientProgramsFullPath = CombineStr(new[]
                    {toolpars.Mplatform, settingPathEntity.IndustryDeployDir, settingPathEntity.Programs});
            }
            var pathEntity = new PathEntity {
                ServerProgramsFullPath = serverProgramsFullPath,
                DeployProgramsFullPath = clientProgramsFullPath,
                ExportFullPath = exportFullPath,
                TypeKeyRootDir = newTypeKeyRootDir,
                TypeKeyFullRootDir = newTypeKeyFullRootDir,
                PkgTypeKeyRootDir =  pkgTypeKeyRootDir,
                PkgTypeKeyFullRootDir =  pkgTypeKeyFullRootDir,
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


        public static string CombineStr(string[] strs) {
            var sb = new StringBuilder();
            strs.ToList().ForEach(str => sb.Append((str ?? string.Empty).Trim()));
            return sb.ToString();
        }

        public static string PathCombine(params string[] strs) {
            var path = string.Empty;
            strs.ToList().ForEach(str => {
                    var pathArg = (str ?? string.Empty).Trim();
                    if (path.Equals(string.Empty)) {
                        path = pathArg;
                    }
                    else if (path.EndsWith(@"\")) {
                        path = $@"{path}{pathArg}";
                    }
                    else {
                        path = $@"{path}\{pathArg}";
                    }
                   
                }
            );
            return path;
        }

        public static bool IsNullOrEmpty(object value) {
            var f = value == null;
            if(f) return true;
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            if (value is string) {
                f = ((string) value).Trim().Equals(string.Empty);
            }
            return f;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool CheckFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception(string.Format(Resources.NotFindFile, filePath));
            return true;
        }

        public static string GetSettingPath(string fileName,Toolpars toolPars) {
            var modelType= toolPars.ModelType;
            var path = string.Empty;
            var mvsToolpath = toolPars.MvsToolpath;
            switch (modelType) {
                case ModelType.Xml:
                    path = fileName + ".xml";
                    break;
                case ModelType.Json:
                    path = fileName + ".json";
                    break;
                case ModelType.Binary:
                    path = fileName + ".data";
                    break;
            }
            path = PathCombine(mvsToolpath, "Config", path);
            return path;
        }
    }
}
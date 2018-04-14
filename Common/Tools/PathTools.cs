// create By 08628 20180411

using System.Linq;
using System.Text;
using Common.Implement.Entity;

namespace Common.Implement.Tools {
    internal class PathTools {
        /// <summary>
        ///     獲取各種路徑
        /// </summary>
        /// <param name="toolpars"></param>
        /// <returns></returns>
        public static PathEntity GetPathEntity(Toolpars toolpars) {
            var settingPathEntity = toolpars.SettingPathEntity;
            var serverProgramsPath = CombineStr(new[] {
                toolpars.Mplatform, settingPathEntity.ServerDir,
                settingPathEntity.Programs
            }); //\\Server\\Application\\Customization\\Programs\\
            var clientProgramsPath = CombineStr(new[] {
                toolpars.Mplatform, settingPathEntity.DeployServerDir,
                settingPathEntity.Programs
            }); //\\DeployServer\\Shared\\Customization\\Programs\\
            var exportPath = CombineStr(new[]
                {toolpars.FormEntity.TxtToPath, settingPathEntity.ExportDir}); //..\\Export\\
            var txtNewTypeKey = toolpars.FormEntity.txtNewTypeKey;
            var rootDir = CombineStr(new[]
                {settingPathEntity.PackageBaseName, txtNewTypeKey}); //..\\Digiwin.ERP.typekey
            var businessDir =
                CombineStr(new[]
                    {rootDir, settingPathEntity.BusinessDirExtention}); //..\\Digiwin.ERP.typekey.Business\\
            var implementDir = CombineStr(new[]
                {businessDir, settingPathEntity.ImplementDirExtention}); //..\\Digiwin.ERP.typekey.Business.Implement\\
            var uiDir = CombineStr(new[] {rootDir, settingPathEntity.UIDirExtention}); //..\\Digiwin.ERP.typekey.UI\\
            var uiImplementDir =
                CombineStr(new[]
                    {uiDir, settingPathEntity.ImplementDirExtention}); //..\\Digiwin.ERP.typekey.UI.Implement\\
            var businessDllName =
                CombineStr(new[]
                    {businessDir, settingPathEntity.DllExtention}); //..\\Digiwin.ERP.typekey.Business.dll\\
            var implementDllName =
                CombineStr(new[]
                    {implementDir, settingPathEntity.DllExtention}); //..\\Digiwin.ERP.typekey.Business.Implement.dll\\
            var uiDllName = CombineStr(new[]
                {uiDir, settingPathEntity.DllExtention}); //..\\Digiwin.ERP.typekey.UI.dll\\
            var uiImplementDllName =
                CombineStr(new[]
                    {uiImplementDir, settingPathEntity.DllExtention}); //..\\Digiwin.ERP.typekey.UI.Implement.dll\\


            if (toolpars.MIndustry) {
                serverProgramsPath = CombineStr(new[]
                    {toolpars.Mplatform, settingPathEntity.IndustryServerDir, settingPathEntity.Programs});
                clientProgramsPath = CombineStr(new[]
                    {toolpars.Mplatform, settingPathEntity.IndustryDeployDir, settingPathEntity.Programs});
            }
            var pathEntity = new PathEntity {
                ServerProgramsPath = serverProgramsPath,
                DeployProgramsPath = clientProgramsPath,
                ExportPath = exportPath,
                RootDir = rootDir,
                BusinessDir = businessDir,
                ImplementDir = implementDir,
                UIDir = uiDir,
                UIImplementDir = uiImplementDir,
                BusinessDllName = businessDllName,
                ImplementDllName = implementDllName,
                UIDllName = uiDllName,
                UIImplementDllName = uiImplementDllName
            };
            return pathEntity;
        }

        public static string CombineStr(string[] strs) {
            var sb = new StringBuilder();
            strs.ToList().ForEach(str => sb.Append((str ?? string.Empty).Trim()));
            return sb.ToString();
        }


        ///根据模板路径生成配置文件
        public static void CreateTemplateSetting() {
        }
    }
}
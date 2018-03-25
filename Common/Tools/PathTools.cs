using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Implement
{
    class PathTools
    {
        /// <summary>
        /// 獲取各種路徑
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PathEntity getPathEntity(toolpars Toolpars) {
            var SettingPathEntity = Toolpars.SettingPathEntity;
            string serverProgramsPath = CombineStr(new[]{Toolpars.Mplatform , SettingPathEntity.ServerDir,
                                SettingPathEntity.Programs});//\\Server\\Application\\Customization\\Programs\\
            string clientProgramsPath = CombineStr(new[]{Toolpars.Mplatform, SettingPathEntity.DeployServerDir,
                                SettingPathEntity.Programs});//\\DeployServer\\Shared\\Customization\\Programs\\
            string ExportPath = CombineStr(new[]{Toolpars.formEntity.txtToPath , SettingPathEntity.ExportDir});//..\\Export\\
            string txtNewTypeKey = Toolpars.formEntity.txtNewTypeKey;
            string rootDir = CombineStr(new[] {SettingPathEntity.PackageBaseName, txtNewTypeKey});//..\\Digiwin.ERP.typekey
            string businessDir = CombineStr(new[]{rootDir, SettingPathEntity.BusinessDirExtention});//..\\Digiwin.ERP.typekey.Business\\
            string ImplementDir = CombineStr(new[]{ businessDir , SettingPathEntity.ImplementDirExtention });//..\\Digiwin.ERP.typekey.Business.Implement\\
            string UIDir = CombineStr(new[]{rootDir , SettingPathEntity.UIDirExtention  });//..\\Digiwin.ERP.typekey.UI\\
            string UIImplementDir = CombineStr(new[]{ UIDir, SettingPathEntity.ImplementDirExtention});//..\\Digiwin.ERP.typekey.UI.Implement\\
            string businessDllName = CombineStr(new[]{businessDir, SettingPathEntity.DllExtention});//..\\Digiwin.ERP.typekey.Business.dll\\
            string ImplementDllName = CombineStr(new[] {ImplementDir, SettingPathEntity.DllExtention});//..\\Digiwin.ERP.typekey.Business.Implement.dll\\
            string UIDllName = CombineStr(new[] { UIDir, SettingPathEntity.DllExtention });//..\\Digiwin.ERP.typekey.UI.dll\\
            string UIImplementDllName = CombineStr(new[] { UIImplementDir, SettingPathEntity.DllExtention });//..\\Digiwin.ERP.typekey.UI.Implement.dll\\


            if (Toolpars.MIndustry)
            {
                serverProgramsPath = CombineStr(new[]{Toolpars.Mplatform , SettingPathEntity.IndustryServerDir, SettingPathEntity.Programs });
                clientProgramsPath = CombineStr(new[] { Toolpars.Mplatform, SettingPathEntity.IndustryDeployDir, SettingPathEntity.Programs});

            }
            PathEntity pathEntity = new PathEntity()
            {
                ServerProgramsPath = serverProgramsPath,
                DeployProgramsPath = clientProgramsPath,
                ExportPath = ExportPath,
                RootDir = rootDir,
                BusinessDir = businessDir,
                ImplementDir = ImplementDir,
                UIDir = UIDir,
                UIImplementDir = UIImplementDir,
                BusinessDllName = businessDllName,
                ImplementDllName = ImplementDllName,
                UIDllName = UIDllName,
                UIImplementDllName = UIImplementDllName
            };
            return pathEntity;

        }

        public static string CombineStr(string[] strs) {
            StringBuilder sb = new StringBuilder();
            strs.ToList().ForEach(str => sb.Append((str ?? string.Empty).Trim()));
            return sb.ToString();
        }
      
    }
}

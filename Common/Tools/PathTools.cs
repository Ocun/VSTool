// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Implement.Entity;

namespace Common.Implement.Tools
{
    class PathTools
    {
        /// <summary>
        /// 獲取各種路徑
        /// </summary>
        /// <param name="toolpars"></param>
        /// <returns></returns>
        public static PathEntity getPathEntity(Toolpars toolpars) {
            var SettingPathEntity = toolpars.SettingPathEntity;
            string serverProgramsPath = CombineStr(new[]{toolpars.Mplatform , SettingPathEntity.ServerDir,
                                SettingPathEntity.Programs});//\\Server\\Application\\Customization\\Programs\\
            string clientProgramsPath = CombineStr(new[]{toolpars.Mplatform, SettingPathEntity.DeployServerDir,
                                SettingPathEntity.Programs});//\\DeployServer\\Shared\\Customization\\Programs\\
            string ExportPath = CombineStr(new[]{toolpars.formEntity.TxtToPath , SettingPathEntity.ExportDir});//..\\Export\\
            string txtNewTypeKey = toolpars.formEntity.txtNewTypeKey;
            string rootDir = CombineStr(new[] {SettingPathEntity.PackageBaseName, txtNewTypeKey});//..\\Digiwin.ERP.typekey
            string businessDir = CombineStr(new[]{rootDir, SettingPathEntity.BusinessDirExtention});//..\\Digiwin.ERP.typekey.Business\\
            string ImplementDir = CombineStr(new[]{ businessDir , SettingPathEntity.ImplementDirExtention });//..\\Digiwin.ERP.typekey.Business.Implement\\
            string UIDir = CombineStr(new[]{rootDir , SettingPathEntity.UIDirExtention  });//..\\Digiwin.ERP.typekey.UI\\
            string UIImplementDir = CombineStr(new[]{ UIDir, SettingPathEntity.ImplementDirExtention});//..\\Digiwin.ERP.typekey.UI.Implement\\
            string businessDllName = CombineStr(new[]{businessDir, SettingPathEntity.DllExtention});//..\\Digiwin.ERP.typekey.Business.dll\\
            string ImplementDllName = CombineStr(new[] {ImplementDir, SettingPathEntity.DllExtention});//..\\Digiwin.ERP.typekey.Business.Implement.dll\\
            string UIDllName = CombineStr(new[] { UIDir, SettingPathEntity.DllExtention });//..\\Digiwin.ERP.typekey.UI.dll\\
            string UIImplementDllName = CombineStr(new[] { UIImplementDir, SettingPathEntity.DllExtention });//..\\Digiwin.ERP.typekey.UI.Implement.dll\\


            if (toolpars.MIndustry)
            {
                serverProgramsPath = CombineStr(new[]{toolpars.Mplatform , SettingPathEntity.IndustryServerDir, SettingPathEntity.Programs });
                clientProgramsPath = CombineStr(new[] { toolpars.Mplatform, SettingPathEntity.IndustryDeployDir, SettingPathEntity.Programs});

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


        ///根据模板路径生成配置文件
        public static void CreateTemplateSetting() {
            
        }
    }
}

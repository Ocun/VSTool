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
                                SettingPathEntity.Programs});
            string clientProgramsPath = CombineStr(new[]{Toolpars.Mplatform, SettingPathEntity.DeployServerDir,
                                SettingPathEntity.Programs});
            string ExportPath = CombineStr(new[]{Toolpars.formEntity.txtToPath , SettingPathEntity.ExportDir});
            string txtNewTypeKey = Toolpars.formEntity.txtNewTypeKey;
            string rootDir = CombineStr(new[] {SettingPathEntity.PackageBaseName, txtNewTypeKey});
            string businessDir = CombineStr(new[]{rootDir, SettingPathEntity.BusinessDirExtention});
            string ImplementDir = CombineStr(new[]{ businessDir , SettingPathEntity.ImplementDirExtention });
            string UIDir = CombineStr(new[]{rootDir , SettingPathEntity.UIDirExtention  });
            string UIImplementDir = CombineStr(new[]{ UIDir, SettingPathEntity.ImplementDirExtention});
            string businessDllName = CombineStr(new[]{businessDir, SettingPathEntity.DllExtention});
            string ImplementDllName = CombineStr(new[] {ImplementDir, SettingPathEntity.DllExtention});
            string UIDllName = CombineStr(new[] { UIDir, SettingPathEntity.DllExtention });
            string UIImplementDllName = CombineStr(new[] { UIImplementDir, SettingPathEntity.DllExtention });
                                      

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

using System;
using System.Collections.Generic;
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
        public static PathEntity getPathEntity(toolpars Toolpars)
        {
            string serverPath = Toolpars.Mplatform + "\\Server\\Application\\Customization\\Programs\\";
            string clientPath = Toolpars.Mplatform + "\\DeployServer\\Shared\\Customization\\Programs\\";
            string ExportPath = Toolpars.formEntity.txtToPath + "\\Export\\";
            string txtNewTypeKey = Toolpars.formEntity.txtNewTypeKey;

            string businessDllName = "Digiwin.ERP." + txtNewTypeKey
                                     + ".Business.dll";
            string ImplementDllName = "Digiwin.ERP." + txtNewTypeKey
                + ".Business.Implement.dll";
            string UIDllName = "Digiwin.ERP." + txtNewTypeKey + ".UI.dll";
            string UIImplementDllName = "Digiwin.ERP." + txtNewTypeKey
                + ".UI.Implement.dll";

            string businessDir = "Digiwin.ERP." + txtNewTypeKey;
            string ImplementDir = "Digiwin.ERP." + txtNewTypeKey + ".Business.Implement";
            string UIDir = "Digiwin.ERP." + txtNewTypeKey + ".UI";
            string UIImplementDir = "Digiwin.ERP." + txtNewTypeKey + ".UI.Implement";



            if (Toolpars.MIndustry)
            {
                serverPath = Toolpars.Mplatform + "\\Server\\Application\\Industry\\Programs\\";
                clientPath = Toolpars.Mplatform + "\\DeployServer\\Shared\\Industry\\Programs\\";

            }
            PathEntity pathEntity = new PathEntity()
            {
                ServerPath = serverPath,
                DeployServerPath = clientPath,
                ExportPath = ExportPath,
                BusinessDllName = businessDllName,
                ImplementDllName = ImplementDllName,
                UIDllName = UIDllName,
                UIImplementDllName = UIImplementDllName
            };
            return pathEntity;
            //   Toolpars.PathEntity = pathEntity;

        } 
    }
}

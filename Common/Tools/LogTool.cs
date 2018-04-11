// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Implement.Entity;
using Common.Implement.UI;

namespace Common.Implement.Tools
{
    public static class LogTool
    {

        #region 日志

        /// <summary>
        /// 日志
        /// </summary>
        public static void WriteLog(toolpars Toolpars, MyTreeView treeView)
        {
            var pathDic = MyTool.GetTreeViewFilePath(treeView.Nodes, Toolpars);
            string txtNewTypeKey = Toolpars.formEntity.txtNewTypeKey;
            string varAppPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log";
            if (!Directory.Exists(varAppPath))
            {
                Directory.CreateDirectory(varAppPath);
            }
            string logPath = string.Format(@"{0}\\{1}.log", varAppPath,
                Toolpars.CustomerName == null || Toolpars.CustomerName.Equals(string.Empty)
                    ? DateTime.Now.ToString("yyyyMMddhhmmss")
                    : Toolpars.CustomerName);


            StringBuilder logStr = new StringBuilder();
            string headStr = string.Empty;
            for (int i = 0; i <= 80; i++)
            {
                headStr += "_";
            }
            logStr.AppendLine(headStr).AppendLine(string.Format("    # CREATEDATE   {0}",
                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")))
                .AppendLine(string.Format("    # CREATEBY  {0}", Environment.MachineName))
                .AppendLine(string.Format("    # TYPEKEY  {0}", txtNewTypeKey)).AppendLine();


            string head = DateTime.Now.ToString("hh:mm:ss:fff");
            foreach (var kv in pathDic)
            {
                foreach (var fileinfo in kv.Value)
                {
                    logStr.AppendLine(String.Format("    # {0} {1}", kv.Key, fileinfo.FunctionName));
                }
            }
            logStr.AppendLine(headStr);
            using (StreamWriter SW = new StreamWriter(logPath, true, Encoding.UTF8))
            {
                SW.WriteLine(logStr.ToString());
                SW.Flush();
                SW.Close();
            }
            ;
        }

        public static void writeToServer(toolpars Toolpars, Dictionary<string, List<FileInfos>> pathDic)
        {
            foreach (var kv in pathDic)
            {
                kv.Value.ForEach(fileinfo => {
                    // sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "Create" + Toolpars.formEntity.txtNewTypeKey);
                    string year = DateTime.Now.ToString("yyyyMMdd");
                    string demandId = string.Format("S01231_{0}_01", year);
                    sqlTools.insertToolInfo(demandId, year,
                        Toolpars.formEntity.txtNewTypeKey + "_" + fileinfo.FunctionName);
                });
            }
        }

        #endregion


    }
}

// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common.Implement.Entity;
using Common.Implement.UI;

namespace Common.Implement.Tools {
    public static class LogTool {
        #region 日志

        /// <summary>
        ///     日志
        /// </summary>
        public static void WriteLogByTreeView(Toolpars toolpars, MyTreeView treeView) {
            var pathDic = MyTool.GetTreeViewFilePath(treeView.Nodes, toolpars);
            var txtNewTypeKey = toolpars.FormEntity.txtNewTypeKey;
            var varAppPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log";
            if (!Directory.Exists(varAppPath))
                Directory.CreateDirectory(varAppPath);
            var logPath = $@"{varAppPath}\\{
                    (toolpars.CustomerName == null || toolpars.CustomerName.Equals(string.Empty)
                        ? DateTime.Now.ToString("yyyyMMddhhmmss")
                        : toolpars.CustomerName)
                }.log";


            var logStr = new StringBuilder();
            var headStr = string.Empty;
            for (var i = 0; i <= 80; i++)
                headStr += "_";
            logStr.AppendLine(headStr).AppendLine(
                    $"    # CREATEDATE   {DateTime.Now:yyyy-MM-dd hh:mm:ss:fff}")
                .AppendLine($"    # CREATEBY  {Environment.MachineName}")
                .AppendLine($"    # TYPEKEY  {txtNewTypeKey}").AppendLine();

            
            foreach (var kv in pathDic)
            {
                foreach (var fileinfo in kv.Value)
                logStr.AppendLine($"    # {kv.Key} {fileinfo.FileName}");
            }
            logStr.AppendLine(headStr);
            WriteToFile(logPath, logStr.ToString());
        }

        public static void WriteToFile(string path,string fileStr) {
            using (var sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                sw.WriteLine(fileStr);
                sw.Flush();
                sw.Close();
            }
        }

        public static void WriteToServer(Toolpars toolpars, IEnumerable<FileInfos> fileInfos) {
            SqlTools.InsertToolInfo(toolpars.FormEntity.txtNewTypeKey, fileInfos);
        }

        #endregion
    }
}
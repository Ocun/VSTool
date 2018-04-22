// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Digiwin.Chun.Common.Model;
using Digiwin.Chun.Common.Views;

namespace Digiwin.Chun.Common.Controller {
    /// <summary>
    /// 日志
    /// </summary>
    public static class LogTools {
        #region 日志

        /// <summary>
        ///     日志
        /// </summary>
        public static void WriteLogByTreeView(MyTreeView treeView) {
            var toolpars = MyTools.Toolpars;
            var pathDic = MyTools.GetTreeViewFilePath(treeView.Nodes);
            var txtNewTypeKey = toolpars.FormEntity.TxtNewTypeKey;
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

        /// <summary>
        /// 记录到文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileStr"></param>
        public static void WriteToFile(string path,string fileStr) {
            using (var sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                sw.WriteLine(fileStr);
                sw.Flush();
            }
        }

        /// <summary>
        /// 记录到Server
        /// </summary>
        /// <param name="fileInfos"></param>
        public static void WriteToServer(IEnumerable<FileInfos> fileInfos) {
            var toolpars = MyTools.Toolpars;
            SqlTools.InsertToolInfo(toolpars.FormEntity.TxtNewTypeKey, fileInfos);
        }

        #endregion
    }
}
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
        public static void WriteLog(Toolpars toolpars, MyTreeView treeView) {
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
            foreach (var fileinfo in kv.Value)
                logStr.AppendLine($"    # {kv.Key} {fileinfo.FileName}");
            logStr.AppendLine(headStr);
            using (var sw = new StreamWriter(logPath, true, Encoding.UTF8)) {
                sw.WriteLine(logStr.ToString());
                sw.Flush();
                sw.Close();
            }
        }

        public static void WriteToServer(Toolpars toolpars, IEnumerable<FileInfos> fileInfos) {
            SqlTools.InsertToolInfo(toolpars.FormEntity.txtNewTypeKey, fileInfos);
            //int count = 0;
            //foreach (var fileinfo in fileInfos)
            //{
            //    // sqlTools.insertToolInfo("S01231_20160503_01", "20160503", "Create" + Toolpars.formEntity.txtNewTypeKey);
            //    string year = DateTime.Now.ToString("yyyyMMdd");
            //    string demandId = string.Format("S01231_{0}_01", year);
            //    string useDate = DateTime.Now.AddMilliseconds(count++).ToString("yyyyMMddHHmmssfff");
            //    sqlTools.insertToolInfo(useDate, demandId, year,
            //        Toolpars.formEntity.txtNewTypeKey + "_" + fileinfo.FileName);
            //};
        }

        #endregion
    }
}
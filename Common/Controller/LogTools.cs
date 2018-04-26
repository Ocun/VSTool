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
        /// 获取log日志目录
        /// </summary>
        /// <returns></returns>
        public static string GetLogDir(string operationLog) {
            var toolpars = MyTools.Toolpars;
            var varAppPath = PathTools.PathCombine(toolpars.MvsToolpath, "log");
            if (!Directory.Exists(varAppPath))
                Directory.CreateDirectory(varAppPath);
            var logPath = $@"{varAppPath}\\{operationLog}.log";
            return logPath;
        }
        /// <summary>
        ///     日志
        /// </summary>
        public static void WriteLogByTreeView(MyTreeView treeView) {
            var toolpars = MyTools.Toolpars;
            var pathDic = MyTools.GetTreeViewFilePath(treeView.Nodes);
            var txtNewTypeKey = toolpars.FormEntity.TxtNewTypeKey;
            var operationLog = (toolpars.CustomerName == null || toolpars.CustomerName.Equals(string.Empty)
                ? DateTime.Now.ToString("yyyyMMddhhmmss")
                : toolpars.CustomerName);
            var logPath=GetLogDir(operationLog);

            var logStr = new StringBuilder();
            var headStr = string.Empty;
            for (var i = 0; i <= 80; i++)
                headStr += "_";
            const string empStr = @"      ";
            logStr.AppendLine(headStr).AppendLine(
                    $"{empStr}# CREATEDATE{empStr}{DateTime.Now:yyyy-MM-dd hh:mm:ss:fff}")
                .AppendLine($"{empStr}# CREATEBY{empStr}{Environment.MachineName}")
                .AppendLine($"{empStr}# TYPEKEY{empStr}{txtNewTypeKey}")
                .AppendLine($"{empStr}OperationTime{empStr}{DateTime.Now:yyyy-MM-dd hh:mm:ss:fff}")
                .AppendLine($"{empStr}OperationUser{empStr}{Environment.MachineName}")
                .AppendLine($"{empStr}WinUser{empStr}{Environment.UserName}")
                .AppendLine($"{empStr}WinverInfo{empStr}{Environment.OSVersion.VersionString}")
                .AppendLine($"{empStr}CurrentDomain{empStr}{Environment.UserDomainName}").AppendLine();
            
            foreach (var kv in pathDic)
            {
                foreach (var fileinfo in kv.Value)
                logStr.AppendLine($"{empStr}# {kv.Key} {empStr}{fileinfo.FileName}");
            }
            logStr.AppendLine(headStr);
            WriteToFile(logPath, logStr.ToString());
        }

        /// <summary>
        /// 记录到文件,已存在则追加
        /// </summary>
        /// <param name="path"></param>
        /// <param name="logMsg"></param>
        public static void WriteToFile(string path,string logMsg) {
            using (var sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                sw.WriteLine(logMsg);
                sw.Flush();
            }
        }

        /// <summary>
        /// 生成一段指定长度的字符
        /// </summary>
        /// <param name="targetStr"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetHeadStr(string targetStr,int len) {
            var headStr = string.Empty;
            for (var i = 0; i <= len; i++)
                headStr += targetStr;
            return headStr;
        }
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="msg"></param>
        public static void LogMsg(string msg) {
            var hardwareInfo =MyTools.HardwareInfo;
            var logPath = GetLogDir($@"error_{DateTime.Now:yyyyMMdd}");
        
            var logStr = new StringBuilder();
            var headStr = GetHeadStr(@"_",100);
            logStr.AppendLine(headStr);
            var empStr = "      ";
            logStr.AppendLine($"{empStr}#OperationTime#{empStr}{DateTime.Now:yyyy-MM-dd hh:mm:fff}")
            .AppendLine($"{empStr}#OperationUser#{empStr}{Environment.MachineName}")
            .AppendLine($"{empStr}#WinUser#{empStr}{Environment.UserName}")
            .AppendLine($"{empStr}#WinverInfo#{empStr}{Environment.OSVersion.VersionString}")
            .AppendLine($"{empStr}#CurrentDomain#{empStr}{Environment.UserDomainName}");

            if (hardwareInfo != null) {
                logStr.AppendLine($"{empStr}#CpuCount#{empStr}{hardwareInfo.CpuInfos.Count}");
                for (var i=0;i<  hardwareInfo.NetworkInfos.Count;i++) {
                    var netInfo = hardwareInfo.NetworkInfos[i];
                    logStr.AppendLine($"{empStr}#Description#{empStr}{netInfo.Description}");
                    logStr.AppendLine($"{empStr}#IpAddress{i}#{empStr}{netInfo.IpAddress}");
                    logStr.AppendLine($"{empStr}#MacAddress{i}#{empStr}{netInfo.MacAddress}");
                }
            
            }

            logStr.AppendLine($"{empStr}{msg}").AppendLine(headStr);


            WriteToFile(logPath, logStr.ToString());
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
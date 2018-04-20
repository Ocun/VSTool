﻿// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Common.Implement.Properties;

namespace Common.Implement.Tools {
    public class OldTools {
        /// <summary>
        ///     kill the process
        /// </summary>
        public static void KillProcess(string[] processNames) {
            var tPs = new List<Process>();
            foreach (var p in Process.GetProcesses())
                processNames.ToList().ForEach(processName => {
                    if (p.ProcessName.Contains(processName))
                        p.Kill();
                    else
                        tPs.Add(p);
                });
        }


        /// <summary>
        ///     刪除文件夾及其子項
        /// </summary>
        /// <param name="pArgs"></param>
        public static void DeleteAll(object pArgs) {
            var pFileName = pArgs.ToString();
            var di = new DirectoryInfo(pFileName);
            if (Directory.Exists(pFileName)) {
                foreach (var d in di.GetDirectories()) {
                    DeleteAll(d.FullName);
                    try {
                        d.Delete();
                    }
                    catch {
                        return;
                    }
                }
                foreach (var f in di.GetFiles())
                    DeleteAll(f.FullName);
            }
            else if (File.Exists(pFileName)) {
                //將唯讀權限拿掉
                File.SetAttributes(pFileName, FileAttributes.Normal);
                try {
                    File.Delete(pFileName);
                    Application.DoEvents();
                }
                catch {
                    // ignored
                }
            }
        }

        #region 复制

        /// <summary>
        ///     目录下的文件copy至另一目录
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="pDistFolder"></param>
        public static void CopyAllPkg(string pFileName, string pDistFolder) {
            if (Directory.Exists(pFileName)) {
                // Folder
                var di = new DirectoryInfo(pFileName);

                Directory.CreateDirectory(pDistFolder); // + "/" +di.Name);
                foreach (var d in di.GetDirectories()) {
                    //string tFolderPath = pDistFolder + "/" + d.Name;
                    var tFolderPath = pDistFolder + @"\" + d.Name;
                    if (!Directory.Exists(tFolderPath))
                        Directory.CreateDirectory(tFolderPath);
                    CopyAllPkg(d.FullName, tFolderPath);
                }
                foreach (var f in di.GetFiles())
                    CopyAllPkg(f.FullName, pDistFolder + @"\" + f.Name);
            }
            else if (File.Exists(pFileName)) {
                if (!Directory.Exists(pDistFolder.Remove(pDistFolder.LastIndexOf("\\", StringComparison.Ordinal))))
                    Directory.CreateDirectory(
                        pDistFolder.Remove(pDistFolder.LastIndexOf("\\", StringComparison.Ordinal)));
                if (File.Exists(pDistFolder))
                    File.SetAttributes(pDistFolder, FileAttributes.Normal);
                try {
                    File.Copy(pFileName, pDistFolder, true);
                }
                catch {
                    CopyFileAndRetry(pFileName, pDistFolder, true);
                }
                Application.DoEvents();
            }
        }

        public static void CopyFileAndRetry(string pFrom, string pTo, bool pOverWriteOrNot)
            //^_^20140521 add by sunny for 防網路順斷，暫停三秒後繼續作業，並重試三次
        {
            Thread.Sleep(3000);
            try {
                File.Copy(pFrom, pTo, pOverWriteOrNot);
            }
            catch {
                Thread.Sleep(3000);
                try {
                    File.Copy(pFrom, pTo, pOverWriteOrNot);
                }
                catch {
                    Thread.Sleep(3000);
                    try {
                        File.Copy(pFrom, pTo, pOverWriteOrNot);
                    }
                    catch {
                        MessageBox.Show(Resources.CopyFailed, Resources.ErrorMsg, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        #endregion
    }
}
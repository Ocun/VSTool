// create By 08628 20180411

using System;
using System.IO;
using System.Windows.Forms;

namespace Digiwin.Chun.Common.Controller {
    /// <summary>
    ///     旧版用到的一些方法
    /// </summary>
    public class OldTools {
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
                }
                catch {
                    // ignored
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="pDistFolder"></param>
        public static void CopynewVsTool(string pFileName, string pDistFolder) {
            if (Directory.Exists(pFileName)) {
                // Folder
                var di = new DirectoryInfo(pFileName);
                if (!Directory.Exists(pDistFolder))
                    Directory.CreateDirectory(pDistFolder);
                foreach (var d in di.GetDirectories()) {
                    var tFolderPath = pDistFolder + @"\" + d.Name;
                    if (!Directory.Exists(tFolderPath))
                        Directory.CreateDirectory(tFolderPath);
                    CopynewVsTool(d.FullName, tFolderPath);
                }
                foreach (var f in di.GetFiles())
                    CopynewVsTool(f.FullName, pDistFolder + @"\" + f.Name);
            }
            else if (File.Exists(pFileName)) {
                if (
                    !Directory.Exists(
                        pDistFolder.Remove(pDistFolder.LastIndexOf("\\", StringComparison.Ordinal))))
                    Directory.CreateDirectory(
                        pDistFolder.Remove(pDistFolder.LastIndexOf("\\", StringComparison.Ordinal)));
                if (File.Exists(pDistFolder))
                    File.SetAttributes(pDistFolder, FileAttributes.Normal);
                try {
                    if (pFileName.Substring(pFileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        != "VSTool.exe")
                        File.Copy(pFileName, pDistFolder, true);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Digiwin.Chun.Common.Tools.Properties;
using Digiwin.Chun.Models;
using MSWord = Microsoft.Office.Interop.Word;

namespace Digiwin.Chun.Common.Tools
{
    public static class CommonTools
    {
       

        /// <summary>
        /// 流形式copyFile，媒体文件亦可
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="tagerPath"></param>
        public static void CopyFile(string fromPath, string tagerPath)
        {
            //创建一个负责读取的流
            using (var fsRead = new FileStream(fromPath, FileMode.Open, FileAccess.Read))
            {
                //创建一个负责写入的流
                using (var fsWrite = new FileStream(tagerPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var buffer = new byte[1024 * 1024 * 5];

                    //因为文件可能比较大所以在读取的时候应该用循坏去读取
                    while (true)
                    {
                        //返回本次实际读取到的字节数
                        var r = fsRead.Read(buffer, 0, buffer.Length);

                        if (r == 0)
                        {
                            break;
                        }
                        fsWrite.Write(buffer, 0, r); //写入
                    }
                    fsWrite.Flush();
                }
            }
        }
        /// <summary>
        ///     递归获取指定文件夹内所有文件全路径
        /// </summary>
        /// <param name="dirpath"></param>
        /// <returns></returns>
        public static List<string> GetFilePath(string dirpath)
        {
            var filepathList = new List<string>();
            if (!Directory.Exists(dirpath))
                return filepathList;
            var dirinfo = new DirectoryInfo(dirpath);
            //递归目录
            var childDirList = dirinfo.GetDirectories();
            if (childDirList.Length > 0)
                childDirList.ToList().ForEach(a => {
                        var res = GetFilePath(a.FullName);
                        if (res.Count > 0)
                            filepathList.AddRange(res);
                    }
                );
            //文件
            var filepaths = dirinfo.GetFiles();
            filepaths.ToList().ForEach(a => filepathList.Add(a.FullName));
            return filepathList;
        }

        /// <summary>
        ///     指定文件文本替换
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fromName"></param>
        /// <param name="toName"></param>
        private static void ChangeText(string path, string fromName, string toName)
        {
            var text = File.ReadAllText(path);
            text = text.Replace(fromName, toName);
            File.SetAttributes(path, FileAttributes.Normal);
            File.Delete(path);
            File.WriteAllText(path, text, Encoding.UTF8);

            //using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            //{
            //    using (StreamReader sr = new StreamReader(fs))
            //    {
            //        con = sr.ReadToEnd();
            //        con = con.Replace(fromName, toName);
            //        sr.Close();

            //        FileStream fs2 = new FileStream(path, FileMode.Open, FileAccess.Write);
            //        StreamWriter sw = new StreamWriter(fs2, Encoding.UTF8);
            //        sw.WriteLine(con);
            //        sw.Flush();
            //        sw.Close();
            //        fs2.Close();

            //    }


            //}
        }

        /// <summary>
        /// 操作插入数据库
        /// </summary>
        /// <param name="operationName"></param>
        public static void InsertInfo(string operationName)
        {
            Task.Factory.StartNew(() => {
                Thread.CurrentThread.IsBackground = false;
                SqlTools.InsertToolInfo($"S01231_{DateTime.Now:yyyyMMdd}_01",
                    $"{DateTime.Now:yyyyMMdd}", operationName);
            });
        }
        /// <summary>
        ///     检查dll是否被占用
        /// </summary>
        /// <param name="processNames"></param>
        /// <returns></returns>
        public static bool CheckProcessRunning(string[] processNames)
        {
            var flag = false;
            var infos = Process.GetProcesses();
            foreach (var info in infos)
                if (processNames.Contains(info.ProcessName))
                    flag = true;
            return flag;
        }

        /// <summary>
        ///     kill the process
        /// </summary>
        public static void KillProcess(string[] processNames)
        {
            CommonTools.InsertInfo("BtnKillProcess");
            foreach (var p in Process.GetProcesses())
                processNames.ToList().ForEach(processName => {
                    if (p.ProcessName.Contains(processName))
                        p.Kill();
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void OpenExe(string path)
        {
            try
            {
                var p = new Process { StartInfo = { FileName = path } };

                p.Start();
            }
            catch (Exception ex)
            {
                LogTools.LogError($"Open{path} Error! Detail:{ex.Message}");
            }
        }

        /// <summary>
        ///     打开文件夹
        /// </summary>
        /// <param name="targetDir"></param>
        public static void OpenDir(string targetDir)
        {
            try
            {
                if (Directory.Exists(targetDir))
                    Process.Start(targetDir);
                else
                    MessageBox.Show(string.Format(Resources.DirNotExisted, targetDir), Resources.WarningMsg,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogTools.LogError($"OpenDir{targetDir} Error! Detail:{ex.Message}");
            }
        }


        /// <summary>
        ///     打开Word
        /// </summary>
        public static void OpenWord(string fileName)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var dirInfo = new DirectoryInfo(basePath);
            var matchFile = dirInfo.GetFiles(fileName, SearchOption.AllDirectories);
            if (matchFile.Any())
            {
                var path = matchFile[0].FullName;

                try
                {
                    var app = new MSWord.Application { Visible = true };
                    app.Documents.Open(path);
                }
                catch (Exception)
                {
                    MessageBox.Show(Resources.OpenDocError);
                }
            }
            else
            {
                MessageBox.Show(Resources.HelpDocNotExiested);
            }
        }
        /// <summary>
        /// 获取TYPEKEY目录
        /// </summary>
        /// <param name="path"></param>
        /// <param name="typeKey"></param>
        /// <returns></returns>
        public static string FindTypekeyDir(string path, string typeKey)
        {
            var dirPath = string.Empty;
            try
            {
                var batchObjectsDir = $@"{path}\BatchObjects";
                var businessObjectsDir = $@"{path}\BusinessObjects";
                var reportObjectsDir = $@"{path}\ReportObjects";
                var businessQueryObjectsDir = $@"{path}\BusinessQueryObjects";
                var dataModelDir = $@"{path}\DataModels";
                var searchList = new List<string> { batchObjectsDir, businessObjectsDir, reportObjectsDir, businessQueryObjectsDir, dataModelDir };

                foreach (var filterStr in searchList)
                {
                    if (!Directory.Exists(filterStr))
                        continue;
                    var directoryInfo = new DirectoryInfo(filterStr);
                    var filterDirs = directoryInfo.GetDirectories(typeKey, SearchOption.TopDirectoryOnly);
                    if (filterDirs.Length <= 0) continue;
                    dirPath = filterDirs[0].FullName;
                    break;
                }
            }
            catch (Exception ex)
            {
                LogTools.LogError($"FindTypeKeyDir Error! Detail:{ex.Message}");
            }

            return dirPath;
        }

        /// <summary>
        /// 根据功能包获取Digiwin.ERP.Typekey
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string GetPkgTypePathFormServer(string path)
        {
            var pkgTypePath = string.Empty;
            var filterPath = path.Split('.');
            if (filterPath.Length <= 3) return pkgTypePath;
            pkgTypePath = $@"{filterPath[0]}.{filterPath[1]}.{filterPath[2]}";
            return pkgTypePath;
        }
        /// <summary>
        ///     刪除文件夾及其子項
        /// </summary>
        /// <param name="pArgs"></param>
        public static void DeleteAll(object pArgs)
        {
            var pFileName = pArgs.ToString();
            var di = new DirectoryInfo(pFileName);
            if (Directory.Exists(pFileName))
            {
                foreach (var d in di.GetDirectories())
                {
                    DeleteAll(d.FullName);
                    try
                    {
                        d.Delete();
                    }
                    catch
                    {
                        return;
                    }
                }
                foreach (var f in di.GetFiles())
                    DeleteAll(f.FullName);
            }
            else if (File.Exists(pFileName))
            {
                //將唯讀權限拿掉
                File.SetAttributes(pFileName, FileAttributes.Normal);
                try
                {
                    File.Delete(pFileName);
                }
                catch
                {
                    // ignored
                }
            }
        }

        #region 开启客户端，服务端

        /// <summary>
        ///     打开服务器
        /// </summary>
        public static void ServerOn(string tServerPath, string args)
        {
            InsertInfo("BtnServerOn");
            if (!File.Exists(tServerPath))
            {
                MessageBox.Show(string.Format(Resources.NotFindFile, tServerPath));
                return;
            }
            var isOn = CheckProcessOn("Digiwin.Mars.ServerStart");
            if (isOn)
            {
                MessageBox.Show(Resources.ServerRunning);
                return;
            }
          
            Process.Start(tServerPath, args);
        }

        /// <summary>
        ///     打开服务器
        /// </summary>
        public static void ClientOn(string tClientPath ,string args)
        {
            InsertInfo("BtnClientOn");
            if (!CheckProcessOn("Digiwin.Mars.ServerStart"))
            {
                MessageBox.Show(Resources.ServerNotRunning);
                return;
            }
            if (CheckProcessOn("Digiwin.Mars.ClientStart"))
            {
                MessageBox.Show(Resources.ClientRunning);
                return;
            }
            if (!File.Exists(tClientPath))
            {
                MessageBox.Show(string.Format(Resources.NotFindFile, tClientPath));
                return;
            }
      
            Process.Start(tClientPath, args);
        }

        /// <summary>
        ///     检查进程是否启动
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool CheckProcessOn(string processName)
        {
            var tIsOpen = false;
            foreach (var p in Process.GetProcesses())
                if (p.ProcessName.Contains(processName))
                    tIsOpen = true;
            return tIsOpen;
        }

        #endregion
        #region 删除菜单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bts"></param>
        public static BuildeType[] FindBuilderTypeAndDelete(string id, BuildeType[] bts)
        {
            if (PathTools.IsNullOrEmpty(id) || bts == null) return bts;
            var btsList = bts.ToList();
            var findIndex = btsList.FirstOrDefault(bt => bt.Id.Equals(id));
            if (findIndex != null)
            {
                btsList.Remove(findIndex);
            }
            else
            {
                btsList.ForEach(bt => bt.BuildeItems = FindBuilderTypeAndDelete(id, bt.BuildeItems));

            }
            return btsList.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bts"></param>
        /// <returns></returns>
        public static BuildeType GetBuildeTypeById(string id, BuildeType[] bts)
        {
            if (PathTools.IsNullOrEmpty(id)
                || bts == null) return null;
            var btsList = bts.ToList();
            var resBt = btsList.FirstOrDefault(bt => bt.Id.Equals(id));
            if (resBt != null) return resBt;

            foreach (var bt in btsList)
            {
                if (bt.BuildeItems == null) continue;
                resBt = GetBuildeTypeById(id, bt.BuildeItems);
                if (resBt != null) break;
            }


            return resBt;
        }

     
        #endregion
    }
}
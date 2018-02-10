using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Common.Implement {
    public class Tools {
        public static DialogResult ShowMessage(string text) {
            return DialogResult.OK;
        }

        /// <summary>
        /// 日志
        /// </summary>
        public static void WriteLog(toolpars Toolpars, ListBox listDATA) {
            string txtNewTypeKey = Toolpars.formEntity.txtNewTypeKey;
            string LogStr = txtNewTypeKey + Environment.NewLine;
            string varAppPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log";
            Directory.CreateDirectory(varAppPath);
            string head = DateTime.Now.ToString("hh:mm:ss:fff");
            for (int s = 0; s < listDATA.Items.Count; s++) {
                LogStr += listDATA.Items[s].ToString() + "\r\n";
            }
            string logMessage = head + Environment.NewLine + LogStr + System.Environment.NewLine;
            string strDate = System.DateTime.Now.ToString("yyyyMMdd");

            string strFile = varAppPath + "\\" + strDate + ".log";
            using (StreamWriter SW = new StreamWriter(strFile, true)) {
                SW.WriteLine(logMessage);
                SW.Flush();
                SW.Close();
            }
            ;
        }


        public static void copyDll(toolpars Toolpars) {
            var PathEntity = Toolpars.PathEntity;
            if (PathEntity != null) {
                string serverPath = PathEntity.ServerProgramsPath;
                string clientPath = PathEntity.DeployProgramsPath;
                string businessDllFullPath = PathEntity.ExportPath + PathEntity.BusinessDllName;
                string ImplementDllFullPath = PathEntity.ExportPath + PathEntity.ImplementDllName;
                string UIDllFullPath = PathEntity.ExportPath + PathEntity.UIDllName;
                string UIImplementDllFullPath = PathEntity.ExportPath + PathEntity.UIImplementDllName;
                //Path.GetDirectoryName()
                string toPath = string.Empty;
                if (File.Exists(businessDllFullPath)) {
                    if (Directory.Exists(serverPath)) {
                        toPath = serverPath + PathEntity.BusinessDllName;
                        File.Copy(businessDllFullPath, toPath, true);
                    }
                    if (Directory.Exists(clientPath)) {
                        toPath = clientPath + PathEntity.BusinessDllName;
                        File.Copy(businessDllFullPath, toPath, true);
                    }
                }
                if (File.Exists(ImplementDllFullPath)) {
                    if (Directory.Exists(serverPath)) {
                        File.Copy(ImplementDllFullPath,
                            serverPath + PathEntity.ImplementDllName, true);
                    }
                }
                if (File.Exists(UIDllFullPath)) {
                    if (Directory.Exists(clientPath)) {
                        File.Copy(UIDllFullPath,
                            clientPath + PathEntity.UIDllName, true);
                    }
                }
                if (File.Exists(UIImplementDllFullPath)) {
                    if (Directory.Exists(clientPath)) {
                        File.Copy(UIImplementDllFullPath,
                            clientPath + PathEntity.UIImplementDllName, true);
                    }
                }
            }
        }

        public static void FileCopy(string fromFile) {
        }


        /// <summary>
        /// 將dll 考入平臺目錄
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        /// <param name="filterStr"></param>
        public static void FileCopyUIdll(string fromPath, string toPath, string filterStr) {
            try {
                string[] filedir = Directory.GetFiles(fromPath, filterStr,
                    SearchOption.AllDirectories);
                foreach (var mfile in filedir) {
                    File.Copy(mfile, mfile.Replace(fromPath, toPath), true);
                }
                MessageBox.Show("复制成功 !!!");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Toolpars"></param>
        public static void ModiName(toolpars Toolpars) {
            string txtNewTypeKey = Toolpars.formEntity.txtNewTypeKey;
            string DirectoryPath = string.Format(@"{0}\Digiwin.ERP.{1}\", Toolpars.GToIni, txtNewTypeKey);
            DirectoryInfo tDes = new DirectoryInfo(DirectoryPath);
            List<string> tSearchPatternList = new List<string>();
            tSearchPatternList.AddRange(new[] {
                "*xml",
                "*.sln",
                "*.repx",
                "*proj",
                "*.complete",
                "*.cs"
            });
            foreach (DirectoryInfo d in tDes.GetDirectories("*", SearchOption.AllDirectories)) {
                if (d.Name.IndexOf(txtNewTypeKey) == -1) {
                    if (d.Name.IndexOf(txtNewTypeKey.Substring(1)) != -1) {
                        if (
                            File.Exists(d.Parent.FullName + "\\" +
                                        d.Name.Replace(txtNewTypeKey.Substring(1), txtNewTypeKey))) {
                            File.SetAttributes(
                                d.Parent.FullName + "\\" +
                                d.Name.Replace(txtNewTypeKey.Substring(1), txtNewTypeKey),
                                FileAttributes.Normal);
                            File.Delete(d.Parent.FullName + "\\" +
                                        d.Name.Replace(txtNewTypeKey.Substring(1), txtNewTypeKey));
                        }
                        if (
                            Directory.Exists(d.Parent.FullName + "\\" +
                                             d.Name.Replace(txtNewTypeKey.Substring(1), txtNewTypeKey)) ==
                            false) {
                            d.MoveTo(d.Parent.FullName + "\\" +
                                     d.Name.Replace(txtNewTypeKey.Substring(1), txtNewTypeKey));
                        }
                        Application.DoEvents();
                    }
                }
            }


            foreach (System.IO.FileInfo f in tDes.GetFiles("*", SearchOption.AllDirectories)) {
                if (f.Name.IndexOf(txtNewTypeKey) == -1) {
                    if (f.Name.IndexOf(txtNewTypeKey.Substring(1)) != -1) {
                        if (File.Exists(f.FullName)) {
                            if (
                                File.Exists(f.Directory.FullName + "\\" +
                                            f.Name.Replace("Digiwin.ERP." + txtNewTypeKey.Substring(1),
                                                "Digiwin.ERP." + txtNewTypeKey)) == false) {
                                f.MoveTo(f.Directory.FullName + "\\" +
                                         f.Name.Replace("Digiwin.ERP." + txtNewTypeKey.Substring(1),
                                             "Digiwin.ERP." + txtNewTypeKey));
                            }
                            Application.DoEvents();
                        }
                    }
                }
            }


            for (int i = 0; i < tSearchPatternList.Count; i++) {
                foreach (System.IO.FileInfo f in tDes.GetFiles(tSearchPatternList[i], SearchOption.AllDirectories)) {
                    if (File.Exists(f.FullName)) {
                        string text = File.ReadAllText(f.FullName);
                        text = text.Replace("Digiwin.ERP." + txtNewTypeKey.Substring(1),
                            "Digiwin.ERP." + txtNewTypeKey);
                        text = text.Replace(@"<HintPath>..\..\", @"<HintPath>..\..\..\..\..\WD_PR\SRC\");
                        File.SetAttributes(f.FullName, FileAttributes.Normal);
                        File.Delete(f.FullName);
                        File.WriteAllText(f.FullName, text, System.Text.UTF8Encoding.UTF8);
                        Application.DoEvents();
                    }
                }
            }
        }

        /// <summary>
        /// kill the process
        /// </summary>
        public static void killProcess(string[] processNames) {
            List<Process> tPs = new List<Process>();
            foreach (Process p in Process.GetProcesses()) {
                processNames.ToList().ForEach(processName => {
                    if (p.ProcessName.Contains(processName)) {
                        p.Kill();
                    }
                    else {
                        tPs.Add(p);
                    }
                });
            }
        }

        #region  修改解决方案的csproj文件 添加类文件

        public static void ModiCS(string xmlPath, string CSName) {
            if (CSName.Contains(".designer.cs")) {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                XmlElement xe1 = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI); //创建一个<book>节点
                xe1.SetAttribute("Include", CSName); //设置该节点genre属性 
                var loc = xmlDoc.CreateElement("DependentUpon", xmlDoc.DocumentElement.NamespaceURI);
                String msname = CSName.Substring(CSName.LastIndexOf("\\") + 1);
                msname = msname.Substring(0, msname.IndexOf("."));
                msname = msname + ".cs";
                //loc.InnerText = model.loc;
                loc.InnerText = msname;
                xe1.AppendChild(loc);
                //xe1.InnerText = "WPF";
                root.AppendChild(xe1);
                xmlDoc.Save(xmlPath);
            }
            else if (CSName.Contains(".resx")) {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                XmlElement xe1 = xmlDoc.CreateElement("EmbeddedResource", xmlDoc.DocumentElement.NamespaceURI);
                //创建一个<book>节点
                xe1.SetAttribute("Include", CSName); //设置该节点genre属性 
                var loc = xmlDoc.CreateElement("DependentUpon", xmlDoc.DocumentElement.NamespaceURI);
                String msname = CSName.Substring(CSName.LastIndexOf("\\") + 1);
                msname = msname.Substring(0, msname.IndexOf("."));
                msname = msname + ".cs";
                //loc.InnerText = model.loc;
                loc.InnerText = msname;
                xe1.AppendChild(loc);
                //xe1.InnerText = "WPF";
                root.AppendChild(xe1);
                xmlDoc.Save(xmlPath);
            }

            else {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                XmlElement xe1 = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI); //创建一个<book>节点
                xe1.SetAttribute("Include", CSName); //设置该节点genre属性              
                root.AppendChild(xe1);
                xmlDoc.Save(xmlPath);
            }
        }

        #endregion

        #region 复制

        public static void CopyAllPKG(string pFileName, string pDistFolder) {
            if (Directory.Exists(pFileName)) {
                // Folder
                var di = new DirectoryInfo(pFileName);

                Directory.CreateDirectory(pDistFolder); // + "/" +di.Name);
                foreach (DirectoryInfo d in di.GetDirectories()) {
                    //string tFolderPath = pDistFolder + "/" + d.Name;
                    string tFolderPath = pDistFolder + @"\" + d.Name;
                    if (!Directory.Exists(tFolderPath)) {
                        Directory.CreateDirectory(tFolderPath);
                    }
                    CopyAllPKG(d.FullName, tFolderPath);
                }
                foreach (FileInfo f in di.GetFiles()) {
                    CopyAllPKG(f.FullName, pDistFolder + @"\" + f.Name);
                }
            }
            else if (File.Exists(pFileName)) {
                if (!Directory.Exists(pDistFolder.Remove(pDistFolder.LastIndexOf("\\")))) {
                    Directory.CreateDirectory(pDistFolder.Remove(pDistFolder.LastIndexOf("\\")));
                }
                if (File.Exists(pDistFolder)) {
                    File.SetAttributes(pDistFolder, FileAttributes.Normal);
                }
                try {
                    File.Copy(pFileName, pDistFolder, true);
                }
                catch {
                    CopyFileAndRetry(pFileName, pDistFolder, true);
                }
                Application.DoEvents();
            }
        }

        #region 复制CS文件

        public static void CopyFileCS(string mfileN, string mtos, string mfrom, toolpars Toolpars) {
            string txtNewTypeKey = Toolpars.formEntity.txtNewTypeKey;
            if (Directory.Exists(mfileN)) {
                if (!Directory.Exists(mtos.Substring(0, mtos.LastIndexOf("\\")))) {
                    Directory.CreateDirectory(mtos.Substring(0, mtos.LastIndexOf("\\")));
                }

                System.IO.File.Copy(mfrom, mtos, true);
            }
            else {
                string mresult = mfileN.Substring(mfileN.LastIndexOf("\\") + 1);
                mresult = mresult.Substring(mresult.IndexOf(".") + 1);
                mresult = mresult.Substring(mresult.IndexOf(".") + 1);
                mresult = mresult.Substring(mresult.IndexOf(".") + 1);
                DialogResult result = MessageBox.Show("路径" + mfileN + "\r\n目錄不存在，是否建立??", "提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes) {
                    CopyAll(Toolpars.MVSToolpath + "Digiwin.ERP.XTEST\\Digiwin.ERP.XTEST." + mresult, mfileN);

                    #region 修改文件名

                    DirectoryInfo tDes = new DirectoryInfo(mfileN + @"\");
                    List<string> tSearchPatternList = new List<string>();
                    tSearchPatternList.Add("*xml");
                    tSearchPatternList.Add("*.sln");
                    tSearchPatternList.Add("*.repx");
                    tSearchPatternList.Add("*proj");
                    tSearchPatternList.Add("*.complete");
                    tSearchPatternList.Add("*.cs");
                    foreach (System.IO.DirectoryInfo d in tDes.GetDirectories("*", SearchOption.AllDirectories)) {
                        if (d.Name.IndexOf(txtNewTypeKey) == -1) {
                            if (d.Name.IndexOf(Toolpars.OldTypekey) != -1) {
                                if (
                                    File.Exists(d.Parent.FullName + "\\" +
                                                d.Name.Replace(Toolpars.OldTypekey, txtNewTypeKey))) {
                                    File.SetAttributes(
                                        d.Parent.FullName + "\\" + d.Name.Replace(Toolpars.OldTypekey, txtNewTypeKey),
                                        FileAttributes.Normal);
                                    File.Delete(d.Parent.FullName + "\\" +
                                                d.Name.Replace(Toolpars.OldTypekey, txtNewTypeKey));
                                }
                                if (
                                    Directory.Exists(d.Parent.FullName + "\\" +
                                                     d.Name.Replace(Toolpars.OldTypekey, txtNewTypeKey)) == false) {
                                    d.MoveTo(d.Parent.FullName + "\\"
                                             + d.Name.Replace(Toolpars.OldTypekey, txtNewTypeKey));
                                }
                                Application.DoEvents();
                            }
                        }
                    }


                    foreach (FileInfo f in tDes.GetFiles("*", SearchOption.AllDirectories)) {
                        if (f.Name.IndexOf(txtNewTypeKey) == -1) {
                            if (f.Name.IndexOf(Toolpars.OldTypekey) != -1) {
                                if (File.Exists(f.FullName)) {
                                    if (
                                        File.Exists(f.Directory.FullName + "\\" +
                                                    f.Name.Replace(Toolpars.OldTypekey, txtNewTypeKey)) == false) {
                                        f.MoveTo(f.Directory.FullName + "\\" +
                                                 f.Name.Replace(Toolpars.OldTypekey, txtNewTypeKey));
                                    }
                                    Application.DoEvents();
                                }
                            }
                        }
                    }


                    for (int i = 0; i < tSearchPatternList.Count; i++) {
                        foreach (
                            FileInfo f in tDes.GetFiles(tSearchPatternList[i], SearchOption.AllDirectories)) {
                            if (File.Exists(f.FullName)) {
                                string text = File.ReadAllText(f.FullName);
                                text = text.Replace(Toolpars.OldTypekey, txtNewTypeKey);
                                File.SetAttributes(f.FullName, FileAttributes.Normal);
                                File.Delete(f.FullName);
                                File.WriteAllText(f.FullName, text, Encoding.UTF8);
                                //^_^ 20160802 by 05485 for 编码方式修改
                                Application.DoEvents();
                            }
                        }
                    }

                    #endregion

                    if (!Directory.Exists(mtos.Substring(0, mtos.LastIndexOf("\\")))) {
                        Directory.CreateDirectory(mtos.Substring(0, mtos.LastIndexOf("\\")));
                    }
                    File.Copy(mfrom, mtos, true);
                }
                else {
                    return;
                }
            }
        }

        #endregion

        public static void CopyAll(string pFileName, string pDistFolder) {
            if (Directory.Exists(pFileName)) {
                // Folder
                var di = new DirectoryInfo(pFileName);

                Directory.CreateDirectory(pDistFolder);
                foreach (DirectoryInfo d in di.GetDirectories()) {
                    if (d.Name != "InterFace"
                        && d.Name != "Implement"
                        && d.Name != "Interceptor"
                        && d.Name != "FunctionWindowOpener"
                        && d.Name != "MenuItem") {
                        string tFolderPath = pDistFolder + @"\" + d.Name;
                        if (!Directory.Exists(tFolderPath)) {
                            Directory.CreateDirectory(tFolderPath);
                        }
                        CopyAll(d.FullName, tFolderPath);
                    }
                }
                foreach (FileInfo f in di.GetFiles()) {
                    CopyAll(f.FullName, pDistFolder + @"\" + f.Name);
                }
            }
            else if (File.Exists(pFileName)) {
                string Myname = pFileName.Substring(0, pFileName.LastIndexOf("\\"));
                Myname = Myname.Substring(Myname.LastIndexOf("\\") + 1);
                if (Myname != "InterFace"
                    && Myname != "Implement"
                    && Myname != "Interceptor"
                    && Myname != "FunctionWindowOpener"
                    && Myname != "MenuItem") {
                    if (!Directory.Exists(pDistFolder.Remove(pDistFolder.LastIndexOf("\\")))) {
                        Directory.CreateDirectory(pDistFolder.Remove(pDistFolder.LastIndexOf("\\")));
                    }
                    if (File.Exists(pDistFolder)) {
                        File.SetAttributes(pDistFolder, FileAttributes.Normal);
                    }
                    try {
                        File.Copy(pFileName, pDistFolder, true);
                    }
                    catch {
                        CopyFileAndRetry(pFileName, pDistFolder, true);
                    }
                    Application.DoEvents();
                }
            }
        }

        public static void CopyFileAndRetry(string pFrom, string pTo, bool pOverWriteOrNot)
            //^_^20140521 add by sunny for 防網路順斷，暫停三秒後繼續作業，並重試三次
        {
            System.Threading.Thread.Sleep(3000);
            try {
                File.Copy(pFrom, pTo, pOverWriteOrNot);
            }
            catch {
                System.Threading.Thread.Sleep(3000);
                try {
                    File.Copy(pFrom, pTo, pOverWriteOrNot);
                }
                catch {
                    System.Threading.Thread.Sleep(3000);
                    try {
                        File.Copy(pFrom, pTo, pOverWriteOrNot);
                    }
                    catch {
                        MessageBox.Show("复制失败，请重试", "复制档案失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="pDistFolder"></param>
        public static void CopynewVSTool(string pFileName, string pDistFolder) {
            if (Directory.Exists(pFileName)) {
                // Folder
                DirectoryInfo di = new DirectoryInfo(pFileName);
                if (!Directory.Exists(pDistFolder)) {
                    Directory.CreateDirectory(pDistFolder);
                }
                foreach (DirectoryInfo d in di.GetDirectories()) {
                    string tFolderPath = pDistFolder + @"\" + d.Name;
                    if (!Directory.Exists(tFolderPath)) {
                        Directory.CreateDirectory(tFolderPath);
                    }
                    CopynewVSTool(d.FullName, tFolderPath);
                }
                foreach (FileInfo f in di.GetFiles()) {
                    CopynewVSTool(f.FullName, pDistFolder + @"\" + f.Name);
                }
            }
            else if (File.Exists(pFileName)) {
                if (
                    !Directory.Exists(
                        pDistFolder.Remove(pDistFolder.LastIndexOf("\\", System.StringComparison.Ordinal)))) {
                    Directory.CreateDirectory(
                        pDistFolder.Remove(pDistFolder.LastIndexOf("\\", System.StringComparison.Ordinal)));
                }
                if (File.Exists(pDistFolder)) {
                    File.SetAttributes(pDistFolder, FileAttributes.Normal);
                }
                try {
                    if (pFileName.Substring(pFileName.LastIndexOf("\\", System.StringComparison.Ordinal) + 1)
                        != "VSTool.exe") {
                        File.Copy(pFileName, pDistFolder, true);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.ToString());
                }
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 刪除文件夾及其子項
        /// </summary>
        /// <param name="pArgs"></param>
        public static void DeleteAll(object pArgs) {
            string pFileName = pArgs.ToString();
            var di = new DirectoryInfo(pFileName);
            if (Directory.Exists(pFileName)) {
                foreach (DirectoryInfo d in di.GetDirectories()) {
                    DeleteAll(d.FullName);
                    try {
                        d.Delete();
                    }
                    catch {
                        return;
                    }
                }
                foreach (FileInfo f in di.GetFiles()) {
                    DeleteAll(f.FullName);
                }
            }
            else if (File.Exists(pFileName)) {
                //將唯讀權限拿掉
                File.SetAttributes(pFileName, FileAttributes.Normal);
                try {
                    File.Delete(pFileName);
                    Application.DoEvents();
                }
                catch {
                    return;
                }
            }
        }
    }
}
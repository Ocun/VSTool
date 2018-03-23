using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text; 
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.UI;

namespace Common.Implement {
    public class paloTool {

        /// <summary>
        /// 把文件拷入指定的文件夹
        /// </summary>
        /// <param name="fromDir"></param>
        /// <param name="toDir"></param>
        /// <param name="filterName"></param>
        public static void copyTo(toolpars Toolpars, string fromDir, string toDir,List<string> fileNames,string fromTypeKey,string ToTypeKey) {
           

            var formFiles = GetFilePath(fromDir);
            foreach (var file in formFiles) {
                if (!File.Exists(file)) 
                    continue;
                FileInfo fileinfo = new FileInfo(file);
                string file_name = Path.GetFileNameWithoutExtension(file);
                if (!fileNames.Contains(file_name)) continue;
                string absolutedir = fileinfo.Directory.FullName.Replace(fromDir, string.Empty);
                string absolutePath = file.Replace(fromDir, string.Empty);
                string newFilePath = Path.Combine(toDir, absolutePath);
                string newFileDir = Path.Combine(toDir, absolutedir).Replace(fromTypeKey,ToTypeKey);
                if (!Directory.Exists(newFileDir)) {
                    Directory.CreateDirectory(newFileDir);
                }
                if (File.Exists(newFilePath)) {
                    if (MessageBox.Show("文件已存在，是否覆盖？", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        != DialogResult.OK) {
                        return;
                    }
                }
                fileinfo.CopyTo(newFilePath);
                changeText(newFilePath, fromTypeKey, ToTypeKey);
                FileInfo newfileinfo = new FileInfo(newFilePath);
                var dir = newfileinfo.Directory;

                //dir.Parent
                //Tools.ModiCS(mpaths + @"\" + StrYY + ".Business.Implement\\" + StrYY +
                //                            ".Business.Implement.csproj", "Interceptor\\DetailInterceptorS" + ra + ".cs");

            }
            ;
        }
        /// <summary>
        /// 获取项目配置路径
        /// </summary>
        /// <returns></returns>
        private static string getcsproj(DirectoryInfo dir) {
            //if(dir!=null)
            return null;
        }
        /// <summary>
        /// 递归获取指定文件夹内所有文件全路径
        /// </summary>
        /// <param name="dirpath"></param>
        /// <returns></returns>
        private static List<string> GetFilePath(string dirpath) {
            List<string> filepathList = new List<string>();
            if (Directory.Exists(dirpath)) {
                DirectoryInfo dirinfo = new DirectoryInfo(dirpath);
                DirectoryInfo[] childDirList = dirinfo.GetDirectories();
                if (childDirList.Length > 0) {
                    childDirList.ToList().ForEach(a => {
                        var res = GetFilePath(a.FullName);
                        if (res.Count > 0) {
                            filepathList.AddRange(res);
                        }
                    }
                        );
                }
                FileInfo[] filepaths = dirinfo.GetFiles();
                filepaths.ToList().ForEach(a => filepathList.Add(a.FullName));
            }


            return filepathList;
        }

       

        private static void changeText(string path, string fromName, string toName) {
            string con = "";
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                using (StreamReader sr = new StreamReader(fs)) {
                    con = sr.ReadToEnd();
                    con = con.Replace(fromName, toName);
                    sr.Close();
                    fs.Close();
                    FileStream fs2 = new FileStream(path, FileMode.Open, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs2);
                    sw.WriteLine(con);
                    sw.Close();
                    fs2.Close();
                }
            }
        }



       public static void createTree(MyTreeView mytree, List<BuildeType> BuildeEntity,bool showCheck)
        {
            mytree.Nodes.Clear();
            if (BuildeEntity != null
                && BuildeEntity.Count > 0)
            {
                var item = BuildeEntity.ToList();

                item.ForEach(BuildeType => { mytree.Nodes.Add(CreateTree(BuildeType, showCheck, false)); });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildeType"></param>
        /// <param name="type">是否读取子节点 </param>
        /// <returns></returns>
        public static TreeNode CreateTree(BuildeType buildeType,bool showCheck, bool readSubView)
        {
            string text = buildeType.Name ?? string.Empty;
            MyTreeNode new_child = new MyTreeNode(text);
            new_child.buildeType = buildeType;
            if (buildeType.BuildeItems == null
                || showCheck)
            {
                new_child.CheckBoxVisible = true;
            }
            //读下层目录
            else if (readSubView && buildeType.BuildeItems.Length > 0)
            {
                buildeType.BuildeItems.ToList().ForEach(BuildeItem => {
                    new_child.Nodes.Add(CreateTree(BuildeItem, showCheck,readSubView));
                });
            }
            return new_child;
        }

    }
}
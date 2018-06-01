using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Digiwin.Chun.Common.Tools;
using Digiwin.Chun.Models;
using Digiwin.Chun.Views.Properties;

namespace Digiwin.Chun.Views.Tools {
    /// <summary>
    ///     获取应用程序图标
    /// </summary>
    public static class IconTools {

        /// <summary>
        ///     获取文件类型的关联图标
        /// </summary>
        /// <param name="fileName">文件类型的扩展名或文件的绝对路径</param>
        /// <param name="isLargeIcon">是否返回大图标</param>
        /// <returns>获取到的图标</returns>
        public static Icon GetIcon(string fileName, bool isLargeIcon) {
            var test = IconHelp.GetJumboIcon(fileName, isLargeIcon, true);
            return test;
        }

        /// <summary>
        ///     获取bitmap
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Bitmap GetBitmap(string path) {
            return (Bitmap) Image.FromFile(path, false);
        }

        /// <summary>
        ///     初始化Tool图标
        /// </summary>
        public static void InitImageList() {
            var bts =MyTools.Toolpars.BuilderEntity.BuildeTypies;
            InitImageList(bts);
        }

        /// <summary>
        ///     初始化图标
        /// </summary>
        /// <param name="bts"></param>
        public static void InitImageList(BuildeType[] bts) {
            if (bts == null)
                return;
            foreach (var buildeType in bts.ToList()) {
                var isTools = buildeType.IsTools;
                var showIcon = buildeType.ShowIcon;
                var url = buildeType.Url;
                

                if (PathTools.IsTrue(isTools) 
                    && PathTools.IsTrue(showIcon)
                    &&!PathTools.IsNullOrEmpty(url)) {
                    var exeName = Path.GetFileNameWithoutExtension(url);
                    if (exeName != null && !MyTools.ImageList.Contains(exeName)) {
                        if (File.Exists(url)) {
                            SetExeIcon(url);
                        }
                        else {
                            MyTools.ImageList.Add(buildeType.Id,Resources.defautApp);
                        }
                    }
                }else if (PathTools.IsTrue(showIcon)) {
                    MyTools.ImageList.Add(buildeType.Id,Resources.defautApp);
                }
                InitImageList(buildeType.BuildeItems);
            }
        }

        #region 获取应用程序图标 （太小）原准备在treeView生成图标，

        /// <summary>
        ///     动态设置图标，从exe文件获取
        /// </summary>
        /// <param name="appPath"></param>
        public static void SetExeIcon(string appPath) {
            try {
                var appExtension = Path.GetExtension(appPath);
                string[] extensions = {".exe", "dll"};
                if (!extensions.Contains(appExtension))
                    return;
                var iconGet = GetIcon(appPath, false);
                var imageGet = iconGet.ToBitmap();
                var exeName = Path.GetFileNameWithoutExtension(appPath);
                if (exeName != null && !MyTools.ImageList.Contains(exeName))
                    MyTools.ImageList.Add(exeName, imageGet);
            }
            catch (Exception) {
                // ignored
            }
        }

        #endregion
    }
}
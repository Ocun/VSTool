using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using Digiwin.Chun.Common.Model;
using Digiwin.Chun.Common.Properties;
using Point = System.Drawing.Point;

namespace Digiwin.Chun.Common.Controller {
    /// <summary>
    ///     获取应用程序图标
    /// </summary>
    public class IconTools {
        /// <summary>
        ///     存储图标
        /// </summary>
        public static Hashtable ImageList { get; set; } = new Hashtable();


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
                    if (exeName != null && !ImageList.Contains(exeName))
                        SetExeIcon(url);
                }else if (PathTools.IsTrue(showIcon)) {
                    ImageList.Add(buildeType.Id,Resources.defautApp);
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
                if (exeName != null && !ImageList.Contains(exeName))
                    ImageList.Add(exeName, imageGet);
            }
            catch (Exception) {
                // ignored
            }
        }

        #endregion
    }


    /// <summary>
    ///     图标获取类
    /// </summary>
    public static class IconHelp {
        //private static BitmapSource bitmap_source_of_icon(Icon ic)
        //{
        //    var ic2 = Imaging.CreateBitmapSourceFromHIcon(ic.Handle,
        //        Int32Rect.Empty,
        //        BitmapSizeOptions.FromEmptyOptions());
        //    ic2.Freeze();
        //    return ic2;
        //}

        /// <summary>
        ///     获取系统图标
        /// </summary>
        /// <param name="small"></param>
        /// <param name="csidl"></param>
        /// <returns></returns>
        public static Icon SystemIcon(bool small, int csidl) {
            var pidlTrash = IntPtr.Zero;
            var hr = NativeMethods.SHGetSpecialFolderLocation(IntPtr.Zero, csidl, ref pidlTrash);
            Debug.Assert(hr == 0);

            var shinfo = new Shfileinfo();

            uint SHGFI_USEFILEATTRIBUTES = 0x000000010;

            // Get a handle to the large icon
            uint flags;
            uint SHGFI_PIDL = 0x000000008;
            if (!small)
                flags = SHGFI_PIDL | NativeMethods.ShgfiIcon | NativeMethods.ShgfiLargeicon | SHGFI_USEFILEATTRIBUTES;
            else
                flags = SHGFI_PIDL | NativeMethods.ShgfiIcon | NativeMethods.ShgfiSmallicon | SHGFI_USEFILEATTRIBUTES;

            var res = NativeMethods.SHGetFileInfo(pidlTrash, 0, ref shinfo, Marshal.SizeOf(shinfo), flags);
            Debug.Assert(res != 0);

            var myIcon = Icon.FromHandle(shinfo.HIcon);

            //Marshal.FreeCoTaskMem(pidlTrash);
            //var bs = bitmap_source_of_icon(myIcon);
            //myIcon.Dispose();
            //bs.Freeze(); // importantissimo se no fa memory leak
            //NativeMethods.DestroyIcon(shinfo.hIcon);
            //NativeMethods.CloseHandle(shinfo.hIcon);
            return myIcon;
        }

        /// <summary>
        ///     获得小或大
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="small"></param>
        /// <param name="checkDisk"></param>
        /// <param name="addOverlay"></param>
        /// <returns></returns>
        public static Icon GetSmall(string fileName, bool small, bool checkDisk, bool addOverlay) {
            var shinfo = new Shfileinfo();

            uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
            uint SHGFI_LINKOVERLAY = 0x000008000;

            uint flags;
            if (small)
                flags = NativeMethods.ShgfiIcon | NativeMethods.ShgfiSmallicon;
            else
                flags = NativeMethods.ShgfiIcon | NativeMethods.ShgfiLargeicon;
            if (!checkDisk)
                flags |= SHGFI_USEFILEATTRIBUTES;
            if (addOverlay)
                flags |= SHGFI_LINKOVERLAY;

            var res = NativeMethods.SHGetFileInfo(fileName, 0, ref shinfo, Marshal.SizeOf(shinfo), flags);
            if (res == 0)
                throw new FileNotFoundException();

            var myIcon = Icon.FromHandle(shinfo.HIcon);

            //var bs = bitmap_source_of_icon(myIcon);
            //myIcon.Dispose();
            //bs.Freeze(); // importantissimo se no fa memory leak
            //NativeMethods.DestroyIcon(shinfo.hIcon);
            // NativeMethods.CloseHandle(shinfo.hIcon);


            return myIcon;
        }

        /// <summary>
        ///     获得大或超大
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="jumbo"></param>
        /// <param name="checkDisk"></param>
        /// <returns></returns>
        public static Icon GetJumboIcon(string fileName, bool jumbo, bool checkDisk) {
            var shinfo = new Shfileinfo();

            uint shgfiUsefileattributes = 0x000000010;
            uint shgfiSysiconindex = 0x4000;

            var fileAttributeNormal = 0x80;

            var flags = shgfiSysiconindex;

            if (!checkDisk) // This does not seem to work. If I try it, a folder icon is always returned.
                flags |= shgfiUsefileattributes;

            var res = NativeMethods.SHGetFileInfo(fileName, fileAttributeNormal, ref shinfo, Marshal.SizeOf(shinfo),
                flags);
            if (res == 0)
                throw new FileNotFoundException();
            var iconIndex = shinfo.Icon;

            // Get the System IImageList object from the Shell:
            var iidImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");

            IImageList iml;
            var size = jumbo ? NativeMethods.ShilJumbo : NativeMethods.ShilExtralarge;
            // ReSharper disable once NotAccessedVariable
            var hres = NativeMethods.SHGetImageList(size, ref iidImageList, out iml); // writes iml
            //if (hres == 0)
            //{
            //    throw (new System.Exception("Error SHGetImageList"));
            //}

            var hIcon = IntPtr.Zero;
            var ildTransparent = 1;
            // ReSharper disable once RedundantAssignment
            hres = iml.GetIcon(iconIndex, ildTransparent, ref hIcon);
            //if (hres == 0)
            //{
            //    throw (new System.Exception("Error iml.GetIcon"));
            //}

            var myIcon = Icon.FromHandle(hIcon);
            //var bs = bitmap_source_of_icon(myIcon);
            //myIcon.Dispose();
            //bs.Freeze(); // very important to avoid memory leak
            //NativeMethods.DestroyIcon(hIcon);
            //NativeMethods.CloseHandle(hIcon);

            return myIcon;
            //return bs;
        }
    }

    internal static class NativeMethods {
        // Constants that we need in the function call

        public const int ShgfiIcon = 0x100;

        public const int ShgfiSmallicon = 0x1;

        public const int ShgfiLargeicon = 0x0;

        public const int ShilJumbo = 0x4;
        public const int ShilExtralarge = 0x2;

        [DllImport("Kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);

        /// SHGetImageList is not exported correctly in XP.  See KB316931
        /// http://support.microsoft.com/default.aspx?scid=kb;EN-US;Q316931
        /// Apparently (and hopefully) ordinal 727 isn't going to change.
        [DllImport("shell32.dll", EntryPoint = "#727")]
        public static extern int SHGetImageList(
            int iImageList,
            ref Guid riid,
            out IImageList ppv
        );

        // The signature of SHGetFileInfo (located in Shell32.dll)
        [DllImport("Shell32.dll")]
        public static extern int SHGetFileInfo(string pszPath, int dwFileAttributes, ref Shfileinfo psfi,
            int cbFileInfo, uint uFlags);

        [DllImport("Shell32.dll")]
        public static extern int SHGetFileInfo(IntPtr pszPath, uint dwFileAttributes, ref Shfileinfo psfi,
            int cbFileInfo, uint uFlags);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHGetSpecialFolderLocation(IntPtr hwndOwner, int nFolder,
            ref IntPtr ppidl);

        [DllImport("user32")]
        public static extern int DestroyIcon(IntPtr hIcon);

        public static int DestroyIcon2(IntPtr hIcon) {
            return DestroyIcon(hIcon);
        }
    }

    /// <summary>
    ///     句柄与图标信息
    /// </summary>
    public struct Shfileinfo {
        /// <summary>
        ///     Handle to the icon representing the file
        /// </summary>
        public IntPtr HIcon;

        /// <summary>
        ///     Index of the icon within the image list
        /// </summary>
        public int Icon;

        /// <summary>
        ///     Various attributes of the file
        /// </summary>
        public uint DwAttributes;

        /// <summary>
        ///     Path to the file
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string SzDisplayName;

        /// <summary>
        ///     File type
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string SzTypeName;
    }


    /// <summary>
    ///     获取图片信息
    /// </summary>
    public struct Imagelistdrawparams {
        /// <summary>
        ///     尺寸
        /// </summary>
        public int CbSize;

        /// <summary>
        ///     Himl
        /// </summary>
        public IntPtr Himl;

        /// <summary>
        ///     I
        /// </summary>
        public int I;

        /// <summary>
        ///     HdcDst
        /// </summary>
        public IntPtr HdcDst;

        /// <summary>
        ///     X
        /// </summary>
        public int X;

        /// <summary>
        ///     Y
        /// </summary>
        public int Y;

        /// <summary>
        ///     Cx
        /// </summary>
        public int Cx;

        /// <summary>
        ///     Cy
        /// </summary>
        public int Cy;

        /// <summary>
        ///     x offest from the upperleft of bitmap
        /// </summary>
        public int XBitmap;

        /// <summary>
        ///     y offset from the upperleft of bitmap
        /// </summary>
        public int YBitmap;

        /// <summary>
        ///     RgbBk
        /// </summary>
        public int RgbBk;

        /// <summary>
        ///     RgbFg
        /// </summary>
        public int RgbFg;

        /// <summary>
        ///     FStyle
        /// </summary>
        public int FStyle;

        /// <summary>
        ///     DwRop
        /// </summary>
        public int DwRop;

        /// <summary>
        ///     FState
        /// </summary>
        public int FState;

        /// <summary>
        ///     Frame
        /// </summary>
        public int Frame;

        /// <summary>
        ///     CrEffect
        /// </summary>
        public int CrEffect;
    }

    /// <summary>
    ///     获取图片信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Imageinfo {
        /// <summary>
        /// 
        /// </summary>
        public readonly IntPtr hbmImage;
        /// <summary>
        /// 
        /// </summary>
        public readonly IntPtr hbmMask;
        /// <summary>
        /// 
        /// </summary>
        public readonly int Unused1;
        /// <summary>
        /// 
        /// </summary>
        public readonly int Unused2;
        /// <summary>
        /// 
        /// </summary>
        public readonly Rect rcImage;
    }

    #region Private ImageList COM Interop (XP)

    /// <summary>
    ///     未知
    /// </summary>
    [ComImport]
    [Guid("46EB5926-582E-4017-9FDF-E8998DAA0950")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //helpstring("Image List"),
    public interface IImageList {
        /// <summary>
        /// </summary>
        /// <param name="hbmImage"></param>
        /// <param name="hbmMask"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [PreserveSig]
        int Add(
            IntPtr hbmImage,
            IntPtr hbmMask,
            ref int pi);

        /// <summary>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="hicon"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [PreserveSig]
        int ReplaceIcon(
            int i,
            IntPtr hicon,
            ref int pi);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iImage"></param>
        /// <param name="iOverlay"></param>
        /// <returns></returns>
        [PreserveSig]
        int SetOverlayImage(
            int iImage,
            int iOverlay);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="hbmImage"></param>
        /// <param name="hbmMask"></param>
        /// <returns></returns>
        [PreserveSig]
        int Replace(
            int i,
            IntPtr hbmImage,
            IntPtr hbmMask);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hbmImage"></param>
        /// <param name="crMask"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [PreserveSig]
        int AddMasked(
            IntPtr hbmImage,
            int crMask,
            ref int pi);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pimldp"></param>
        /// <returns></returns>
        [PreserveSig]
        int Draw(
            ref Imagelistdrawparams pimldp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [PreserveSig]
        int Remove(
            int i);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="flags"></param>
        /// <param name="picon"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetIcon(
            int i,
            int flags,
            ref IntPtr picon);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="pImageInfo"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetImageInfo(
            int i,
            ref Imageinfo pImageInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDst"></param>
        /// <param name="punkSrc"></param>
        /// <param name="iSrc"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [PreserveSig]
        int Copy(
            int iDst,
            IImageList punkSrc,
            int iSrc,
            int uFlags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="punk2"></param>
        /// <param name="i2"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        /// <returns></returns>
        [PreserveSig]
        int Merge(
            int i1,
            IImageList punk2,
            int i2,
            int dx,
            int dy,
            ref Guid riid,
            ref IntPtr ppv);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        /// <returns></returns>
        [PreserveSig]
        int Clone(
            ref Guid riid,
            ref IntPtr ppv);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="prc"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetImageRect(
            int i,
            ref Rect prc);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetIconSize(
            ref int cx,
            ref int cy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        [PreserveSig]
        int SetIconSize(
            int cx,
            int cy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetImageCount(
            ref int pi);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uNewCount"></param>
        /// <returns></returns>
        [PreserveSig]
        int SetImageCount(
            int uNewCount);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clrBk"></param>
        /// <param name="pclr"></param>
        /// <returns></returns>
        [PreserveSig]
        int SetBkColor(
            int clrBk,
            ref int pclr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pclr"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetBkColor(
            ref int pclr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTrack"></param>
        /// <param name="dxHotspot"></param>
        /// <param name="dyHotspot"></param>
        /// <returns></returns>
        [PreserveSig]
        int BeginDrag(
            int iTrack,
            int dxHotspot,
            int dyHotspot);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int EndDrag();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndLock"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [PreserveSig]
        int DragEnter(
            IntPtr hwndLock,
            int x,
            int y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndLock"></param>
        /// <returns></returns>
        [PreserveSig]
        int DragLeave(
            IntPtr hwndLock);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [PreserveSig]
        int DragMove(
            int x,
            int y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="punk"></param>
        /// <param name="iDrag"></param>
        /// <param name="dxHotspot"></param>
        /// <param name="dyHotspot"></param>
        /// <returns></returns>
        [PreserveSig]
        int SetDragCursorImage(
            ref IImageList punk,
            int iDrag,
            int dxHotspot,
            int dyHotspot);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fShow"></param>
        /// <returns></returns>
        [PreserveSig]
        int DragShowNolock(
            int fShow);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ppt"></param>
        /// <param name="pptHotspot"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetDragImage(
            ref Point ppt,
            ref Point pptHotspot,
            ref Guid riid,
            ref IntPtr ppv);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetItemFlags(
            int i,
            ref int dwFlags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOverlay"></param>
        /// <param name="piIndex"></param>
        /// <returns></returns>
        [PreserveSig]
        int GetOverlayImage(
            int iOverlay,
            ref int piIndex);
    }

    #endregion

    /// <summary>
    /// </summary>
    public struct MyPair {
        /// <summary>
        /// 
        /// </summary>
        public Icon Icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IntPtr IconHandleToDestroy { set; get; }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using Common.Implement.Entity;
using Point = System.Drawing.Point;

namespace Common.Implement.Tools {
    public class IconTool {
        private static Hashtable _imageList = new Hashtable();

        public static Hashtable ImageList {
            get => _imageList;
            set => _imageList = value;
        }


        /// <summary>
        ///     获取文件类型的关联图标
        /// </summary>
        /// <param name="fileName">文件类型的扩展名或文件的绝对路径</param>
        /// <param name="isLargeIcon">是否返回大图标</param>
        /// <returns>获取到的图标</returns>
        public static Icon GetIcon(string fileName, bool isLargeIcon) {
            var test = CIconOfPath.Icon_of_path_large(fileName, isLargeIcon, false);
            return test;
        }

        public static Bitmap GetBitmap(string path) {
            return (Bitmap) Image.FromFile(path, false);
        }

        /// <summary>
        /// 初始化Tool图标
        /// </summary>
        /// <param name="toolpars"></param>
        public static void InitImageList(Toolpars toolpars) {
            var bts = toolpars.BuilderEntity.BuildeTypies;
            SetImageList(bts);

        }

        public static void SetImageList(BuildeType[] bts) {
            if(bts == null) return;
            foreach (var buildeType in bts.ToList()) {
                var isTools = buildeType.IsTools;
                var showIcon = buildeType.ShowIcon;
                var url = buildeType.Url;
                if (isTools != null && showIcon != null
                    && isTools.Equals("True")
                    && showIcon.Equals("True")
                    && url != null
                    && !url.Trim().Equals(string.Empty))
                {
                    var exeName = Path.GetFileNameWithoutExtension(url);
                    if (!ImageList.Contains(exeName))
                    {
                        SetExeIcon(url);
                    }
                }
                SetImageList(buildeType.BuildeItems);
            }
         
        }

        #region 获取应用程序图标 （太小）原准备在treeView生成图标，

        public static void SetExeIcon(string appPath) {
            try {
                var appExtension = Path.GetExtension(appPath);
                string[] extensions = {".exe", "dll"};
                if (!extensions.Contains(appExtension))
                    return;
                var iconGet = GetIcon(appPath, true);
                var imageGet = iconGet.ToBitmap();
                var exeName = Path.GetFileNameWithoutExtension(appPath);
                if (exeName != null && !ImageList.Contains(exeName)) {
                    ImageList.Add(exeName, imageGet);
                }
                //var images = new List<Image> {imageGet};
                //foreach (var image in images)
                //    using (var fs =
                //        new FileStream($"{Application.StartupPath}\\Images\\{exeName}.png", FileMode.OpenOrCreate)) {
                //        image.Save(fs, ImageFormat.Png);
                //        image.Dispose();
                //    }
            }
            catch (Exception) {
                // ignored
            }
        }

        #endregion
    }


    public static class CIconOfPath {
        //private static BitmapSource bitmap_source_of_icon(Icon ic)
        //{
        //    var ic2 = Imaging.CreateBitmapSourceFromHIcon(ic.Handle,
        //        Int32Rect.Empty,
        //        BitmapSizeOptions.FromEmptyOptions());
        //    ic2.Freeze();
        //    return ic2;
        //}

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

            var myIcon = Icon.FromHandle(shinfo.hIcon);

            //Marshal.FreeCoTaskMem(pidlTrash);
            //var bs = bitmap_source_of_icon(myIcon);
            //myIcon.Dispose();
            //bs.Freeze(); // importantissimo se no fa memory leak
            //NativeMethods.DestroyIcon(shinfo.hIcon);
            //NativeMethods.CloseHandle(shinfo.hIcon);
            return myIcon;
        }

        public static Icon Icon_of_path(string fileName, bool small, bool checkDisk, bool addOverlay) {
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

            var myIcon = Icon.FromHandle(shinfo.hIcon);

            //var bs = bitmap_source_of_icon(myIcon);
            //myIcon.Dispose();
            //bs.Freeze(); // importantissimo se no fa memory leak
            //NativeMethods.DestroyIcon(shinfo.hIcon);
            // NativeMethods.CloseHandle(shinfo.hIcon);


            return myIcon;
        }

        public static Icon Icon_of_path_large(string FileName, bool jumbo, bool checkDisk) {
            var shinfo = new Shfileinfo();

            uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
            uint SHGFI_SYSICONINDEX = 0x4000;

            var FILE_ATTRIBUTE_NORMAL = 0x80;

            uint flags;
            flags = SHGFI_SYSICONINDEX;

            if (!checkDisk) // This does not seem to work. If I try it, a folder icon is always returned.
                flags |= SHGFI_USEFILEATTRIBUTES;

            var res = NativeMethods.SHGetFileInfo(FileName, FILE_ATTRIBUTE_NORMAL, ref shinfo, Marshal.SizeOf(shinfo),
                flags);
            if (res == 0)
                throw new FileNotFoundException();
            var iconIndex = shinfo.iIcon;

            // Get the System IImageList object from the Shell:
            var iidImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");

            IImageList iml;
            var size = jumbo ? NativeMethods.ShilJumbo : NativeMethods.ShilExtralarge;
            var hres = NativeMethods.SHGetImageList(size, ref iidImageList, out iml); // writes iml
            //if (hres == 0)
            //{
            //    throw (new System.Exception("Error SHGetImageList"));
            //}

            var hIcon = IntPtr.Zero;
            var ILD_TRANSPARENT = 1;
            hres = iml.GetIcon(iconIndex, ILD_TRANSPARENT, ref hIcon);
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

    public struct Shfileinfo {
        // Handle to the icon representing the file

        public IntPtr hIcon;

        // Index of the icon within the image list

        public int iIcon;

        // Various attributes of the file

        public uint dwAttributes;

        // Path to the file

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string szDisplayName;

        // File type

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szTypeName;
    }


    public struct Imagelistdrawparams {
        public int cbSize;
        public IntPtr himl;
        public int i;
        public IntPtr hdcDst;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public int xBitmap; // x offest from the upperleft of bitmap
        public int yBitmap; // y offset from the upperleft of bitmap
        public int rgbBk;
        public int rgbFg;
        public int fStyle;
        public int dwRop;
        public int fState;
        public int Frame;
        public int crEffect;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Imageinfo {
        public readonly IntPtr hbmImage;
        public readonly IntPtr hbmMask;
        public readonly int Unused1;
        public readonly int Unused2;
        public readonly Rect rcImage;
    }

    #region Private ImageList COM Interop (XP)

    [ComImport]
    [Guid("46EB5926-582E-4017-9FDF-E8998DAA0950")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //helpstring("Image List"),
    public interface IImageList {
        [PreserveSig]
        int Add(
            IntPtr hbmImage,
            IntPtr hbmMask,
            ref int pi);

        [PreserveSig]
        int ReplaceIcon(
            int i,
            IntPtr hicon,
            ref int pi);

        [PreserveSig]
        int SetOverlayImage(
            int iImage,
            int iOverlay);

        [PreserveSig]
        int Replace(
            int i,
            IntPtr hbmImage,
            IntPtr hbmMask);

        [PreserveSig]
        int AddMasked(
            IntPtr hbmImage,
            int crMask,
            ref int pi);

        [PreserveSig]
        int Draw(
            ref Imagelistdrawparams pimldp);

        [PreserveSig]
        int Remove(
            int i);

        [PreserveSig]
        int GetIcon(
            int i,
            int flags,
            ref IntPtr picon);

        [PreserveSig]
        int GetImageInfo(
            int i,
            ref Imageinfo pImageInfo);

        [PreserveSig]
        int Copy(
            int iDst,
            IImageList punkSrc,
            int iSrc,
            int uFlags);

        [PreserveSig]
        int Merge(
            int i1,
            IImageList punk2,
            int i2,
            int dx,
            int dy,
            ref Guid riid,
            ref IntPtr ppv);

        [PreserveSig]
        int Clone(
            ref Guid riid,
            ref IntPtr ppv);

        [PreserveSig]
        int GetImageRect(
            int i,
            ref Rect prc);

        [PreserveSig]
        int GetIconSize(
            ref int cx,
            ref int cy);

        [PreserveSig]
        int SetIconSize(
            int cx,
            int cy);

        [PreserveSig]
        int GetImageCount(
            ref int pi);

        [PreserveSig]
        int SetImageCount(
            int uNewCount);

        [PreserveSig]
        int SetBkColor(
            int clrBk,
            ref int pclr);

        [PreserveSig]
        int GetBkColor(
            ref int pclr);

        [PreserveSig]
        int BeginDrag(
            int iTrack,
            int dxHotspot,
            int dyHotspot);

        [PreserveSig]
        int EndDrag();

        [PreserveSig]
        int DragEnter(
            IntPtr hwndLock,
            int x,
            int y);

        [PreserveSig]
        int DragLeave(
            IntPtr hwndLock);

        [PreserveSig]
        int DragMove(
            int x,
            int y);

        [PreserveSig]
        int SetDragCursorImage(
            ref IImageList punk,
            int iDrag,
            int dxHotspot,
            int dyHotspot);

        [PreserveSig]
        int DragShowNolock(
            int fShow);

        [PreserveSig]
        int GetDragImage(
            ref Point ppt,
            ref Point pptHotspot,
            ref Guid riid,
            ref IntPtr ppv);

        [PreserveSig]
        int GetItemFlags(
            int i,
            ref int dwFlags);

        [PreserveSig]
        int GetOverlayImage(
            int iOverlay,
            ref int piIndex);
    }

    #endregion

    public struct MyPair {
        public Icon Icon { get; set; }
        public IntPtr IconHandleToDestroy { set; get; }
    }
}
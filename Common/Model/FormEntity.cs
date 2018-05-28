// create By 08628 20180411

using System;

namespace Digiwin.Chun.Common.Model {
    /// <summary>
    ///     界面參數
    /// </summary>
    public class FormEntity {

        /// <summary>
        ///     個案路徑 E:\E10_5.0.0\TEST
        /// </summary>
        public string ToPath { get; set; } = string.Empty;

        /// <summary>
        /// 不知何用，可能是标准？
        /// </summary>
        public bool IsPkg { get; set; }
        /// <summary>
        ///     個案源码路徑  E:\E10_5.0.0\TEST\WD_PR_C\SRC
        /// </summary>
        public string SrcToPath
        {
            get
            {
                if (string.IsNullOrEmpty(ToPath))
                {
                    return ToPath;
                }
                else if (IsPkg) {
                    return   $@"{ToPath}\WD_PR\SRC";
                }
                else {
                    return $@"{ToPath}\WD_PR_C\SRC";
                }
               
            }

        }
        /// <summary>
        ///     借用路徑 （客户目录）
        /// </summary>
        public string PkgPath { get; set; } = string.Empty;

        /// <summary>
        ///     借用源码路徑
        /// </summary>
        public string PkgSrcPath {
            get {
                if (string.IsNullOrEmpty(PkgPath))
                {
                    return PkgPath;
                }
                else
                {
                    return $@"{PkgPath}\WD_PR\SRC";
                }
            }
        } 

        /// <summary>
        ///     個案typekey
        /// </summary>
        public string TxtNewTypeKey { get; set; } = string.Empty;

        /// <summary>
        ///     行業包
        /// </summary>
        public bool Industry { get; set; }

        /// <summary>
        ///     修改状态
        /// </summary>
        public bool IsModi { get; set; } = false;

        /// <summary>
        ///     借用TypeKey
        /// </summary>
        public string PkgTypekey { get; set; } = string.Empty;

        /// <summary>
        /// 分割宽度，每间隔此宽度分割一列
        /// </summary>
        public int SpiltWidth { get; set; }

        /// <summary>
        /// 最大分割列数
        /// </summary>
        public int MaxSplitCount { get; set; }

        /// <summary>
        /// 是否出现编辑菜单
        /// </summary>
        public bool EditState { get; set; }
    }
}
// create By 08628 20180411

namespace Digiwin.Chun.Common.Model {
    /// <summary>
    ///     界面參數
    /// </summary>
    public class FormEntity {
        /// <summary>
        ///     個案路徑
        /// </summary>
        public string TxtToPath { get; set; } = string.Empty;

        /// <summary>
        ///     借用路徑
        /// </summary>
        public string TxtPkGpath { get; set; } = string.Empty;

        /// <summary>
        ///     個案typekey
        /// </summary>
        public string TxtNewTypeKey { get; set; } = string.Empty;

        /// <summary>
        ///     行業包
        /// </summary>
        public bool Industry { get; set; } = false;

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
    }
}
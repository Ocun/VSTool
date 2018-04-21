// create By 08628 20180411

namespace Common.Implement.Entity {
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
        ///     借用TypeKey
        /// </summary>
        public string PkgTypekey { get; set; } = string.Empty;
    }
}
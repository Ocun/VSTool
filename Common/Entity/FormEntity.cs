// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Implement.Entity
{
    /// <summary>
    /// 界面參數
    /// </summary>
    public class FormEntity {

        private string _txtToPathField = string.Empty;
        private string _txtPkGpathField = string.Empty;
       
        private string _txtNewTypeKeyField = string.Empty;
        private bool _industryField = false;

        private string _copyTypekeyField = string.Empty;

        /// <summary>
        /// 個案路徑
        /// </summary>
        public string TxtToPath {
            get => _txtToPathField;
            set => _txtToPathField = value;
        }
        /// <summary>
        /// 標準版路徑
        /// </summary>
        public string txtPKGpath {
            get => _txtPkGpathField;
            set => _txtPkGpathField = value;
        }
        /// <summary>
        /// 個案typekey
        /// </summary>
        public string txtNewTypeKey {
            get => _txtNewTypeKeyField;
            set => _txtNewTypeKeyField = value;
        }
        /// <summary>
        /// 行業包
        /// </summary>
        public bool Industry {
            get => _industryField;
            set => _industryField = value;
        }

        /// <summary>
        /// 要借用的TypeKey
        /// </summary>
        public string PkgTypekey {
            get => _copyTypekeyField;
            set => _copyTypekeyField = value;
        }
    }
}

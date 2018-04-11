// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Implement
{
    /// <summary>
    /// 界面參數
    /// </summary>
    public class FormEntity {

        private string txtToPathField = string.Empty;
        private string txtPKGpathField = string.Empty;
       
        private string txtNewTypeKeyField = string.Empty;
        private bool IndustryField = false;

        private string copyTypekeyField = string.Empty;

        /// <summary>
        /// 個案路徑
        /// </summary>
        public string txtToPath {
            get { return txtToPathField; }
            set { txtToPathField = value; }
        }
        /// <summary>
        /// 標準版路徑
        /// </summary>
        public string txtPKGpath {
            get { return txtPKGpathField; }
            set { txtPKGpathField = value; }
        }
        /// <summary>
        /// 個案typekey
        /// </summary>
        public string txtNewTypeKey {
            get { return txtNewTypeKeyField; }
            set { txtNewTypeKeyField = value; }
        }
        /// <summary>
        /// 行業包
        /// </summary>
        public bool Industry {
            get { return IndustryField; }
            set { IndustryField = value; }
        }

        /// <summary>
        /// 要借用的TypeKey
        /// </summary>
        public string PkgTypekey {
            get { return copyTypekeyField; }
            set { copyTypekeyField = value; }
        }
    }
}

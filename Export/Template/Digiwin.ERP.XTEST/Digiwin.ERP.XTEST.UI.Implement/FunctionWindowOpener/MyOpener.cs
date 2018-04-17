using System;
using System.Windows.Forms;
using Digiwin.Common.Advanced;
using Digiwin.Common.UI;

namespace Digiwin.ERP.XTEST.UI.Implement {
    internal sealed class _MyOpener_ : ISelectWindowDialogEx {
        private  IResourceServiceProvider _provider = null;
        private  string _typeKey = string.Empty;
        private SelectWindowContext _context = null;

        #region ISelectWindowDialogEx 成员

        public IResourceServiceProvider ResourceServiceProvider {
            get { return _provider; }
        }

        public string TypeKey {
            get { return _typeKey; }
        }

        #endregion

        #region ISelectWindowDialog 成员

        public void ConnectOpeningContext(string queryTypeKey, SelectWindowContext context) {
            throw new NotImplementedException();
        }

        public DialogResult ShowDialog() {
            throw new NotImplementedException();
        }

        #endregion
    }
}
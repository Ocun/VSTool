using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Digiwin.Common.Advanced;
using Digiwin.Common.Torridity;
using Digiwin.Common.UI;
using Digiwin.ERP.Common.Utils;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    internal sealed class FWOpener : ISelectWindowDialogEx
    {
        IResourceServiceProvider _provider = null;
        SelectWindowContext _context = null;
        string _typeKey = string.Empty;

        #region ISelectWindowDialogEx 成员

        public Digiwin.Common.Advanced.IResourceServiceProvider ResourceServiceProvider
        {
            get { return _provider; }
        }

        public string TypeKey
        {
            get { return _typeKey; }
        }

        #endregion


        #region ISelectWindowDialog 成员

        public void ConnectOpeningContext(string queryTypeKey, SelectWindowContext context)
        {
            throw new NotImplementedException();
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common;
using Digiwin.Common.Torridity;
using Digiwin.Common.UI;

// ReSharper disable once CheckNamespace
namespace Digiwin.ERP.XTEST.UI.Implement
{
    [EventInterceptorClass]
    class _ChangeInterceptor_ : ServiceComponent
    {
        [DataEntityChangedInterceptor(Path = "", DependencyItems = "", ActivePoints = new string[] { "" }, IsRunAtInitialized = false)]
        public void DataChanging(IDataEntityBase[] activeObjs, DataChangedCallbackResponseContext context)
        {

        }
    }
}

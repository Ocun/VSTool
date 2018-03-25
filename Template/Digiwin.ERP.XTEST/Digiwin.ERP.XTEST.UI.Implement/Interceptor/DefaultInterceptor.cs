using System.Linq;
using Digiwin.Common;
using Digiwin.Common.Torridity;
using Digiwin.Common.UI;
using Digiwin.ERP.Common.Utils;


namespace Digiwin.ERP.XTEST.UI.Implement
{

    [EventInterceptorClass]
    internal class DefaultInterceptor : ServiceComponent {
        [DataEntityChangedInterceptorAttribute(Path = "", DependencyItems = "", IsRunAtInitialized = false)]
        private void Default001(IDataEntityBase[] activeObjs, DataChangedCallbackResponseContext context) {
           
        }

       
    }
}

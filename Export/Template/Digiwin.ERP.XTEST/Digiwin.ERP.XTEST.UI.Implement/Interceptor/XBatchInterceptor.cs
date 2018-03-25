using Digiwin.Common;
using System.ComponentModel;
using Digiwin.Common.Torridity;
using Digiwin.Common.UI;
using Digiwin.Common.Services;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    [EventInterceptorClass]
    internal sealed class XBatchInterceptor : ServiceComponent
    {
        [EventInterceptor(typeof(IDocumentBatchServiceEvents), "InitializedParameter")]
        private void BeforeDeletingHintPrompt(object sender, DocumentBatchServiceEventArgs e)
        {
            
        }
    }
}

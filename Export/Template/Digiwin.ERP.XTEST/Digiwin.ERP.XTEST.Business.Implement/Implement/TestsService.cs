using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common;

namespace Digiwin.ERP.XTEST.Business.Implement
{
    [ServiceClass(typeof(_ITestsService_))]
	[SingleGetCreator]
    sealed class _TestsService_ : ServiceComponent, _ITestsService_
    {
    }
}

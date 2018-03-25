using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common;

namespace Digiwin.ERP.XTEST.Business.Implement
{
    [ServiceClass(typeof(ITestsService))]
	[SingleGetCreator]
    sealed class TestsService : ServiceComponent, ITestsService
    {
    }
}

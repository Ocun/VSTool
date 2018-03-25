using System;
using Digiwin.Common;
using Digiwin.Common.Services;
using System.ComponentModel;
using Digiwin.Common.Query2;
using Digiwin.Common.Core;
using System.Data;
using Digiwin.Common.Torridity;
using Digiwin.ERP.Common.Utils;
using Digiwin.ERP.Common.Business;

namespace Digiwin.ERP.XTEST.Business.Implement
{
    [SingleGetCreator]
    [ServiceClass(typeof(IBatchService))]    
  
    sealed class XBatchService : FreeBatchService<FreeBatchEventsArgs> {
        /// <summary>
        /// CreateFreeBatchEventsArgs
        /// </summary>
        /// <param name="args"></param>
        protected override FreeBatchEventsArgs CreateFreeBatchEventsArgs(BatchContext context) {
            return new FreeBatchEventsArgs(context);
        }

        /// <summary>
        /// 核心逻辑处理
        /// </summary>
        /// <param name="args"></param>
        protected override void DoProcess(FreeBatchEventsArgs args) {

        }


    }
}
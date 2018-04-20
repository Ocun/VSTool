
/*--------------------------------------------------------
 * createDate 20180409
 * createBy 08628
 * version 0.0.0.1
 * remark 注意 这些常用服务默认是调当前typekey下
 *--------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common;
using Digiwin.Common.Advanced;
using Digiwin.Common.UI;

namespace Digiwin.ERP.XTEST.UI.Implement.Tools
{
    public class UIServiceTools
    {
        #region MyRegion
        private ServiceCallContext _serviceCallContext;

        // ReSharper disable once MemberCanBePrivate.Global
        public IResourceServiceProvider Provider { get; set; }

        // ReSharper disable once MemberCanBePrivate.Global
        public ServiceCallContext CallContext
        {
            get { return _serviceCallContext; }
            set { _serviceCallContext = value; }
        }

      

        public UIServiceTools(IServiceComponentEvents component)
        {
            Provider = component.ResourceServiceProvider;
            CallContext = component.ServiceCallContext;
        }
        public UIServiceTools(IResourceServiceProvider provider, ServiceCallContext callContext)
        {
            Provider = provider;
            CallContext = callContext;
        }
        public T GetService<T>(string typeKey) where T : class
        {
            var ser = Provider.GetService(typeof(T), typeKey) as T;
            return ser;
        } 
        #endregion

        private IDocumentWindowCreateService _documentWindowCreateSrv;

        public IDocumentWindowCreateService DocumentWindowCreateSrv
        {
            get {
                return _documentWindowCreateSrv
                       ?? (_documentWindowCreateSrv = GetService<IDocumentWindowCreateService>(CallContext.TypeKey));
            }
        }
    }
}

/*--------------------------------------------------------
 * createDate 20180409
 * createBy 08628
 * version 0.0.0.1
 * remark 注意 这些常用服务默认是调当前typekey下
 *--------------------------------------------------------
 */

using Digiwin.Common;
using Digiwin.Common.Advanced;
using Digiwin.Common.Query2;
using Digiwin.Common.Services;
using Digiwin.ERP.Common.Business;

namespace Digiwin.ERP.XTEST.Business.Implement {
    public class ServiceTools {
        #region 容器

        private ServiceCallContext _serviceCallContext;

        public ServiceTools(IResourceServiceProvider resourceServiceProvider, ServiceCallContext serviceCallContext) {
            Provider = resourceServiceProvider;
            CallContext = serviceCallContext;
        }

        public ServiceTools(IServiceComponentEvents component) {
            Provider = component.ResourceServiceProvider;
            CallContext = component.ServiceCallContext;
        }
        // ReSharper disable once MemberCanBePrivate.Global
        public IResourceServiceProvider Provider { get; set; }

        // ReSharper disable once MemberCanBePrivate.Global
        public ServiceCallContext CallContext {
            get { return _serviceCallContext; }
            set { _serviceCallContext = value; }
        }

        public T GetService<T>(string typeKey) where T : class {
            var ser = Provider.GetService(typeof (T), typeKey) as T;
            return ser;
        }

        #endregion

        #region 基础属性

        private IAccountingPeriodService _accountingPeriodSrv;
        private IApproveStatusService _approveStatusSrv;
        private IBusinessTypeService _businessTypeSrv;
        private ICurrencyPrecisionService _currencyPrecisionSrv;
        private IDateTimeService _dateTimeSrv;
        private IInfoEncodeContainer _encodeSrv;
        private IEnhancedValidateService _enhancedvalidatorSrv;
        private IItemQtyConversionService _itemQtyConversionSrv;
        private IItemQuantityCalculateService _itemQuantityCalculateSrv;
        private ILogOnService _logOnSrv;
        private IMESEnableService _mesEnalbeSrv;
        private IPrimaryKeyService _primaryKeySrv;
        private IQueryService _querySrv;
        private ISysParameterService _sysParameterSrv;

        /// <summary>
        ///     登陆服务
        /// </summary>
        public ILogOnService LogOnSrv {
            get { return _logOnSrv ?? (_logOnSrv = GetService<ILogOnService>(null)); }
        }

        /// <summary>
        ///     系统时间服务
        /// </summary>
        public IDateTimeService DateTimeSrv {
            get { return _dateTimeSrv ?? (_dateTimeSrv = GetService<IDateTimeService>(null)); }
        }

        /// <summary>
        ///     信息编码服务
        /// </summary>
        public IInfoEncodeContainer InfoEncodeSrv {
            get { return _encodeSrv ?? (_encodeSrv = GetService<IInfoEncodeContainer>(null)); }
        }

        /// <summary>
        /// </summary>
        public IBusinessTypeService BusinessTypeSrv {
            get {
                return _businessTypeSrv ?? (_businessTypeSrv = GetService<IBusinessTypeService>(CallContext.TypeKey));
            }
        }

        /// <summary>
        ///     系统参数
        /// </summary>
        public ISysParameterService SysParameterSrv {
            get { return _sysParameterSrv ?? (_sysParameterSrv = GetService<ISysParameterService>(null)); }
        }

        /// <summary>
        ///     系统参数
        /// </summary>
        public IMESEnableService MesEnableSrv {
            get { return _mesEnalbeSrv ?? (_mesEnalbeSrv = GetService<IMESEnableService>(CallContext.TypeKey)); }
        }

        /// <summary>
        ///     OOQL sql执行服务
        /// </summary>
        public IQueryService QuerySrv {
            get { return _querySrv ?? (_querySrv = GetService<IQueryService>(null)); }
        }

        /// <summary>
        ///     主键服务
        /// </summary>
        public IPrimaryKeyService PrimaryKerSrv {
            get { return _primaryKeySrv ?? (_primaryKeySrv = GetService<IPrimaryKeyService>(CallContext.TypeKey)); }
        }

        /// <summary>
        ///     开放日期范围检查服务
        /// </summary>
        public IAccountingPeriodService AccountingPeriodSrv {
            get {
                return _accountingPeriodSrv
                       ?? (_accountingPeriodSrv = GetService<IAccountingPeriodService>(CallContext.TypeKey));
            }
        }

        /// <summary>
        ///     校验服务
        /// </summary>
        public IEnhancedValidateService EnhancedvalidatorSrv {
            get {
                return _enhancedvalidatorSrv ?? (_enhancedvalidatorSrv = GetService<IEnhancedValidateService>(null));
            }
        }

        /// <summary>
        ///     设置审核状态
        /// </summary>
        public IApproveStatusService ApproveStatusSrv {
            get {
                return _approveStatusSrv ?? (_approveStatusSrv = GetService<IApproveStatusService>(CallContext.TypeKey));
            }
        }

        /// <summary>
        ///     数量推算服务
        /// </summary>
        public IItemQuantityCalculateService ItemQuantityCalculateSrv {
            get {
                return _itemQuantityCalculateSrv
                       ?? (_itemQuantityCalculateSrv = GetService<IItemQuantityCalculateService>(CallContext.TypeKey));
            }
        }

        /// <summary>
        ///     数量推算服务
        /// </summary>
        public IItemQtyConversionService ItemQtyConversionSrv {
            get {
                return _itemQtyConversionSrv
                       ?? (_itemQtyConversionSrv = GetService<IItemQtyConversionService>(CallContext.TypeKey));
            }
        }

        /// <summary>
        ///     金额取位服务
        /// </summary>
        public ICurrencyPrecisionService CurrencyPrecisionSrv {
            get {
                return _currencyPrecisionSrv
                       ?? (_currencyPrecisionSrv = GetService<ICurrencyPrecisionService>(CallContext.TypeKey));
            }
        }

        #endregion
    }
}
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
using Digiwin.Common.Query2;
using Digiwin.Common.Services;
using Digiwin.ERP.Common.Business;

namespace Digiwin.ERP.XTEST.Business.Implement
{
    public class ServiceTools
    {
        #region 容器
        private IResourceServiceProvider _resourceServiceProvider;
        private ServiceCallContext _serviceCallContext;

        public IResourceServiceProvider Provider
        {
            get { return _resourceServiceProvider; }
            set { _resourceServiceProvider = value; }
        }

        public ServiceCallContext CallContext
        {
            get { return _serviceCallContext; }
            set { _serviceCallContext = value; }
        }

        public ServiceTools(IResourceServiceProvider ResourceServiceProvider, ServiceCallContext ServiceCallContext)
        {
            this.Provider = ResourceServiceProvider;
            this.CallContext = ServiceCallContext;
        }

        public ServiceTools(IServiceComponentEvents Component)
        {
            this.Provider = Component.ResourceServiceProvider;
            this.CallContext = Component.ServiceCallContext;
        }

        public T GetService<T>(string typeKey) where T : class
        {
            T ser = Provider.GetService(typeof(T), typeKey) as T;
            return ser;
        } 
        #endregion

        #region 基础属性
        private ILogOnService _logOnSrv = null;
        private IDateTimeService _dateTimeSrv;
        private IInfoEncodeContainer _encodeSrv;
        private IBusinessTypeService _businessTypeSrv;
        private ISysParameterService _sysParameterSrv;
        private IMESEnableService _mesEnalbeSrv;
        private IQueryService _querySrv;
        private IPrimaryKeyService _primaryKeySrv;
        private IAccountingPeriodService _accountingPeriodSrv;
        private IEnhancedValidateService _enhancedvalidatorSrv;
        private IApproveStatusService _approveStatusSrv;
        private IItemQuantityCalculateService _itemQuantityCalculateSrv;
        private IItemQtyConversionService _itemQtyConversionSrv;
        private ICurrencyPrecisionService _currencyPrecisionSrv; 

        /// <summary>
        /// 登陆服务
        /// </summary>
        public ILogOnService LogOnSrv
        {
            get
            {
                if (_logOnSrv == null)
                    _logOnSrv = this.GetService<ILogOnService>(null);
                return _logOnSrv;
            }
        }
        /// <summary>
        /// 系统时间服务
        /// </summary>
        public IDateTimeService DateTimeSrv
        {
            get
            {
                if (_dateTimeSrv == null)
                    _dateTimeSrv = this.GetService<IDateTimeService>(null);
                return _dateTimeSrv;
            }
        }

        /// <summary>
        /// 信息编码服务
        /// </summary>
        public IInfoEncodeContainer InfoEncodeSrv
        {
            get
            {
                if (_encodeSrv == null)
                    _encodeSrv = this.GetService<IInfoEncodeContainer>(null);

                return _encodeSrv;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IBusinessTypeService BusinessTypeSrv {
            get
            {
                if (_businessTypeSrv == null)
                    _businessTypeSrv = this.GetService<IBusinessTypeService>(CallContext.TypeKey);
                return _businessTypeSrv;
            }
        }
        /// <summary>
        /// 系统参数
        /// </summary>
        public ISysParameterService SysParameterSrv {
            get
            {
                if (_sysParameterSrv == null)
                    _sysParameterSrv = this.GetService<ISysParameterService>(null);
                return  _sysParameterSrv;
            }
        } 
        /// <summary>
        /// 系统参数
        /// </summary>
        public IMESEnableService MesEnableSrv{
          
            get
            {
                if (_mesEnalbeSrv == null)
                    _mesEnalbeSrv = this.GetService<IMESEnableService>(CallContext.TypeKey);;
                return  _mesEnalbeSrv;
            }
        }
        /// <summary>
        /// OOQL sql执行服务
        /// </summary>
        public IQueryService QuerySrv {
            get
            {
                if (_querySrv == null)
                    _querySrv = this.GetService<IQueryService>(null); ;
                return _querySrv; 
            }
        } 
        /// <summary>
        /// 主键服务
        /// </summary>
        public IPrimaryKeyService PrimaryKerSrv
        {
            get
            {
                if (_primaryKeySrv == null)
                    _primaryKeySrv = this.GetService<IPrimaryKeyService>(CallContext.TypeKey); ;
                return _primaryKeySrv; 
            }
        } 
        /// <summary>
        /// 开放日期范围检查服务
        /// </summary>
        public IAccountingPeriodService AccountingPeriodSrv
        {
            get
            {
                if (_accountingPeriodSrv == null)
                    _accountingPeriodSrv  = this.GetService<IAccountingPeriodService>(CallContext.TypeKey);
                 return _accountingPeriodSrv; 
            }
        }
        /// <summary>
        /// 校验服务
        /// </summary>
        public IEnhancedValidateService EnhancedvalidatorSrv
        {
            get
            {
                if (_enhancedvalidatorSrv == null)
                    _enhancedvalidatorSrv = this.GetService<IEnhancedValidateService>(null);
                return _enhancedvalidatorSrv; 
            }
        } 
        /// <summary>
        /// 设置审核状态
        /// </summary>
        public   IApproveStatusService ApproveStatusSrv
        {
            get
            {
                if (_approveStatusSrv == null)
                    _approveStatusSrv = this.GetService<IApproveStatusService>(CallContext.TypeKey);
                return _approveStatusSrv; 
            }
        }
        /// <summary>
        /// 数量推算服务
        /// </summary>
        public IItemQuantityCalculateService ItemQuantityCalculateSrv
        {
            get { 
                  if (_itemQuantityCalculateSrv == null)
                    _itemQuantityCalculateSrv = this.GetService<IItemQuantityCalculateService>(CallContext.TypeKey);
                return _itemQuantityCalculateSrv; }
        }
         /// <summary>
        /// 数量推算服务
        /// </summary>
        public IItemQtyConversionService ItemQtyConversionSrv
        {
            get
            {
                if (_itemQtyConversionSrv == null)
                    _itemQtyConversionSrv = this.GetService<IItemQtyConversionService>(CallContext.TypeKey);
                return _itemQtyConversionSrv;
            }
        }
        /// <summary>
        /// 金额取位服务
        /// </summary>
        public ICurrencyPrecisionService CurrencyPrecisionSrv
        {
            get
            {
                if (_currencyPrecisionSrv == null)
                    _currencyPrecisionSrv = this.GetService<ICurrencyPrecisionService>(CallContext.TypeKey);
                return _currencyPrecisionSrv;
            }
        }
        #endregion


    }
}

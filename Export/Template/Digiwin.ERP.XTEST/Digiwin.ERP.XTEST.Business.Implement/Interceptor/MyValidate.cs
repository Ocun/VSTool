using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common.Services;
using Digiwin.Common;

namespace Digiwin.ERP.XTEST.Business.Implement
{
    [ValidatorAttribute, Serializable]
    internal sealed class  MyValidate : ValidatorBase
    {
		/// <summary>
        /// 设置Id并指定时机点
        /// </summary>
        public MyValidate()
        {
            this.Id = "{}";
            this.Path = "";
            this.ActivePoints.Add(new ValidateActivePoint() {
                ActivePoint = ValidateActivePoint.ACTIVE_POINT_CANCELAVAILABLE });
        }
        /// <summary>
        /// 校验具体逻辑
        /// </summary>
        /// <param name="entities">当前实体集合</param>
        /// <param name="context">校验上下文</param>
        /// <returns>校验是否成立{校验成立则返回true}</returns>
        public override bool Validate(object[] entities, ValidateContext context) {
            throw new NotImplementedException();
		}

        public override void PreExecute(object[] entities, ValidateContext context)
        {
            base.PreExecute(entities, context);
        }
        public override void SetPropertyError(object entity, string propertyName, BusinessRuleException ex)
        {
            base.SetPropertyError(entity, propertyName, ex);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common.Services;
using Digiwin.Common;

namespace Digiwin.ERP.XTEST.Business.Implement
{
    [ValidatorAttribute, Serializable]
    internal sealed class testValidate : ValidatorBase
    {
		/// <summary>
        /// 设置Id并指定时机点
        /// </summary>
        public testValidate() {
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

        
    }
}

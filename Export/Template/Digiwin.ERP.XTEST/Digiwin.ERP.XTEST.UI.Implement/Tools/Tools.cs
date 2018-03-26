using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using Digiwin.Common;
using Digiwin.Common.Advanced;
using Digiwin.Common.Torridity;
using Digiwin.Common.UI;
using Digiwin.ERP.FormBaseExtensions.UI.Implement;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    public class Tools
    {
        public IResourceServiceProvider _Provider { get; set; }
    
        public ServiceCallContext _Context{ get; set; }


        public Tools(IResourceServiceProvider provider,ServiceCallContext serContext) {
            this._Provider = provider;
            this._Context = serContext;
        }

        public Tools(IServiceComponentEvents Component)
        {
            this._Provider = Component.ResourceServiceProvider;
            this._Context = Component.ServiceCallContext;
        }

        /// <summary>
        /// 调用发送消息
        /// </summary>
        /// <param name="contents"></param>
        public void sendMessage(string[] contents) {
            var documentWindowCreateService = _Provider.GetService(typeof(IDocumentWindowCreateService), "Sys_SendedMessage") as IDocumentWindowCreateService;
            if (documentWindowCreateService != null) {
                IDocumentWindow windowService = documentWindowCreateService.Create();

                StringBuilder content = new StringBuilder();
                contents.ToList().ForEach(str => content.Append(str));
                DependencyObject Sys_SendedMessage = windowService.EditController.EditorView.DataSource as DependencyObject;
                Sys_SendedMessage["Content"] = content.ToString();
            }
        }

        #region 设置控件可读
        /// <summary>
        /// 设置所有控件只读
        /// </summary>
        public void setComponentReadOnly(bool readOnly)
        {
            IFindControlService findSrv = this.GetService<IFindControlService>(null); ;
            var componets = findSrv.FindComponents();

            #region 将所有控件置为只读
            foreach (var component in componets)
            {
                if (component is DigiwinTextBox)
                    ((DigiwinTextBox)component).ReadOnly = readOnly;
                else if (component is GridColumn)
                {
                    ((GridColumn)component).OptionsColumn.AllowEdit = !readOnly;
                    //((GridColumn)component).OptionsColumn.ReadOnly = true;
                }
                else if (component is DigiwinDateTimePicker)
                    ((DigiwinDateTimePicker)component).ReadOnly = readOnly;
                else if (component is DigiwinSelectControl)
                    ((DigiwinSelectControl)component).ReadOnly = readOnly;
                else if (component is DigiwinCheckBox)
                    //((DigiwinSelectControl)component).Enabled = true;//20160929 mark by xaingdl for B001-160928077
                    ((DigiwinCheckBox)component).Enabled = !readOnly;//20160929 add by xaingdl for B001-160928077
                else if (component is DigiwinPickListLookUpEdit)
                    ((DigiwinPickListLookUpEdit)component).ReadOnly = readOnly;
            }
            #endregion
        }

        /// <summary>
        /// 设置某些控件可读或不可读
        /// </summary>
        /// <param name="paraControlName"></param>
        /// <param name="readOnly"></param>
        public void setComponentRead(string[] paraControlName, bool readOnly) {

            IFindControlService findSrv = this.GetService<IFindControlService>(null);;
            var componets = findSrv.FindComponents();
            Control control;
            paraControlName.ToList().ForEach(name =>
            {
                if (findSrv.TryGet(name, out control))
                {
                    control.Enabled = readOnly;
                    if (control is DigiwinTextBox)
                        ((DigiwinTextBox)control).ReadOnly = readOnly;
                    else if (control is DigiwinDateTimePicker)
                        ((DigiwinDateTimePicker)control).ReadOnly = readOnly;
                    else if (control is DigiwinSelectControl)
                        ((DigiwinSelectControl)control).ReadOnly = readOnly;
                    else if (control is DigiwinCheckBox)
                        //((DigiwinSelectControl)component).Enabled = true;//20160929 mark by xaingdl for B001-160928077
                        ((DigiwinCheckBox)control).Enabled = !readOnly;//20160929 add by xaingdl for B001-160928077
                    else if (control is DigiwinPickListLookUpEdit)
                        ((DigiwinPickListLookUpEdit)control).ReadOnly = readOnly;
                }
            });

        }

        public T GetService<T>(string typeKey) where T : class
        {
            T Xser = this._Provider.GetService(typeof(T), typeKey) as T;
            return Xser;
        } 
        #endregion
       
        /// <summary>
        /// 图片预览
        /// </summary>
        /// <param name="context"></param>
        /// <param name="typeKey"></param>
        /// <returns></returns>
        public DependencyObject ImagesPreviewWindowOpener(SelectWindowContext context ,string typeKey) 
        {
             //SelectWindowContext context = new SelectWindowContext(_entity, null, null);
             //ServiceComponent.Connection(context, this.ResourceServiceProvider, new ServiceCallContext(null, this.ServiceCallContext.TypeKey));
             //context.Parameters.Add(new Parameter("ProgramId", "SALES_ORDER_DOC.I01"));
             //context.Parameters.Add(new Parameter("SerialPicId", id));
             //context.Parameters.Add(new Parameter("TypeKey", "SALES_ORDER_DOC"));
             //context.Parameters.Add(new Parameter("Path", ""));
             //context.Parameters.Add(new Parameter("PicPropertyName", name));
             //context.Parameters.Add(new Parameter("PrimaryKey", "SALES_ORDER_DOC_ID"));
             //context.Parameters.Add(new Parameter("PrimaryKeyValue", _entity["SALES_ORDER_DOC_ID"]));
             //context.Parameters.Add(new Parameter("PicReturnType", "0"));      //20140718 edit by liyba for B001-140716019
             //context.Parameters.Add(new Parameter("IsSelfOpener", "Y"));
            DependencyObject doResult = null;
             using (Digiwin.ERP.ImagesPreviewWindowOpener.UI.Implement.ImagesPreviewWindowOpener imageFWO = new Digiwin.ERP.ImagesPreviewWindowOpener.UI.Implement.ImagesPreviewWindowOpener())
             {
                 imageFWO.ConnectOpeningContext(this._Context.TypeKey, context);
                 imageFWO.StartPosition = FormStartPosition.CenterScreen;
                 imageFWO.imageCount = 1;
                 if (imageFWO.ShowDialog() == DialogResult.OK)
                 {
                      doResult = context.SelectedObjects[0] as DependencyObject;
                 }
             }
            return doResult;

        }

      

        #region 配置开窗
        /// <summary>
        /// 创建开窗,这里仅用于了解开窗的配置过程,单控件、表单通用
        /// </summary>
        /// <param name="opernPars"></param>
        /// <param name="returnExpressions"></param>
        /// <returns></returns>
        public GeneralWindowOpener createOper(List<OpeningParameter> opernPars, List<ReturnExpression> returnExpressions, 
            string ReturnField, string QueryTypeKey,string tip)
        {
            var generalWindowOpener1 = new GeneralWindowOpener();
            opernPars.ForEach(pars => generalWindowOpener1.OpeningParameters.Add(pars));
            returnExpressions.ForEach(returnExpression => generalWindowOpener1.ReturnExpressions.Add(returnExpression));
            generalWindowOpener1.ReturnField = ReturnField;
            generalWindowOpener1.SearchEnabled = true;
            generalWindowOpener1.SearchQuery.QueryProjectId = null;
            generalWindowOpener1.SearchQuery.QueryTypeKey = null;
            generalWindowOpener1.SearchQuery.ReturnField = null;
            generalWindowOpener1.Shortcut = System.Windows.Forms.Keys.F2;
            generalWindowOpener1.RelatedType = Digiwin.Common.UI.RelatedType.Control;
            generalWindowOpener1.QueryTypeKey = QueryTypeKey;
            generalWindowOpener1.Tip = tip;
            return generalWindowOpener1;

        }

        /// <summary>
        /// 开窗参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public OpeningParameter createOperParameter(string name, string value)
        {
            var openingParameter1 = new Digiwin.Common.UI.OpeningParameter();
            openingParameter1.Name = name;
            openingParameter1.Value = value;
            return openingParameter1;
        }
        /// <summary>
        /// 返回值设定，可用表达式
        /// </summary>
        /// <param name="left"></param>
        /// <param name="name"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public ReturnExpression createReturnExpression(string left, string name, string right)
        {
            var ReturnExpression = new Digiwin.Common.UI.ReturnExpression();
            ReturnExpression.Left = left;
            ReturnExpression.Name = name;
            ReturnExpression.Right = right;
            return ReturnExpression;
        }  
        #endregion

        /// <summary>
        ///  1 加载 2保存  3删除 视图
        /// </summary>
        /// <param name="ELEMENT_TYPE"></param>
        public void OperationView(Form form, string ELEMENT_TYPE, int type)
        {
            try
            {
                if (type == 1 ) {

                    UiConfigurationUtils util = new UiConfigurationUtils(this._Provider, _Context.TypeKey);
                    util.LoadConfiguration(form, _Context.TypeKey, ELEMENT_TYPE);
                }else if (type == 2) {
                    UiConfigurationUtils util = new UiConfigurationUtils(this._Provider, _Context.TypeKey);
                    util.SaveConfiguration(form, _Context.TypeKey, ELEMENT_TYPE);
                    DigiwinMessageBox.ShowInfo("保存成功");
                }
                else if (type == 3) {
                    UiConfigurationUtils util = new UiConfigurationUtils(this._Provider, _Context.TypeKey);
                    util.ClearConfiguration(_Context.TypeKey, ELEMENT_TYPE);
                    DigiwinMessageBox.ShowInfo("删除成功");
                }
               
            }
            catch (BusinessRuleException) { }
        }

        #region 菜单按钮
        /// <summary>
        /// 设置菜单按钮可读
        /// </summary>
        public void MenuBtnView(string buttonName, bool readOnly)
        {
            ICommandsService cmdService = this.GetService<ICommandsService>(_Context.TypeKey);
            CommandBase cmd = cmdService.Commands[buttonName] as CommandBase;//禁用撤销审核按钮Command
            cmd.Enabled = readOnly;

        }
        /// <summary>
        /// 菜单按钮添加表决器
        /// </summary>
        public void MenuBtnView<T>(string buttonName) where T : CommandEnabledDecider
        {
            ICommandsService cmdService = this.GetService<ICommandsService>(_Context.TypeKey);
            CommandBase cmdConfirm = cmdService.Commands[buttonName] as CommandBase;//修改按钮
            cmdConfirm.EnabledDeciders.Add(cmdService.GetCommandEnabledDecider<T>()); //^

        } 
        #endregion

    }
}

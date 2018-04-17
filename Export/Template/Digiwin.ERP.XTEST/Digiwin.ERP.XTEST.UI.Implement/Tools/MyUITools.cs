/*--------------------------------------------------------
 * createDate 20180409
 * createBy 08628
 * version 0.0.0.1
 * remark 
 *          because of encode's problem,
 *          this description of the part create by english 
 *--------------------------------------------------------
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using Digiwin.Common;
using Digiwin.Common.Advanced;
using Digiwin.Common.Torridity;
using Digiwin.Common.UI;
using Digiwin.ERP.FormBaseExtensions.UI.Implement;

// ReSharper disable once CheckNamespace
namespace Digiwin.ERP.XTEST.UI.Implement
{
    // ReSharper disable once InconsistentNaming
    public class MyUITools
    {
        public MyUITools(IResourceServiceProvider provider, ServiceCallContext serContext)
        {
            Provider = provider;
            Context = serContext;
        }

        public MyUITools(IServiceComponentEvents component)
        {
            Provider = component.ResourceServiceProvider;
            Context = component.ServiceCallContext;
        }

        public IResourceServiceProvider Provider { get; set; }

        public ServiceCallContext Context { get; set; }

        /// <summary>
        ///     CallSendMessage
        /// </summary>
        /// <param name="contents"></param>
        public void SendMessage(IEnumerable<string> contents)
        {
            var documentWindowCreateService =
                Provider.GetService(typeof(IDocumentWindowCreateService), "Sys_SendedMessage") as
                    IDocumentWindowCreateService;
            if (documentWindowCreateService != null)
            {
                IDocumentWindow windowService = documentWindowCreateService.Create();

                var content = new StringBuilder();
                contents.ToList().ForEach(str => content.Append(str));
                var sysSendedMessage = windowService.EditController.EditorView.DataSource as DependencyObject;
                if (sysSendedMessage != null)
                {
                    sysSendedMessage["Content"] = content.ToString();
                }
            }
        }

        /// <summary>
        ///    PicPreview
        /// </summary>
        /// <param name="context"></param>
        /// <param name="typeKey"></param>
        /// <returns></returns>
        public DependencyObject ImagesPreviewWindowOpener(SelectWindowContext context, string typeKey)
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
            //context.Parameters.Add(new Parameter("PicReturnType", "0"));   
            //context.Parameters.Add(new Parameter("IsSelfOpener", "Y"));
            DependencyObject doResult = null;
            using (var imageFwo = new ImagesPreviewWindowOpener.UI.Implement.ImagesPreviewWindowOpener())
            {
                imageFwo.ConnectOpeningContext(Context.TypeKey, context);
                imageFwo.StartPosition = FormStartPosition.CenterScreen;
                imageFwo.imageCount = 1;
                if (imageFwo.ShowDialog() == DialogResult.OK)
                {
                    doResult = context.SelectedObjects[0] as DependencyObject;
                }
            }
            return doResult;
        }

        /// <summary>
        ///     1 Load 2 Save  3 Delete Views
        /// </summary>
        /// <param name="form"></param>
        /// <param name="elementType"></param>
        /// <param name="type"></param>
        public void OperationView(Form form, string elementType, int type)
        {
            try
            {
                if (type == 1)
                {
                    var util = new UiConfigurationUtils(Provider, Context.TypeKey);
                    util.LoadConfiguration(form, Context.TypeKey, elementType);
                }
                else if (type == 2)
                {
                    var util = new UiConfigurationUtils(Provider, Context.TypeKey);
                    util.SaveConfiguration(form, Context.TypeKey, elementType);
                    DigiwinMessageBox.ShowInfo("SaveSuccess");
                }
                else if (type == 3)
                {
                    var util = new UiConfigurationUtils(Provider, Context.TypeKey);
                    util.ClearConfiguration(Context.TypeKey, elementType);
                    DigiwinMessageBox.ShowInfo("DeleteSuccess");
                }
            }
            catch (BusinessRuleException)
            {
            }
        }

        #region MenuBtn

        /// <summary>
        ///     Set Menu Btn ReadOnly or Not
        /// </summary>
        public void MenuBtnView(string buttonName, bool readOnly)
        {
            var cmdService = GetService<ICommandsService>(Context.TypeKey);
            var cmd = cmdService.Commands[buttonName] as CommandBase; 
            if (cmd != null)
            {
                cmd.Enabled = readOnly;
            }
        }

        /// <summary>
        ///     Add Decider for Menu Btn 
        /// </summary>
        public void MenuBtnView<T>(string buttonName) where T : CommandEnabledDecider
        {
            var cmdService = GetService<ICommandsService>(Context.TypeKey);
            var cmdConfirm = cmdService.Commands[buttonName] as CommandBase;
            if (cmdConfirm != null)
            {
                cmdConfirm.EnabledDeciders.Add(cmdService.GetCommandEnabledDecider<T>()); 
            }
        }

        #endregion

        #region set windowOper

        /// <summary>
        ///    CreateWindowOper
        /// </summary>
        /// <param name="opernPars"></param>
        /// <param name="returnExpressions"></param>
        /// <param name="returnField"></param>
        /// <param name="queryTypeKey"></param>
        /// <param name="tip"></param>
        /// <returns></returns>
        public GeneralWindowOpener CreateOper(List<OpeningParameter> opernPars, List<ReturnExpression> returnExpressions,
            string returnField, string queryTypeKey, string tip)
        {
            var generalWindowOpener1 = new GeneralWindowOpener();
            opernPars.ForEach(pars => generalWindowOpener1.OpeningParameters.Add(pars));
            returnExpressions.ForEach(returnExpression => generalWindowOpener1.ReturnExpressions.Add(returnExpression));
            generalWindowOpener1.ReturnField = returnField;
            generalWindowOpener1.SearchEnabled = true;
            generalWindowOpener1.SearchQuery.QueryProjectId = null;
            generalWindowOpener1.SearchQuery.QueryTypeKey = null;
            generalWindowOpener1.SearchQuery.ReturnField = null;
            generalWindowOpener1.Shortcut = Keys.F2;
            generalWindowOpener1.RelatedType = RelatedType.Control;
            generalWindowOpener1.QueryTypeKey = queryTypeKey;
            generalWindowOpener1.Tip = tip;
            return generalWindowOpener1;
        }

        /// <summary>
        ///     OperParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public OpeningParameter CreateOperParameter(string name, string value)
        {
            var openingParameter1 = new OpeningParameter { Name = name, Value = value };
            return openingParameter1;
        }

        /// <summary>
        ///     ReturnExpression
        /// </summary>
        /// <param name="left"></param>
        /// <param name="name"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public ReturnExpression CreateReturnExpression(string left, string name, string right)
        {
            var returnExpression = new ReturnExpression { Left = left, Name = name, Right = right };
            return returnExpression;
        }

        #endregion

        #region Set Control ReadOnly or not

        /// <summary>
        ///     Set Control ReadOnly or not
        /// </summary>
        public void SetComponentReadOnly(bool readOnly)
        {
            var findSrv = GetService<IFindControlService>(null);
            ReadOnlyCollection<IComponent> componets = findSrv.FindComponents();

            #region Set All Control ReadOnly

            foreach (IComponent component in componets)
            {
                if (component is DigiwinTextBox)
                {
                    ((DigiwinTextBox)component).ReadOnly = readOnly;
                }
                else if (component is GridColumn)
                {
                    ((GridColumn)component).OptionsColumn.AllowEdit = !readOnly;
                    //((GridColumn)component).OptionsColumn.ReadOnly = true;
                }
                else if (component is DigiwinDateTimePicker)
                {
                    ((DigiwinDateTimePicker)component).ReadOnly = readOnly;
                }
                else if (component is DigiwinSelectControl)
                {
                    ((DigiwinSelectControl)component).ReadOnly = readOnly;
                }
                else if (component is DigiwinCheckBox)
                {
                    //((DigiwinSelectControl)component).Enabled = true;
                    ((DigiwinCheckBox)component).Enabled = !readOnly; 
                }
                else if (component is DigiwinPickListLookUpEdit)
                {
                    ((DigiwinPickListLookUpEdit)component).ReadOnly = readOnly;
                }
            }

            #endregion
        }

        /// <summary>
        ///     Set Some Control ReadOnly 
        /// </summary>
        /// <param name="paraControlName"></param>
        /// <param name="readOnly"></param>
        public void SetComponentRead(IEnumerable<string> paraControlName, bool readOnly)
        {
            var findSrv = GetService<IFindControlService>(null);
            paraControlName.ToList().ForEach(name =>
            {
                Control control;
                if (findSrv.TryGet(name, out control))
                {
                    control.Enabled = readOnly;
                    if (control is DigiwinTextBox)
                    {
                        ((DigiwinTextBox)control).ReadOnly = readOnly;
                    }
                    else if (control is DigiwinDateTimePicker)
                    {
                        ((DigiwinDateTimePicker)control).ReadOnly = readOnly;
                    }
                    else if (control is DigiwinSelectControl)
                    {
                        ((DigiwinSelectControl)control).ReadOnly = readOnly;
                    }
                    else if (control is DigiwinCheckBox)
                    {
                        //((DigiwinSelectControl)component).Enabled = true;
                        control.Enabled = !readOnly; 
                    }
                    else if (control is DigiwinPickListLookUpEdit)
                    {
                        ((DigiwinPickListLookUpEdit)control).ReadOnly = readOnly;
                    }
                }
            });
        }

        public T GetService<T>(string typeKey) where T : class
        {
            var xser = Provider.GetService(typeof(T), typeKey) as T;
            return xser;
        }

        #endregion
    }
}
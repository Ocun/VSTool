/*--------------------------------------------------------
 * createDate 20180409
 * createBy 08628
 * version 0.0.0.1
 * remark 保存/数据跟踪
 * ----ActionPoint----
 * 
 * 
 * -------------------
 *--------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common.UI;
using Digiwin.Common;
using Digiwin.Common.Torridity;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    [EventInterceptorClass]
    sealed class _Interceptor_ : ServiceComponent
    {
        #region __InsertHere__
        #region SaveInterceptor
        [EventInterceptor(typeof(IDocumentSaveServiceEvents), "Starting")]
        private void SaveTransactionDoc(object sender, DocumentSaveEventArgs args)
        {
            IWindowsService ws = this.GetService<IWindowsService>();  //获取控件
            DocumentWindow dw = ws.ActivateWindow as DocumentWindow;

            DependencyObject entity = dw.EditController.EditorView.DataSource as DependencyObject;  //取得单头
            DependencyObjectCollection entityD = entity[""] as DependencyObjectCollection;  //取得单身

            // IOrderedEnumerable<DependencyObject> entityD_Order = entityD.OrderByDescending(p => Maths.Decimal(p["SequenceNumber"]));  //按单身序号排序
          
            //string rtk = (entity["Owner_Org"] as DependencyObject)["RTK"].ToString();
            //object roid = (entity["Owner_Org"] as DependencyObject)["ROid"];

            //object getCustomerItmeId = Maths.GuidDefaultValue();



        }
          #endregion SaveInterceptor
        #region DataEntityChangedInterceptor
        [DataEntityChangedInterceptor(Path = "", DependencyItems = "", ActivePoints = new string[] { "" }, IsRunAtInitialized = false)]
        public void DataChanging(IDataEntityBase[] activeObjs, DataChangedCallbackResponseContext context)
        {
            //ICurrentDocumentWindow win = GetServiceForThisTypeKey<ICurrentDocumentWindow>();
            //EditState state = window.EditController.Document.EditState;
            //if (win.EditController.EditorView.Id != "BOM.I01")
            //{
            //    return;
            //}
            //IFindControlService findSer = GetService<IFindControlService>();
            //DependencyObject entity = (DependencyObject)win.EditController.EditorView.DataSource;

            DependencyObject obj = activeObjs[0] as DependencyObject;//当前记录

        }
        #endregion DataEntityChangedInterceptor
        #endregion __InsertHere__
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common.UI;
using Digiwin.Common.Torridity;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    class _MyCommand2_: EditControllerCommandBase
    {
        public _MyCommand2_()
            : base("_MyCommand2_")
        {
        }

        public override string ActionName
        {
            get
            {
                return "_MyCommand2_";
            }
        }

        protected override void InitializeServiceComponent()
        {
            base.InitializeServiceComponent();
           
        }

        public override void Execute()
        {
			ICurrentDocumentWindow window = this.GetServiceForThisTypeKey<ICurrentDocumentWindow>();
            DependencyObject entity = (DependencyObject)window.EditController.EditorView.DataSource;
			
        }

        
    }
}
    
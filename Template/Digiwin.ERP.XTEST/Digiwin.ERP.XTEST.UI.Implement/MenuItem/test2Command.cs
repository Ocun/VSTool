using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common.UI;
using Digiwin.Common.Torridity;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    class test2Command: EditControllerCommandBase
    {
        public test2Command()
            : base("test2Command")
        {
        }

        public override string ActionName
        {
            get
            {
                return "test2";
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
    
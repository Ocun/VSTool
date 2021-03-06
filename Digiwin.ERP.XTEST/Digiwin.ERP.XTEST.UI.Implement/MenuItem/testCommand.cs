﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digiwin.Common.UI;
using Digiwin.Common.Torridity;

namespace Digiwin.ERP.XTEST.UI.Implement
{
    class testCommand : EditControllerCommandBase
    {
        public testCommand()
            : base("testCommand")
        {
        }

        public override string ActionName
        {
            get
            {
                return "test";
            }
        }

        protected override void InitializeServiceComponent()
        {
            base.InitializeServiceComponent();
            var commandsService = GetServiceForThisTypeKey<ICommandsService>();
            EnabledDeciders.Add(commandsService.GetCommandEnabledDecider<EnableDecider>());
        }

        public override void Execute()
        {
			ICurrentDocumentWindow window = this.GetServiceForThisTypeKey<ICurrentDocumentWindow>();
            DependencyObject entity = (DependencyObject)window.EditController.EditorView.DataSource;
			
        }

        internal class EnableDecider : CommandEnabledDecider
        {
            public EnableDecider()
                : base(0, true)
            {
            }

            protected override bool QueryEnabled(IResourceServiceProvider provider, ServiceCallContext callContext, IDataObject context)
            {

                return false;
            }
        }
    }
}

using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.MessageService
{
    public class ControlMessageProcessor : IMessageProcessor
    {
        public ControlMessageProcessor(Control control)
        {
            Control = control;
        }

        private Control Control { get; set; }

        public void ProcessMessage(Message message)
        {
            Console.WriteLine("ModuleMessageProcessor ProcessMessage");

            var msg = message as IViewModelMessage;

            if (msg != null && Control != null)
                Control.SetViewModel(msg.Model);
        }
    }
}
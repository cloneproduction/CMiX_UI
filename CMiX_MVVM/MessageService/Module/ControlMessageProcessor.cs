using CMiX.MVVM.ViewModels;

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
            System.Console.WriteLine("ModuleMessageProcessor ProcessMessage");
            var msg = message as IViewModelMessage;

            if (msg != null)
                Control.SetViewModel(msg.Model);
        }
    }
}
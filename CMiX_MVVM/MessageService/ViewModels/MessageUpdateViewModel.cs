using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.MessageService
{
    public class MessageUpdateViewModel : Message, IViewModelMessage
    {
        public MessageUpdateViewModel()
        {

        }

        public MessageUpdateViewModel(IModel model)
        {
            Model = model;
        }

        public MessageUpdateViewModel(Control control)
        {
            Model = control.GetModel();
        }

        public IModel Model { get; set; }

        public override void Process<T>(T receiver)
        {
            Console.WriteLine("MessageUpdateViewModel ProcessMessage");
            var control = receiver as Control;
            control.IsReceiving = true;
            control.SetViewModel(Model);
            control.IsReceiving = false;
        }
    }
}
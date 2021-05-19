using CMiX.MVVM.Interfaces;
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

        public IModel Model { get; set; }
    }
}
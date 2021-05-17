using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.MessageService
{
    public class MessageUpdateViewModel : Message, IViewModelMessage
    {
        public MessageUpdateViewModel()
        {

        }

        public MessageUpdateViewModel(Guid id, IModel model)
        {
            Model = model;
            this.AddID(id);
        }

        public IModel Model { get; set; }
    }
}
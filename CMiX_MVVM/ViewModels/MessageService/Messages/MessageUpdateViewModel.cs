using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageUpdateViewModel : IViewModelMessage
    {
        public MessageUpdateViewModel()
        {

        }

        public MessageUpdateViewModel(Guid componentID, Guid id, IModel model)
        {
            ComponentID = componentID;
            Model = model;
            ID = id;
        }

        public IModel Model { get; set; }
        public Guid ID { get; set; }
        public Guid ComponentID { get; set; }

        public void Process(IMessageProcessor messageProcessor)
        {
            messageProcessor.SetViewModel(Model);
        }
    }
}

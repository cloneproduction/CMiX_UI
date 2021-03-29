using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageUpdateViewModel : IViewModelMessage
    {
        public MessageUpdateViewModel()
        {

        }

        public MessageUpdateViewModel(Guid moduleID, IModel model)
        {
            Model = model;
            ModuleID = moduleID;
        }

        public IModel Model { get; set; }
        public Guid ModuleID { get; set; }
        public Guid ComponentID { get; set; }

        public void Process(MessageReceiver messageReceiver)
        {
            IComponentMessageProcessor messageProcessor;
            if (messageReceiver.MessageProcessors.TryGetValue(ComponentID, out messageProcessor))
            {
                Component component = messageProcessor as Component;
                IMessageProcessor messageCommunicator;
                if(component.MessageDispatcher.Colleagues.TryGetValue(ModuleID, out messageCommunicator))
                {
                    messageCommunicator.SetViewModel(Model);
                }
            }
        }
    }
}

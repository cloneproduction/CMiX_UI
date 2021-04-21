using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Module : ViewModel, IMessageCommunicator
    {
        public Guid ID { get; set; }


        private IMessageSender ModuleMessageSender;
        public void SetSender(IMessageSender moduleMessageSender)
        {
            ModuleMessageSender = moduleMessageSender;
        }

        public void SendMessage()
        {
            if (ModuleMessageSender != null)
            {
                var message = new MessageUpdateViewModel(this.ID, this.GetModel());
                ModuleMessageSender.ProcessMessage(message);
            }
        }

        public virtual void SetReceiver(IMessageReceiver messageReceiver)
        {
            messageReceiver.RegisterReceiver(this);
        }

        public void ReceiveMessage(IMessage message)
        {
            var msg = message as MessageUpdateViewModel;
            if(msg != null)
                this.SetViewModel(msg.Model);
        }

        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
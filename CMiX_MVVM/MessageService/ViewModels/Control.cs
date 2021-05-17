using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.MessageService.Module;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Control : ViewModel
    {
        public Guid ID { get; set; }

        public ControlMessageEmitter MessageEmitter { get; set; }
        public ControlMessageProcessor MessageProcessor { get; set; }




        public virtual void SetSender(IMessageSender messageSender)
        {
            MessageEmitter = new ControlMessageEmitter();
            MessageEmitter.SetSender(messageSender);
        }

        public virtual void SetReceiver(MessageReceiver messageReceiver)
        {
            MessageProcessor = new ControlMessageProcessor(this, messageReceiver);
            messageReceiver.RegisterMessageProcessor(this.ID, MessageProcessor);
        }

        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
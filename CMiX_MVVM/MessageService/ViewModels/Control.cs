using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.MessageService.Module;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Control :  ViewModel, IIDobject
    {
        public Guid ID { get; set; }

        public ControlMessageEmitter MessageEmitter { get; set; }

        public virtual void SetSender(IMessageSender messageSender)
        {
            var sender = new MessageSender(this);
            sender.SetSender(messageSender);
            MessageEmitter = new ControlMessageEmitter(sender);
        }

        public virtual void SetReceiver(IMessageReceiver messageReceiver)
        {
            var messageProcessor = new ControlMessageProcessor(this);
            var receiver = new MessageReceiver(messageProcessor);
            messageReceiver.RegisterMessageReceiver(this.ID, receiver);
        }

        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
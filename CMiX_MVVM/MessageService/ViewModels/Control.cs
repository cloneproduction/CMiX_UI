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
        public IMessageProcessor MessageProcessor { get; set; }

        public virtual void SetSender(IMessageSender messageSender)
        {
            var sender = new MessageSender(this);
            sender.SetSender(messageSender);
            MessageEmitter = new ControlMessageEmitter(sender);
        }

        public virtual void SetReceiver(IMessageReceiver messageReceiver)
        {
            MessageProcessor = new ControlMessageProcessor(this);
            var receiver = new MessageReceiver(MessageProcessor);
            messageReceiver.RegisterMessageReceiver(receiver);
        }

        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
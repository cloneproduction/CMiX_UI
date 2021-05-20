using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.MessageService.Module;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Control :  ViewModel, IIDObject
    {
        public Guid ID { get; set; }
        public ControlMessageEmitter MessageEmitter { get; set; }
        public IMessageProcessor MessageProcessor { get; set; }
        public IMessageReceiver MessageReceiver { get; set; }


        public virtual void SetSender(IMessageSender messageSender)
        {
            var sender = new MessageSender(this);
            sender.SetSender(messageSender);
            MessageEmitter = new ControlMessageEmitter(sender);
        }

        public virtual void SetReceiver(IMessageReceiver messageReceiver)
        {
            var receiver = new MessageReceiver(this);
            messageReceiver.RegisterReceiver(receiver);
            MessageProcessor = new ControlMessageProcessor(this);
        }


        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
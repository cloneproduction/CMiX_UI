using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Control :  ViewModel, IIDObject
    {
        public Guid ID { get; set; }
        public IMessageSender MessageSender { get; set; }
        public IMessageProcessor MessageProcessor { get; set; }
        public IMessageReceiver MessageReceiver { get; set; }


        public virtual void SetSender(IMessageSender messageSender)
        {
            MessageSender = new MessageSender(this);
            MessageSender.SetSender(messageSender);
        }

        public virtual void SetReceiver(IMessageReceiver messageReceiver)
        {
            MessageProcessor = new ControlMessageProcessor(this);
            var receiver = new MessageReceiver(MessageProcessor);
            messageReceiver.RegisterReceiver(receiver);

        }


        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();

        public void ProcessMessage(Message message)
        {
            MessageProcessor.ProcessMessage(message);
        }
    }
}
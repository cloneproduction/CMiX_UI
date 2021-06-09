using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Control : ViewModel, IIDObject, IGetSetModel<IModel>
    {
        public Guid ID { get; set; }
        public IMessageSender MessageSender { get; set; }
        public IMessageReceiver MessageReceiver { get; set; }



        public virtual void SetReceiver(IMessageReceiver messageReceiver)
        {
            var messageProcessor = new ControlMessageProcessor(this);
            MessageReceiver = new MessageReceiver(messageProcessor);
            messageReceiver.RegisterReceiver(MessageReceiver);
        }

        public virtual void SetSender(IMessageSender messageSender)
        {
            MessageSender = new MessageSender(this);
            MessageSender.SetSender(messageSender);
        }

        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
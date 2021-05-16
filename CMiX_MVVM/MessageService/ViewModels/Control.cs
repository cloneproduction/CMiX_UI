using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Control : ViewModel
    {
        public Guid ID { get; set; }
        public ViewModelMessageFactory ViewModelMessageFactory { get; set; }


        public virtual void SetSender(ViewModelSender messageSender)
        {
            ViewModelMessageFactory = new ViewModelMessageFactory(messageSender);
        }

        public virtual void SetReceiver(MessageReceiver messageReceiver)
        {
            ViewModelMessageProcessor moduleMessageProcessor = new ViewModelMessageProcessor(this);
            messageReceiver.RegisterReceiver(this.ID, moduleMessageProcessor);
        }


        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
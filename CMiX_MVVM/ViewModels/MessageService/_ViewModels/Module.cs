using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Module : ViewModel
    {
        public Guid ID { get; set; }
        public ModuleMessageFactory ModuleMessageFactory  { get; set; }

        public virtual void SetSender(ModuleSender messageSender)
        {
            ModuleMessageFactory = new ModuleMessageFactory(messageSender);
        }

        public virtual void SetReceiver(MessageReceiver messageReceiver)
        {
            ModuleMessageProcessor moduleMessageProcessor = new ModuleMessageProcessor(this);
            messageReceiver.RegisterReceiver(this.ID, moduleMessageProcessor);
        }

        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
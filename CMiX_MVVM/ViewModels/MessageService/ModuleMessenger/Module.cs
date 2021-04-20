using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Module : ViewModel
    {
        private ModuleMessageSender ModuleMessageSender;
        public ModuleMessageSender SetSender(ModuleMessageSender moduleMessageSender)
        {
            ModuleMessageSender = moduleMessageSender;
            return moduleMessageSender;
        }

        public void SendViewModelUpdate()
        {
            if (ModuleMessageSender != null)
            {
                var message = new MessageUpdateViewModel(this.ID, this.GetModel());
                ModuleMessageSender.ProcessMessage(message);
            }
        }

        public virtual void SetReceiver(ModuleMessageReceiver moduleMessageReceiver)
        {
            moduleMessageReceiver.RegisterReceiver(this);
        }

        public void ReceiveViewModelUpdate(IModel model)
        {
            this.SetViewModel(model);
        }

        public Guid ID { get; set; }
        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Module : ViewModel
    {
        private ModuleMessageSender _nextHandler;
        public ModuleMessageSender SetNextSender(ModuleMessageSender handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public void SendViewModelUpdate()
        {
            if (_nextHandler != null)
            {
                var message = new MessageUpdateViewModel(this.ID, this.GetModel());
                _nextHandler.ProcessMessage(message);
            }
        }

        public virtual void SetModuleReceiver(ModuleMessageReceiver moduleMessageReceiver)
        {
            moduleMessageReceiver.RegisterMessageReceiver(this);
        }

        public Guid ID { get; set; }
        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
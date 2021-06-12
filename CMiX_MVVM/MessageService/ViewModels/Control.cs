using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.MessageService.ViewModels;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Control : ViewModel, IIDObject
    {
        public Guid ID { get; set; }
        public ICommunicator Communicator { get; set; }


        public virtual void SetCommunicator(ICommunicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public virtual void UnsetCommunicator(ICommunicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }

        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
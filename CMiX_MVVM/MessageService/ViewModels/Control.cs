using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.MessageService.ViewModels;
using System;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Control : ViewModel, IIDObject, IGetSetModel<IModel>
    {
        public Guid ID { get; set; }
        public IMessageProcessor MessageProcessor { get; set; }
        public ICommunicator Communicator { get; set; }


        public virtual void SetCommunicator(ICommunicator communicator)
        {
            MessageProcessor = new ControlMessageProcessor(this);
            Communicator = new ControlCommunicator(this);
            Communicator.SetNextCommunicator(communicator);
        }


        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();
    }
}
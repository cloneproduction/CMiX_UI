using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public interface IControl : IIDObject
    {
        void SetCommunicator(Communicator communicator);
        void UnsetCommunicator(Communicator communicator);

        void SetViewModel(IModel model);
        IModel GetModel();
    }
}
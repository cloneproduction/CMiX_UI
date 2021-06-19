using CMiX.Core.Interfaces;
using CMiX.Core.MessageService;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public interface IControl : IIDObject
    {
        void SetCommunicator(Communicator communicator);
        void UnsetCommunicator(Communicator communicator);

        void SetViewModel(IModel model);
        IModel GetModel();
    }
}
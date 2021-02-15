using System;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public interface ISenderTest
    {
        void SetViewModel(IModel model);
        IModel GetModel();
    }
}
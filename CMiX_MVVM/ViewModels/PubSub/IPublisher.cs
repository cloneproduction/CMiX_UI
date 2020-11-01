using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public interface IPublisher
    {
        string GetMessageAddress();

        event EventHandler<ModelEventArgs> SendChangeEvent;

        void OnSendChange(IModel model, string messageAddress);
        void OnChildPropertyToSendChange(object sender, ModelEventArgs e);
    }
}

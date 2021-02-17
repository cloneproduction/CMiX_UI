using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels.Mediator
{
    public interface IMessageProcessor
    {
        MessageDispatcher MessageDispatcher { get; set; }
        string GetAddress();
        void SetViewModel(IModel model);
        IModel GetModel();
    }
}
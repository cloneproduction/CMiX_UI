using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageUpdateViewModel : IViewModelMessage
    {
        public MessageUpdateViewModel()
        {

        }

        public MessageUpdateViewModel(string address, IModel model)
        {
            Model = model;
            Address = address;
        }

        public IModel Model { get; set; }
        public string Address { get; set; }

        public void Process(IMessageProcessor messageProcessor)
        {
            messageProcessor.SetViewModel(Model);
        }
    }
}

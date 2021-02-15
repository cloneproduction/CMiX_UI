using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageUpdateViewModel : IMessage
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
        public object Obj { get; set; }
        public string Address { get; set; }

        public void Process(ViewModel viewModel)
        {
            var slider = viewModel as ISenderTest;
            slider.SetViewModel(Model);
        }
    }
}

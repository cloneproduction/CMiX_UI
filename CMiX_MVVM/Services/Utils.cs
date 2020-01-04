using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.Services
{
    public static class Utils
    {
        public static void UpdateViewModel(Receiver Receiver, string messageAddress, ICopyPasteModel viewModel)
        {
            if (messageAddress == Receiver.ReceivedAddress 
                && Receiver.ReceivedData != null
                && MessageCommand.VIEWMODEL_UPDATE == Receiver.ReceivedCommand)
                    viewModel.PasteModel(Receiver.ReceivedData as IModel);
        }
    }
}
using CMiX.MVVM.Commands;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public interface ISendable
    {
        string MessageAddress { get; set; }
        ObservableCollection<ServerValidation> ServerValidation { get; set; }

        void SendMessages(string topic, MessageCommand command, object parameter, object payload);

        void SendMessagesWithoutValidation(string topic, MessageCommand command, object parameter, object payload);

        void DisabledMessages();

        void EnabledMessages();
    }
}

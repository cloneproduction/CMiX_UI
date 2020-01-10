using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.Services
{
    public class MessageService : ViewModel
    {
        public MessageService(ObservableCollection<Server> servers)
        {
            //Servers = servers;
        }

        public ObservableCollection<MessageValidation> MessageValidations{ get; set; }

        public void SendMessages(string topic, MessageCommand command, object parameter, object payload)
        {
            if (Enabled)
            {
                foreach (var messageValidation in MessageValidations)
                {
                    messageValidation.SendMessage(topic, command, parameter, payload);
                }
            }
        }

        public void Disable()
        {
            this.Enabled = false;
        }

        public void Enable()
        {
            this.Enabled = true;
        }
    }
}
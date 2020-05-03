using CMiX.MVVM.Commands;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public class MessageService : ViewModel
    {
        public MessageService()
        {
           // MessageValidations = new ObservableCollection<MessageValidation>();
            Enabled = true;
        }


        public void SendMessages(string topic, MessageCommand command, object parameter, object payload)
        {
            
            if (Enabled)
            {
                System.Console.WriteLine("SendMessage from MessageService " + topic);
                //foreach (var messageValidation in MessageValidations)
                //{
                //    messageValidation.SendMessage(topic, command, parameter, payload);
                //}
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
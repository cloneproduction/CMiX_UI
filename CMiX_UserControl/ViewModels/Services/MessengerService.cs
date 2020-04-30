using CMiX.MVVM.Commands;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class MessengerService
    {
        public MessengerService()
        {
            Messengers = new ObservableCollection<Messenger>();
            Enabled = true;
        }

        public bool Enabled { get; set; }
        public ObservableCollection<Messenger> Messengers { get; set; }

        public void SendMessages(string topic, MessageCommand command, object parameter, object payload)
        {
            if (Enabled)
            {
                foreach (var messenger in Messengers)
                {
                    messenger.SendMessage(topic);
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
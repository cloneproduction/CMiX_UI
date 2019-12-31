using CMiX.MVVM.Commands;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class MessageValidation : ViewModel
    {
        public MessageValidation(Server server)
        {
            Enabled = true;
            Server = server;
        }

        private Server _server;
        public Server Server
        {
            get => _server;
            set => SetAndNotify(ref _server, value);
        }

        #region MESSENGERS
        public void SendMessage(string topic, MessageCommand command, object parameter, object payload)
        {
            if (this.Enabled)
            {
                Server.Send(topic, command, parameter, payload);
                Console.WriteLine("ServerValidation Sent with topic " + topic);
            }
        }

        public void SendMessageWithoutValidation(string topic, MessageCommand command, object parameter, object payload)
        {
            Server.Send(topic, command, parameter, payload);
        }

        public void DisableMessageValidation()
        {
            this.Enabled = false;
        }

        public void EnableMessageValidation()
        {
            this.Enabled = true;
        }
        #endregion
    }
}

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

        private string _name;
        public string Name
        {
            get => _server.Name;
            set => SetAndNotify(ref _name, value);
        }


        #region MESSENGERS
        public void SendMessage(string topic, MessageCommand command, object parameter, object payload)
        {
            if (this.Enabled)
            {
                Server.Send(topic, command, parameter, payload);
            }
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

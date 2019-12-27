using CMiX.MVVM.Commands;
using Memento;
using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public class SendableViewModel : ViewModel, ISendable, IUndoable
    {
        public SendableViewModel()
        {

        }

        public SendableViewModel(ObservableCollection<ServerValidation> servervalidation, Mementor mementor)
        {
            ServerValidation = servervalidation;
            Mementor = mementor;
        }

        public string MessageAddress { get; set; }
        public ObservableCollection<ServerValidation> ServerValidation { get; set; }
        public Mementor Mementor { get; set; }

        #region MESSENGERS
        public void SendMessages(string topic, MessageCommand command, object parameter, object payload)
        {
            if (ServerValidation != null)
            {
                foreach (var servervalidation in ServerValidation)
                {
                    if (servervalidation.SendEnabled && servervalidation.Server.Enabled)
                    {
                        servervalidation.Server.Send(topic, command, parameter, payload);
                        Console.WriteLine("ServerValidation Sent with topic " + topic);
                    }
                }
            }
        }

        public void SendMessagesWithoutValidation(string topic, MessageCommand command, object parameter, object payload)
        {
            if (ServerValidation != null)
            {
                foreach (var servervalidation in ServerValidation)
                {
                    if (servervalidation.Server.Enabled)
                    {
                        servervalidation.Server.Send(topic, command, parameter, payload);
                    }
                }
            }
        }

        public void DisabledMessages()
        {
            if (ServerValidation != null)
            {
                foreach (var serverValidation in ServerValidation)
                {
                    serverValidation.Server.Enabled = false;
                }
            }
        }

        public void EnabledMessages()
        {
            if (ServerValidation != null)
            {
                foreach (var serverValidation in ServerValidation)
                {
                    serverValidation.Server.Enabled = true;
                }
            }
        }
        #endregion
    }
}

using CMiX.MVVM.Commands;
using CMiX.MVVM.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace CMiX.MVVM.Services
{
    public class Sender
    {
        public Sender(ObservableCollection<Server> servers)
        {
            Enabled = true;
            Servers = servers;
            //Servers.CollectionChanged += ServerCollectionChanged;
            //MessageValidations = new ObservableCollection<MessageValidation>();
            CreateMessageValidation();
        }

        //public ObservableCollection<MessageValidation> MessageValidations { get; set; }
        public ObservableCollection<Server> Servers { get; set; }

        public bool Enabled { get; set; }

        private void CreateMessageValidation()
        {
            foreach (var server in Servers)
            {
                //MessageValidation messageValidation = new MessageValidation(server);
                //MessageValidations.Add(messageValidation);
            }
        }

        //private void ServerCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    switch (e.Action)
        //    {
        //        case NotifyCollectionChangedAction.Add:
        //            for (int i = 0; i < e.NewItems.Count; i++)
        //            {
        //                Server server = e.NewItems[i] as Server;
        //                MessageValidations.Insert(e.NewStartingIndex + i, new MessageValidation(server));
        //            }
        //            break;

        //        case NotifyCollectionChangedAction.Move:
        //            if (e.OldItems.Count == 1)
        //            {
        //                MessageValidations.Move(e.OldStartingIndex, e.NewStartingIndex);
        //            }
        //            else
        //            {
        //                List<MessageValidation> items = MessageValidations.Skip(e.OldStartingIndex).Take(e.OldItems.Count).ToList();
        //                for (int i = 0; i < e.OldItems.Count; i++)
        //                    MessageValidations.RemoveAt(e.OldStartingIndex);

        //                for (int i = 0; i < items.Count; i++)
        //                    MessageValidations.Insert(e.NewStartingIndex + i, items[i]);
        //            }
        //            break;

        //        case NotifyCollectionChangedAction.Remove:
        //            for (int i = 0; i < e.OldItems.Count; i++)
        //            {
        //                MessageValidations.RemoveAt(e.OldStartingIndex);
        //            }
                        
        //            break;

        //        case NotifyCollectionChangedAction.Replace:
        //            // remove
        //            for (int i = 0; i < e.OldItems.Count; i++)
        //                MessageValidations.RemoveAt(e.OldStartingIndex);

        //            // add
        //            goto case NotifyCollectionChangedAction.Add;

        //        case NotifyCollectionChangedAction.Reset:
        //            MessageValidations.Clear();
        //            for (int i = 0; i < e.NewItems.Count; i++)
        //            {
        //                Server server = e.NewItems[i] as Server;
        //                MessageValidations.Add(new MessageValidation(server));
        //            }
        //            break;

        //        default:
        //            break;
        //    }
        //}

        #region SEND MESSAGES
        public void SendMessages(string topic, MessageCommand command, object parameter, object payload)
        {
            if (this.Enabled)
            {
                //foreach (var messageValidation in MessageValidations)
                //{
                //    messageValidation.SendMessage(topic, command, parameter, payload);
                //}
            }
        }

        public void SendMessagesWithoutValidation(string topic, MessageCommand command, object parameter, object payload)
        {
            //foreach (var messageValidation in MessageValidations)
            //{
            //    messageValidation.SendMessage(topic, command, parameter, payload);
            //}
        }

        public void Disable()
        {
            this.Enabled = false;
        }

        public void Enable()
        {
            this.Enabled = true;
        }
        #endregion
    }
}

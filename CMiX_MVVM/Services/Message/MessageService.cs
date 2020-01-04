using CMiX.MVVM.Message;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.Services
{
    public class MessageService : ViewModel
    {
        public MessageService(MessageFactory messageFactory)
        {
            MessageFactory = messageFactory;

            Servers = new ObservableCollection<Server>();
            AddServer();

            AddServerCommand = new RelayCommand(p => AddServer());
            DeleteServerCommand = new RelayCommand(p => DeleteServer(p));
        }

        public ICommand AddServerCommand { get; set; }
        public ICommand DeleteServerCommand { get; set; }

        public MessageFactory MessageFactory { get; set; }

        public ObservableCollection<Server> Servers { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        public ObservableCollection<Messenger> Messengers { get; set; }
        public ObservableCollection<Receiver> Receivers { get; set; }

        private Server _selectedServer;
        public Server SelectedServer
        {
            get => _selectedServer;
            set => SetAndNotify(ref _selectedServer, value);
        }

        public Messenger CreateMessenger()
        {
            return new Messenger(Servers);
        }

        //public Receiver CreateReceiver()
        //{
        //    return new Receiver(Clients);
        //}

        public void AddServer()
        {
            Server server = MessageFactory.CreateServer();
            server.Start();
            Servers.Add(server);
        }

        private void DeleteServer(object server)
        {
            Server s = server as Server;
            s.Stop();
            Servers.Remove(s);
        }

        public void AddClient()
        {
            Client client = MessageFactory.CreateClient();
            client.Start();
            Clients.Add(client);
        }

        public void DeleteClient(object client)
        {
            Client c = client as Client;
            c.Stop();
            Clients.Remove(c);
        }
    }
}

using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.Services
{
    public class MessageService : ViewModel
    {
        public MessageService()
        {
            Servers = new ObservableCollection<Server>();
            Clients = new ObservableCollection<Client>();
            //AddServer();

            AddServerCommand = new RelayCommand(p => AddServer());
            DeleteServerCommand = new RelayCommand(p => DeleteServer(p));
        }

        public ICommand AddServerCommand { get; set; }
        public ICommand DeleteServerCommand { get; set; }

        public ObservableCollection<Server> Servers { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        public ObservableCollection<Sender> Senders { get; set; }
        public ObservableCollection<Receiver> Receivers { get; set; }

        private Server _selectedServer;
        public Server SelectedServer
        {
            get => _selectedServer;
            set => SetAndNotify(ref _selectedServer, value);
        }

        public Sender CreateSender()
        {
            return new Sender(Servers);
        }

        public Receiver CreateReceiver()
        {
            Receiver receiver = new Receiver(Clients);
            return receiver;
        }

        int ServerID = 0;
        int ClientID = 0;

        public void AddServer()
        {
            Server server = new Server($"Server({ServerID.ToString()})", "127.0.0.1", 1111 + ServerID, $"/Device{ServerID}");
            server.Start();
            Servers.Add(server);
            ServerID++;
        }

        private void DeleteServer(object server)
        {
            Server s = server as Server;
            s.Stop();
            Servers.Remove(s);
        }

        public void AddClient()
        {
            Client client = new Client($"Client({ClientID.ToString()})", "127.0.0.1", 1111 + ClientID, $"/Device{ClientID}");
            client.Start();
            Clients.Add(client);
            ClientID++;
        }

        public void DeleteClient(object client)
        {
            Client c = client as Client;
            c.Stop();
            Clients.Remove(c);
        }

        public void SendMessages(string topic, MessageCommand command, object parameter, object payload)
        {
            if (this.Enabled)
            {
                foreach (var sender in Senders)
                {
                    sender.SendMessages(topic, command, parameter, payload);
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
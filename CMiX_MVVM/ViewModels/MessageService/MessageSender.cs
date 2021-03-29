using Ceras;
using CMiX.MVVM.Views;
using CMiX.Studio.ViewModels.MessageService;
using MvvmDialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessageSender : ViewModel, IMessageTerminal
    {
        public MessageSender()
        {
            Serializer = new CerasSerializer();
            //MessageProcessors = new List<IMessageProcessor>();

            MessengerFactory = new MessengerFactory();
            Messengers = new ObservableCollection<Messenger>();
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());

            AddMessengerCommand = new RelayCommand(p => AddMessenger());
            DeleteMessengerCommand = new RelayCommand(p => DeleteMessenger(p as Messenger));
            RenameMessengerCommand = new RelayCommand(p => RenameServer(p as Server));
            EditMessengerSettingsCommand = new RelayCommand(p => EditMessengerSettings(p as Messenger));
        }

        public CerasSerializer Serializer { get; set; }

        MessengerFactory MessengerFactory { get; set; }
        public IDialogService DialogService { get; set; }
        public ICommand EditMessengerSettingsCommand { get; }
        public ICommand AddMessengerCommand { get; set; }
        public ICommand DeleteMessengerCommand { get; set; }
        public ICommand RenameMessengerCommand { get; set; }


        public bool HasMessengerRunning { get => Messengers.Any(x => x.ServerIsRunning); }

        private ObservableCollection<Messenger> _messengers;
        public ObservableCollection<Messenger> Messengers
        {
            get => _messengers;
            set => SetAndNotify(ref _messengers, value);
        }

        public void EditMessengerSettings(Messenger messenger)
        {
            Settings settings = messenger.GetSettings();
            bool? success = DialogService.ShowDialog<MessengerSettingsWindow>(this, settings);
            if (success == true)
                messenger.SetSettings(settings);
        }

        public void AddMessenger()
        {
            var messenger = MessengerFactory.CreateMessenger();
            Messengers.Add(messenger);
        }

        private void DeleteMessenger(Messenger messenger)
        {
            if (messenger != null)
            {
                messenger.StopServer();
                Messengers.Remove(messenger);
            }
        }

        private void RenameServer(Server obj)
        {
            if (obj != null)
                obj.IsRenaming = true;
        }


        //public void ProcessMessage(IMessage message)
        //{

        //}

        private void MessageProcessor_MessageNotification(IMessageProcessor arg1, IMessage message)
        {
            var address = message.ComponentID;
            var data = Serializer.Serialize(message);

            foreach (var messenger in Messengers)
            {
                messenger.SendMessage(address, data);
            }
            System.Console.WriteLine("MessageSender send message to : " + address);
        }


        public void RegisterMessageProcessor(IComponentMessageProcessor messageProcessor)
        {
            messageProcessor.MessageNotification += MessageProcessor_MessageNotification;
        }


        public void UnregisterMessageProcessor(IComponentMessageProcessor messageProcessor)
        {
            messageProcessor.MessageNotification -= MessageProcessor_MessageNotification;
        }
    }
}
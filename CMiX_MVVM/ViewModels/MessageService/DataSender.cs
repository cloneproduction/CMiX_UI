using Ceras;
using CMiX.MVVM.ViewModels.Messages;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using CMiX.MVVM.Views;
using CMiX.Studio.ViewModels.MessageService;
using MvvmDialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class DataSender : ViewModel, IMessageSender
    {
        public DataSender()
        {
            Serializer = new CerasSerializer();

            MessengerFactory = new MessengerFactory();
            Messengers = new ObservableCollection<Messenger>();
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());

            AddMessengerCommand = new RelayCommand(p => AddMessenger());
            DeleteMessengerCommand = new RelayCommand(p => DeleteMessenger(p as Messenger));
            RenameMessengerCommand = new RelayCommand(p => RenameServer(p as Server));
            EditMessengerSettingsCommand = new RelayCommand(p => EditMessengerSettings(p as Messenger));
        }


        private MessengerFactory MessengerFactory { get; set; }

        public CerasSerializer Serializer { get; set; }
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


        public IMessageSendHandler SetSender(IMessageSendHandler handler)
        {
            throw new NotImplementedException();
        }


        public void SendMessagePack(IMessagePack messagePack)
        {
            Console.WriteLine("DataSender SendMessageAggregator");
            //var address = message.ComponentID;
            var address = Guid.NewGuid(); //for testing
            var message = messagePack as MessagePack;
            byte[] data = Serializer.Serialize(message);

            foreach (var messenger in Messengers)
            {
                messenger.SendMessage(address, data);
                Console.WriteLine("MessageSender SendMessage");
            }
        }
    }
}
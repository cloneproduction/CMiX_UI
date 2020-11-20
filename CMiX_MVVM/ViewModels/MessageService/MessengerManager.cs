using System.Windows.Input;
using MvvmDialogs;
using CMiX.MVVM.Views;
using System.Collections.ObjectModel;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class Sender : ViewModel
    {
        public Sender()
        {
            MessengerFactory = new MessengerFactory();
            Messengers = new ObservableCollection<Messenger>();
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());

            AddMessengerCommand = new RelayCommand(p => AddMessenger());
            DeleteMessengerCommand = new RelayCommand(p => DeleteMessenger(p as Messenger));
            RenameMessengerCommand = new RelayCommand(p => RenameServer(p as Server));
            EditMessengerSettingsCommand = new RelayCommand(p => EditMessengerSettings(p as Messenger));
        }

        MessengerFactory MessengerFactory { get; set; }
        public ICommand EditMessengerSettingsCommand { get; }
        public ICommand AddMessengerCommand { get; set; }
        public ICommand DeleteMessengerCommand { get; set; }
        public ICommand RenameMessengerCommand { get; set; }
        public IDialogService DialogService { get; set; }

        public void EditMessengerSettings(Messenger messenger)
        {
            Settings settings = messenger.GetSettings();
            bool? success = DialogService.ShowDialog<MessengerSettingsWindow>(this, settings);
            if (success == true)
            {
                messenger.SetSettings(settings);
            }
        }

        private ObservableCollection<Messenger> _messengers;
        public ObservableCollection<Messenger> Messengers
        {
            get => _messengers;
            set => SetAndNotify(ref _messengers, value);
        }

        public void ProcessMessage(string address, byte[] message)
        {
            foreach (var messenger in Messengers)
            {
                messenger.Server.Send(address, message);
            }
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

        public void SendMessage(string address, byte[] data)
        {
            foreach (var messenger in Messengers)
            {
                messenger.Server.Send(address, data);
            }
        }
    }
}
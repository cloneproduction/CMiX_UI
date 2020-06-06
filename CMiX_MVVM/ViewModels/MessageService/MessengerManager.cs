using System.Windows.Input;
using System.Collections.Generic;
using MvvmDialogs;
using CMiX.MVVM.Views;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace CMiX.Studio.ViewModels.MessageService
{
    public class MessengerManager : ViewModel
    {
        public MessengerManager(Project project, IDialogService dialogService)
        {
            Project = project;
            Addresses = new List<string>();
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());

            AddMessengerCommand = new RelayCommand(p => AddMessenger());
            DeleteMessengerCommand = new RelayCommand(p => DeleteMessenger(p as Messenger));
            RenameMessengerCommand = new RelayCommand(p => RenameServer(p as Server));
            EditMessengerSettingsCommand = new RelayCommand(p => EditMessengerSettings(p as Messenger));
        }

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


        public ICommand EditMessengerSettingsCommand { get; }
        public ICommand AddMessengerCommand { get; set; }
        public ICommand DeleteMessengerCommand { get; set; }
        public ICommand RenameMessengerCommand { get; set; }

        public Project Project { get; set; }

        public List<string> Addresses { get; set; }

        public ObservableCollection<Settings> SettingsCollection { get; set; }

        private Messenger _selectedMessenger;
        public Messenger SelectedMessenger
        {
            get => _selectedMessenger;
            set => SetAndNotify(ref _selectedMessenger, value);
        }
        
        public void AddMessenger()
        {
            var messenger = MessengerFactory.CreateMessenger();
            Project.SendChangeEvent += messenger.Value_SendChangeEvent;
            Project.Messengers.Add(messenger);
        }

        private void DeleteMessenger(Messenger messenger)
        {
            if (messenger != null)
            {
                messenger.StopServer();
                Project.Messengers.Remove(messenger);
            }
        }

        private void RenameServer(Server obj)
        {
            if (obj != null)
                obj.IsRenaming = true;
        }
    }
}
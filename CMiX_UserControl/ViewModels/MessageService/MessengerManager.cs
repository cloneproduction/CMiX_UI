using System.Windows.Input;
using System.Collections.Generic;
using MvvmDialogs;
using CMiX.Views;
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
            DeleteMessengerCommand = new RelayCommand(p => DeleteServer());
            RenameMessengerCommand = new RelayCommand(p => RenameServer(p));
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
                System.Console.WriteLine("SetSettings");
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
            Project.Messengers.Add(MessengerFactory.CreateMessenger());
        }

        private void DeleteServer()
        {
            if (SelectedMessenger != null)
            {
                //SelectedMessenger.Stop();
                Project.Messengers.Remove(SelectedMessenger);
                //Addresses.Remove(SelectedMessenger.Settings.Address);
            }
        }

        private void RenameServer(object obj)
        {
            if (obj != null)
                ((Server)obj).IsRenaming = true;
        }
    }
}
using CMiX.MVVM.ViewModels;
using System.Windows.Input;
using System.Collections.Generic;
using MvvmDialogs;
using CMiX.Studio.Views;
using CMiX.Views;

namespace CMiX.Studio.ViewModels
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
            OpenSettingsCommand = new RelayCommand(p => OpenSettings());
        }

        public IDialogService DialogService { get; set; }

        public void OpenSettings()
        {
            System.Console.WriteLine("OpenSettings");
            var messengerSettings = new MessengerSettings();
            bool? success = DialogService.ShowDialog<MessengerSettingsWindow>(this, messengerSettings);
            //if (success == true)
            //{

            //}
            //DialogService.ShowDialog<MessengerSettingsWindow>(this, new MessengerSettings());
        }


        public ICommand OpenSettingsCommand { get; }


        public ICommand AddMessengerCommand { get; set; }
        public ICommand DeleteMessengerCommand { get; set; }
        public ICommand RenameMessengerCommand { get; set; }

        public Project Project { get; set; }

        public List<string> Addresses { get; set; }

        private Messenger _selectedMessenger;
        public Messenger SelectedMessenger
        {
            get => _selectedMessenger;
            set => SetAndNotify(ref _selectedMessenger, value);
        }
        
        public void AddMessenger()
        {
            Messenger messenger = MessengerFactory.CreateMessenger(Addresses);
            Project.Messengers.Add(messenger);
        }

        private void DeleteServer()
        {
            if (SelectedMessenger != null)
            {
                //SelectedMessenger.Stop();
                Project.Messengers.Remove(SelectedMessenger);
            }
        }

        private void RenameServer(object obj)
        {
            if (obj != null)
                ((MVVM.ViewModels.Server)obj).IsRenaming = true;
        }
    }
}
using CMiX.MVVM.ViewModels;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class MessengerManager : ViewModel
    {
        public MessengerManager(Project project)
        {
            Project = project;

            AddMessengerCommand = new RelayCommand(p => AddMessenger());
            DeleteMessengerCommand = new RelayCommand(p => DeleteServer());
            RenameMessengerCommand = new RelayCommand(p => RenameServer(p));
        }

        public ICommand AddMessengerCommand { get; set; }
        public ICommand DeleteMessengerCommand { get; set; }
        public ICommand RenameMessengerCommand { get; set; }

        public Project Project { get; set; }

        private Messenger _selectedMessenger;
        public Messenger SelectedMessenger
        {
            get => _selectedMessenger;
            set => SetAndNotify(ref _selectedMessenger, value);
        }

        public void AddMessenger()
        {
            //$"Server({ServerID.ToString()})", "127.0.0.1", 1111 + ServerID, $"/Device{ServerID}"
            //server.Start();
            Messenger messenger = MessengerFactory.CreateMessenger();
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
                ((Server)obj).IsRenaming = true;
        }
    }
}
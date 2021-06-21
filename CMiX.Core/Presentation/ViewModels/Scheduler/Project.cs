using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Ceras;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class Project : ViewModel
    {
        public Project()
        {
            Serializer = new CerasSerializer();

            Devices = new ObservableCollection<Device>();
            Playlists = new ObservableCollection<Playlist>();
            RunningJobs = new ObservableCollection<IJob>();

            Scheduler = new Scheduler();
            JobEditor = new JobEditor(Devices, Playlists, RunningJobs);

            PlaylistEditor = new PlaylistEditor(Playlists);

            //PROBLEMS
            //CompoSelector = new FileSelector(string.Empty, "Single", new List<string>() { ".COMPMIX" }, new CMiX.MVVM.Services.MessageCommunicator(), new Mementor());
            
            AddClientCommand = new RelayCommand(p => AddClient());
            DeleteClientCommand = new RelayCommand(p => DeleteClient(p));
            ResetAllClientCommand = new RelayCommand(p => ResetAllClient());
        }

        public ICommand AddClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand SendAllCommand { get; }
        public ICommand ResetAllClientCommand { get; }

        public CerasSerializer Serializer { get; set; }
        public Scheduler Scheduler { get; set; }
        public JobEditor JobEditor { get; set; }

        //public FileSelector CompoSelector { get; set; }
        public PlaylistEditor PlaylistEditor { get; set; }

        public ObservableCollection<IJob> RunningJobs { get; set; }
        public ObservableCollection<Device> Devices { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set; }

        public string Name => throw new NotImplementedException();

        int clientcreationindex = 0;

        private void AddClient()
        {
            clientcreationindex++;
            var client = new Device(Serializer, Playlists);
            client.Name = $"Server ({clientcreationindex})";
            Devices.Add(client);
        }

        private void DeleteClient(object client)
        {
            Devices.Remove(client as Device);
            if(Devices.Count == 0)
            {
                clientcreationindex = 0;
            }
        }

        private void ResetAllClient()
        {
            foreach(var client in Devices)
            {
                client.ResetClient();
            }
        }
    }
}

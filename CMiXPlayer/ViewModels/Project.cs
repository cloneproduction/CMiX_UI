using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Ceras;
using CMiX.MVVM.ViewModels;
using CMiXPlayer.Jobs;
using FluentScheduler;
using Memento;


namespace CMiXPlayer.ViewModels
{
    public class Project : ViewModel
    {
        #region CONSTRUCTORS
        public Project()
        {
            Serializer = new CerasSerializer();

            Devices = new ObservableCollection<Device>();
            Playlists = new ObservableCollection<Playlist>();
            RunningJobs = new ObservableCollection<IJob>();

            Scheduler = new Scheduler(Devices, RunningJobs);
            JobEditor = new JobEditor(Devices, Playlists, RunningJobs);

            PlaylistEditor = new PlaylistEditor(Serializer, Playlists);

            CompoSelector = new FileSelector(string.Empty, "Single", new List<string>() { ".COMPMIX" }, new ObservableCollection<OSCValidation>(), new Mementor());
            
            AddClientCommand = new RelayCommand(p => AddClient());
            DeleteClientCommand = new RelayCommand(p => DeleteClient(p));
            SendAllCommand = new RelayCommand(p => SendAllClient());
            ResetAllClientCommand = new RelayCommand(p => ResetAllClient());
        }
        #endregion

        #region PROPERTIES
        public ICommand AddClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand SendAllCommand { get; }
        public ICommand ResetAllClientCommand { get; }
        public ICommand MakeJobCommand { get; }
        public ICommand InitJobCommand { get; }

        public CerasSerializer Serializer { get; set; }
        public Scheduler Scheduler { get; set; }
        public JobEditor JobEditor { get; set; }

        public FileSelector CompoSelector { get; set; }
        public PlaylistEditor PlaylistEditor { get; set; }

        public ObservableCollection<IJob> RunningJobs { get; set; }
        public ObservableCollection<Device> Devices { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set; }

        public string Name => throw new NotImplementedException();
        #endregion

        #region METHODS

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

        private void SendAllClient()
        {
            foreach (var client in Devices)
            {
                client.SendComposition();
            }
        }

        private void ResetAllClient()
        {
            foreach(var client in Devices)
            {
                client.ResetClient();
            }
        }
        #endregion
    }
}

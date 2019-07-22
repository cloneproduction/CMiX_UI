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
            Scheduler = new Scheduler(Devices);
            
            JobEditor = new JobEditor(Devices, Playlists);
            PlaylistEditor = new PlaylistEditor(Serializer, Playlists);

            CompoSelector = new FileSelector(string.Empty, "Single", new List<string>() { ".COMPMIX" }, new ObservableCollection<OSCValidation>(), new Mementor());
            

            AddClientCommand = new RelayCommand(p => AddClient());
            DeleteClientCommand = new RelayCommand(p => DeleteClient(p));
            SendAllCommand = new RelayCommand(p => SendAllClient());
            ResetAllClientCommand = new RelayCommand(p => ResetAllClient());
            MakeJobCommand = new RelayCommand(p => MakeJob());
            InitJobCommand = new RelayCommand(p => InitJob());
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

        public ObservableCollection<Device> Devices { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set; }

        public string Name => throw new NotImplementedException();
        #endregion

        #region METHODS

        int clientcreationindex = 0;
        private void AddClient()
        {
            clientcreationindex++;
            Console.WriteLine("Added Client");
            var client = new Device(Serializer, Playlists);
            client.OSCMessenger.Name = $"Server ({clientcreationindex})";
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


        //public Registry Registry { get; set; }

        //public List<Action<Schedule>> action { get; set; }

        //public IJob job { get; set; }

        public void MakeJob()
        {
            //job = new JobSendComposition(Devices[0].SelectedPlaylist, Devices[0].OSCMessenger);
            //ToRunType toruntype = new ToRunType();
            //action = new List<Action<Schedule>>();

            //action.Add((s) => toruntype.SetRunType(s));
            //Registry = new Registry();
            //JobManager.Initialize(Registry);
        }

        public void InitJob()
        {
            //JobManager.AddJob(job, (s) => action[0].Invoke(s));
        }
    }
}

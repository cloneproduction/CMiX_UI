using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Ceras;
using CMiX.MVVM.ViewModels;
using FluentScheduler;
using Memento;


namespace CMiXPlayer.ViewModels
{
    public class Project : ViewModel
    {
        #region CONSTRUCTORS
        public Project()
        {
            Devices = new ObservableCollection<Device>();
            Playlists = new ObservableCollection<Playlist>();

            Serializer = new CerasSerializer();

            CompoSelector = new FileSelector(string.Empty, "Single", new List<string>() { ".COMPMIX" }, new ObservableCollection<OSCValidation>(), new Mementor());
            PlaylistEditor = new PlaylistEditor(Serializer, Playlists);

            AddClientCommand = new RelayCommand(p => AddClient());
            DeleteClientCommand = new RelayCommand(p => DeleteClient(p));
            SendAllCommand = new RelayCommand(p => SendAllClient());
            ResetAllClientCommand = new RelayCommand(p => ResetAllClient());
            MakeJobCommand = new RelayCommand(p => MakeJob());
        }
        #endregion

        #region PROPERTIES
        public ICommand AddClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand SendAllCommand { get; }
        public ICommand ResetAllClientCommand { get; }
        public ICommand MakeJobCommand { get; }

        public CerasSerializer Serializer { get; set; }
        public ObservableCollection<Device> Devices { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set; }
        public FileSelector CompoSelector { get; set; }
        public PlaylistEditor PlaylistEditor { get; set; }

        public string Name => throw new NotImplementedException();
        #endregion

        #region METHODS
        private void AddClient()
        {
            var client = new Device(Serializer, Playlists) { Name = "pouet" };
            Devices.Add(client);
        }

        private void DeleteClient(object client)
        {
            Devices.Remove(client as Device);
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


        public void MakeJob()
        {
            Console.WriteLine("StartMakeJob");
            var registry = new Registry();
            JobManager.Initialize(registry);

            IJob job = new Jobs.JobSendComposition(Devices[0].SelectedPlaylist, Devices[0].OSCMessenger);

            JobManager.AddJob(job, s => s.ToRunEvery(30).Seconds());
        }
    }
}

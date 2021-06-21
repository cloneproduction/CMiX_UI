﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Ceras;
using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class Device : ViewModel
    {
        public Device(CerasSerializer cerasSerializer, ObservableCollection<Playlist> playlists)
        {
            Playlists = playlists;

            //CompoSelector = new FileSelector(string.Empty,"Single", new List<string>() { ".COMPMIX" }, MessageService, new Mementor());
            //CompoSelector.SelectedFileNameItem = new FileNameItem();

            //OSCSender = new OSCSender("127.0.0.1", 1111);
            Serializer = cerasSerializer;

            ResetClientCommand = new RelayCommand(p => ResetClient());
        }

        public ICommand SendCompositionCommand { get; }
        public ICommand ResetClientCommand { get; }

        public ObservableCollection<Playlist> Playlists { get; set; }
        public CerasSerializer Serializer { get; set; }
        //public FileSelector CompoSelector { get; set; }
        public string MessageAddress { get; set; }
        //public MessageService MessageService { get; set; }
        //public MessageCommunicator MessageCommunicator { get; set; }

        private Playlist _selectedplaylist;
        public Playlist SelectedPlaylist
        {
            get => _selectedplaylist;
            set => SetAndNotify(ref _selectedplaylist, value);
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public void ResetClient()
        {
            //OSCSender.SendMessage("/CompositionReloaded", true);
        }
    }
}

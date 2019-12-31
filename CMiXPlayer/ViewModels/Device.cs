using System;
using System.Collections.Generic;
using Memento;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Ceras;
using CMiX.MVVM.Services;

namespace CMiXPlayer.ViewModels
{
    public class Device : ViewModel, ISendable
    {
        #region CONSTRUCTORS
        public Device(CerasSerializer cerasSerializer, ObservableCollection<Playlist> playlists)
        {
            Playlists = playlists;

            CompoSelector = new FileSelector(string.Empty,"Single", new List<string>() { ".COMPMIX" }, Messenger, new Mementor());
            CompoSelector.SelectedFileNameItem = new FileNameItem();

            //OSCMessenger = new OSCMessenger("127.0.0.1", 1111);
            Serializer = cerasSerializer;

            ResetClientCommand = new RelayCommand(p => ResetClient());
        }
        #endregion

        #region PROPERTIES
        public ICommand SendCompositionCommand { get; }
        public ICommand ResetClientCommand { get; }

        public ObservableCollection<Playlist> Playlists { get; set; }
        public CerasSerializer Serializer { get; set; }
        public FileSelector CompoSelector { get; set; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Messenger Messenger { get; set; }

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


        #endregion

        #region METHODS
        public void ResetClient()
        {
            //OSCMessenger.SendMessage("/CompositionReloaded", true);
        }
        #endregion
    }
}
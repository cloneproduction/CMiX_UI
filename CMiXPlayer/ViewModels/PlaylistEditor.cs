using CMiX.MVVM.ViewModels;
using Ceras;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;

namespace CMiXPlayer.ViewModels
{
    public class PlaylistEditor : ViewModel
    {
        #region CONSTRUCTORS
        public PlaylistEditor(CerasSerializer serializer, ObservableCollection<Playlist> playlists)
        {
            Playlists = playlists;
            Serializer = serializer;

            var play = new Playlist(Serializer) { Name = "NewPlaylist" };
            Playlists.Add(play);
            SelectedPlaylist = play;

            NewPlaylistCommand = new RelayCommand(p => NewPlaylist());
            DeletePlaylistCommand = new RelayCommand(p => DeletePlaylist());
        }
        #endregion

        #region PROPERTIES
        public CerasSerializer Serializer;

        public ObservableCollection<Playlist> Playlists { get; set; }

        private Playlist _selectedplaylist;
        public Playlist SelectedPlaylist
        {
            get => _selectedplaylist;
            set => SetAndNotify(ref _selectedplaylist, value);
        }

        public ICommand DeleteCompoFromPlaylistCommand { get; set; }
        public ICommand NewPlaylistCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }

        #endregion

        #region METHODS
        int plCreateIndex = 0;

        public void NewPlaylist()
        {
            plCreateIndex++;
            Playlist playlist = new Playlist(Serializer);
            playlist.Name = $"Playlist ({plCreateIndex})";
            Playlists.Add(playlist);
            SelectedPlaylist = playlist;
        }

        public void DeletePlaylist()
        {
            Playlists.Remove(SelectedPlaylist);
            if (Playlists.Count == 0)
                plCreateIndex = 0;
        }
        #endregion
    }
}
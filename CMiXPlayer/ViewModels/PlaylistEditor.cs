using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiXPlayer.ViewModels
{
    public class PlaylistEditor : ViewModel
    {
        public PlaylistEditor(ObservableCollection<Playlist> playlists)
        {
            Playlists = playlists;
            AddCompoToPlaylistCommand = new RelayCommand(p => AddCompoToPlaylist());
            CreatePlaylistCommand = new RelayCommand(p => CreatePlaylist(p));
            DeletePlaylistCommand = new RelayCommand(p => DeletePlaylist());
            EditPlaylistCommand = new RelayCommand(p => EditPlaylist());
        }

        #region PROPERTIES
        public ObservableCollection<CompositionModel> AvailableCompositions { get; set; }
        public CompositionModel SelectedComposition { get; set; }

        public ObservableCollection<Playlist> Playlists { get; set; }
        public Playlist SelectedPlaylist { get; set; }


        public Playlist EditablePlaylist { get; set; }

        public ICommand AddCompoToPlaylistCommand { get; set; }
        public ICommand DeleteCompoFromPlaylistCommand { get; set; }
        public ICommand CreatePlaylistCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }
        public ICommand EditPlaylistCommand { get; set; }
        #endregion


        public void AddCompoToPlaylist()
        {
            if(EditablePlaylist != null)
                EditablePlaylist.Compositions.Add(SelectedComposition);
        }

        public void DeleteCompoFromPlaylist()
        {
            if(SelectedComposition != null)
                EditablePlaylist.Compositions.Remove(SelectedComposition);
        }

        public void CreatePlaylist(object playlist)
        {
            var pl = playlist as Playlist;
            Playlists.Add(pl);
        }

        public void DeletePlaylist()
        {

            Playlists.Remove(EditablePlaylist);
        }
           
        public void EditPlaylist()
        {
            EditablePlaylist = SelectedPlaylist;
        }
    }
}

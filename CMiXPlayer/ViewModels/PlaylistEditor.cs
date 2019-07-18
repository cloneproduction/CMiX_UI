using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

using Ceras;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.IO;

namespace CMiXPlayer.ViewModels
{
    public class PlaylistEditor : ViewModel
    {
        public PlaylistEditor(CerasSerializer serializer, ObservableCollection<Playlist> playlists)
        {
            Playlists = playlists;
            Serializer = serializer;

            EditablePlaylist = new PlaylistEditable(Serializer);

            FileSelector = new FileSelector(string.Empty, "Single", new List<string> { ".COMPMIX" }, new ObservableCollection<OSCValidation>(), new Memento.Mementor());
            AddCompoToPlaylistCommand = new RelayCommand(p => AddCompoToPlaylist());

            NewPlaylistCommand = new RelayCommand(p => NewPlaylist());
            CreatePlaylistCommand = new RelayCommand(p => CreatePlaylist());
            DeletePlaylistCommand = new RelayCommand(p => DeletePlaylist());
            EditPlaylistCommand = new RelayCommand(p => EditPlaylist());
            SavePlaylistCommand = new RelayCommand(p => SavePlaylist());
        }

        #region PROPERTIES
        public CerasSerializer Serializer;

        public ObservableCollection<CompositionModel> AvailableCompositions { get; set; }
        public CompositionModel SelectedComposition { get; set; }

        public ObservableCollection<Playlist> Playlists { get; set; }
        public Playlist SelectedPlaylist { get; set; }
        public PlaylistEditable EditablePlaylist { get; set; }

        public string PlaylistName { get; set; }
        public FileSelector FileSelector { get; set; }
        
        public ICommand AddCompoToPlaylistCommand { get; set; }
        public ICommand DeleteCompoFromPlaylistCommand { get; set; }
        public ICommand NewPlaylistCommand { get; set; }
        public ICommand SavePlaylistCommand { get; set; }
        public ICommand CreatePlaylistCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }
        public ICommand EditPlaylistCommand { get; set; }
        #endregion

        public void SavePlaylist()
        {
            Console.WriteLine("Save Playlist");
        }

        public void NewPlaylist()
        {
            //Playlist playlist = new Playlist();
            //EditablePlaylist = playlist;
            //Console.WriteLine("New Playlist");
        }

        public void AddCompoToPlaylist()
        {

        }

        public void DeleteCompoFromPlaylist()
        {

        }

        public void CreatePlaylist()
        {
            //Playlist pl = EditablePlaylist;
            //foreach (var compo in FileSelector.FilePaths)
            //{
            //    var filename = compo.FileName;
            //    byte[] data = File.ReadAllBytes(filename);
            //    CompositionModel compositionmodel = Serializer.Deserialize<CompositionModel>(data);
            //    pl.Compositions.Add(compositionmodel);
            //    pl.Name = PlaylistName;
            //}
            //Playlists.Add(pl);
        }

        public void DeletePlaylist()
        {
            //Playlists.Remove(EditablePlaylist);
        }
           
        public void EditPlaylist()
        {
            //EditablePlaylist = SelectedPlaylist;
        }
    }
}

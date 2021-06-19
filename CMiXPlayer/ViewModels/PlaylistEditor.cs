using CMiX.Core.Presentation.ViewModels;
using Ceras;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using CMiX.Core.Models;
using System.IO;
using CMiXPlayer.Models;
using System.Linq;

namespace CMiXPlayer.ViewModels
{
    public class PlaylistEditor : ViewModel, IDropTarget
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

            DeleteSelectedCompoCommand = new RelayCommand(p => DeleteSelectedCompo());
            DuplicateSelectedCompoCommand = new RelayCommand(p => DuplicateSelectedCompo());
            DeleteAllCompoCommand = new RelayCommand(p => DeleteAllCompo());
            ExportSelectedPlaylistCommand = new RelayCommand(p => ExportSelectedPlaylist());
            ImportPlaylistCommand = new RelayCommand(p => ImportPlaylist());
        }
        #endregion

        public void ExportSelectedPlaylist()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            savedialog.Filter = "Playlist (*.playlist)|*.playlist";
            savedialog.DefaultExt = "playlist";
            savedialog.FileName = SelectedPlaylist.Name;
            savedialog.AddExtension = true;

            if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PlaylistModel playlistmodel = new PlaylistModel();
                SelectedPlaylist.Copy(playlistmodel);
                string folderPath = savedialog.FileName;
                var data = Serializer.Serialize(playlistmodel);
                File.WriteAllBytes(folderPath, data);
            }
        }

        public void ImportPlaylist()
        {
            System.Windows.Forms.OpenFileDialog opendialog = new System.Windows.Forms.OpenFileDialog();
            opendialog.Filter = "Playlist (*.playlist)|*.playlist";

            if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = opendialog.FileName;
                if (opendialog.FileName.Trim() != string.Empty) // Check if you really have a file name 
                {
                    byte[] data = File.ReadAllBytes(folderPath);
                    PlaylistModel playlistmodel = Serializer.Deserialize<PlaylistModel>(data);
                    Playlist playlist = new Playlist(Serializer);

                    playlist.Paste(playlistmodel);
                    Playlists.Add(playlist);
                    SelectedPlaylist = playlist;
                }
            }
        }

        #region PROPERTIES
        public CerasSerializer Serializer;

        public ObservableCollection<Playlist> Playlists { get; set; }

        private Playlist _selectedplaylist;
        public Playlist SelectedPlaylist
        {
            get => _selectedplaylist;
            set => SetAndNotify(ref _selectedplaylist, value);
        }

        private CompositionModel _selectedComposition;
        public CompositionModel SelectedComposition
        {
            get => _selectedComposition;
            set => SetAndNotify(ref _selectedComposition, value);
        }

        public ICommand NewPlaylistCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }

        public ICommand DeleteSelectedCompoCommand { get; set; }
        public ICommand DuplicateSelectedCompoCommand { get; set; }
        public ICommand DeleteAllCompoCommand { get; set; }
        public ICommand ExportSelectedPlaylistCommand { get; set; }
        public ICommand ImportPlaylistCommand { get; set; }
        #endregion


        #region METHODS
        public void DeleteSelectedCompo()
        {
            if (SelectedComposition != null)
                SelectedPlaylist.Compositions.Remove(SelectedComposition);
        }

        public void DuplicateSelectedCompo()
        {
            if (SelectedComposition != null)
            {
                //CompositionModel compo = new CompositionModel();
                //compo = SelectedComposition;
                //SelectedPlaylist.Compositions.Add(compo);
            }
        }

        public void DeleteAllCompo()
        {
            if (SelectedPlaylist != null)
                SelectedPlaylist.Compositions.Clear();
        }

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

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            var dataObject = dropInfo.Data as IDataObject;

            if (dataObject != null 
                && dataObject.GetDataPresent(DataFormats.FileDrop)
                || dropInfo.Data.GetType() == typeof(CompositionModel))
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;

            if (dataObject != null && SelectedPlaylist != null)
            {
                if (dataObject.ContainsFileDropList())
                {
                    var filedrop = dataObject.GetFileDropList();
                    foreach (string str in filedrop)
                    {
                        if (Path.GetExtension(str).ToUpperInvariant() == ".COMPMIX")
                        {
                            byte[] data = File.ReadAllBytes(str);
                            var compositionmodel = Serializer.Deserialize<CompositionModel>(data);
                            SelectedPlaylist.Compositions.Add(compositionmodel);
                        }
                    }
                }
            }

            if (dropInfo.DragInfo != null)
            {
                int sourceindex = dropInfo.DragInfo.SourceIndex;
                int insertindex = dropInfo.InsertIndex;

                if (sourceindex != insertindex)
                {
                    if (insertindex >= SelectedPlaylist.Compositions.Count - 1)
                        insertindex -= 1;
                    SelectedPlaylist.Compositions.Move(sourceindex, insertindex);
                }
            }
        }
        #endregion
    }
}
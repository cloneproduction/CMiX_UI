using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CMiX.Core.Models;
using CMiX.Core.Models.Scheduling;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Presentation.ViewModels.Components;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.Core.Presentation.ViewModels.Scheduling
{
    public class PlaylistEditor : ViewModel, IControl, IDropTarget
    {
        public PlaylistEditor(PlaylistEditorModel playlistEditorModel, ObservableCollection<Playlist> playlists)
        {
            this.ID = playlistEditorModel.ID;

            Playlists = playlists;

            Playlist play = new Playlist() { Name = "NewPlaylist" };
            Playlists.Add(play);
            SelectedPlaylist = play;

            NewPlaylistCommand = new RelayCommand(p => NewPlaylist());
            DeletePlaylistCommand = new RelayCommand(p => DeletePlaylist());

            DeleteSelectedCompoCommand = new RelayCommand(p => DeleteSelectedCompo());
            DeleteAllCompoCommand = new RelayCommand(p => DeleteAllCompo());
            AddCompositionToPlaylistCommand = new RelayCommand(p => AddCompositionToPlaylist(p as Composition));
        }


        private void AddCompositionToPlaylist(Composition composition)
        {
            if (composition != null)
            {
                SelectedPlaylist.Compositions.Add(composition);
                DropDownOpen = false;
            }
        }


        public ObservableCollection<Playlist> Playlists { get; set; }


        private bool _dropDownOpen;
        public bool DropDownOpen
        {
            get => _dropDownOpen;
            set => SetAndNotify(ref _dropDownOpen, value);
        }

        private Playlist _selectedplaylist;
        public Playlist SelectedPlaylist
        {
            get => _selectedplaylist;
            set => SetAndNotify(ref _selectedplaylist, value);
        }

        private Composition _selectedComposition;
        public Composition SelectedComposition
        {
            get => _selectedComposition;
            set => SetAndNotify(ref _selectedComposition, value);
        }


        public ICommand AddCompositionToPlaylistCommand { get; set; }
        public ICommand NewPlaylistCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }
        public ICommand DeleteSelectedCompoCommand { get; set; }
        public ICommand DuplicateSelectedCompoCommand { get; set; }
        public ICommand DeleteAllCompoCommand { get; set; }
        public Guid ID { get; set; }


        public void DeleteSelectedCompo()
        {
            if (SelectedComposition != null)
                SelectedPlaylist.Compositions.Remove(SelectedComposition);
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
            Playlist playlist = new Playlist();
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
                            //var compositionmodel = new CompositionModel();// Serializer.Deserialize<CompositionModel>(data);
                            //SelectedPlaylist.Compositions.Add(compositionmodel);
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


        public ControlCommunicator Communicator { get; set; }

        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }

        public void SetViewModel(IModel model)
        {
            PlaylistEditorModel playlistEditorModel = model as PlaylistEditorModel;
            this.ID = playlistEditorModel.ID;
        }

        public IModel GetModel()
        {
            PlaylistEditorModel playlistEditorModel = new PlaylistEditorModel();
            playlistEditorModel.ID = this.ID;
            return playlistEditorModel;
        }
    }
}

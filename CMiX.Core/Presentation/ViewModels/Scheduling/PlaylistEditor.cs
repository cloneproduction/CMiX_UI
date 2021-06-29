using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Core.Models;
using CMiX.Core.Models.Scheduler;
using CMiX.Core.Models.Scheduling;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Presentation.ViewModels.Components;

namespace CMiX.Core.Presentation.ViewModels.Scheduling
{
    public class PlaylistEditor : ViewModel, IControl
    {
        public PlaylistEditor(Project project)
        {
            this.ID = new Guid("33223344-5566-7788-99AA-BBCCDDEEFF00");
            Project = project;

            Playlists = new ObservableCollection<Playlist>();

            NewPlaylistCommand = new RelayCommand(p => NewPlaylist());
            DeletePlaylistCommand = new RelayCommand(p => DeletePlaylist());

            DeleteSelectedCompoCommand = new RelayCommand(p => DeleteSelectedCompo());
            DeleteAllCompoCommand = new RelayCommand(p => DeleteAllCompo());
            AddCompositionToPlaylistCommand = new RelayCommand(p => AddCompositionToPlaylist(p as Composition));
        }


        int plCreateIndex = 0;
        public Guid ID { get; set; }
        public ICommand AddCompositionToPlaylistCommand { get; set; }
        public ICommand NewPlaylistCommand { get; set; }
        public ICommand DeletePlaylistCommand { get; set; }
        public ICommand DeleteSelectedCompoCommand { get; set; }
        public ICommand DuplicateSelectedCompoCommand { get; set; }
        public ICommand DeleteAllCompoCommand { get; set; }


        public Project Project { get; set; }
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


        public void AddCompositionToPlaylist(Composition composition)
        {
            if (composition != null)
            {
                SelectedPlaylist.Compositions.Add(composition);
                DropDownOpen = false;
                Communicator?.SendMessageAddComposition(composition);
                Console.WriteLine("Composition added to playlist " + SelectedPlaylist.Name);
            }
        }

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



        public void NewPlaylist()
        {
            plCreateIndex++;
            Playlist playlist = new Playlist(new PlaylistModel());
            playlist.Name = $"Playlist ({plCreateIndex})";
            Playlists.Add(playlist);
            SelectedPlaylist = playlist;

            Communicator?.SendMessageAddPlaylist(playlist);
            Console.WriteLine("New Playlist Created");
        }

        public void DeletePlaylist()
        {
            Playlists.Remove(SelectedPlaylist);
            if (Playlists.Count == 0)
                plCreateIndex = 0;
        }



        public PlaylistEditorCommunicator Communicator { get; set; }

        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new PlaylistEditorCommunicator(this);
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

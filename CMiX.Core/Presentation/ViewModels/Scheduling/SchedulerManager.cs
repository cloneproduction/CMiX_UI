// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Core.Models;
using CMiX.Core.Models.Scheduling;
using CMiX.Core.Network.Communicators;

namespace CMiX.Core.Presentation.ViewModels.Scheduling
{
    public class SchedulerManager : ViewModel, IControl
    {
        public SchedulerManager(SchedulerManagerModel schedulerModel)
        {
            this.ID = schedulerModel.ID;

            CompositionSchedulers = new ObservableCollection<CompositionScheduler>();
            Playlists = new ObservableCollection<Playlist>();

            PlaylistEditor = new PlaylistEditor(schedulerModel.PlaylistEditorModel, Playlists);

            CreateSchedulerCommand = new RelayCommand(p => CreateScheduler());
            DeleteSchedulerCommand = new RelayCommand(p => DeleteScheduler());
        }


        public Guid ID { get; set; }
        public ICommand CreateSchedulerCommand { get; set; }
        public ICommand DeleteSchedulerCommand { get; set; }

        public PlaylistEditor PlaylistEditor { get; set; }
        public ObservableCollection<CompositionScheduler> CompositionSchedulers { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set; }



        private CompositionScheduler _selectedScheduler;
        public CompositionScheduler SelectedScheduler
        {
            get => _selectedScheduler;
            set => SetAndNotify(ref _selectedScheduler, value);
        }


        private int _selectedSchedulerIndex;
        public int SelectedSchedulerIndex
        {
            get => _selectedSchedulerIndex;
            set
            {
                SetAndNotify(ref _selectedSchedulerIndex, value);
                Communicator?.SendMessageSelectedSchedulerIndex(value);
                Console.WriteLine("SelectedSchedulerIndex = " + SelectedSchedulerIndex);
            }
        }


        public void CreateScheduler()
        {
            CompositionSchedulerModel compositionSchedulerModel = new CompositionSchedulerModel();
            CompositionScheduler compositionScheduler = new CompositionScheduler(compositionSchedulerModel, Playlists);
            compositionScheduler.SetCommunicator(Communicator);
            CompositionSchedulers.Add(compositionScheduler);

            Communicator.SendMessageAddScheduler(compositionSchedulerModel);
        }

        public void CreateScheduler(CompositionSchedulerModel compositionSchedulerModel)
        {
            CompositionScheduler compositionScheduler = new CompositionScheduler(compositionSchedulerModel, Playlists);
            compositionScheduler.SetCommunicator(Communicator);
            CompositionSchedulers.Add(compositionScheduler);
            Console.WriteLine("Scheduler Created");
        }

        public void DeleteScheduler()
        {
            CompositionSchedulers.Remove(SelectedScheduler);
            SelectedScheduler?.UnsetCommunicator(Communicator);
            SelectedScheduler = null;
        }


        public SchedulerCommunicator Communicator { get; set; }

        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new SchedulerCommunicator(this);
            Communicator.SetCommunicator(communicator);

            PlaylistEditor.SetCommunicator(Communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);

            PlaylistEditor.UnsetCommunicator(Communicator);
        }


        public IModel GetModel()
        {
            SchedulerManagerModel schedulerModel = new SchedulerManagerModel();
            schedulerModel.ID = this.ID;
            return schedulerModel;
        }

        public void SetViewModel(IModel model)
        {
            SchedulerManagerModel schedulerModel = model as SchedulerManagerModel;
            this.ID = schedulerModel.ID;
        }
    }
}

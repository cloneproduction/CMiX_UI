// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using CMiX.Core.Models;
using CMiX.Core.Models.Scheduling;
using CMiX.Core.Network.Communicators;

namespace CMiX.Core.Presentation.ViewModels.Scheduling
{
    public class CompositionScheduler : ViewModel, IControl
    {
        public CompositionScheduler(CompositionSchedulerModel compositionSchedulerModel)
        {
            this.ID = compositionSchedulerModel.ID;
            this.Name = compositionSchedulerModel.Name;

            Playlists = new ObservableCollection<Playlist>();

            PlaylistEditor = new PlaylistEditor(compositionSchedulerModel.PlaylistEditorModel, Playlists);

            JobScheduler = new JobScheduler(compositionSchedulerModel.JobSchedulerModel);
            JobEditor = new JobEditor(compositionSchedulerModel.JobEditorModel, Playlists, JobScheduler);
        }


        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }


        public JobScheduler JobScheduler { get; set; }
        public JobEditor JobEditor { get; set; }
        public PlaylistEditor PlaylistEditor { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set; }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);

            JobEditor.SetCommunicator(Communicator);
            JobScheduler.SetCommunicator(Communicator);
            PlaylistEditor.SetCommunicator(Communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);

            JobEditor.UnsetCommunicator(Communicator);
            JobScheduler.UnsetCommunicator(Communicator);
            PlaylistEditor.UnsetCommunicator(Communicator);
        }


        public IModel GetModel()
        {
            CompositionSchedulerModel compositionSchedulerModel = new CompositionSchedulerModel();
            compositionSchedulerModel.ID = this.ID;
            compositionSchedulerModel.JobEditorModel = (JobEditorModel)this.JobEditor.GetModel();
            compositionSchedulerModel.JobSchedulerModel = (JobSchedulerModel)this.JobScheduler.GetModel();
            compositionSchedulerModel.PlaylistEditorModel = (PlaylistEditorModel)this.PlaylistEditor.GetModel();
            return compositionSchedulerModel;
        }

        public void SetViewModel(IModel model)
        {
            CompositionSchedulerModel compositionSchedulerModel = model as CompositionSchedulerModel;
            this.ID = compositionSchedulerModel.ID;
            this.JobEditor.SetViewModel(compositionSchedulerModel.JobEditorModel);
            this.JobScheduler.SetViewModel(compositionSchedulerModel.JobSchedulerModel);
            this.PlaylistEditor.SetViewModel(compositionSchedulerModel.PlaylistEditorModel);
        }
    }
}

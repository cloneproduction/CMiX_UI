// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Presentation.ViewModels.Components;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class CompositionScheduler : ViewModel, IControl
    {
        public CompositionScheduler(ObservableCollection<Component> compositions)
        {
            Playlists = new ObservableCollection<Playlist>();
            PlaylistEditor = new PlaylistEditor(Playlists, compositions);
            JobEditor = new JobEditor(Playlists);
            JobsOverview = new JobsOverview();
        }


        public Guid ID { get; set; }
        public SchedulerCommunicator Communicator { get; set; }


        public JobsOverview JobsOverview { get; set; }
        public JobEditor JobEditor { get; set; }
        public PlaylistEditor PlaylistEditor { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set; }


        public IModel GetModel()
        {
            throw new NotImplementedException();
        }

        public void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new SchedulerCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }
        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }
    }
}

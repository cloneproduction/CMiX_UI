// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Linq;
using CMiX.Core.Presentation.ViewModels.Components;
using CMiX.Core.Presentation.ViewModels.Scheduling;

namespace CMiX.Core.Network.Messages
{
    public class MessageAddCompositionToPlaylist : Message
    {
        public MessageAddCompositionToPlaylist()
        {

        }

        public MessageAddCompositionToPlaylist(Composition composition)
        {
            CompositionID = composition.ID;
        }

        public Guid CompositionID { get; set; }

        public override void Process<T>(T receiver)
        {
            PlaylistEditor playlistEditor = receiver as PlaylistEditor;
            //Composition composition = playlistEditor.Compositions.First(x => x.ID == CompositionID) as Composition;
            //playlistEditor.AddCompositionToPlaylist(composition);
        }
    }
}

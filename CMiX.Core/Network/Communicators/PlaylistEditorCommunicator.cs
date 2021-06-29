// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels.Components;
using CMiX.Core.Presentation.ViewModels.Scheduling;

namespace CMiX.Core.Network.Communicators
{
    public class PlaylistEditorCommunicator : Communicator
    {
        public PlaylistEditorCommunicator(PlaylistEditor playlistEditor) : base()
        {
            IIDObject = playlistEditor;
        }

        public void SendMessageAddPlaylist(Playlist playlist)
        {
            this.SendMessage(new MessageAddNewPlaylist(playlist));
        }

        internal void SendMessageAddComposition(Composition composition)
        {
            this.SendMessage(new MessageAddCompositionToPlaylist(composition));
        }
    }
}

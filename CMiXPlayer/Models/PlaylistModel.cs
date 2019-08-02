using CMiX.MVVM.Models;
using System.Collections.Generic;

namespace CMiXPlayer.Models
{
    public class PlaylistModel
    {
        public PlaylistModel()
        {
            Compositions = new List<CompositionModel>();
        }

        public string Name { get; set; }
        public List<CompositionModel> Compositions { get; set;}
    }
}

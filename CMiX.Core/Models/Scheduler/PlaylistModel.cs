using CMiX.Core.Models;
using System.Collections.Generic;

namespace CMiX.Core.Models.Scheduler
{
    public class PlaylistModel : IModel
    {
        public PlaylistModel()
        {
            Compositions = new List<CompositionModel>();
        }

        public string Name { get; set; }
        public List<CompositionModel> Compositions { get; set;}
        public bool Enabled { get; set; }
    }
}

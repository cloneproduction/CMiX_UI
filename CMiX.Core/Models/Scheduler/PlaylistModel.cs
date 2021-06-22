using System;
using System.Collections.Generic;

namespace CMiX.Core.Models.Scheduler
{
    public class PlaylistModel : IModel
    {
        public PlaylistModel()
        {
            Compositions = new List<CompositionModel>();
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<CompositionModel> Compositions { get; set; }
        public bool Enabled { get; set; }
    }
}

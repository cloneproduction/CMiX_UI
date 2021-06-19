using CMiX.Core.Interfaces;
using System;

namespace CMiX.Core.Models.Beat
{
    public class MasterBeatModel : BeatModel, IModel
    {
        public MasterBeatModel()
        {
            this.ID = Guid.NewGuid();
            ResyncModel = new ResyncModel();
        }

        public ResyncModel ResyncModel { get; set; }
    }
}

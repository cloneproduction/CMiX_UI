using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models.Beat
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

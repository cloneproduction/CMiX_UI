using CMiX.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models.Beat
{
    public class MasterBeatModel : BeatModel, IModel
    {
        public MasterBeatModel()
        {
            ResyncModel = new ResyncModel();
        }

        public ResyncModel ResyncModel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public class ScaleModifier : XYZModifier
    {
        public ScaleModifier(string name, MasterBeat beat) : base (name, beat)
        {
            Uniform = new AnimParameter(nameof(Uniform), 0.0, beat, true, this);
        }

        public ScaleModifier(string name, MasterBeat beat, Sendable parentSendable) : this(name, beat)
        {
            SubscribeToEvent(parentSendable);
        }

        public AnimParameter Uniform { get; set; }
    }
}

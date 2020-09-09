using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public class ScaleModifier : XYZModifier
    {
        public ScaleModifier(string name, MasterBeat beat, Counter counter) : base (name, beat, counter)
        {
            Uniform = new AnimParameter(nameof(Uniform), 1.0, counter, beat, true, this);
            X = new AnimParameter(nameof(Uniform), 1.0, counter, beat, true, this);
            Y = new AnimParameter(nameof(Uniform), 1.0, counter, beat, true, this);
            Z = new AnimParameter(nameof(Uniform), 1.0, counter, beat, true, this);
        }

        public ScaleModifier(string name, MasterBeat beat, Counter counter, Sendable parentSendable) : this(name, beat, counter)
        {
            SubscribeToEvent(parentSendable);
        }

        public AnimParameter Uniform { get; set; }
    }
}

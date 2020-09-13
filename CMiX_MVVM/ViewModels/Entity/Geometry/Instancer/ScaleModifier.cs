using System;

namespace CMiX.MVVM.ViewModels
{
    public class ScaleModifier : XYZModifier
    {
        public ScaleModifier(string name, MasterBeat beat, Counter counter, Sendable parentSendable) : base (name, beat, counter)
        {
            Uniform = new AnimParameter(nameof(Uniform), 1.0, counter, beat, true, this);
            X = new AnimParameter(nameof(X), 1.0, counter, beat, true, this);
            Y = new AnimParameter(nameof(Y), 1.0, counter, beat, true, this);
            Z = new AnimParameter(nameof(Z), 1.0, counter, beat, true, this);
            SubscribeToEvent(parentSendable);
        }

        public AnimParameter Uniform { get; set; }
    }
}
using System;

namespace CMiX.MVVM.ViewModels
{
    public class ScaleModifier : XYZModifier
    {
        public ScaleModifier(string name, Sender parentSender, MasterBeat beat, double[] defaultValue) : base (name, parentSender, beat, counter)
        {
            Uniform = new AnimParameter(nameof(Uniform), this, 1.0, counter, beat, true);
            X = new AnimParameter(nameof(X), this, 1.0, counter, beat, true);
            Y = new AnimParameter(nameof(Y), this, 1.0, counter, beat, true);
            Z = new AnimParameter(nameof(Z), this, 1.0, counter, beat, true);
        }

        public AnimParameter Uniform { get; set; }
    }
}
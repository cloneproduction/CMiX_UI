using System;

namespace CMiX.MVVM.ViewModels
{
    public class ScaleModifier : XYZModifier
    {
        public ScaleModifier(string name, Sender parentSender, MasterBeat beat) : base (name, parentSender, beat)
        {
            Uniform = new AnimParameter(nameof(Uniform), this, 1.0, beat);
            X = new AnimParameter(nameof(X), this, 1.0, beat);
            Y = new AnimParameter(nameof(Y), this, 1.0, beat);
            Z = new AnimParameter(nameof(Z), this, 1.0, beat);

            this.Attach(Uniform);
        }

        public AnimParameter Uniform { get; set; }
    }
}
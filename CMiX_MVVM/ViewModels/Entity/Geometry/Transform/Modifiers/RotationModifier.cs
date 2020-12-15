using System;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class RotationModifier : Sender, IModifier
    {
        public RotationModifier(string name, Sender parentSender, Rotation rotation, MasterBeat beat) : base(name, parentSender)
        {
            X = new AnimParameter(nameof(X), this, rotation.X.Amount, beat);
            Y = new AnimParameter(nameof(Y), this, rotation.X.Amount, beat);
            Z = new AnimParameter(nameof(Z), this, rotation.X.Amount, beat);
        }

        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }

        public AnimParameter X { get; set; }
        public AnimParameter Y { get; set; }
        public AnimParameter Z { get; set; }
    }
}
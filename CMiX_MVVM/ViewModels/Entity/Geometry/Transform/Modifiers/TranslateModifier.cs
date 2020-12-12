using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System;
using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public class TranslateModifier : Sender, IModifier
    {
        public TranslateModifier(string name, Sender parentSender, Vector3D vector3D, MasterBeat beat) : base(name, parentSender)
        {
            X = new AnimParameter(nameof(X), this, vector3D.X, beat);
            Y = new AnimParameter(nameof(Y), this, vector3D.Y, beat);
            Z = new AnimParameter(nameof(Z), this, vector3D.Z, beat);
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

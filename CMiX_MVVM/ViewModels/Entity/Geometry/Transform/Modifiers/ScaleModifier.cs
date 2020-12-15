using System;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class ScaleModifier : Sender, IModifier
    {
        public ScaleModifier(string name, Sender parentSender, Scale scale, MasterBeat beat) : base(name, parentSender)
        {
            IsUniform = false;
            X = new AnimParameter(nameof(X), this, scale.X.Amount, beat);
            Y = new AnimParameter(nameof(Y), this, scale.Y.Amount, beat);
            Z = new AnimParameter(nameof(Z), this, scale.Z.Amount, beat);
        }

        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }

        public AnimParameter X { get; set; }
        public AnimParameter Y { get; set; }
        public AnimParameter Z { get; set; }

        private bool _isUniform;
        public bool IsUniform
        {
            get => _isUniform;
            set => SetAndNotify(ref _isUniform, value);
        }
    }
}
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class ScaleModifier : Sender, IModifier
    {
        public ScaleModifier(string name, IColleague parentSender, MasterBeat beat, ITransform transform) : base(name, parentSender)
        {
            IsUniform = false;
            X = new AnimParameter(nameof(X), this, transform.Transform.Scale.X.Amount, beat);
            Y = new AnimParameter(nameof(Y), this, transform.Transform.Scale.Y.Amount, beat);
            Z = new AnimParameter(nameof(Z), this, transform.Transform.Scale.Z.Amount, beat);
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


        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }
    }
}

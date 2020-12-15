using System;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class TranslateModifier : Sender, IModifier
    {
        public TranslateModifier(string name, Sender parentSender, Translate translate, MasterBeat beat) : base(name, parentSender)
        {
            X = new AnimParameter(nameof(X), this, translate.X.Amount, beat);
            Y = new AnimParameter(nameof(Y), this, translate.Y.Amount, beat);
            Z = new AnimParameter(nameof(Z), this, translate.Z.Amount, beat);
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

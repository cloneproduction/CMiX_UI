using System;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class TranslateModifier : Sender, IModifier, IObserver
    {
        public TranslateModifier(string name, Sender parentSender, Translate translate, MasterBeat beat) : base(name, parentSender)
        {
            Translate = translate;

            X = new AnimParameter(nameof(X), this, new double[1] { 0.0 }, beat);
            //Y = new AnimParameter(nameof(Y), this, translate.Y.Amount, beat);
            //Z = new AnimParameter(nameof(Z), this, translate.Z.Amount, beat);
        }

        Translate Translate { get; set; }
        public int Count { get; set; }
        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }

        public void Update(int count)
        {
            this.Count = count;
            X.Parameters = new double[count];
        }


        public AnimParameter X { get; set; }
        public AnimParameter Y { get; set; }
        public AnimParameter Z { get; set; }
    }
}

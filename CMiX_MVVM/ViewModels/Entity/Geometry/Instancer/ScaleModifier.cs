using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Observer;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels
{
    public class ScaleModifier : XYZModifier
    {
        public ScaleModifier(string name, Sender parentSender, MasterBeat beat) : base (name, parentSender, beat)
        {
            Name = name;
            Observers = new List<IObserver>();

            Uniform = new AnimParameter(nameof(Uniform), this, 1.0, beat);
            X = new AnimParameter(nameof(X), this, 1.0, beat);
            Y = new AnimParameter(nameof(Y), this, 1.0, beat);
            Z = new AnimParameter(nameof(Z), this, 1.0, beat);

            this.Attach(Uniform);
            Attach(X);
            Attach(Y);
            Attach(Z);
        }

        private List<IObserver> Observers { get; set; }

        private int _isExpandedUniform;
        public int IsExpandedUniform
        {
            get => _isExpandedUniform;
            set => SetAndNotify(ref _isExpandedUniform, value);
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as ScaleModifierModel);
        }

        public AnimParameter Uniform { get; set; }
    }
}
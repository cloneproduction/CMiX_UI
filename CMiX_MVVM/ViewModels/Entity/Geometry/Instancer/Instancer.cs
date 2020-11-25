using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Instancer : Sender, ITransform
    {
        public Instancer(string name, IColleague parentSender, MasterBeat beat) :base(name, parentSender)
        {
            Transform = new Transform(nameof(Transform), this);
            Counter = new Counter(nameof(Counter), this);

            TranslateModifier = new XYZModifier(nameof(TranslateModifier), this, beat, this.Counter);
            ScaleModifier = new ScaleModifier(nameof(ScaleModifier), this, beat, this.Counter);
            RotationModifier = new XYZModifier(nameof(RotationModifier), this, beat, this.Counter);

            NoAspectRatio = false;
        }


        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as InstancerModel);
        }

        public Transform Transform { get; set; }
        public Counter Counter { get; set; }
        public XYZModifier TranslateModifier { get; set; }
        public XYZModifier ScaleModifier { get; set; }
        public XYZModifier RotationModifier { get; set; }


        private bool _noAspectRatio;
        public bool NoAspectRatio
        {
            get => _noAspectRatio;
            set => SetAndNotify(ref _noAspectRatio, value);
        }
    }
}
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Instancer : Sender, ITransform
    {
        public Instancer(MasterBeat beat)
        {
            Transform = new Transform(this);
            Counter = new Counter(this);

            TranslateModifier = new XYZModifier(nameof(Translate), beat, Counter, this);
            ScaleModifier = new ScaleModifier(nameof(Scale), beat, Counter, this);
            RotationModifier = new XYZModifier(nameof(Rotation), beat, Counter, this);

            NoAspectRatio = false;
        }

        public Instancer(MasterBeat beat, Sender parentSender) : this(beat)
        {
            SubscribeToEvent(parentSender);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as InstancerModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
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

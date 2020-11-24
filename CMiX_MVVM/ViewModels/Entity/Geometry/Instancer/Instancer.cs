using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Instancer : ViewModel, IColleague, ITransform
    {
        public Instancer(string name, IColleague parentSender, MasterBeat beat)
        {
            this.Address = $"{parentSender.Address}{name}/";
            this.MessageMediator = parentSender.MessageMediator;
            this.MessageMediator.RegisterColleague(this);

            Transform = new Transform(nameof(Transform), this);
            Counter = new Counter();

            //TranslateModifier = new XYZModifier(); // (nameof(Translate), beat, Counter, this);
            //ScaleModifier = new ScaleModifier(); // (nameof(Scale), beat, Counter, this);
            //RotationModifier = new XYZModifier(); // (nameof(Rotation), beat, Counter, this);

            NoAspectRatio = false;
        }

        public void Send(Message message)
        {
            MessageMediator?.Notify(MessageDirection.OUT, message);
        }

        public void Receive(Message message)
        {
            this.SetViewModel(message.Obj as InstancerModel);
        }

        public MessageMediator MessageMediator { get; set; }
        public string Address { get; set; }
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

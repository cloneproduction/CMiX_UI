using CMiX.MVVM.Controls;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class BeatModifier : Beat
    {
        public BeatModifier(string name, IColleague parentSender, MasterBeat masterBeat) : base(name, parentSender)
        {
            Index = 0;
            ChanceToHit = new Slider(nameof(ChanceToHit), this) { Amount = 100.0 };
            Beat = masterBeat;
            Multiplier = 1.0;
            this.Period = masterBeat.Period;

            masterBeat.IndexChanged += (s, newvalue) =>
            {
                SetAnimatedDouble();
            };

            masterBeat.PeriodChanged += (s, newvalue) =>
            {
                SetAnimatedDouble();
            };
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as BeatModifierModel);
        }

        public MasterBeat Beat { get; set; }
        public Slider ChanceToHit { get; set; }


        private int maxIndex = 4;
        private int minIndex = -4;

        private int _index;
        public int Index
        {
            get => _index;
            set => _index = value;
        }

        private double _period;
        public override double Period
        {
            get => _period;
            set => SetAndNotify(ref _period, value);
        }

        private int _beatIndex;
        public int BeatIndex
        {
            get { return _beatIndex; }
            set => _beatIndex = value;
        }

        private AnimatedDouble _animatedDouble;
        public AnimatedDouble AnimatedDouble
        {
            get => _animatedDouble;
            set => SetAndNotify(ref _animatedDouble, value);
        }

        protected override void Multiply()
        {
            if (Index <= minIndex)
                return;
            Index--;
            SetAnimatedDouble();
        }

        protected override void Divide()
        {
            if (Index >= maxIndex)
                return;
            Index++;
            SetAnimatedDouble();
        }

        private void SetAnimatedDouble()
        {
            BeatIndex = Index + Beat.BeatIndex;
            Period = Beat.Periods[Index + Beat.BeatIndex];
            AnimatedDouble = Beat.BeatAnimations.AnimatedDoubles[Index + Beat.BeatIndex];
            Notify(nameof(BPM));
            this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
        }
    }
}
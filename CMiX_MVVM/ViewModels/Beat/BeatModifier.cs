using System;
using CMiX.MVVM.Controls;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class BeatModifier : Beat
    {
        public BeatModifier(string name, IMessageProcessor parentSender, MasterBeat masterBeat) : base (name, parentSender)
        {
            Index = 0;
            ChanceToHit = new Slider(nameof(ChanceToHit), this) { Minimum = 0, Maximum = 100, Amount = 100.0 };
            MasterBeat = masterBeat;
            Multiplier = 1.0;
            Period = masterBeat.Period;

            SetAnimatedDouble();

            MasterBeat.IndexChanged += MasterBeat_IndexChanged;
            MasterBeat.PeriodChanged += MasterBeat_PeriodChanged;
        }

        public override void Dispose()
        {
            MasterBeat.IndexChanged -= MasterBeat_IndexChanged;
            MasterBeat.PeriodChanged -= MasterBeat_PeriodChanged;
            base.Dispose();
        }
        private void MasterBeat_PeriodChanged(Beat sender, double newValue)
        {
            SetAnimatedDouble();
        }

        private void MasterBeat_IndexChanged(object sender, EventArgs e)
        {
            SetAnimatedDouble();
        }

        public MasterBeat MasterBeat { get; set; }
        public Slider ChanceToHit { get; set; }

        public bool CheckHitOnBeatTick()
        {
            return (RandomNumbers.RandomDouble(0.0, 1.0) <= this.ChanceToHit.Amount / this.ChanceToHit.Maximum) ? true : false;
        }


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
            BeatIndex = Index + MasterBeat.BeatIndex;
            Period = MasterBeat.Periods[Index + MasterBeat.BeatIndex];
            AnimatedDouble = MasterBeat.BeatAnimations.AnimatedDoubles[Index + MasterBeat.BeatIndex];
            Notify(nameof(BPM));
            this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
        }

        public override void SetViewModel(IModel model)
        {
            BeatModifierModel beatModifierModel = model as BeatModifierModel;
            this.BeatIndex = beatModifierModel.BeatIndex;
            this.Multiplier = beatModifierModel.Multiplier;
            this.ChanceToHit.SetViewModel(beatModifierModel.ChanceToHit);
        }

        public override IModel GetModel()
        {
            BeatModifierModel model = new BeatModifierModel();
            model.BeatIndex = this.BeatIndex;
            model.ChanceToHit = (SliderModel)this.ChanceToHit.GetModel();
            model.Multiplier = this.Multiplier;
            return model;
        }
    }
}
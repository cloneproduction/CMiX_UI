﻿using CMiX.MVVM.Controls;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels.Beat
{
    public class BeatModifier : Beat, IMessageProcessor, IBeatObserver
    {
        //public BeatModifier(IMessageProcessor parentSender, MasterBeat masterBeat) : base (parentSender)
        //{
        //    ChanceToHit = new Slider(nameof(ChanceToHit), this) { Minimum = 0, Maximum = 100, Amount = 100.0 };
        //    Multiplier = 1.0;

        //    MasterBeat = masterBeat;
        //    MasterBeat.Attach(this);
        //    Period = masterBeat.Period;
            
        //    SetAnimatedDouble();
        //}


        public BeatModifier(IMessageProcessor parentSender, MasterBeat masterBeat, BeatModifierModel beatModifierModel) : base (parentSender)
        {
            ChanceToHit = new Slider(nameof(ChanceToHit), this, beatModifierModel.ChanceToHit) { Minimum = 0, Maximum = 100 };
            Multiplier = beatModifierModel.Multiplier;

            MasterBeat = masterBeat;
            MasterBeat.Attach(this);
            Period = masterBeat.Period;

            SetAnimatedDouble();
        }


        public MasterBeat MasterBeat { get; set; }
        public Slider ChanceToHit { get; set; }

        private int maxIndex = 4;
        private int minIndex = -4;

        private int _index = 0;
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
            get => _beatIndex;
            set => _beatIndex = value;
        }

        private AnimatedDouble _animatedDouble;
        public AnimatedDouble AnimatedDouble
        {
            get => _animatedDouble;
            set => SetAndNotify(ref _animatedDouble, value);
        }

        public bool CheckHitOnBeatTick()
        {
            return (RandomNumbers.RandomDouble(0.0, 1.0) <= this.ChanceToHit.Amount / this.ChanceToHit.Maximum) ? true : false;
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
            this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
        }

        public override void SetViewModel(IModel model)
        {
            BeatModifierModel beatModifierModel = model as BeatModifierModel;
            this.BeatIndex = beatModifierModel.BeatIndex;
            this.Multiplier = beatModifierModel.Multiplier;
            this.Period = beatModifierModel.Period;
            this.ChanceToHit.SetViewModel(beatModifierModel.ChanceToHit);
        }

        public override IModel GetModel()
        {
            BeatModifierModel model = new BeatModifierModel();
            model.Period = this.Period;
            model.Multiplier = this.Multiplier;
            model.BeatIndex = this.BeatIndex;
            model.ChanceToHit = (SliderModel)this.ChanceToHit.GetModel();
            model.Multiplier = this.Multiplier;
            return model;
        }

        public override void Dispose()
        {
            MasterBeat.Detach(this);
            base.Dispose();
        }

        public void UpdatePeriod(double period)
        {
            SetAnimatedDouble();
        }
    }
}
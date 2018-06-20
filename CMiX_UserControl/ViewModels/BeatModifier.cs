using CMiX.Services;
using System;

namespace CMiX.ViewModels
{
    public class BeatModifier : Beat
    {
        public BeatModifier(Beat masterBeat, string layerName, IMessenger messenger)
            : this(
                  messenger: messenger,
                  layerName: layerName,
                  masterBeat: masterBeat,
                  multiplier: 1.0,
                  chanceToHit: 1.0)
        { }

        public BeatModifier(
            IMessenger messenger,
            Beat masterBeat, 
            string layerName,
            double multiplier, 
            double chanceToHit)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(Messenger));
            LayerName = layerName;
            MasterBeat = masterBeat ?? throw new ArgumentNullException(nameof(masterBeat));
            Multiplier = multiplier;
            ChanceToHit = chanceToHit;

            masterBeat.PeriodChanged += (s, newValue) =>
            {
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
            };
        }

        private IMessenger Messenger { get; }

        private string Address => LayerName;

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }

        private Beat MasterBeat { get; }

        public override double Period
        {
            get => MasterBeat.Period * Multiplier;
            set => throw new InvalidOperationException("Property is readonly. When binding, use Mode=OneWay.");
        }

        public override double Multiplier
        {
            get => base.Multiplier;
            set
            {
                base.Multiplier = value;
                OnPeriodChanged(Period);
                Notify(nameof(Period));
                Notify(nameof(BPM));
                Messenger.SendMessage(Address + "/BeatMultiplier", Multiplier.ToString());
            }
        }

        private double _chanceToHit;
        public double ChanceToHit
        {
            get => _chanceToHit;
            set
            {
                SetAndNotify(ref _chanceToHit, value);
                Messenger.SendMessage(Address + "/ChanceToHit", ChanceToHit.ToString());
            }
        }

        protected override void Multiply()
        {
            Multiplier /= 2;
        }

        protected override void Divide()
        {
            Multiplier *= 2;
        }
    }
}
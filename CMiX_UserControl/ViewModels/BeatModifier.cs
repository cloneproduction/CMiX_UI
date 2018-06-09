using System;

namespace CMiX.ViewModels
{
    public class BeatModifier : Beat
    {
        public BeatModifier(Beat masterBeat, string layerName, string containerName)
            : this(
                  message: new Messenger(),
                  containerName: containerName,
                  layerName: layerName,
                  masterBeat: masterBeat,
                  multiplier: 1.0,
                  chanceToHit: 1.0)
        { }

        public BeatModifier(
            Beat masterBeat, 
            string layerName,
            string containerName,
            double multiplier, 
            double chanceToHit, 
            Messenger message)
        {
            LayerName = layerName;
            ContainerName = containerName;
            Message = message ?? throw new ArgumentNullException(nameof(Messenger));
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

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }

        private string _containerName;
        public string ContainerName
        {
            get => _containerName;
            set => SetAndNotify(ref _containerName, value);
        }

        private Messenger _message;
        public Messenger Message
        {
            get => _message;
            set => SetAndNotify(ref _message, value);
        }

        private string Address => String.Format("{0}/{1}/", LayerName, ContainerName);

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
                Message.SendOSC(Address + "BeatMultiplier", Multiplier.ToString());
            }
        }

        private double _chanceToHit;
        public double ChanceToHit
        {
            get => _chanceToHit;
            set
            {
                SetAndNotify(ref _chanceToHit, value);
                Message.SendOSC(Address + "ChanceToHit", ChanceToHit.ToString());
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
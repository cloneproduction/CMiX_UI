namespace CMiX.ViewModels
{
    public class BeatModifier : ViewModel
    {
        public BeatModifier()
            : this(period: 0.0, multiplier: 1, chanceToHit: 1.0)
        { }

        public BeatModifier(double period, int multiplier, double chanceToHit)
        {
            Period = period;
            Multiplier = multiplier;
            ChanceToHit = chanceToHit;
        }

        private double _period;
        public double Period
        {
            get => _period;
            set => SetAndNotify(ref _period, value);
        }

        private int _multiplier;
        public int Multiplier
        {
            get => _multiplier;
            set => SetAndNotify(ref _multiplier, value);
        }

        private double _chanceToHit;
        public double ChanceToHit
        {
            get => _chanceToHit;
            set => SetAndNotify(ref _chanceToHit, value);
        }
    }
}

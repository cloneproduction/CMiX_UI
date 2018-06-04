namespace CMiX.ViewModels
{
    public class BeatModifier : ViewModel
    {
        public BeatModifier()
            : this(multiplier: 1, chanceToHit: 1.0)
        { }

        public BeatModifier(int multiplier, double chanceToHit)
        {
            Multiplier = multiplier;
            ChanceToHit = chanceToHit;
        }

        int _multiplier;
        public int Multiplier
        {
            get => _multiplier;
            set => SetAndNotify(ref _multiplier, value);
        }

        double _chanceToHit;
        public double ChanceToHit
        {
            get => _chanceToHit;
            set => SetAndNotify(ref _chanceToHit, value);
        }
    }
}

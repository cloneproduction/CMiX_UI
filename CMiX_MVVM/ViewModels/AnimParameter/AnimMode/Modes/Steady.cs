using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : Sendable, IAnimMode
    {
        public Steady()
        {
            Range = new Range(0, 0);
            SteadyType = SteadyType.Linear;
        }

        public void Update()
        {
           
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public Range Range { get; set; }

        private SteadyType _steadyType;
        public SteadyType SteadyType
        {
            get => _steadyType;
            set => SetAndNotify(ref _steadyType, value);
        }
    }
}

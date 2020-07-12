using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : Sendable, IAnimMode
    {
        public Steady()
        {
            Range = new Range(0, 0);
        }
        public Steady(Stopwatcher stopwatcher)
        {
            //Stopwatcher = stopwatcher;
            //UpdateValue = new Action(Update);
            //ParameterValue = 0.0;
        }
        public void Update()
        {
            //Console.WriteLine("Steady Update");
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public SteadyType SteadyType {get; set;}
        public Range Range { get; set; }

        private EasingType _selectedEasingType;
        public EasingType SelectedEasingType
        {
            get => _selectedEasingType;
            set => SetAndNotify(ref _selectedEasingType, value);
        }
    }
}

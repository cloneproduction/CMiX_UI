using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class None : Sendable, IAnimMode
    {
        public None()
        {

        }
        public None(Stopwatcher stopwatcher)
        {
            //Stopwatcher = stopwatcher;
            SelectedEasingType = EasingType.Linear;
            //UpdateValue = new Action(Update);
        }

        public void Update()
        {
            //Console.WriteLine("LFO Update");
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private EasingType _selectedEasingType;
        public EasingType SelectedEasingType
        {
            get => _selectedEasingType;
            set => SetAndNotify(ref _selectedEasingType, value);
        }
        public Range Range { get; set; }
    }
}
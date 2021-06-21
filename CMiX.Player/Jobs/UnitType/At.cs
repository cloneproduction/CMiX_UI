using CMiX.Core.Presentation.ViewModels;

namespace CMiXPlayer.Jobs
{
    public class At : ViewModel
    {
        public At()
        {
            Name = "At";
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private int _hours;
        public int Hours
        {
            get => _hours;
            set => SetAndNotify(ref _hours, value);
        }

        private int _minutes;
        public int Minutes
        {
            get => _minutes;
            set => SetAndNotify(ref _minutes, value);
        }
    }
}
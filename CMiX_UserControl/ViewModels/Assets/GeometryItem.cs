using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class GeometryItem : ViewModel, IAssets
    {
        public GeometryItem(string name, string path)
        {
            Name = name;
            Path = path;
        }

        private string _ponderation = "aa";
        public string Ponderation
        {
            get => _ponderation;
            set => _ponderation = value;
        }

        private string _path;
        public string Path
        {
            get => _path;
            set => SetAndNotify(ref _path, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }
    }
}

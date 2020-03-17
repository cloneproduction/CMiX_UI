using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class TextureItem : ViewModel, IAssets
    {

        #region CONSTRUCTORS
        public TextureItem()
        {

        }
        public TextureItem(string name, string path)
        {
            Name = name;
            Path = path;
        }
        #endregion

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

        public IAssetModel GetModel()
        {
            TextureAssetModel textureAssetModel = new TextureAssetModel();

            textureAssetModel.Name = Name;
            textureAssetModel.Path = Path;

            return textureAssetModel;
        }

        public void SetViewModel(IAssetModel model)
        {
            Name = model.Name;
            Path = model.Path;
        }
    }
}
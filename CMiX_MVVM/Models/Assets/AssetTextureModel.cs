using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    public class AssetTextureModel : IAssetModel
    {
        public AssetTextureModel()
        {

        }

        public bool Enabled { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public string Ponderation { get; set; }
        public ObservableCollection<IAssetModel> AssetModels { get; set; }
    }
}

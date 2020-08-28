using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    public class AssetDirectoryModel : IAssetModel
    {
        public AssetDirectoryModel()
        {
            AssetModels = new ObservableCollection<IAssetModel>();
        }

        public ObservableCollection<IAssetModel> AssetModels { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public string Ponderation { get; set; }
    }
}
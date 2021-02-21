using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    public class AssetModel : IAssetModel
    {
        public AssetModel()
        {

        }

        public string Name { get; set; }
        public string Ponderation { get; set; }
        public string Path { get; set; }

        public ObservableCollection<IAssetModel> AssetModels { get ; set; }
        public bool Enabled { get; set; }
    }
}

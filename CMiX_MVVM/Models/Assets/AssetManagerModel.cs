using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public class AssetManagerModel : Model
    {
        public AssetManagerModel()
        {
            AssetModels = new ObservableCollection<IAssetModel>();
            FlattenAssetModels = new ObservableCollection<IAssetModel>();
        }

        public ObservableCollection<IAssetModel> AssetModels { get; set; }
        public ObservableCollection<IAssetModel> FlattenAssetModels { get; set; }
    }
}

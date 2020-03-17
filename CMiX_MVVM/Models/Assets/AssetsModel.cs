using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public class AssetsModel : Model
    {
        public AssetsModel()
        {
            AssetModels = new ObservableCollection<IAssetModel>();
        }

        public ObservableCollection<IAssetModel> AssetModels { get; set; }
    }
}

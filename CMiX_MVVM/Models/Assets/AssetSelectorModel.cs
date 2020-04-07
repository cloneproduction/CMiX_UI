using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class AssetSelectorModel : Model
    {
        public AssetSelectorModel()
        {

        }

        public IAssetModel SelectedAssetModel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public class DirectoryAssetModel : IAssetModel
    {
        public DirectoryAssetModel()
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

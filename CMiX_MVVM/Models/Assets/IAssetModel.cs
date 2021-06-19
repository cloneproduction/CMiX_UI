using CMiX.Core.Interfaces;
using System.Collections.ObjectModel;

namespace CMiX.Core.Models
{
    public interface IAssetModel : IModel
    {
        string Path { get; set; }
        string Name { get; set; }
        string Ponderation { get; set; }

        ObservableCollection<IAssetModel> AssetModels { get; set; }
    }
}
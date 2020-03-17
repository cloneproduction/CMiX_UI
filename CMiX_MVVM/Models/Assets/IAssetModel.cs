using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    public interface IAssetModel
    {
        string Path { get; set; }
        string Name { get; set; }
        bool IsSelected { get; set; }
        string Ponderation { get; set; }
        ObservableCollection<IAssetModel> AssetModels { get; set; }
    }
}
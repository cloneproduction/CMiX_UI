using CMiX.MVVM.Models;

namespace CMiX.MVVM.MessageService
{
    public interface IAssetPathSelectorMessage
    {
        IAssetModel AssetModel { get; set; }
    }
}

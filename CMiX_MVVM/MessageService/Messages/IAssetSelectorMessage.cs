using CMiX.Core.Models;

namespace CMiX.Core.MessageService
{
    public interface IAssetPathSelectorMessage
    {
        IAssetModel AssetModel { get; set; }
    }
}

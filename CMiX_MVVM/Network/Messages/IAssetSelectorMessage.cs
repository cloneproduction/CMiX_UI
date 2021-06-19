using CMiX.Core.Models;

namespace CMiX.Core.Network.Messages
{
    public interface IAssetPathSelectorMessage
    {
        IAssetModel AssetModel { get; set; }
    }
}

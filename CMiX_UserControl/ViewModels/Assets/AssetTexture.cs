using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class AssetTexture : Asset, IAssets
    {
        public AssetTexture()
        {

        }

        public AssetTexture(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public override void SetViewModel(IAssetModel assetModel)
        {
            Name = assetModel.Name;
            Path = assetModel.Path;
            Ponderation = assetModel.Ponderation;
        }
    }
}
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

        public override IAssetModel GetModel()
        {
            IAssetModel assetModel = new AssetTextureModel();

            assetModel.Name = Name;
            assetModel.Path = Path;
            assetModel.Ponderation = Ponderation;

            return assetModel;
        }

        public override void SetViewModel(IAssetModel assetModel)
        {
            Name = assetModel.Name;
            Path = assetModel.Path;
            Ponderation = assetModel.Ponderation;
        }
    }
}
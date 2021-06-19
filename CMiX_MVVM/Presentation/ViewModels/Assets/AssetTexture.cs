using CMiX.Core.Interfaces;
using CMiX.Core.Models;

namespace CMiX.Core.Presentation.ViewModels.Assets
{
    public class AssetTexture : Asset
    {
        public AssetTexture()
        {

        }

        public AssetTexture(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public override IModel GetModel()
        {
            IAssetModel assetModel = new AssetTextureModel();

            assetModel.Name = this.Name;
            assetModel.Path = this.Path;
            assetModel.Ponderation = this.Ponderation;

            return assetModel;
        }

        public override void SetViewModel(IModel model)
        {
            AssetTextureModel assetTextureModel = model as AssetTextureModel;
            this.Name = assetTextureModel.Name;
            this.Path = assetTextureModel.Path;
            this.Ponderation = assetTextureModel.Ponderation;
        }
    }
}
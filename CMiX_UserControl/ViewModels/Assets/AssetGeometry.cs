using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class AssetGeometry : Asset, IAssets
    {
        public AssetGeometry()
        {

        }

        public AssetGeometry(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public override IAssetModel GetModel()
        {
            IAssetModel assetModel = new AssetGeometryModel();

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

using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels.Assets
{
    public class AssetGeometry : Asset
    {
        public AssetGeometry()
        {

        }

        public AssetGeometry(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public override IModel GetModel()
        {
            var assetModel = new AssetGeometryModel();

            assetModel.Name = this.Name;
            assetModel.Path = this.Path;
            assetModel.Ponderation = this.Ponderation;

            return assetModel;
        }

        public override void SetViewModel(IModel model)
        {
            AssetGeometryModel assetGeometryModel = new AssetGeometryModel();
            this.Name = assetGeometryModel.Name;
            this.Path = assetGeometryModel.Path;
            this.Ponderation = assetGeometryModel.Ponderation;
        }
    }
}

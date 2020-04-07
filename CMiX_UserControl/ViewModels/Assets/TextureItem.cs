using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.IO;

namespace CMiX.Studio.ViewModels
{
    public class TextureItem : Asset, IAssets
    {
        public TextureItem()
        {

        }

        public TextureItem(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public override IAssetModel GetModel()
        {
            IAssetModel assetModel = new AssetModel();

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
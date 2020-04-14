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

    }
}

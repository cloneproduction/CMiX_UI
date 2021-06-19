namespace CMiX.Core.Presentation.ViewModels.Assets
{
    public class AssetFactory
    {
        public AssetFactory()
        {

        }

        public AssetDirectory CreateRootDirectory(string name)
        {
            AssetDirectory assetDirectory = new AssetDirectory(name);
            assetDirectory.IsRoot = true;
            return assetDirectory;
        }

        public AssetDirectory CreateDirectory(string name)
        {
            return new AssetDirectory(name);
        }

        public AssetTexture CreateAssetTexture(string name, string path)
        {
            return new AssetTexture(name, path);
        }

        public AssetGeometry CreateAssetGeometry(string name, string path)
        {
            return new AssetGeometry(name, path);
        }

        //public Asset CreateAsset(Type type, string name)
        //{
        //    Asset asset = null;

        //    if (type == typeof(AssetDirectory))
        //        asset = new AssetDirectory();
        //    else if (type == typeof(AssetTexture))
        //        asset = new AssetTexture();
        //    else if (type == typeof(AssetGeometry))
        //        asset = new AssetGeometry();

        //    return asset;
        //}
    }
}

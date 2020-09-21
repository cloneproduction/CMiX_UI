using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class AssetPathSelectorModelFactory
    {
        public static AssetPathSelectorModel GetModel(this AssetPathSelector instance)
        {
            AssetPathSelectorModel model = new AssetPathSelectorModel();
            System.Console.WriteLine("AssetPathSelector GetModel");
            if (instance.SelectedAsset != null)
                model.SelectedAsset = instance.SelectedAsset.GetModel();

            return model;
        }

        public static void SetViewModel(this AssetPathSelector instance, AssetPathSelectorModel model)
        {
            System.Console.WriteLine("AssetPathSelector SetViewModel");
            if (instance.SelectedAsset == null)
            {
                if(model is AssetTextureModel)
                    instance.SelectedAsset = new AssetTexture();
                else if(model is AssetGeometryModel)
                    instance.SelectedAsset = new AssetGeometry();
            }       
                

            if (model.SelectedAsset != null)
                instance.SelectedAsset.SetViewModel(model.SelectedAsset);
        }
    }
}
using CMiX.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public static class AssetModelFactory
    {
        public static IAssetModel GetModel(this IAssets instance)
        {
            if (instance is AssetDirectory)
                return ((AssetDirectory)instance).GetModel();
            else if (instance is AssetTexture)
                return ((AssetTexture)instance).GetModel();
            else if (instance is AssetGeometry)
                return ((AssetGeometry)instance).GetModel();
            else return null;
        }

        public static void SetViewModel(this IAssets instance, IAssetModel model)
        {
            if (instance is AssetDirectory)
                ((AssetDirectory)instance).SetViewModel(model);
            else if (instance is AssetTexture)
                ((AssetTexture)instance).SetViewModel(model);
            else if (instance is AssetGeometry)
                ((AssetGeometry)instance).SetViewModel(model);
        }


        public static IAssetModel GetModel(this AssetDirectory instance)
        {
            IAssetModel directoryAssetModel = new AssetDirectoryModel();

            directoryAssetModel.Name = instance.Name;
            foreach (var asset in instance.Assets)
            {
                directoryAssetModel.AssetModels.Add(asset.GetModel());
            }
            return directoryAssetModel;
        }

        public static void SetViewModel(this AssetDirectory instance, IAssetModel model)
        {
            instance.Name = model.Name;
            instance.Assets.Clear();
            foreach (var assetModel in model.AssetModels)
            {
                IAssets asset = null;

                if (assetModel is AssetDirectoryModel)
                    asset = new AssetDirectory();
                else if (assetModel is AssetGeometryModel)
                    asset = new AssetGeometry();
                else if (assetModel is AssetTextureModel)
                    asset = new AssetTexture();

                if (asset != null)
                {
                    asset.SetViewModel(assetModel);
                    instance.Assets.Add(asset);
                }
            }
            instance.SortAssets();
        }


        public static IAssetModel GetModel(this AssetGeometry instance)
        {
            IAssetModel assetModel = new AssetGeometryModel();

            assetModel.Name = instance.Name;
            assetModel.Path = instance.Path;
            assetModel.Ponderation = instance.Ponderation;

            return assetModel;
        }

        public static void SetViewModel(this AssetGeometry instance, IAssetModel assetModel)
        {
            instance.Name = assetModel.Name;
            instance.Path = assetModel.Path;
            instance.Ponderation = assetModel.Ponderation;
        }


        public static IAssetModel GetModel(this AssetTexture instance)
        {
            IAssetModel assetModel = new AssetTextureModel();
            
            assetModel.Name = instance.Name;
            assetModel.Path = instance.Path;
            assetModel.Ponderation = instance.Ponderation;

            return assetModel;
        }

        public static void SetViewModel(this AssetTexture instance, IAssetModel assetModel)
        {
            instance.Name = assetModel.Name;
            instance.Path = assetModel.Path;
            instance.Ponderation = assetModel.Ponderation;
        }
    }
}

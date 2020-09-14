using CMiX.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public static class AssetPathSelectorModelFactory
    {
        public static AssetPathSelectorModel GetModel(this AssetPathSelector instance)
        {
            AssetPathSelectorModel model = new AssetPathSelectorModel();

            if (instance.SelectedPath != null)
                model.SelectedPath = instance.SelectedPath;

            return model;
        }

        public static void SetViewModel(this AssetPathSelector instance, AssetPathSelectorModel model)
        {
            if (model.SelectedPath != null)
                instance.SelectedPath = model.SelectedPath;
        }


        //public static AssetPathSelectorModel GetModel(this AssetPathSelector instance)
        //{
        //    AssetPathSelectorModel model = new AssetPathSelectorModel();

        //    if (instance.SelectedPath != null)
        //        model.SelectedPath = instance.SelectedPath;

        //    return model;
        //}

        //public static void SetViewModel(this AssetPathSelector instance, AssetPathSelectorModel model)
        //{
        //    if (model.SelectedPath != null)
        //        instance.SelectedPath = model.SelectedPath;
        //}


        //public static AssetPathSelectorModel GetModel(this AssetPathSelector instance)
        //{
        //    AssetPathSelectorModel model = new AssetPathSelectorModel();
        //    model.SelectedPath = instance.SelectedPath;
        //    return model;
        //}

        //public static void SetViewModel(this AssetPathSelector instance, AssetPathSelectorModel model)
        //{
        //    instance.SelectedPath = model.SelectedPath;
        //}
    }
}

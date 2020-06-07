using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class MaskModelFactory
    {
        public static MaskModel GetModel(this Mask instance)
        {
            MaskModel maskModel = new MaskModel();

            maskModel.IsMask = instance.IsMask;
            maskModel.MaskType = instance.MaskType;
            maskModel.MaskControlType = instance.MaskControlType;

            return maskModel;
        }


        public static void SetViewModel(this Mask instance, MaskModel model)
        {
            instance.IsMask = model.IsMask;
            instance.MaskType = model.MaskType;
            instance.MaskControlType = model.MaskControlType;
        }
    }
}
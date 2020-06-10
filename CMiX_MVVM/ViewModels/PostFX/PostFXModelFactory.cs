using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class PostFXModelFactory
    {
        public static PostFXModel GetModel(this PostFX instance)
        {
            PostFXModel postFXModel = new PostFXModel();
            postFXModel.Feedback = instance.Feedback.GetModel();
            postFXModel.Blur = instance.Blur.GetModel();
            postFXModel.Transforms = instance.Transforms;
            postFXModel.View = instance.View;
            return postFXModel;
        }

        public static void SetViewModel(this PostFX instance, PostFXModel postFXmodel)
        {
            instance.Transforms = postFXmodel.Transforms;
            instance.View = postFXmodel.View;
            instance.Feedback.SetViewModel(postFXmodel.Feedback);
            instance.Blur.SetViewModel(postFXmodel.Blur);
        }
    }
}
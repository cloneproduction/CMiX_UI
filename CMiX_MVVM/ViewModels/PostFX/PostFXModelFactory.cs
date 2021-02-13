using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

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

        public static void SetViewModel(this PostFX instance, PostFXModel model)
        {
            Set(instance, model);
        }

        //public static void SetViewModel(this PostFX instance, Message message)
        //{
        //    var model = message.Obj as PostFXModel;

        //    Set(instance, model);
        //}

        private static void Set(PostFX instance, PostFXModel model)
        {
            instance.Transforms = model.Transforms;
            instance.View = model.View;
            instance.Feedback.SetViewModel(model.Feedback);
            instance.Blur.SetViewModel(model.Blur);
        }
    }
}
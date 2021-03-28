using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class Transform : MessageCommunicator, IObserver
    {
        public Transform(MessageDispatcher messageDispatcher, TransformModel transformModel) : base (messageDispatcher)
        {
            Translate = new Translate(nameof(Translate), messageDispatcher, transformModel.TranslateModel);
            Scale = new Scale(nameof(Scale), messageDispatcher, transformModel.ScaleModel);
            Rotation = new Rotation(nameof(Rotation), messageDispatcher, transformModel.RotationModel);
            Is3D = false;
        }

        public Translate Translate { get; set; }
        public Scale Scale { get; set; }
        public Rotation Rotation { get; set; }

       
        private bool _is3D;
        public bool Is3D
        {
            get => _is3D;
            set
            {
                SetAndNotify(ref _is3D, value);
                RaiseMessageNotification();
            }
        }


        public void Update(int count)
        {
            //this.Count = count;
        }

        public override void SetViewModel(IModel model)
        {
            TransformModel transformModel = model as TransformModel;
            this.Translate.SetViewModel(transformModel.TranslateModel);
            this.Scale.SetViewModel(transformModel.ScaleModel);
            this.Rotation.SetViewModel(transformModel.RotationModel);
        }

        public override IModel GetModel()
        {
            TransformModel model = new TransformModel();
            model.TranslateModel = (TranslateModel)this.Translate.GetModel();
            model.ScaleModel = (ScaleModel)this.Scale.GetModel();
            model.RotationModel = (RotationModel)this.Rotation.GetModel();
            model.Is3D = this.Is3D;
            return model;
        }
    }
}
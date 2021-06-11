using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class Transform : Control, IObserver
    {
        public Transform(TransformModel transformModel)
        {
            Translate = new Translate(nameof(Translate), transformModel.TranslateModel);
            Scale = new Scale(nameof(Scale), transformModel.ScaleModel);
            Rotation = new Rotation(nameof(Rotation), transformModel.RotationModel);
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

            }
        }


        public void Update(int count)
        {
            //this.Count = count;
        }


        public override void SetCommunicator(ICommunicator communicator)
        {
            base.SetCommunicator(communicator);

            Translate.SetCommunicator(Communicator);
            Scale.SetCommunicator(Communicator);
            Rotation.SetCommunicator(Communicator);
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
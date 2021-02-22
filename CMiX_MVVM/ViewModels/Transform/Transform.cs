using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class Transform : MessageCommunicator, IObserver
    {
        public Transform(string name, IMessageProcessor parentSender) : base (name, parentSender)
        {
            Translate = new Translate(nameof(Translate), this);
            Scale = new Scale(nameof(Scale), this);
            Rotation = new Rotation(nameof(Rotation), this);
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
                this.MessageDispatcher?.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
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
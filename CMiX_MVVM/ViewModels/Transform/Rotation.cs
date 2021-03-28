using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Rotation : MessageCommunicator
    {
        public Rotation(string name, MessageDispatcher messageDispatcher, RotationModel rotationModel) : base (messageDispatcher)
        {
            X = new Slider(nameof(X), messageDispatcher, rotationModel.X);
            Y = new Slider(nameof(Y), messageDispatcher, rotationModel.Y);
            Z = new Slider(nameof(Z), messageDispatcher, rotationModel.Z);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }


        public override IModel GetModel()
        {
            RotationModel model = new RotationModel();
            model.X = (SliderModel)this.X.GetModel();
            model.Y = (SliderModel)this.Y.GetModel();
            model.Z = (SliderModel)this.Z.GetModel();
            return model;
        }

        public override void SetViewModel(IModel model)
        {
            RotationModel rotationModel = model as RotationModel;
            this.X.SetViewModel(rotationModel.X);
            this.Y.SetViewModel(rotationModel.Y);
            this.Z.SetViewModel(rotationModel.Z);
        }
    }
}
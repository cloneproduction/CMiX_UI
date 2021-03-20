using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Rotation : MessageCommunicator
    {
        public Rotation(string name, IMessageProcessor parentSender, RotationModel rotationModel) : base (name, parentSender)
        {
            X = new Slider(nameof(X), this, rotationModel.X);
            Y = new Slider(nameof(Y), this, rotationModel.Y);
            Z = new Slider(nameof(Z), this, rotationModel.Z);
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
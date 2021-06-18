using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class Rotation : Control
    {
        public Rotation(string name, RotationModel rotationModel)
        {
            this.ID = rotationModel.ID;
            X = new Slider(nameof(X), rotationModel.X);
            Y = new Slider(nameof(Y), rotationModel.Y);
            Z = new Slider(nameof(Z), rotationModel.Z);
        }


        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }


        public override void SetCommunicator(Communicator communicator)
        {
            base.SetCommunicator(communicator);

            X.SetCommunicator(Communicator);
            Y.SetCommunicator(Communicator);
            Z.SetCommunicator(Communicator);
        }

        public override IModel GetModel()
        {
            RotationModel model = new RotationModel();
            model.ID = this.ID;
            model.X = (SliderModel)this.X.GetModel();
            model.Y = (SliderModel)this.Y.GetModel();
            model.Z = (SliderModel)this.Z.GetModel();
            return model;
        }

        public override void SetViewModel(IModel model)
        {
            RotationModel rotationModel = model as RotationModel;
            this.ID = rotationModel.ID;
            this.X.SetViewModel(rotationModel.X);
            this.Y.SetViewModel(rotationModel.Y);
            this.Z.SetViewModel(rotationModel.Z);
        }
    }
}
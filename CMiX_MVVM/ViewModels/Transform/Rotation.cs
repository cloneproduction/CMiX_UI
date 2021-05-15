using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class Rotation : Module
    {
        public Rotation(string name, RotationModel rotationModel)
        {
            X = new Slider(nameof(X), rotationModel.X);
            Y = new Slider(nameof(Y), rotationModel.Y);
            Z = new Slider(nameof(Z), rotationModel.Z);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        //public override void SetReceiver(IMessageReceiver messageReceiver)
        //{
        //    //messageReceiver?.RegisterReceiver(this, ID);

        //    X.SetReceiver(messageReceiver);
        //    Y.SetReceiver(messageReceiver);
        //    Z.SetReceiver(messageReceiver);
        //}

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
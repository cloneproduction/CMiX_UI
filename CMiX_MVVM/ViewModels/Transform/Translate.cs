using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class Translate : Control, IObserver
    {
        public Translate(string name, TranslateModel translateModel)
        {
            this.ID = translateModel.ID;
            X = new Slider(nameof(X), translateModel.X);
            Y = new Slider(nameof(Y), translateModel.Y);
            Z = new Slider(nameof(Z), translateModel.Z);
        }


        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        public void Update(int count)
        {
            //XYZ = new Vector3D[count];
        }

        public override void SetCommunicator(Communicator communicator)
        {
            base.SetCommunicator(communicator);

            X.SetCommunicator(Communicator);
            Y.SetCommunicator(Communicator);
            Z.SetCommunicator(Communicator);
        }

        public override IModel GetModel()
        {
            TranslateModel model = new TranslateModel();
            model.ID = this.ID;
            model.X = (SliderModel)this.X.GetModel();
            model.Y = (SliderModel)this.Y.GetModel();
            model.Z = (SliderModel)this.Z.GetModel();
            return model;
        }

        public override void SetViewModel(IModel model)
        {
            TranslateModel translateModel = model as TranslateModel;
            this.ID = translateModel.ID;
            this.X.SetViewModel(translateModel.X);
            this.Y.SetViewModel(translateModel.Y);
            this.Z.SetViewModel(translateModel.Z);
        }
    }
}
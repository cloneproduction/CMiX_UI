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


        public override void SetReceiver(IMessageReceiver messageReceiver)
        {
            base.SetReceiver(messageReceiver);

            X.SetReceiver(messageReceiver);
            Y.SetReceiver(messageReceiver);
            Z.SetReceiver(messageReceiver);
        }

        public override void SetSender(IMessageSender messageSender)
        {
            base.SetSender(messageSender);

            X.SetSender(messageSender);
            Y.SetSender(messageSender);
            Z.SetSender(messageSender);
        }

        public override IModel GetModel()
        {
            TranslateModel model = new TranslateModel();
            model.X = (SliderModel)this.X.GetModel();
            model.Y = (SliderModel)this.Y.GetModel();
            model.Z = (SliderModel)this.Z.GetModel();
            return model;
        }

        public override void SetViewModel(IModel model)
        {
            TranslateModel translateModel = model as TranslateModel;
            this.X.SetViewModel(translateModel.X);
            this.Y.SetViewModel(translateModel.Y);
            this.Z.SetViewModel(translateModel.Z);
        }
    }
}
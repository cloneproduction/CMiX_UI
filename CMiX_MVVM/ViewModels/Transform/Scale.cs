using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Scale : MessageCommunicator
    {
        public Scale(string name, ScaleModel scaleModel)
        {
            Uniform = new Slider(nameof(Uniform), scaleModel.Uniform);
            Uniform.Amount = 1.0;

            X = new Slider(nameof(X), scaleModel.X);
            X.Amount = 1.0;

            Y = new Slider(nameof(Y), scaleModel.Y);
            Y.Amount = 1.0;

            Z = new Slider(nameof(Z), scaleModel.Z);
            Z.Amount = 1.0;

            IsUniform = true;
        }


        public override void SetModuleReceiver(ModuleMessageDispatcher messageDispatcher)
        {
            //messageDispatcher.RegisterMessageProcessor(this);
            Uniform.SetModuleReceiver(messageDispatcher);
            X.SetModuleReceiver(messageDispatcher);
            Y.SetModuleReceiver(messageDispatcher);
            Z.SetModuleReceiver(messageDispatcher);
        }


        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        public Slider Uniform { get; set; }

        private bool _isUniform;
        public bool IsUniform
        {
            get => _isUniform;
            set => SetAndNotify(ref _isUniform, value);
        }


        public override void SetViewModel(IModel model)
        {
            ScaleModel scaleModel = new ScaleModel();
            this.X.SetViewModel(scaleModel.X);
            this.Y.SetViewModel(scaleModel.Y);
            this.Z.SetViewModel(scaleModel.Z);
            this.Uniform.SetViewModel(scaleModel.Uniform);
        }

        public override IModel GetModel()
        {
            ScaleModel model = new ScaleModel();
            model.X = (SliderModel)this.X.GetModel();
            model.Y = (SliderModel)this.Y.GetModel();
            model.Z = (SliderModel)this.Z.GetModel();
            model.Uniform = (SliderModel)this.Uniform.GetModel();
            return model;
        }
    }
}

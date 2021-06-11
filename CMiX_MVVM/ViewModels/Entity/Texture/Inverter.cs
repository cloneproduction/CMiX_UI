using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class Inverter : Control
    {
        public Inverter(string name, InverterModel inverterModel)
        {
            this.ID = inverterModel.ID;
            Invert = new Slider(nameof(Invert), inverterModel.Invert);
            InvertMode = new ComboBox<TextureInvertMode>(inverterModel.InvertMode);
        }

        public Slider Invert { get; set; }
        public ComboBox<TextureInvertMode> InvertMode { get; set; }


        public override void SetCommunicator(ICommunicator communicator)
        {
            base.SetCommunicator(communicator);

            Invert.SetCommunicator(Communicator);
            InvertMode.SetCommunicator(Communicator);
        }

        public override void SetViewModel(IModel model)
        {
            InverterModel inverterModel = model as InverterModel;
            this.ID = inverterModel.ID;
            this.Invert.SetViewModel(inverterModel.Invert);
            this.InvertMode.SetViewModel(inverterModel.InvertMode);
        }

        public override IModel GetModel()
        {
            InverterModel model = new InverterModel();
            model.ID = this.ID;
            model.Invert = (SliderModel)this.Invert.GetModel();
            model.InvertMode = (ComboBoxModel<TextureInvertMode>)this.InvertMode.GetModel();
            return model;
        }
    }
}
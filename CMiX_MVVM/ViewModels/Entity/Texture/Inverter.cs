using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Inverter : ViewModel, IControl
    {
        public Inverter(string name, InverterModel inverterModel)
        {
            this.ID = inverterModel.ID;
            Invert = new Slider(nameof(Invert), inverterModel.Invert);
            InvertMode = new ComboBox<TextureInvertMode>(inverterModel.InvertMode);
        }


        public Guid ID{ get; set; }
        public ControlCommunicator Communicator { get; set; }
        public Slider Invert { get; set; }
        public ComboBox<TextureInvertMode> InvertMode { get; set; }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);

            Invert.SetCommunicator(Communicator);
            InvertMode.SetCommunicator(Communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);

            Invert.UnsetCommunicator(Communicator);
            InvertMode.UnsetCommunicator(Communicator);
        }


        public void SetViewModel(IModel model)
        {
            InverterModel inverterModel = model as InverterModel;
            this.ID = inverterModel.ID;
            this.Invert.SetViewModel(inverterModel.Invert);
            this.InvertMode.SetViewModel(inverterModel.InvertMode);
        }

        public IModel GetModel()
        {
            InverterModel model = new InverterModel();
            model.ID = this.ID;
            model.Invert = (SliderModel)this.Invert.GetModel();
            model.InvertMode = (ComboBoxModel<TextureInvertMode>)this.InvertMode.GetModel();
            return model;
        }
    }
}
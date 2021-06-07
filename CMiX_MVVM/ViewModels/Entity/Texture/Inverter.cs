using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class Inverter : Control
    {
        public Inverter(string name, InverterModel inverterModel) 
        {
            this.ID = inverterModel.ID;
            Invert = new Slider(nameof(Invert), inverterModel.Invert);
            InvertMode = inverterModel.InvertMode;
        }

        public Slider Invert { get; set; }

        private string _invertMode;
        public string InvertMode
        {
            get => _invertMode;
            set
            {
                SetAndNotify(ref _invertMode, value);

            }
        }

        public override void SetViewModel(IModel model)
        {
            InverterModel inverterModel = model as InverterModel;
            this.Invert.SetViewModel(inverterModel.Invert);
            this.InvertMode = inverterModel.InvertMode;
        }

        public override IModel GetModel()
        {
            InverterModel model = new InverterModel();
            model.Invert = (SliderModel)this.Invert.GetModel();
            model.InvertMode = this.InvertMode;
            return model;
        }
    }
}
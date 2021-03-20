using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class Inverter : MessageCommunicator
    {
        public Inverter(string name, IMessageProcessor parentSender, InverterModel inverterModel) : base (name, parentSender)
        {
            Invert = new Slider(nameof(Invert), this, inverterModel.Invert);
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
                this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
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
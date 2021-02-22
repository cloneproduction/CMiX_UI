using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Coloration : MessageCommunicator
    {
        public Coloration(string name, IMessageProcessor parentSender, MasterBeat masterBeat) : base (name, parentSender)
        {
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, masterBeat);
            ColorSelector = new ColorSelector(nameof(ColorSelector), this);
        }


        public ColorSelector ColorSelector { get; set; }
        public BeatModifier BeatModifier { get; set; }


        public override void SetViewModel(IModel model)
        {
            ColorationModel colorationModel = model as ColorationModel;
            this.ColorSelector.SetViewModel(colorationModel.ColorSelectorModel);
            this.BeatModifier.SetViewModel(colorationModel.BeatModifierModel);
        }

        public override IModel GetModel()
        {
            ColorationModel colorationModel = new ColorationModel();
            colorationModel.ColorSelectorModel = (ColorSelectorModel)this.ColorSelector.GetModel();
            colorationModel.BeatModifierModel = (BeatModifierModel)this.BeatModifier.GetModel();
            return colorationModel;
        }

        public override void Dispose()
        {
            BeatModifier.Dispose();
            ColorSelector.Dispose();
            base.Dispose();
        }
    }
}
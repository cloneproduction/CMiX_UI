using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Coloration : MessageCommunicator
    {
        public Coloration(IMessageDispatcher messageDispatcher, MasterBeat masterBeat, ColorationModel colorationModel) 
            : base (messageDispatcher, colorationModel)
        {
            BeatModifier = new BeatModifier(messageDispatcher, masterBeat, colorationModel.BeatModifierModel);
            ColorSelector = new ColorSelector(messageDispatcher, colorationModel.ColorSelectorModel);
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

        //public override void Dispose()
        //{
        //    BeatModifier.Dispose();
        //    ColorSelector.Dispose();
        //    base.Dispose();
        //}
    }
}
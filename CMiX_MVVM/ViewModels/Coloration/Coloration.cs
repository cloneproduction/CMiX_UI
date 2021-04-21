using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;

namespace CMiX.MVVM.ViewModels
{
    public class Coloration : Module
    {
        public Coloration(MasterBeat masterBeat, ColorationModel colorationModel) 
        {
            BeatModifier = new BeatModifier(masterBeat, colorationModel.BeatModifierModel);
            ColorSelector = new ColorSelector(colorationModel.ColorSelectorModel);
        }

        public override void SetReceiver(IMessageReceiver messageReceiver)
        {
            messageReceiver?.RegisterReceiver(this);

            BeatModifier.SetReceiver(messageReceiver);
            ColorSelector.SetReceiver(messageReceiver);
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
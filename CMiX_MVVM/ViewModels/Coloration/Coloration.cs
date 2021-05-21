using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;

namespace CMiX.MVVM.ViewModels
{
    public class Coloration : Control
    {
        public Coloration(MasterBeat masterBeat, ColorationModel colorationModel) 
        {
            this.ID = colorationModel.ID;
            //BeatModifier = new BeatModifier(masterBeat, colorationModel.BeatModifierModel);
            ColorSelector = new ColorSelector(colorationModel.ColorSelectorModel);
        }

        public override void SetReceiver(IMessageReceiver messageReceiver)
        {
            base.SetReceiver(messageReceiver);

            //BeatModifier.SetReceiver(messageReceiver);
            ColorSelector.SetReceiver(messageReceiver);
        }

        public override void SetSender(IMessageSender messageSender)
        {
            base.SetSender(messageSender);

            //BeatModifier.SetSender(messageSender);
            ColorSelector.SetSender(messageSender);
        }

        public ColorSelector ColorSelector { get; set; }
        public BeatModifier BeatModifier { get; set; }


        public override void SetViewModel(IModel model)
        {
            ColorationModel colorationModel = model as ColorationModel;
            colorationModel.ID = this.ID;
            this.ColorSelector.SetViewModel(colorationModel.ColorSelectorModel);
            //this.BeatModifier.SetViewModel(colorationModel.BeatModifierModel);
        }

        public override IModel GetModel()
        {
            ColorationModel model = new ColorationModel();
            model.ColorSelectorModel = (ColorSelectorModel)this.ColorSelector.GetModel();
            model.ID = this.ID;
            //colorationModel.BeatModifierModel = (BeatModifierModel)this.BeatModifier.GetModel();
            return model;
        }
    }
}
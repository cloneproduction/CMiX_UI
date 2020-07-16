using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Coloration : Sendable
    {
        #region CONSTRUCTORS
        public Coloration(MasterBeat beat) 
        {
            BeatModifier = new BeatModifier(beat);
            ColorSelector = new ColorSelector(this);

            //Hue = new RangeControl(nameof(Hue));
            //Saturation = new RangeControl(nameof(Saturation));
            //Value = new RangeControl(nameof(Value));
        }


        #endregion
        public Coloration(MasterBeat beat, Sendable parentSendable) : this(beat)
        {
            SubscribeToEvent(parentSendable);
        }


        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as ColorationModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand CopyColorationCommand { get; }
        public ICommand PasteColorationCommand { get; }
        public ICommand ResetColorationCommand { get; }

        public ColorSelector ColorSelector { get; }
        public BeatModifier BeatModifier { get; }
        //public RangeControl Hue { get; }
        //public RangeControl Saturation { get; }
        //public RangeControl Value { get; }
    }
}
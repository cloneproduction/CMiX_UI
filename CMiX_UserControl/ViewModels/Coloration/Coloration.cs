using System;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class Coloration : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Coloration(string messageaddress, Messenger messenger, Mementor mementor, Beat masterbeat) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Coloration));
            Messenger = messenger;
            Mementor = mementor;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, messenger, mementor);
            ColorSelector = new ColorSelector(MessageAddress, messenger, mementor);

            Hue = new RangeControl(MessageAddress + nameof(Hue), messenger, mementor);
            Saturation = new RangeControl(MessageAddress + nameof(Saturation), messenger, mementor);
            Value = new RangeControl(MessageAddress + nameof(Value), messenger, mementor);

            CopyColorationCommand = new RelayCommand(p => CopyColoration());
            PasteColorationCommand = new RelayCommand(p => PasteColoration());
            ResetColorationCommand = new RelayCommand(p => ResetColoration());
            ResetCommand = new RelayCommand(p => Reset());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand CopyColorationCommand { get; }
        public ICommand PasteColorationCommand { get; }
        public ICommand ResetColorationCommand { get; }

        public ColorSelector ColorSelector { get; }
        public BeatModifier BeatModifier { get; }
        public RangeControl Hue { get; }
        public RangeControl Saturation { get; }
        public RangeControl Value { get; }
        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            ColorSelector.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(ColorSelector)));
            BeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(BeatModifier)));
            Hue.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Hue)));
            Saturation.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Saturation)));
            Value.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Value)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(ColorationModel colorationmodel)
        {
            colorationmodel.MessageAddress = MessageAddress;
            ColorSelector.Copy(colorationmodel.ColorSelectorModel);
            BeatModifier.Copy(colorationmodel.BeatModifierModel);
            Hue.Copy(colorationmodel.HueDTO);
            Saturation.Copy(colorationmodel.SatDTO);
            Value.Copy(colorationmodel.ValDTO);
        }

        public void Paste(ColorationModel colorationmodel)
        {
            Messenger.Disable();

            MessageAddress = colorationmodel.MessageAddress;
            ColorSelector.Paste(colorationmodel.ColorSelectorModel);
            BeatModifier.Paste(colorationmodel.BeatModifierModel);
            Hue.Paste(colorationmodel.HueDTO);
            Saturation.Paste(colorationmodel.SatDTO);
            Value.Paste(colorationmodel.ValDTO);

            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();

            ColorSelector.Reset();
            BeatModifier.Reset();
            Hue.Reset();
            Saturation.Reset();
            Value.Reset();

            Messenger.Enable();
        }

        public void CopyColoration()
        {
            ColorationModel colorationmodel = new ColorationModel();
            this.Copy(colorationmodel);
            IDataObject data = new DataObject();
            data.SetData("ColorationModel", colorationmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteColoration()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ColorationModel"))
            {
                this.Mementor.BeginBatch();
                this.Messenger.Disable();

                var colorationmodel = data.GetData("ColorationModel") as ColorationModel;
                var colorationmessageaddress = MessageAddress;
                this.Paste(colorationmodel);
                this.UpdateMessageAddress(colorationmessageaddress);

                this.Copy(colorationmodel);
                this.Messenger.Enable();
                this.Mementor.EndBatch();

                //SendMessages(nameof(ColorationModel), colorationmodel);
            }
        }

        public void ResetColoration()
        {
            ColorationModel colorationmodel = new ColorationModel();
            this.Reset();
            this.Copy(colorationmodel);
            //SendMessages(nameof(Coloration), colorationmodel);
        }
        #endregion
    }
}
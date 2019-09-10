using System;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Coloration : ViewModel
    {
        #region CONSTRUCTORS
        public Coloration(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor, Beat masterbeat) 
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Coloration));

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, oscvalidation, mementor);

            Hue = new RangeControl(MessageAddress + nameof(Hue), oscvalidation, mementor);
            Saturation = new RangeControl(MessageAddress + nameof(Saturation), oscvalidation, mementor);
            Value = new RangeControl(MessageAddress + nameof(Value), oscvalidation, mementor);

            ColorSelector = new ColorSelector(MessageAddress + nameof(ColorSelector), oscvalidation, mementor);

            ResetCommand = new RelayCommand(p => Reset());

            CopyColorationCommand = new RelayCommand(p => CopyColoration());
            PasteColorationCommand = new RelayCommand(p => PasteColoration());
            ResetColorationCommand = new RelayCommand(p => ResetColoration());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand CopyColorationCommand { get; }
        public ICommand PasteColorationCommand { get; }
        public ICommand ResetColorationCommand { get; }

        public BeatModifier BeatModifier { get; }
        public RangeControl Hue { get; }
        public RangeControl Saturation { get; }
        public RangeControl Value { get; }

        public ColorSelector ColorSelector { get; }

        public ColorPicker.ViewModels.ColorPicker ColorPicker { get;  }

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

        #region COPY/PASTE
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
            DisabledMessages();

            MessageAddress = colorationmodel.MessageAddress;
            ColorSelector.Paste(colorationmodel.ColorSelectorModel);
            BeatModifier.Paste(colorationmodel.BeatModifierModel);
            Hue.Paste(colorationmodel.HueDTO);
            Saturation.Paste(colorationmodel.SatDTO);
            Value.Paste(colorationmodel.ValDTO);

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();

            ColorSelector.Reset();
            BeatModifier.Reset();
            Hue.Reset();
            Saturation.Reset();
            Value.Reset();

            EnabledMessages();
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
                Mementor.BeginBatch();
                var colorationmodel = data.GetData("ColorationModel") as ColorationModel;
                this.Paste(colorationmodel);
                Mementor.EndBatch();
            }
        }

        public void ResetColoration()
        {
            ColorationModel colorationmodel = new ColorationModel();
            this.Reset();
            this.Copy(colorationmodel);
            QueueObjects(colorationmodel);
            SendQueues();
        }
        #endregion
    }
}
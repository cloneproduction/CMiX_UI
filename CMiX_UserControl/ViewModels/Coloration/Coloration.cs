using System;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

using CMiX.MVVM.ViewModels;
using CMiX.Services;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.ColorPicker.ViewModels;

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

            ObjColor = Utils.HexStringToColor("#FF00FF");
            BgColor = Utils.HexStringToColor("#FF00FF");

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, oscvalidation, mementor);

            Hue = new RangeControl(MessageAddress + nameof(Hue), oscvalidation, mementor);
            Saturation = new RangeControl(MessageAddress + nameof(Saturation), oscvalidation, mementor);
            Value = new RangeControl(MessageAddress + nameof(Value), oscvalidation, mementor);

            ColorSelector = new ColorSelector(MessageAddress + nameof(ColorSelector), oscvalidation, mementor);


            ResetCommand = new RelayCommand(p => Reset());
            MouseDownCommand = new RelayCommand(p => MouseDown());

            CopyColorationCommand = new RelayCommand(p => CopyColoration());
            PasteColorationCommand = new RelayCommand(p => PasteColoration());
            ResetColorationCommand = new RelayCommand(p => ResetColoration());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand MouseDownCommand { get; }
        public ICommand CopyColorationCommand { get; }
        public ICommand PasteColorationCommand { get; }
        public ICommand ResetColorationCommand { get; }

        public BeatModifier BeatModifier { get; }
        public RangeControl Hue { get; }
        public RangeControl Saturation { get; }
        public RangeControl Value { get; }

        public ColorSelector ColorSelector { get; }

        public ColorPicker.ViewModels.ColorPicker ColorPicker { get;  }


        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                SetAndNotify(ref _selectedColor, value);
                //SendMessages(MessageAddress + nameof(SelectedColor), SelectedColor);
            }
        }

        private Color _objColor;
        public Color ObjColor
        {
            get => _objColor;
            set
            {
                SetAndNotify(ref _objColor, value);
                SendMessages(MessageAddress + nameof(ObjColor), ObjColor);
            }
        }

        private Color _bgColor;
        public Color BgColor
        {
            get => _bgColor;
            set
            {
                SetAndNotify(ref _bgColor, value);
                SendMessages(MessageAddress + nameof(BgColor), BgColor);
            }
        }
        #endregion
         
        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            BeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(BeatModifier)));
            Hue.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Hue)));
            Saturation.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Saturation)));
            Value.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Value)));
        }

        private void MouseDown()
        {
            Mementor.PropertyChange(this, "ObjColor");
        }
        #endregion

        #region COPY/PASTE
        public void Copy(ColorationModel colorationmodel)
        {
            colorationmodel.MessageAddress = MessageAddress;
            colorationmodel.ObjColor = Utils.ColorToHexString(ObjColor);
            colorationmodel.BgColor = Utils.ColorToHexString(BgColor);
            BeatModifier.Copy(colorationmodel.BeatModifierModel);
            Hue.Copy(colorationmodel.HueDTO);
            Saturation.Copy(colorationmodel.SatDTO);
            Value.Copy(colorationmodel.ValDTO);
        }

        public void Paste(ColorationModel colorationmodel)
        {
            DisabledMessages();

            MessageAddress = colorationmodel.MessageAddress;

            ObjColor = Utils.HexStringToColor(colorationmodel.ObjColor);
            BgColor = Utils.HexStringToColor(colorationmodel.BgColor);

            BeatModifier.Paste(colorationmodel.BeatModifierModel);
            Hue.Paste(colorationmodel.HueDTO);
            Saturation.Paste(colorationmodel.SatDTO);
            Value.Paste(colorationmodel.ValDTO);

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();

            ObjColor = Utils.HexStringToColor("#FF00FF");
            BgColor = Utils.HexStringToColor("#FF00FF");

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
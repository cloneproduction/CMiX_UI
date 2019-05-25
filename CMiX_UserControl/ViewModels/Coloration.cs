using System;
using System.Windows.Media;
using CMiX.Services;
using CMiX.Models;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Coloration : ViewModel
    {
        #region CONSTRUCTORS
        public Coloration(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor, Beat masterbeat) 
            : base(oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Coloration));

            ObjColor = Utils.HexStringToColor("#FF00FF");
            BgColor = Utils.HexStringToColor("#FF00FF");

            BeatModifier = new BeatModifier(MessageAddress, oscmessengers, masterbeat, mementor);

            Hue = new RangeControl(oscmessengers, MessageAddress + nameof(Hue), mementor);
            Saturation = new RangeControl(oscmessengers, MessageAddress + nameof(Saturation), mementor);
            Value = new RangeControl(oscmessengers, MessageAddress + nameof(Value), mementor);

            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
            MouseDownCommand = new RelayCommand(p => MouseDown());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }
        public ICommand MouseDownCommand { get; }

        public BeatModifier BeatModifier { get; }
        public RangeControl Hue { get; }
        public RangeControl Saturation { get; }
        public RangeControl Value { get; }

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
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Coloration));

            BeatModifier.UpdateMessageAddress(MessageAddress);
            Hue.UpdateMessageAddress(MessageAddress + nameof(Hue));
            Saturation.UpdateMessageAddress(MessageAddress + nameof(Saturation));
            Value.UpdateMessageAddress(MessageAddress + nameof(Value));
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

        public void CopySelf()
        {
            ColorationModel colorationmodel = new ColorationModel();
            Copy(colorationmodel);
            IDataObject data = new DataObject();
            data.SetData("Coloration", colorationmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Coloration"))
            {
                var colorationmodel = (ColorationModel)data.GetData("Coloration") as ColorationModel;
                Paste(colorationmodel);
                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            ColorationModel colorationmodel = new ColorationModel();
            var messageaddress = this.MessageAddress;
            Paste(colorationmodel);
            UpdateMessageAddress(messageaddress);
        }
        #endregion
    }
}
using System;
using System.Windows.Media;
using CMiX.Services;
using CMiX.Models;
using System.Windows.Input;
using System.Windows;
using GuiLabs.Undo;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Coloration : ViewModel
    {
        #region CONSTRUCTORS
        public Coloration(Beat masterbeat, string layername, ObservableCollection<OSCMessenger> messengers, ActionManager actionmanager, Mementor mementor)
        : this
        (
            actionmanager: actionmanager,
            messengers: messengers,
            messageaddress: String.Format("{0}/{1}/", layername, nameof(Coloration)),
            objColor: Colors.BlueViolet,
            bgColor: Colors.Black,
            backgroundColor: Colors.Black,
            beatModifier: new BeatModifier(String.Format("{0}/{1}", layername, nameof(Coloration)), messengers, masterbeat, actionmanager, mementor),
            hue: new RangeControl(messengers, String.Format("{0}/{1}", layername, nameof(Coloration)) + "/" + nameof(Hue), actionmanager, mementor),
            saturation: new RangeControl(messengers, String.Format("{0}/{1}", layername, nameof(Coloration)) + "/" + nameof(Saturation), actionmanager, mementor),
            value: new RangeControl(messengers, String.Format("{0}/{1}", layername, nameof(Coloration)) + "/" + nameof(Value), actionmanager, mementor)
        )
        { }

        public Coloration
            (
                ActionManager actionmanager,
                ObservableCollection<OSCMessenger> messengers,
                string messageaddress,
                BeatModifier beatModifier,
                Color objColor,
                Color bgColor,
                Color backgroundColor,
                RangeControl hue,
                RangeControl saturation,
                RangeControl value
            )
            : base(actionmanager, messengers)
        {
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
            MessageAddress = messageaddress;
            ObjColor = objColor;
            BgColor = bgColor;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Hue = hue ?? throw new ArgumentNullException(nameof(hue));
            Saturation = saturation ?? throw new ArgumentNullException(nameof(saturation));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

        public BeatModifier BeatModifier { get; }
        public RangeControl Hue { get; }
        public RangeControl Saturation { get; }
        public RangeControl Value { get; }

        private Color _objColor;
        [OSC]
        public Color ObjColor
        {
            get => _objColor;
            set
            {
                //SetAndRecord(() => _objColor, value);
                SetAndNotify(ref _objColor, value);
                SendMessages(MessageAddress + nameof(ObjColor), ObjColor);
            }
        }

        private Color _bgColor;
        [OSC]
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

        #region COPY/PASTE
        public void Copy(ColorationDTO colorationdto)
        {
            colorationdto.ObjColor = Utils.ColorToHexString(ObjColor);
            colorationdto.BgColor = Utils.ColorToHexString(BgColor);
            BeatModifier.Copy(colorationdto.BeatModifierDTO);
            Hue.Copy(colorationdto.HueDTO);
            Saturation.Copy(colorationdto.SatDTO);
            Value.Copy(colorationdto.ValDTO);
        }

        public void Paste(ColorationDTO colorationdto)
        {
            DisabledMessages();
            ObjColor = Utils.HexStringToColor(colorationdto.ObjColor);
            BgColor = Utils.HexStringToColor(colorationdto.BgColor);
            BeatModifier.Paste(colorationdto.BeatModifierDTO);
            Hue.Paste(colorationdto.HueDTO);
            Saturation.Paste(colorationdto.SatDTO);
            Value.Paste(colorationdto.ValDTO);
            EnabledMessages();
        }

        public void CopySelf()
        {
            ColorationDTO colorationdto = new ColorationDTO();
            this.Copy(colorationdto);
            IDataObject data = new DataObject();
            data.SetData("Coloration", colorationdto, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Coloration"))
            {
                var colorationdto = (ColorationDTO)data.GetData("Coloration") as ColorationDTO;
                this.Paste(colorationdto);

                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            ColorationDTO colorationdto = new ColorationDTO();
            this.Paste(colorationdto);
        }
        #endregion
    }
}
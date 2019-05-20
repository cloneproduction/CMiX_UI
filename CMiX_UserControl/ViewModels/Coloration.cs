﻿using System;
using System.Windows.Media;
using CMiX.Services;
using CMiX.Models;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Coloration : ViewModel
    {
        #region CONSTRUCTORS
        public Coloration(Beat masterbeat, string layername, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base(oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}/{1}/", layername, nameof(Coloration));
            ObjColor = Colors.BlueViolet;
            BgColor = Colors.Black;
            BeatModifier = new BeatModifier(String.Format("{0}/{1}", layername, nameof(Coloration)), oscmessengers, masterbeat, mementor);
            Hue = new RangeControl(oscmessengers, String.Format("{0}/{1}", layername, nameof(Coloration)) + "/" + nameof(Hue), mementor);
            Saturation = new RangeControl(oscmessengers, String.Format("{0}/{1}", layername, nameof(Coloration)) + "/" + nameof(Saturation), mementor);
            Value = new RangeControl(oscmessengers, String.Format("{0}/{1}", layername, nameof(Coloration)) + "/" + nameof(Value), mementor);
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
        [OSC]
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

        #region METHODS
        private void MouseDown()
        {
            Mementor.PropertyChange(this, "ObjColor");
        }
        #endregion

        #region COPY/PASTE
        public void Copy(ColorationModel colorationdto)
        {
            colorationdto.ObjColor = Utils.ColorToHexString(ObjColor);
            colorationdto.BgColor = Utils.ColorToHexString(BgColor);
            BeatModifier.Copy(colorationdto.BeatModifierModel);
            Hue.Copy(colorationdto.HueDTO);
            Saturation.Copy(colorationdto.SatDTO);
            Value.Copy(colorationdto.ValDTO);
        }

        public void Paste(ColorationModel colorationdto)
        {
            DisabledMessages();
            ObjColor = Utils.HexStringToColor(colorationdto.ObjColor);
            BgColor = Utils.HexStringToColor(colorationdto.BgColor);
            BeatModifier.Paste(colorationdto.BeatModifierModel);
            Hue.Paste(colorationdto.HueDTO);
            Saturation.Paste(colorationdto.SatDTO);
            Value.Paste(colorationdto.ValDTO);
            EnabledMessages();
        }

        public void CopySelf()
        {
            ColorationModel colorationdto = new ColorationModel();
            Copy(colorationdto);
            IDataObject data = new DataObject();
            data.SetData("Coloration", colorationdto, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Coloration"))
            {
                var colorationdto = (ColorationModel)data.GetData("Coloration") as ColorationModel;
                Paste(colorationdto);
                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            ColorationModel colorationdto = new ColorationModel();
            Paste(colorationdto);
        }
        #endregion
    }
}
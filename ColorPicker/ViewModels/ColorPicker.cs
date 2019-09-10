using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using ColorMine.ColorSpaces;
using Memento;

namespace CMiX.ColorPicker.ViewModels
{
    public class ColorPicker : ViewModel
    {
        public ColorPicker(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor)
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorPicker));
            SelectedColor = Color.FromArgb(255, 255, 0, 0);
        }

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                SetAndNotify(ref _selectedColor, value);
                Red = SelectedColor.R;
                Green = SelectedColor.G;
                Blue = SelectedColor.B;
            }
        }

        private byte _red;
        public byte Red
        {
            get => _red;
            set
            {
                SetAndNotify(ref _red, value);

                var hsv = new Rgb() { R = _selectedColor.R, G = _selectedColor.G, B = _selectedColor.B }.To<Hsv>();
                _hue = hsv.H;
                Notify("Hue");
                _sat = hsv.S;
                Notify("Sat");
                _val = hsv.V;
                Notify("Val");

                this._selectedColor.R = value;
                Notify("SelectedColor");
                SendMessages(MessageAddress + nameof(SelectedColor), SelectedColor.ToString());
            }
        }

        private byte _green;
        public byte Green
        {
            get => _green;
            set
            {
                SetAndNotify(ref _green, value);

                var hsv = new Rgb() { R = _selectedColor.R, G = _selectedColor.G, B = _selectedColor.B }.To<Hsv>();
                _hue = hsv.H;
                Notify("Hue");
                _sat = hsv.S;
                Notify("Sat");
                _val = hsv.V;
                Notify("Val");

                this._selectedColor.G = value;
                Notify("SelectedColor");
                SendMessages(MessageAddress + nameof(SelectedColor), SelectedColor.ToString());
            }
        }

        private byte _blue;
        public byte Blue
        {
            get => _blue;
            set
            {
                SetAndNotify(ref _blue, value);

                var hsv = new Rgb() { R = _selectedColor.R, G = _selectedColor.G, B = _selectedColor.B }.To<Hsv>();
                _hue = hsv.H;
                Notify("Hue");
                _sat = hsv.S;
                Notify("Sat");
                _val = hsv.V;
                Notify("Val");

                this._selectedColor.B = value;
                Notify("SelectedColor");
                SendMessages(MessageAddress + nameof(SelectedColor), SelectedColor.ToString());
            }
        }

        private double _hue;
        public double Hue
        {
            get => _hue;
            set
            {
                var hsv = new Rgb() { R = SelectedColor.R, G = SelectedColor.G, B = SelectedColor.B }.To<Hsv>();
                hsv.H = value;

                var rgb = hsv.To<Rgb>();
                _red = (byte)rgb.R;
                Notify("Red");
                _green = (byte)rgb.G;
                Notify("Green");
                _blue = (byte)rgb.B;
                Notify("Blue");

                SelectedColor = Color.FromRgb(_red, _green, _blue);
                Notify("SelectedColor");
                SetAndNotify(ref _hue, value);
                SendMessages(MessageAddress + nameof(SelectedColor), SelectedColor.ToString());
            }
        }

        private double _sat;
        public double Sat
        {
            get => _sat;
            set
            {
                var hsv = new Rgb() { R = SelectedColor.R, G = SelectedColor.G, B = SelectedColor.B }.To<Hsv>();
                hsv.V = _val;
                hsv.S = value;
                hsv.H = _hue;

                var rgb = hsv.To<Rgb>();
                _red = (byte)rgb.R;
                Notify("Red");
                _green = (byte)rgb.G;
                Notify("Green");
                _blue = (byte)rgb.B;
                Notify("Blue");

                SelectedColor = Color.FromRgb(_red, _green, _blue);
                Notify("SelectedColor");
                SetAndNotify(ref _sat, value);
                SendMessages(MessageAddress + nameof(SelectedColor), SelectedColor.ToString());
            }
        }

        private double _val;
        public double Val
        {
            get => _val;
            set
            {
                var hsv = new Rgb() { R = SelectedColor.R, G = SelectedColor.G, B = SelectedColor.B }.To<Hsv>();
                hsv.V = value;
                hsv.S = _sat;
                hsv.H = _hue;

                if (value > 0)
                {
                    var rgb = hsv.To<Rgb>();
                    _red = (byte)rgb.R;
                    Notify("Red");
                    _green = (byte)rgb.G;
                    Notify("Green");
                    _blue = (byte)rgb.B;
                    Notify("Blue");

                    SelectedColor = Color.FromRgb(_red, _green, _blue);
                }
                else
                {
                    SelectedColor = Color.FromRgb(0, 0, 0);
                }
                Notify("SelectedColor");
                SetAndNotify(ref _val, value);
                SendMessages(MessageAddress + nameof(SelectedColor), SelectedColor.ToString());
            }
        }


        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }


        public void Copy(ColorPickerModel colorpickermodel)
        {
            colorpickermodel.MessageAddress = MessageAddress;
            colorpickermodel.SelectedColor = Utils.ColorToHexString(SelectedColor);
        }

        public void Paste(ColorPickerModel colorpickermodel)
        {
            DisabledMessages();

            MessageAddress = colorpickermodel.MessageAddress;
            SelectedColor = Utils.HexStringToColor(colorpickermodel.SelectedColor);

            EnabledMessages();
        }

        public void Reset()
        {
            SelectedColor = Color.FromArgb(255, 255, 0, 0);
        }
    }
}
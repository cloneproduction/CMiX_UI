using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using ColorMine.ColorSpaces;
using Memento;
using System.Windows.Input;
using CMiX.MVVM.Commands;

namespace CMiX.ColorPicker.ViewModels
{
    public class ColorPicker : SendableViewModel
    {
        #region CONSTRUCTORS
        public ColorPicker(string messageaddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor)
            : base(serverValidations, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorPicker));

            SelectedColor = Color.FromArgb(255, 255, 0, 0);
            Red = SelectedColor.R;
            Green = SelectedColor.G;
            Blue = SelectedColor.B;
            MouseDown = false;

            PreviewMouseDownCommand = new RelayCommand(p => PreviewMouseDown());
            PreviewMouseUpCommand = new RelayCommand(p => PreviewMouseUp());
            PreviewMouseLeaveCommand = new RelayCommand(p => PreviewMouseLeave());
        }
        #endregion

        #region PROPERTIES
        public ICommand PreviewMouseDownCommand { get; }
        public ICommand PreviewMouseUpCommand { get; }
        public ICommand PreviewMouseLeaveCommand { get; }

        public bool MouseDown { get; set; }

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                SetAndNotify(ref _selectedColor, value);
                //UpdateMementor(nameof(SelectedColor));
            }
        }

        private void UpdateMementor(string propertyname)
        {
            if (MouseDown)
                Mementor.PropertyChange(this, propertyname);
            
        }

        private void SendSelectedColor()
        {
            ColorPickerModel colorPickerModel = new ColorPickerModel();
            this.Copy(colorPickerModel);
            SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, colorPickerModel);
        }

        private byte _red;
        public byte Red
        {
            get => _red;
            set
            {
                UpdateMementor(nameof(Red));
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
                SendSelectedColor();
            }
        }

        private byte _green;
        public byte Green
        {
            get => _green;
            set
            {
                UpdateMementor(nameof(Green));
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
                SendSelectedColor();
            }
        }

        private byte _blue;
        public byte Blue
        {
            get => _blue;
            set
            {
                UpdateMementor(nameof(Blue));
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
                SendSelectedColor();
            }
        }

        private double _hue;
        public double Hue
        {
            get => _hue;
            set
            {
                UpdateMementor(nameof(Hue));
                SetAndNotify(ref _hue, value);

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
                SendSelectedColor();
            }
        }

        private double _sat;
        public double Sat
        {
            get => _sat;
            set
            {
                UpdateMementor(nameof(Sat));
                SetAndNotify(ref _sat, value);

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
                SendSelectedColor();
            }
        }

        private double _val;
        public double Val
        {
            get => _val;
            set
            {
                UpdateMementor(nameof(Val));
                SetAndNotify(ref _val, value);

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
                SendSelectedColor();
            }
        }
        #endregion

        #region METHODS
        public void PreviewMouseDown()
        {
            if(!Mementor.IsInBatch)
                Mementor.BeginBatch();
            MouseDown = true;
        }

        public void PreviewMouseUp()
        {
            if(Mementor.IsInBatch)
                Mementor.EndBatch();
            MouseDown = false;
        }

        public void PreviewMouseLeave()
        {
            if(Mementor.IsInBatch)
                Mementor.EndBatch();
            MouseDown = false;
        }

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }
        #endregion

        #region COPY/PASTE/RESET
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
            Red = SelectedColor.R;
            Green = SelectedColor.G;
            Blue = SelectedColor.B;

            EnabledMessages();
        }

        public void Reset()
        {
            SelectedColor = Color.FromArgb(255, 255, 0, 0);
            Red = SelectedColor.R;
            Green = SelectedColor.G;
            Blue = SelectedColor.B;
        }
        #endregion
    }
}
﻿using CMiX.Core.Interfaces;
using CMiX.Core.MessageService;
using CMiX.Core.Models;
using CMiX.Core.Tools;
using ColorMine.ColorSpaces;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace CMiX.Core.Presentation.ViewModels
{
    public class ColorPicker : ViewModel, IControl
    {
        public ColorPicker(ColorPickerModel colorPickerModel)
        {
            SelectedColor = Utils.HexStringToColor(colorPickerModel.SelectedColor);
            this.ID = colorPickerModel.ID;
            Red = SelectedColor.R;
            Green = SelectedColor.G;
            Blue = SelectedColor.B;
            MouseDown = false;

            PreviewMouseDownCommand = new RelayCommand(p => PreviewMouseDown());
            PreviewMouseUpCommand = new RelayCommand(p => PreviewMouseUp());
            PreviewMouseLeaveCommand = new RelayCommand(p => PreviewMouseLeave());
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }
        public ICommand PreviewMouseDownCommand { get; set; }
        public ICommand PreviewMouseUpCommand { get; set; }
        public ICommand PreviewMouseLeaveCommand { get; set; }
        public bool MouseDown { get; set; }


        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                SetAndNotify(ref _selectedColor, value);
                Communicator?.SendMessageUpdateViewModel(this);
            }
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        private byte _red;
        public byte Red
        {
            get => _red;
            set
            {
                if (_red != value)
                {
                    SetAndNotify(ref _red, value);

                    var hsv = new Rgb() { R = _selectedColor.R, G = _selectedColor.G, B = _selectedColor.B }.To<Hsv>();

                    _hue = hsv.H;
                    Notify(nameof(Hue));
                    _sat = hsv.S;
                    Notify(nameof(Sat));
                    _val = hsv.V;
                    Notify(nameof(Val));

                    SelectedColor = Color.FromRgb(_red, _green, _blue);
                }
            }
        }


        private byte _green;
        public byte Green
        {
            get => _green;
            set
            {
                if (_green != value)
                {
                    SetAndNotify(ref _green, value);

                    var hsv = new Rgb() { R = _selectedColor.R, G = _selectedColor.G, B = _selectedColor.B }.To<Hsv>();
                    _hue = hsv.H;
                    Notify(nameof(Hue));
                    _sat = hsv.S;
                    Notify(nameof(Sat));
                    _val = hsv.V;
                    Notify(nameof(Val));

                    SelectedColor = Color.FromRgb(_red, _green, _blue);
                }
            }
        }


        private byte _blue;
        public byte Blue
        {
            get => _blue;
            set
            {
                if (_blue != value)
                {
                    SetAndNotify(ref _blue, value);

                    var hsv = new Rgb() { R = _selectedColor.R, G = _selectedColor.G, B = _selectedColor.B }.To<Hsv>();

                    _hue = hsv.H;
                    Notify(nameof(Hue));
                    _sat = hsv.S;
                    Notify(nameof(Sat));
                    _val = hsv.V;
                    Notify(nameof(Val));

                    SelectedColor = Color.FromRgb(_red, _green, _blue);
                }
            }
        }


        private double _hue;
        public double Hue
        {
            get => _hue;
            set
            {
                if (_hue != value)
                {
                    SetAndNotify(ref _hue, value);

                    var hsv = new Rgb() { R = SelectedColor.R, G = SelectedColor.G, B = SelectedColor.B }.To<Hsv>();
                    hsv.H = value;

                    var rgb = hsv.To<Rgb>();
                    _red = (byte)rgb.R;
                    Notify(nameof(Red));
                    _green = (byte)rgb.G;
                    Notify(nameof(Green));
                    _blue = (byte)rgb.B;
                    Notify(nameof(Blue));

                    SelectedColor = Color.FromRgb(_red, _green, _blue);
                }
            }
        }


        private double _sat;
        public double Sat
        {
            get => _sat;
            set
            {
                if (_sat != value)
                {
                    SetAndNotify(ref _sat, value);

                    var hsv = new Rgb() { R = SelectedColor.R, G = SelectedColor.G, B = SelectedColor.B }.To<Hsv>();
                    hsv.V = _val;
                    hsv.S = value;
                    hsv.H = _hue;

                    var rgb = hsv.To<Rgb>();
                    _red = (byte)rgb.R;
                    Notify(nameof(Red));
                    _green = (byte)rgb.G;
                    Notify(nameof(Green));
                    _blue = (byte)rgb.B;
                    Notify(nameof(Blue));

                    SelectedColor = Color.FromRgb(_red, _green, _blue);
                }
            }
        }


        private double _val;
        public double Val
        {
            get => _val;
            set
            {
                if (_val != value)
                {
                    SetAndNotify(ref _val, value);

                    var hsv = new Rgb() { R = SelectedColor.R, G = SelectedColor.G, B = SelectedColor.B }.To<Hsv>();
                    hsv.V = value;
                    hsv.S = _sat;
                    hsv.H = _hue;

                    if (value > 0)
                    {
                        var rgb = hsv.To<Rgb>();
                        _red = (byte)rgb.R;
                        Notify(nameof(Red));
                        _green = (byte)rgb.G;
                        Notify(nameof(Green));
                        _blue = (byte)rgb.B;
                        Notify(nameof(Blue));

                        SelectedColor = Color.FromRgb(_red, _green, _blue);
                    }
                    else
                    {
                        SelectedColor = Color.FromRgb(0, 0, 0);
                    }
                }
            }
        }


        public void PreviewMouseDown()
        {
            MouseDown = true;
        }

        public void PreviewMouseUp()
        {
            MouseDown = false;
        }

        public void PreviewMouseLeave()
        {
            MouseDown = false;
        }


        public void SetViewModel(IModel model)
        {
            ColorPickerModel colorPickerModel = model as ColorPickerModel;
            this.ID = colorPickerModel.ID;
            this.SelectedColor = Utils.HexStringToColor(colorPickerModel.SelectedColor);
            System.Console.WriteLine("ColorPicker SetViewModel Color " + SelectedColor);
        }

        public IModel GetModel()
        {
            ColorPickerModel model = new ColorPickerModel();
            model.ID = this.ID;
            model.SelectedColor = Utils.ColorToHexString(this.SelectedColor);
            return model;
        }
    }
}
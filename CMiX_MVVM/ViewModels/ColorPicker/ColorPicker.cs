using System.Windows.Media;
using System.Windows.Input;
using ColorMine.ColorSpaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class ColorPicker : Sender
    {
        public ColorPicker(string name, IMessageProcessor parentSender) : base (name, parentSender)
        {
            SelectedColor = Color.FromArgb(255, 0, 255, 0);
            Red = SelectedColor.R;
            Green = SelectedColor.G;
            Blue = SelectedColor.B;
            MouseDown = false;

            PreviewMouseDownCommand = new RelayCommand(p => PreviewMouseDown());
            PreviewMouseUpCommand = new RelayCommand(p => PreviewMouseUp());
            PreviewMouseLeaveCommand = new RelayCommand(p => PreviewMouseLeave());
        }

        public override void Receive(IMessage message)
        {
            this.SetViewModel(message.Obj as ColorPickerModel);
        }

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
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
            }
        }

        private void UpdateMementor(string propertyname)
        {
            //if (MouseDown)
               // Mementor.PropertyChange(this, propertyname);
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
                Notify(nameof(Hue));
                _sat = hsv.S;
                Notify(nameof(Sat));
                _val = hsv.V;
                Notify(nameof(Val));

                this._selectedColor.R = value;
                Notify(nameof(SelectedColor));
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
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
                Notify(nameof(Hue));
                _sat = hsv.S;
                Notify(nameof(Sat));
                _val = hsv.V;
                Notify(nameof(Val));

                this._selectedColor.G = value;
                Notify(nameof(SelectedColor));
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
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
                Notify(nameof(Hue));
                _sat = hsv.S;
                Notify(nameof(Sat));
                _val = hsv.V;
                Notify(nameof(Val));

                this._selectedColor.B = value;
                Notify(nameof(SelectedColor));
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
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
                Notify(nameof(Red));
                _green = (byte)rgb.G;
                Notify(nameof(Green));
                _blue = (byte)rgb.B;
                Notify(nameof(Blue));

                SelectedColor = Color.FromRgb(_red, _green, _blue);
                Notify(nameof(SelectedColor));
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
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
                Notify(nameof(Red));
                _green = (byte)rgb.G;
                Notify(nameof(Green));
                _blue = (byte)rgb.B;
                Notify(nameof(Blue));

                SelectedColor = Color.FromRgb(_red, _green, _blue);
                Notify(nameof(SelectedColor));
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
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
                Notify(nameof(SelectedColor));
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
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
    }
}
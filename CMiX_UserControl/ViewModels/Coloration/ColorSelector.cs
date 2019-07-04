using CMiX.Services;
using Memento;
using ColorMine;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace CMiX.ViewModels
{
    public class ColorSelector : ViewModel
    {
        public ColorSelector(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, ObservableCollection<OSCValidation> cansendmessage, Mementor mementor) 
            : base(oscmessengers, cansendmessage, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorSelector));
            SelectedColor = Color.FromArgb(255, 51, 125, 253);

            Value = new SliderValue(this, MessageAddress + nameof(Value), oscmessengers, cansendmessage, mementor);
            Saturation = new SliderSaturation(this, MessageAddress + nameof(Value), oscmessengers, cansendmessage, mementor);
            HueWheel = new SliderHue(this, MessageAddress + nameof(HueWheel), oscmessengers, cansendmessage, mementor);

        }



        public SliderValue Value { get; }
        public SliderSaturation Saturation { get; }
        public SliderHue HueWheel { get; }

        private Color _selectedcolor;
        public Color SelectedColor
        {
            get { return _selectedcolor; }
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "SelectedColor");
                SetAndNotify(ref _selectedcolor, value);
                if(ColorChangedEvent != null)
                    ColorChangedEvent(SelectedColor);
                //SendMessages(MessageAddress + nameof(SelectedColor), SelectedColor);
            }
        }

        #region EVENT
        public delegate void ColorChanged(Color selectedcolor);
        public event ColorChanged ColorChangedEvent;
        #endregion
    }
}

using CMiX.Services;
using CMiX.Models;
using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Memento;
using System.Windows.Media;

namespace CMiX.ViewModels
{
    public class SliderValue : ViewModel
    {
        #region CONSTRUCTORS
        public SliderValue(ColorSelector colorselector, string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) 
            : base (oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}/", messageaddress);
            ColorSelector = colorselector;
            Amount = 0.0;
            
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
            MouseDownCommand = new RelayCommand(p => MouseDown());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }

        public ICommand MouseDownCommand { get; }
        public ICommand DragCompletedCommand { get; }
        public ICommand ValueChangedCommand { get; }

        public ColorSelector ColorSelector { get; set; }

        private Color _leftcolor;
        public Color LeftColor
        {
            get { return _leftcolor; }
            set
            {
                SetAndNotify(ref _leftcolor, value);
            }
        }

        private Color _rightcolor;
        public Color RightColor
        {
            get { return _rightcolor; }
            set
            {
                SetAndNotify(ref _rightcolor, value);
            }
        }

        private void MouseDown()
        {
            if(Mementor != null)
                Mementor.PropertyChange(this, "Amount");     
        }

        private double _amount;
        public double Amount
        {
            get => _amount;
            set
            {
                SetAndNotify(ref _amount, value);
                double hue, sat, val;
                Utils.ConvertRgbToHsv(ColorSelector.SelectedColor, out hue, out sat, out val);
                LeftColor = Utils.ConvertHsvToRgb(hue, sat, 0.0);
                RightColor = Utils.ConvertHsvToRgb(hue, sat, 1.0);

                ColorSelector.SelectedColor = Utils.ConvertHsvToRgb(hue, sat, Amount);
            }
        }
        #endregion

        #region ADD/SUB
        private void Add()
        {
            if (Amount < 1.0)
                Amount += 0.01;
        }

        private void Sub()
        {
            if (Amount > 0.0)
                Amount -= 0.01;
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            DisabledMessages();
            Amount = 0.0;
            EnabledMessages();
        }

        public void Copy(SliderModel slidermodel)
        {
            slidermodel.Amount = Amount;
            slidermodel.MessageAddress = MessageAddress;
        }

        public void Paste(SliderModel slidermodel)
        {
            DisabledMessages();
            MessageAddress = slidermodel.MessageAddress;
            Amount = slidermodel.Amount;
            EnabledMessages();
        }
        #endregion
    }
}

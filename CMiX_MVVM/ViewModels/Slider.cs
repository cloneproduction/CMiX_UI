
using System;
using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

using System.Collections.ObjectModel;
using Memento;

namespace CMiX.MVVM.ViewModels
{
    public class Slider : ViewModel
    {
        #region CONSTRUCTORS
        public Slider(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) : base (oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}/", messageaddress);
            Amount = 0.0;

            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
            ResetCommand = new RelayCommand(p => ResetSlider());
            CopySliderCommand = new RelayCommand(p => CopySlider());
            PasteSliderCommand = new RelayCommand(p => PasteSlider());
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
        public ICommand ResetCommand { get; }
        public ICommand CopySliderCommand { get; }
        public ICommand PasteSliderCommand { get; }
        public ICommand MouseDownCommand { get; }
        public ICommand DragCompletedCommand { get; }
        public ICommand ValueChangedCommand { get; }

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
                SendMessages(MessageAddress + nameof(Amount), Amount);
            }
        }

        private double minimum = 0.0;
        public double Minimum
        {
            get { return minimum; }
            set { minimum = value; }
        }

        private double maximum = 1.0;
        public double Maximum
        {
            get { return maximum; }
            set { maximum = value; }
        }

        #endregion

        #region ADD/SUB
        private void Add()
        {
            if (Amount >= Maximum)
                Amount = Maximum;
            else
                Amount += 0.01;
        }

        private void Sub()
        {
            if (Amount <= Minimum)
                Amount = Minimum;
            else
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

        public void ResetSlider()
        {
            Amount = 0.0;
        }

        public void CopySlider()
        {
            SliderModel slidermodel = new SliderModel();
            this.Copy(slidermodel);
            IDataObject data = new DataObject();
            data.SetData("SliderModel", slidermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSlider()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("SliderModel"))
            {
                Mementor.BeginBatch();
                var slidermodel = data.GetData("SliderModel") as SliderModel;
                this.Paste(slidermodel);
                Mementor.EndBatch();
            }
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


using System;
using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Slider : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Slider(string messageaddress, Messenger messenger, Mementor mementor)
        {
            MessageAddress = $"{messageaddress}/";
            Messenger = messenger;

            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());

            ResetSliderCommand = new RelayCommand(p => ResetSlider());
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
        public ICommand ResetSliderCommand { get; }

        public ICommand MouseDownCommand { get; }
        public ICommand DragCompletedCommand { get; }
        public ICommand ValueChangedCommand { get; }

        private void MouseDown()
        {
            if(Mementor != null)
                Mementor.PropertyChange(this, "Amount");     
        }

        private double _amount = 0.0;
        public double Amount
        {
            get => _amount;
            set
            {
                SetAndNotify(ref _amount, value);
                SliderModel sliderModel = new SliderModel();
                this.Copy(sliderModel);
                Messenger.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, sliderModel);
            }
        }

        private double _minimum = 0.0;
        public double Minimum
        {
            get => _minimum; 
            set => SetAndNotify(ref _minimum, value);
        }

        private double _maximum = 1.0;
        public double Maximum
        {
            get => _maximum; 
            set => SetAndNotify(ref _maximum, value);
        }
        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        
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
            Messenger.Disable();
            Amount = 0.0;
            Messenger.Enable();
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
            Messenger.Disable();
            MessageAddress = slidermodel.MessageAddress;
            Amount = slidermodel.Amount;
            Messenger.Enable();
        }
        #endregion
    }
}

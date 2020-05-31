using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Slider : Sendable
    {
        public Slider(string name)
        {
            Name = name;

            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());

            ResetCommand = new RelayCommand(p => Reset());
            CopySliderCommand = new RelayCommand(p => CopySlider());
            PasteSliderCommand = new RelayCommand(p => PasteSlider());
            MouseDownCommand = new RelayCommand(p => MouseDown());
        }

        public Slider(string name, Sendable parentSendable) : this(name)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
            {
                this.SetViewModel(e.Model as SliderModel);
                Console.WriteLine(this.Name + " ViewModelUpdated");
                Console.WriteLine("Amount = " + this.Amount);
                Console.WriteLine("----------------------------");
            }
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

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
            //if(Mementor != null)
            //    Mementor.PropertyChange(this, "Amount");     
        }

        public override string GetMessageAddress()
        {
            return $"{Name}/";
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }


        private double _amount;
        public double Amount
        {
            get => _amount;
            set
            {
                SetAndNotify(ref _amount, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
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
        #endregion

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


        public void Reset()
        {
            Amount = 0.0;
        }

        public void CopySlider()
        {
            IDataObject data = new DataObject();
            //data.SetData("SliderModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSlider()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("SliderModel"))
            {
                //Mementor.BeginBatch();
                var slidermodel = data.GetData("SliderModel") as SliderModel;
                //this.SetViewModel(slidermodel);
                //Mementor.EndBatch();
            }
        }
    }
}

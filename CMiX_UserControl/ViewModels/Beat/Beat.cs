﻿using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public abstract class Beat : ViewModel
    {
        #region CONSTRUCTOR
        public Beat()
        {
            ResetCommand = new RelayCommand(p => Reset());
            MultiplyCommand = new RelayCommand(p => Multiply());
            DivideCommand = new RelayCommand(p => Divide());
        }
        #endregion

        #region PROPERTIES
        public ICommand ResetCommand { get; }
        public ICommand MultiplyCommand { get; }
        public ICommand DivideCommand { get; }

        //public MessageService MessageService { get; set; }
        public abstract double Period { get; set; }

        private double _bpm;
        public double BPM
        {
            get
            {
                _bpm = 60000 / Period;
                if (double.IsInfinity(_bpm) || double.IsNaN(_bpm))
                    return 0;
                else
                    return _bpm;
            }
            set
            {
                Period = 60000 / value;
                _bpm = value;
            }
        }

        private double _multiplier;
        public virtual double Multiplier
        {
            get => _multiplier;
            set => SetAndNotify(ref _multiplier, value);
        }

        private void Reset() => Multiplier = 1;

        protected abstract void Multiply();
        protected abstract void Divide();
        #endregion

        #region EVENTS
        public delegate void PeriodChangedEventHandler(Beat sender, double newValue);

        public event PeriodChangedEventHandler PeriodChanged;

        protected void OnPeriodChanged(double newPeriod) => PeriodChanged?.Invoke(this, newPeriod);
        #endregion

        public MasterBeatModel GetModel()
        {
            MasterBeatModel masterBeatModel = new MasterBeatModel();
            masterBeatModel.Period = Period;
            return masterBeatModel;
            
        }

        public void SetViewModel(MasterBeatModel masterBeatControl)
        {
            //MessageService.Disable();
            Period = masterBeatControl.Period;
            //MessageService.Enable();
        }
    }
}
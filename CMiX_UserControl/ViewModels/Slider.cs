using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;
using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Memento;


namespace CMiX.ViewModels
{
    public class Slider : ViewModel
    {
        #region CONSTRUCTORS
        public Slider(string layername, ObservableCollection<OSCMessenger> messengers, ActionManager actionmanager, Mementor mementor)
            : this
            (
                messageaddress: String.Format("{0}/", layername),
                messengers: messengers,
                amount: 0.0,
                mementor: mementor,
                actionmanager: actionmanager
            )
        {}

        public Slider
            (
                ObservableCollection<OSCMessenger> messengers,
                string messageaddress,
                double amount,
                Mementor mementor,
                ActionManager actionmanager
            )
            : base(actionmanager, messengers)
        {
            Mementor = mementor;
            MessageAddress = messageaddress;
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
            Amount = amount;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
            MouseDownCommand = new RelayCommand(p => MouseDown());
            ValueChangedCommand = new RelayCommand(p => ValueChanged());
        }
        #endregion

        #region PROPERTIES
        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }

        public ICommand MouseDownCommand { get; }
        public ICommand DragCompletedCommand { get; }
        public ICommand ValueChangedCommand { get; }

        private void ValueChanged()
        {
            
        }

        private void MouseDown()
        {
            Mementor.PropertyChange(this, "Amount");
        }

        private double _amount;
        [OSC]
        public double Amount
        {
            get => _amount;
            set
            {
                SetAndNotify(ref _amount, value);
                SendMessages(MessageAddress + nameof(Amount), Amount);
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

        #region COPY/PASTE
        public void Copy(SliderDTO sliderdto)
        {
            sliderdto.Amount = Amount;
        }

        public void Paste(SliderDTO sliderdto)
        {
            DisabledMessages();

            Amount = sliderdto.Amount;

            EnabledMessages();
        }
        #endregion
    }
}

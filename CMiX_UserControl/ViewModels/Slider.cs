using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;
using System;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class Slider : ViewModel
    {
        #region CONSTRUCTORS
        public Slider(string layername, OSCMessenger messenger, ActionManager actionmanager)
            : this
            (
                messageaddress: String.Format("{0}/", layername),
                messenger: messenger,
                amount: 0.0,
                actionmanager: actionmanager
            )
        {}

        public Slider
            (
                OSCMessenger messenger,
                string messageaddress,
                double amount,
                ActionManager actionmanager
            )
            : base(actionmanager, messenger)
        {
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Amount = amount;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }
        #endregion

        #region PROPERTIES
        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }

        private double _amount;
        [OSC]
        public double Amount
        {
            get => _amount;
            set
            {
                SetAndNotify(ref _amount, value);
                Messenger.SendMessage(MessageAddress + nameof(Amount), Amount);
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
            Messenger.SendEnabled = false;
            Amount = sliderdto.Amount;
            Messenger.SendEnabled = true;
        }
        #endregion
    }
}

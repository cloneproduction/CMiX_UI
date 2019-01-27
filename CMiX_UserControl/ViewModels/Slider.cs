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
                val: 0.0,
                actionmanager: actionmanager,
                messenger: messenger,
                messageaddress: String.Format(layername)
            )
        {}

        public Slider
            (
                double val,
                OSCMessenger messenger,
                string messageaddress,
                ActionManager actionmanager
            )
            : base(actionmanager, messenger)
        {
            Val = val;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }

        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }

        private double _val;
        [OSC]
        public double Val
        {
            get => _val;
            set
            {
                SetAndNotify(ref _val, value);
                Messenger.SendMessage(MessageAddress, Val);
            }
        }
        #endregion

        #region ADD/SUB
        private void Add()
        {
            if (Val < 1.0)
                Val += 0.01;
        }

        private void Sub()
        {
            if (Val > 0.0)
                Val -= 0.01;
        }
        #endregion

        #region COPY/PASTE
        public void Copy(SliderDTO sliderdto)
        {
            sliderdto.Val = Val;
        }

        public void Paste(SliderDTO sliderdto)
        {
            Messenger.SendEnabled = false;
            Val = sliderdto.Val;
            Messenger.SendEnabled = true;
        }
        #endregion
    }
}

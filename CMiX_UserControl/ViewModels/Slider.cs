using CMiX.Services;
using GuiLabs.Undo;
using System;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class Slider : ViewModel, IMessengerData
    {
        #region CONSTRUCTORS
        public Slider(string layername, IMessenger messenger, ActionManager actionmanager)
            : this
            (
                val: 0.0,
                actionmanager: actionmanager,
                messenger: messenger,
                messageaddress: String.Format(layername),
                messageEnabled: true,
                enable: false
            )
        { }

        public Slider
            (
                double val,
                IMessenger messenger,
                string messageaddress,
                bool messageEnabled,
                bool enable,
                ActionManager actionmanager
            )
            : base(actionmanager)
        {
            Val = val;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }
        #endregion

        private void Sub()
        {
            if (Val > 0.0)
            {
                Val -= 0.01;
            }
        }

        private void Add()
        {
            if (Val < 1.0)
            {
                Val += 0.01;
            }
        }

        #region PROPERTIES
        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

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
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress, Val);
            }
        }
        #endregion
    }
}

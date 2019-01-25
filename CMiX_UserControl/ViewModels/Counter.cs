using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class Counter : ViewModel, IMessengerData
    {
        #region CONSTRUCTORS
        public Counter(string layername, IMessenger messenger, ActionManager actionmanager)
                : this
                (
                    actionmanager: actionmanager,
                    messenger: messenger,
                    messageaddress: layername,
                    messageEnabled: true,
                    count: 1
                )
        {}

        public Counter
            (
                IMessenger messenger,
                string messageaddress,
                bool messageEnabled,
                int count,
                ActionManager actionmanager
            )
                : base (actionmanager)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
            Count = count;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }
        #endregion

        #region PROPERTIES
        public IMessenger Messenger { get; }
        public string MessageAddress { get; set; }
        public bool MessageEnabled { get; set; }

        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }

        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                SetAndNotify(ref _count, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress, Count);
            }
        }
        #endregion

        #region ADD/SUB
        private void Add()
        {
            Count *= 2;
        }

        private void Sub()
        {
            if (Count > 1)
                Count /= 2;
        }
        #endregion

        #region COPY/PASTE
        public void Copy(CounterDTO counterdto)
        {
            counterdto.Count = Count;
        }

        public void Paste(CounterDTO counterdto)
        {
            Count = counterdto.Count;
        }
        #endregion
    }
}
using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class Counter : ViewModel
    {
        #region CONSTRUCTORS
        public Counter(string layername, OSCMessenger messenger, ActionManager actionmanager)
                : this
                (
                    actionmanager: actionmanager,
                    messenger: messenger,
                    messageaddress: String.Format("{0}/", layername),
                    count: 1
                )
        {}

        public Counter
            (
                OSCMessenger messenger,
                string messageaddress,
                int count,
                ActionManager actionmanager
            )
            : base (actionmanager, messenger)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            Count = count;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }
        #endregion

        #region PROPERTIES
        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }

        private int _count;
        [OSC]
        public int Count
        {
            get { return _count; }
            set
            {
                SetAndNotify(ref _count, value);
                Messenger.SendMessage(MessageAddress + nameof(Count), Count);
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
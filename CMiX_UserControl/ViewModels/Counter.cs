using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace CMiX.ViewModels
{
    public class Counter : ViewModel
    {
        #region CONSTRUCTORS
        public Counter(string layername, ObservableCollection<OSCMessenger> messengers, ActionManager actionmanager)
                : this
                (
                    actionmanager: actionmanager,
                    messengers: messengers,
                    messageaddress: String.Format("{0}/", layername),
                    count: 1
                )
        {}

        public Counter
            (
                ObservableCollection<OSCMessenger> messengers,
                string messageaddress,
                int count,
                ActionManager actionmanager
            )
            : base (actionmanager, messengers)
        {
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
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
                SendMessages(MessageAddress + nameof(Count), Count);
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
            DisabledMessages();
            Count = counterdto.Count;
            EnabledMessages();
        }
        #endregion
    }
}
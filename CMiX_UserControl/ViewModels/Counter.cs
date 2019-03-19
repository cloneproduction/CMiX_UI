using System;
using CMiX.Services;
using CMiX.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Counter : ViewModel
    {
        #region CONSTRUCTORS
        public Counter(string layername, ObservableCollection<OSCMessenger> messengers, Mementor mementor)
                : this
                (
                    mementor: mementor,
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
                Mementor mementor
            )
            : base (messengers)
        {
            Mementor = mementor;
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
                Mementor.PropertyChange(this, "Count");
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
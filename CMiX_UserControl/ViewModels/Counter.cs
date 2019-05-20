﻿using System;
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
        public Counter(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base (oscmessengers, mementor)
        {
            MessageAddress = messageaddress;
            Count = 1;
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
                if(Mementor != null)
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
        public void Copy(CounterModel counterdto)
        {
            counterdto.Count = Count;
        }

        public void Paste(CounterModel counterdto)
        {
            DisabledMessages();
            Count = counterdto.Count;
            EnabledMessages();
        }
        #endregion
    }
}
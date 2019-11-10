using System;

using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

using System.Windows.Input;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Counter : ViewModel
    {
        #region CONSTRUCTORS
        public Counter(string messageaddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor) 
            : base (serverValidations, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Counter));
            Count = 1;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }

        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, "Count");
                SetAndNotify(ref _count, value);
                //SendMessages(MessageAddress + nameof(Count), Count);
            }
        }
        #endregion

        #region ADD/SUB
        private void Add()
        {
            Count += 1;
        }

        private void Sub()
        {
            if (Count > 1)
                Count -= 1;
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            DisabledMessages();
            Count = 1;
            EnabledMessages();
        }

        public void Copy(CounterModel countermodel)
        {
            countermodel.MessageAddress = MessageAddress;
            countermodel.Count = Count;
        }

        public void Paste(CounterModel countermodel)
        {
            DisabledMessages();

            MessageAddress = countermodel.MessageAddress;
            Count = countermodel.Count;

            EnabledMessages();
        }
        #endregion
    }
}
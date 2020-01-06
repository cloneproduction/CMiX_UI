using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class Counter : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Counter(string messageaddress, Sender sender, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Counter));
            Sender = sender;
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

        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
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
            Sender.Enable();
            Count = 1;
            Sender.Enable();
        }

        public void Copy(CounterModel countermodel)
        {
            countermodel.MessageAddress = MessageAddress;
            countermodel.Count = Count;
        }

        public void Paste(CounterModel countermodel)
        {
            Sender.Enable();

            MessageAddress = countermodel.MessageAddress;
            Count = countermodel.Count;

            Sender.Enable();
        }
        #endregion
    }
}
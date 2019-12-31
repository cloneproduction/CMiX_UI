using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class Counter : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Counter(string messageaddress, Messenger messenger, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Counter));
            Messenger = messenger;
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
        public Messenger Messenger { get; set; }
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
            Messenger.Enable();
            Count = 1;
            Messenger.Enable();
        }

        public void Copy(CounterModel countermodel)
        {
            countermodel.MessageAddress = MessageAddress;
            countermodel.Count = Count;
        }

        public void Paste(CounterModel countermodel)
        {
            Messenger.Enable();

            MessageAddress = countermodel.MessageAddress;
            Count = countermodel.Count;

            Messenger.Enable();
        }
        #endregion
    }
}
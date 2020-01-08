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
        public Counter(string messageaddress, MessageService messageService, Mementor mementor) 
        {
            MessageAddress = $"{messageaddress}{nameof(Counter)}/";
            MessageService = messageService;
            Count = 1;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
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
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }
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
            MessageService.Enable();
            Count = 1;
            MessageService.Enable();
        }

        public void Copy(CounterModel countermodel)
        {
            countermodel.Count = Count;
        }

        public void Paste(CounterModel countermodel)
        {
            MessageService.Enable();

            Count = countermodel.Count;

            MessageService.Enable();
        }
        #endregion
    }
}
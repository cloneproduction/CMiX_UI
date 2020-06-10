using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class Counter : Sendable
    {
        public Counter() 
        {
            Count = 1;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }

        public Counter(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as CounterModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }

        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                SetAndNotify(ref _count, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private void Add() => Count += 1;

        private void Sub()
        {
            if (Count > 1)
                Count -= 1;
        }
    }
}
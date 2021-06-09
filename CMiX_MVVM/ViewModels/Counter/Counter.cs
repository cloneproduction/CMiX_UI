using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using System;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class Counter : Control
    {
        public Counter(CounterModel counterModel)
        {
            this.ID = counterModel.ID;
            Count = 1;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }


        public event EventHandler<CounterEventArgs> CounterChangeEvent;
        public void OnCountChange()
        {
            CounterChangeEvent?.Invoke(this, new CounterEventArgs(this.Count));
        }


        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                SetAndNotify(ref _count, value);
                OnCountChange();
                MessageSender?.SendMessage(new MessageUpdateViewModel(this));
            }
        }

        private void Add() => Count += 1;

        private void Sub()
        {
            if (Count > 1)
                Count -= 1;
        }

        public override void SetViewModel(IModel model)
        {
            CounterModel counterModel = model as CounterModel;
            this.Count = counterModel.Count;
        }

        public override IModel GetModel()
        {
            CounterModel model = new CounterModel();
            model.Count = this.Count;
            return model;
        }
    }
}
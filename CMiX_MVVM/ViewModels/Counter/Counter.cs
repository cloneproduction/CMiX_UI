using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class Counter : MessageCommunicator
    {
        public Counter(string name, IMessageProcessor parentSender) : base(name, parentSender) 
        {
            Count = 1;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }

        //public override void Receive(IMessage message)
        //{
        //    base.Receive(message);
        //    OnCountChange();
        //}

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
                this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
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
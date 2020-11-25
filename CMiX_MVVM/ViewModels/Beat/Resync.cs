﻿using CMiX.MVVM.Models.Beat;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class Resync : Sender
    {
        public Resync(string name, IColleague parentSender, BeatAnimations beatAnimations) : base (name, parentSender)
        {
            BeatAnimations = beatAnimations;
            ResyncCommand = new RelayCommand(p => DoResync());
        }


        //public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        //{
        //    if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
        //    {
        //        if (Resynced)
        //            Resynced = false;
        //        else
        //            Resynced = true;
        //    }
        //}
        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as ResyncModel);
        }

        public BeatAnimations BeatAnimations { get; set; }

        private bool _resynced;
        public bool Resynced
        {
            get { return _resynced; }
            set { _resynced = value; }
        }

        public void DoResync()
        {
            BeatAnimations.ResetAnimation();
            OnBeatResync();
            this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
        }

        public ICommand ResyncCommand { get; }

        public event EventHandler BeatResync;
        public void OnBeatResync()
        {
            EventHandler handler = BeatResync;
            if (null != handler) handler(this, EventArgs.Empty);
        }
    }
}

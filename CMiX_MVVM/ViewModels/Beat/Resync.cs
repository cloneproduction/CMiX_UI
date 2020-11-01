using CMiX.MVVM.Services;
using System;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class Resync : Sender
    {
        public Resync(BeatAnimations beatAnimations)
        {
            BeatAnimations = beatAnimations;
            ResyncCommand = new RelayCommand(p => DoResync());
        }

        public Resync(BeatAnimations beatAnimations, Sender parentSender) : this(beatAnimations)
        {
            SubscribeToEvent(parentSender);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
            {
                if (Resynced)
                    Resynced = false;
                else
                    Resynced = true;
            }
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
            OnSendChange(this.GetModel(), this.GetMessageAddress());
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

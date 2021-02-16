using CMiX.MVVM.Models.Beat;
using System;
using System.Windows.Input;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels
{
    public class Resync : Sender
    {
        public Resync(string name, IMessageProcessor parentSender, BeatAnimations beatAnimations) : base (name, parentSender)
        {
            BeatAnimations = beatAnimations;
            ResyncCommand = new RelayCommand(p => DoResync());
        }

        public override void Receive(IMessage message)
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
            this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
        }

        public ICommand ResyncCommand { get; }

        public event EventHandler BeatResync;
        public void OnBeatResync()
        {
            EventHandler handler = BeatResync;
            if (null != handler) handler(this, EventArgs.Empty);
        }

        public override void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public override IModel GetModel()
        {
            ResyncModel model = new ResyncModel();

            return model;
        }
    }
}

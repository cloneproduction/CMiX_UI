using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models.Beat;
using CMiX.MVVM.ViewModels.MessageService;
using System;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Beat
{
    public class Resync : MessageCommunicator, IMessageProcessor
    {
        public Resync(BeatAnimations beatAnimations, ResyncModel resyncModel) 
        {
            BeatAnimations = beatAnimations;
            ResyncCommand = new RelayCommand(p => DoResync());
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

        public override void SetModuleReceiver(ModuleMessageDispatcher messageDispatcher)
        {
            throw new NotImplementedException();
        }
    }
}
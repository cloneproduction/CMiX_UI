using CMiX.Core.Interfaces;
using CMiX.Core.MessageService;
using CMiX.Core.Models.Beat;
using System;
using System.Windows.Input;

namespace CMiX.Core.Presentation.ViewModels.Beat
{
    public class Resync : ViewModel, IControl
    {
        public Resync(BeatAnimations beatAnimations, ResyncModel resyncModel) 
        {
            BeatAnimations = beatAnimations;
            ResyncCommand = new RelayCommand(p => DoResync());
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }
        public BeatAnimations BeatAnimations { get; set; }
        public ICommand ResyncCommand { get; }

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


        public event EventHandler BeatResync;
        public void OnBeatResync()
        {
            EventHandler handler = BeatResync;
            if (null != handler) handler(this, EventArgs.Empty);
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public void SetViewModel(IModel model)
        {
            ResyncModel resyncModel = model as ResyncModel;
            this.ID = resyncModel.ID;
        }

        public IModel GetModel()
        {
            ResyncModel model = new ResyncModel();
            model.ID = this.ID;
            return model;
        }
    }
}
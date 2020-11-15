using CMiX.MVVM.Interfaces;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Layer : Component, IBeat
    {
        public Layer(int id, MessengerManager messengerManager, MasterBeat beat) : base(id, messengerManager)
        {
            MasterBeat = beat;
            PostFX = new PostFX();
            BlendMode = new BlendMode();
            Fade = new Slider(nameof(Fade), this, this.MessageMediator);
        }

        private bool _out;
        public bool Out
        {
            get => _out;
            set => SetAndNotify(ref _out, value);
        }

        public MasterBeat MasterBeat { get; set; }
        public Slider Fade { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BlendMode BlendMode { get; set; }
    }
}
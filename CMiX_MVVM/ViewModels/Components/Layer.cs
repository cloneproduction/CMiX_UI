using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Layer : Component, IBeat, IComponent
    {
        public Layer(int id, MessengerTerminal messengerTerminal, MasterBeat beat) : base(id, messengerTerminal)
        {
            MasterBeat = beat;
            PostFX = new PostFX(nameof(PostFX), this);
            BlendMode = new BlendMode(nameof(BlendMode), this);
            Fade = new Slider(nameof(Fade), this);
            ComponentFactory = new SceneFactory(messengerTerminal);
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
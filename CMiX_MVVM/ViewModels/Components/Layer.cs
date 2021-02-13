using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Layer : Component, IBeat
    {
        public Layer(int id, MessageTerminal MessageTerminal, MasterBeat beat) : base (id, MessageTerminal)
        {
            MasterBeat = beat;
            PostFX = new PostFX(nameof(PostFX), this);
            BlendMode = new BlendMode(nameof(BlendMode), this);
            Fade = new Slider(nameof(Fade), this);
            Mask = new Mask(nameof(Mask), this);
            ComponentFactory = new SceneFactory(MessageTerminal);
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

        public override void Dispose()
        {
            Fade.Dispose();
            Mask.Dispose();
            PostFX.Dispose();
            BlendMode.Dispose();
            base.Dispose();
        }
    }
}
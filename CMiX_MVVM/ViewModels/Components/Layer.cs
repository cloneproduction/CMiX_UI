using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;

namespace CMiX.MVVM.ViewModels
{
    public class Layer : Component, IBeat
    {
        public Layer(int id, MasterBeat beat) : base(id)
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
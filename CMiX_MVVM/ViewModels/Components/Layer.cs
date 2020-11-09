using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels
{
    public class Layer : Component, IBeat
    {
        public Layer(int id, MasterBeat beat) : base(id)
        {
            MasterBeat = beat;
            PostFX = new PostFX();
            BlendMode = new BlendMode(this);
            Fade = new Slider(nameof(Fade), this);
        }

        private bool _out;
        public bool Out
        {
            get => _out;
            set => SetAndNotify(ref _out, value);
        }

        private double _sliderTest;
        public double SliderTest
        {
            get => _sliderTest;
            set
            {
                SetAndNotify(ref _sliderTest, value);
                SendMessage(this.GetMessageAddress(), this.GetModel());
            }
        }

        public Slider Fade { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BlendMode BlendMode { get; set; }
        public MasterBeat MasterBeat { get; set; }
    }
}
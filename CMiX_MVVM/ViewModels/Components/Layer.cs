

using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Layer : Component
    {
        public Layer(int id, Beat beat) : base(id, beat)
        {
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

        public Slider Fade { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BlendMode BlendMode { get; set; }
    }
}
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Scene : Component
    {
        public Scene(int id, Beat beat) : base(id, beat)
        {
            BeatModifier = new BeatModifier(beat);
            PostFX = new PostFX();
            Mask = new Mask(this);
        }

        public Mask Mask { get; set; }
        public BeatModifier BeatModifier { get; set; }
        public PostFX PostFX { get; set; }
    }
}
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
        }

        public BeatModifier BeatModifier { get; }
        public PostFX PostFX { get; }
    }
}
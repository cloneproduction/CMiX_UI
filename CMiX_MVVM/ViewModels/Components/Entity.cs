using System;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services.Message;
using PubSub;

namespace CMiX.MVVM.ViewModels
{
    public class Entity : Component, IBeat
    {
        public Entity(int id, MasterBeat beat) : base(id)
        {
            BeatModifier = new BeatModifier(beat);
            Geometry = new Geometry(beat, this);
            Texture = new Texture(this);
            Coloration = new Coloration(beat, this);
        }

        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public Coloration Coloration { get; }
        public MasterBeat MasterBeat { get; set; }
    }
}
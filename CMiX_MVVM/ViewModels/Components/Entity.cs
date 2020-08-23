using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;

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

        public ICommand CopyEntityCommand { get; }
        public ICommand PasteEntityCommand { get; }
        public ICommand ResetEntityCommand { get; }
        
        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public Coloration Coloration { get; }
        public MasterBeat MasterBeat { get; set; }
    }
}
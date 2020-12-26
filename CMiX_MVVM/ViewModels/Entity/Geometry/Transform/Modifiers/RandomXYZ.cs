using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class RandomXYZ : Sender, ITransformModifier
    {
        public RandomXYZ(string name, Sender parentSender, MasterBeat masterBeat) : base (name, parentSender)
        {
            Random = new Random();
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, masterBeat);
            SelectedModifierType = ModifierType.OBJECT;
        }


        public BeatModifier BeatModifier { get; set; }
        private Random Random { get; set; }
        public Range Range { get; set; }
        public Counter Counter { get; set; }

        private int _count;
        public int Count
        {
            get => _count;
            set => SetAndNotify(ref _count, value);
        }

        private bool _randomizeLocation;
        public bool RandomizeLocation
        {
            get => _randomizeLocation;
            set => SetAndNotify(ref _randomizeLocation, value);
        }

        private bool _randomizeScale;
        public bool RandomizeScale
        {
            get => _randomizeScale;
            set => SetAndNotify(ref _randomizeScale, value);
        }

        private bool _randomizeRotation;
        public bool RandomizeRotation
        {
            get => _randomizeRotation;
            set => SetAndNotify(ref _randomizeRotation, value);
        }



        private ModifierType _selectedModifierType;
        public ModifierType SelectedModifierType
        {
            get => _selectedModifierType;
            set => SetAndNotify(ref _selectedModifierType, value);
        }

        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }
    }
}

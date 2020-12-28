using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using System;
using System.Windows.Media.Media3D;

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

        private TransformType _selectedTransformType;
        public TransformType SelectedTransformType
        {
            get => _selectedTransformType;
            set => SetAndNotify(ref _selectedTransformType, value);
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

        private Vector3D[] oldRandom;
        private Vector3D[] newRandom;

        private Vector3D[] GetNewRandoms(int count)
        {
            var rands = new Vector3D[count];

            for (int i = 0; i < count; i++)
            {
                rands[i].X = Random.NextDouble();
                rands[i].Y = Random.NextDouble();
                rands[i].Z = Random.NextDouble();
            }
            return rands;
        }

        public void UpdateOnBeatTick(Vector3D[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {
            oldRandom = newRandom;
            if (beatModifier.CheckHitOnBeatTick())
                newRandom = GetNewRandoms(doubleToAnimate.Length);
        }

        public void UpdateOnGameLoop(Vector3D[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {
            bool ease = easing.IsEnabled;

            for (int i = 0; i < doubleToAnimate.Length; i++)
            {
                if (ease)
                {
                    double eased = Easings.Interpolate((float)period, easing.SelectedEasing);
                    Vector3D lerped = Utils.Lerp(oldRandom[i], newRandom[i], eased);

                    doubleToAnimate[i].X = Utils.Map(lerped.X, 0.0, 1.0, 0.0 - range.Width / 2, 0.0 + range.Width / 2);
                    doubleToAnimate[i].Y = Utils.Map(lerped.Y, 0.0, 1.0, 0.0 - range.Width / 2, 0.0 + range.Width / 2);
                    doubleToAnimate[i].Z = Utils.Map(lerped.Z, 0.0, 1.0, 0.0 - range.Width / 2, 0.0 + range.Width / 2);
                }
                else
                {
                    doubleToAnimate[i].X = Utils.Map(newRandom[i].X, 0.0, 1.0, 0.0 - range.Width / 2, 0.0 + range.Width / 2);
                    doubleToAnimate[i].Y = Utils.Map(newRandom[i].Y, 0.0, 1.0, 0.0 - range.Width / 2, 0.0 + range.Width / 2);
                    doubleToAnimate[i].Z = Utils.Map(newRandom[i].Z, 0.0, 1.0, 0.0 - range.Width / 2, 0.0 + range.Width / 2);
                }
            }
        }
    }
}

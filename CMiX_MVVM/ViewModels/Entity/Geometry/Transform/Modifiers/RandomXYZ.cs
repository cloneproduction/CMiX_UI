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
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, masterBeat);
            SelectedModifierType = ModifierType.OBJECT;
        }


        public BeatModifier BeatModifier { get; set; }
        public Range Range { get; set; }
        public Counter Counter { get; set; }

        public Slider SliderX { get; set; }
        public Slider SliderY { get; set; }
        public Slider SliderZ { get; set; }

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

        //private Vector3D[] GetNewRandoms(int count)
        //{
        //    var rands = new Vector3D[count];

        //    for (int i = 0; i < count; i++)
        //    {
        //        rands[i].X = RandomNumbers.RandomDouble(-SliderX.Amount + 2.0, SliderX.Amount);
        //        rands[i].Y = RandomNumbers.RandomDouble(-SliderY.Amount + 2.0, SliderY.Amount);
        //        rands[i].Z = RandomNumbers.RandomDouble(-SliderZ.Amount + 2.0, SliderZ.Amount);
        //    }
        //    return rands;
        //}

        public void UpdateOnBeatTick(Vector3D[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {
            if(newRandom.Length != doubleToAnimate.Length)
                newRandom = new Vector3D[doubleToAnimate.Length];

            oldRandom = newRandom;

            for (int i = 0; i < doubleToAnimate.Length; i++)
            {
                if (beatModifier.CheckHitOnBeatTick())
                {
                    var org_X = doubleToAnimate[i].X;
                    var org_Y = doubleToAnimate[i].Y;
                    var org_Z = doubleToAnimate[i].Z;

                    var sca_X = RandomNumbers.RandomDouble(-SliderX.Amount + 2.0, SliderX.Amount);
                    var sca_Y = RandomNumbers.RandomDouble(-SliderY.Amount + 2.0, SliderY.Amount);
                    var sca_Z = RandomNumbers.RandomDouble(-SliderZ.Amount + 2.0, SliderZ.Amount);

                    newRandom[i].X = sca_X * org_X;
                    newRandom[i].Y = sca_Y * org_Y;
                    newRandom[i].Z = sca_Z * org_Z;
                }
            }
        }

        public void UpdateOnGameLoop(Vector3D[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {
            for (int i = 0; i < doubleToAnimate.Length; i++)
            {
                if (easing.IsEnabled)
                {
                    double eased = Easings.Interpolate((float)period, easing.SelectedEasing);
                    Vector3D lerped = Utils.Lerp(oldRandom[i], newRandom[i], eased);

                    doubleToAnimate[i].X = lerped.X;
                    doubleToAnimate[i].Y = lerped.Y;
                    doubleToAnimate[i].Z = lerped.Z;
                }
                else
                {
                    doubleToAnimate[i].X = newRandom[i].X;
                    doubleToAnimate[i].Y = newRandom[i].Y;
                    doubleToAnimate[i].Z = newRandom[i].Z;
                }
            }
        }
    }
}

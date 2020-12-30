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


        public Slider LocationX { get; set; }
        public Slider LocationY { get; set; }
        public Slider LocationZ { get; set; }

        public Slider ScaleX { get; set; }
        public Slider ScaleY { get; set; }
        public Slider ScaleZ { get; set; }

        public Slider RotationX { get; set; }
        public Slider RotationY { get; set; }
        public Slider RotationZ { get; set; }


        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                SetAndNotify(ref _count, value);
                Location = new Vector3D[value];
                Scale = new Vector3D[value];
                Rotation = new Vector3D[value];
            }
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

        public Vector3D[] Location { get; set; }
        public Vector3D[] Scale { get; set; }
        public Vector3D[] Rotation { get; set; }

        private Vector3D[] PreviousLocation { get; set; }
        private Vector3D[] PreviousScale { get; set; }
        private Vector3D[] PreviousRotation { get; set; }

        private Vector3D[] NextLocation { get; set; }
        private Vector3D[] NextScale { get; set; }
        private Vector3D[] NextRotation { get; set; }

        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }


        //private void RandomizeVector(Vector3D vector3D)
        //{
        //    var org_X = vector3D.X;
        //    var org_Y = vector3D.Y;
        //    var org_Z = vector3D.Z;

        //    var new_X = RandomNumbers.RandomDouble(-LocationX.Amount + 2.0, LocationX.Amount);
        //    var new_Y = RandomNumbers.RandomDouble(-LocationY.Amount + 2.0, LocationY.Amount);
        //    var new_Z = RandomNumbers.RandomDouble(-LocationZ.Amount + 2.0, LocationZ.Amount);

        //    var aX = new_X * org_X;
        //    var aY = new_Y * org_Y;
        //    var aZ = new_Z * org_Z;

        //    Location[i] = new Vector3D(aX, aY, aZ);
        //}

        public void UpdateOnBeatTick(double period)
        {
            PreviousLocation = NextLocation;
            PreviousScale = NextScale;
            PreviousRotation = NextRotation;

            for (int i = 0; i < Count; i++)
            {
                if (BeatModifier.CheckHitOnBeatTick())
                {
                    if (RandomizeLocation)
                    {
                        var org_X = Location[i].X;
                        var org_Y = Location[i].Y;
                        var org_Z = Location[i].Z;

                        var new_X = RandomNumbers.RandomDouble(-LocationX.Amount + 2.0, LocationX.Amount);
                        var new_Y = RandomNumbers.RandomDouble(-LocationY.Amount + 2.0, LocationY.Amount);
                        var new_Z = RandomNumbers.RandomDouble(-LocationZ.Amount + 2.0, LocationZ.Amount);

                        var aX = new_X * org_X;
                        var aY = new_Y * org_Y;
                        var aZ = new_Z * org_Z;

                        Location[i] = new Vector3D(aX, aY, aZ);
                    }

                    if (RandomizeScale)
                    {
                        var org_X = Scale[i].X;
                        var org_Y = Scale[i].Y;
                        var org_Z = Scale[i].Z;

                        var new_X = RandomNumbers.RandomDouble(-ScaleX.Amount + 2.0, ScaleX.Amount);
                        var new_Y = RandomNumbers.RandomDouble(-ScaleY.Amount + 2.0, ScaleY.Amount);
                        var new_Z = RandomNumbers.RandomDouble(-ScaleZ.Amount + 2.0, ScaleZ.Amount);

                        var aX = new_X * org_X;
                        var aY = new_Y * org_Y;
                        var aZ = new_Z * org_Z;

                        Scale[i] = new Vector3D(aX, aY, aZ);
                    }

                    if (RandomizeRotation)
                    {
                        var org_X = Rotation[i].X;
                        var org_Y = Rotation[i].Y;
                        var org_Z = Rotation[i].Z;

                        var new_X = RandomNumbers.RandomDouble(-RotationX.Amount + 2.0, RotationX.Amount);
                        var new_Y = RandomNumbers.RandomDouble(-RotationY.Amount + 2.0, RotationY.Amount);
                        var new_Z = RandomNumbers.RandomDouble(-RotationZ.Amount + 2.0, RotationZ.Amount);

                        var aX = new_X * org_X;
                        var aY = new_Y * org_Y;
                        var aZ = new_Z * org_Z;

                        Rotation[i] = new Vector3D(aX, aY, aZ);
                    }
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

                    Vector3D lerpedLocation = Utils.Lerp(PreviousLocation[i], NextLocation[i], eased);
                    Vector3D lerpedScale = Utils.Lerp(PreviousScale[i], NextScale[i], eased);
                    Vector3D lerpedRotation = Utils.Lerp(PreviousRotation[i], NextRotation[i], eased);

                    if (RandomizeLocation)
                    {
                        Location[i].X = lerpedLocation.X;
                        Location[i].Y = lerpedLocation.Y;
                        Location[i].Z = lerpedLocation.Z;
                    }

                    if (RandomizeScale)
                    {
                        Scale[i].X = lerpedScale.X;
                        Scale[i].Y = lerpedScale.Y;
                        Scale[i].Z = lerpedScale.Z;
                    }

                    if (RandomizeScale)
                    {
                        Rotation[i].X = lerpedRotation.X;
                        Rotation[i].Y = lerpedRotation.Y;
                        Rotation[i].Z = lerpedRotation.Z;
                    }
                }
                else
                {
                    if (RandomizeLocation)
                    {
                        Location[i].X = NextLocation[i].X;
                        Location[i].Y = NextLocation[i].Y;
                        Location[i].Z = NextLocation[i].Z;
                    }

                    if (RandomizeScale)
                    {
                        Scale[i].X = NextScale[i].X;
                        Scale[i].Y = NextScale[i].Y;
                        Scale[i].Z = NextScale[i].Z;
                    }

                    if (RandomizeScale)
                    {
                        Rotation[i].X = NextRotation[i].X;
                        Rotation[i].Y = NextRotation[i].Y;
                        Rotation[i].Z = NextRotation[i].Z;
                    }
                }
            }
        }
    }
}

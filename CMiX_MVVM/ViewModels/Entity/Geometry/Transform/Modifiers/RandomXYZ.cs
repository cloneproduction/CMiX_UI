using System.Windows.Media.Media3D;
using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class RandomXYZ : TransformModifier, IColleague
    {
        public RandomXYZ(string name, IColleague parentSender, int id, MasterBeat masterBeat) : base (name, parentSender, id)
        {
            Counter = new Counter(nameof(Counter), this);
            Counter.CounterChangeEvent += Counter_CounterChangeEvent;

            Easing = new Easing(nameof(Easing), this); 
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, masterBeat);

            LocationX = new Slider(nameof(LocationX), this);
            LocationY = new Slider(nameof(LocationY), this);
            LocationZ = new Slider(nameof(LocationZ), this);

            ScaleX = new Slider(nameof(ScaleX), this);
            ScaleY = new Slider(nameof(ScaleY), this);
            ScaleZ = new Slider(nameof(ScaleZ), this);

            RotationX = new Slider(nameof(RotationX), this);
            RotationZ = new Slider(nameof(RotationY), this);
            RotationZ = new Slider(nameof(RotationZ), this);

            Count = 1;
            SelectedModifierType = ModifierType.OBJECT;
            RandomizeLocation = true;
            RandomizeScale = true;
            RandomizeRotation = true;
        }

        private void Counter_CounterChangeEvent(object sender, CounterEventArgs e)
        {
            this.Count = e.Value;
            Location = new Vector3D[e.Value];
            Scale = new Vector3D[e.Value];
            Rotation = new Vector3D[e.Value];
        }

        public BeatModifier BeatModifier { get; set; }
        public Easing Easing { get; set; }
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

        private Vector3D[] PreviousLocation { get; set; }
        private Vector3D[] PreviousScale { get; set; }
        private Vector3D[] PreviousRotation { get; set; }

        private Vector3D[] NextLocation { get; set; }
        private Vector3D[] NextScale { get; set; }
        private Vector3D[] NextRotation { get; set; }


        public override void UpdateOnBeatTick(double period, ITransformModifier transformModifier)
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

                        NextLocation[i] = new Vector3D(aX, aY, aZ);
                    }
                    else
                    {
                        NextLocation[i] = new Vector3D(0.0, 0.0, 0.0);
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

                        NextScale[i] = new Vector3D(aX, aY, aZ);
                    }
                    else
                    {
                        NextScale[i] = new Vector3D(1.0, 1.0, 1.0);
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

                        NextRotation[i] = new Vector3D(aX, aY, aZ);
                    }
                    else
                    {
                        NextRotation[i] = new Vector3D(0.0, 0.0, 0.0);
                    }
                }
            }
        }

        public override void UpdateOnGameLoop(double period, ITransformModifier transformModifier)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Easing.IsEnabled)
                {
                    double eased = Easings.Interpolate((float)period, Easing.SelectedEasing);

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

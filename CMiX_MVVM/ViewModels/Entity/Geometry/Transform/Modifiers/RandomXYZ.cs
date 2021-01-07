using System.Collections.ObjectModel;
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
            Transforms = new ObservableCollection<Transform>();

            SelectedModifierType = ModifierType.OBJECT;
            RandomizeLocation = true;
            RandomizeScale = true;
            RandomizeRotation = true;
        }

        private void Counter_CounterChangeEvent(object sender, CounterEventArgs e)
        {
            Location = new Vector3D[e.Value];
            Scale = new Vector3D[e.Value];
            Rotation = new Vector3D[e.Value];

            Transforms.Clear();
            for (int i = 0; i < e.Value; i++)
            {
                Transforms.Add(new Transform("Transform" + i, this));
            }
        }

        public BeatModifier BeatModifier { get; set; }
        public Easing Easing { get; set; }
        public Counter Counter { get; set; }


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

            
            foreach (var transform in Transforms)
            {
                var index = Transforms.IndexOf(transform);

                if (BeatModifier.CheckHitOnBeatTick())
                {
                    NextLocation[index] = new Vector3D(0.0, 0.0, 0.0);
                    NextScale[index] = new Vector3D(1.0, 1.0, 1.0);
                    NextRotation[index] = new Vector3D(0.0, 0.0, 0.0);

                    if (RandomizeLocation)
                    {
                        var org_X = transform.Translate.X.Amount;
                        var org_Y = transform.Translate.Y.Amount;
                        var org_Z = transform.Translate.Z.Amount;

                        var new_X = RandomNumbers.RandomDouble(-transform.Translate.X.Amount + 2.0, transform.Translate.X.Amount);
                        var new_Y = RandomNumbers.RandomDouble(-transform.Translate.Y.Amount + 2.0, transform.Translate.Y.Amount);
                        var new_Z = RandomNumbers.RandomDouble(-transform.Translate.Z.Amount + 2.0, transform.Translate.Z.Amount);

                        var aX = new_X * org_X;
                        var aY = new_Y * org_Y;
                        var aZ = new_Z * org_Z;

                        NextLocation[index] = new Vector3D(aX, aY, aZ);
                    }

                    
                    if (RandomizeScale)
                    {
                        var org_X = transform.Scale.X.Amount;
                        var org_Y = transform.Scale.Y.Amount;
                        var org_Z = transform.Scale.Z.Amount;

                        var new_X = RandomNumbers.RandomDouble(-transform.Scale.X.Amount + 2.0, transform.Scale.X.Amount);
                        var new_Y = RandomNumbers.RandomDouble(-transform.Scale.Y.Amount + 2.0, transform.Scale.Y.Amount);
                        var new_Z = RandomNumbers.RandomDouble(-transform.Scale.Z.Amount + 2.0, transform.Scale.Z.Amount);

                        var aX = new_X * org_X;
                        var aY = new_Y * org_Y;
                        var aZ = new_Z * org_Z;

                        NextScale[index] = new Vector3D(aX, aY, aZ);
                    }


                    if (RandomizeRotation)
                    {
                        var org_X = transform.Rotation.X.Amount;
                        var org_Y = transform.Rotation.Y.Amount;
                        var org_Z = transform.Rotation.Z.Amount;

                        var new_X = RandomNumbers.RandomDouble(-transform.Rotation.X.Amount + 2.0, transform.Rotation.X.Amount);
                        var new_Y = RandomNumbers.RandomDouble(-transform.Rotation.Y.Amount + 2.0, transform.Rotation.Y.Amount);
                        var new_Z = RandomNumbers.RandomDouble(-transform.Rotation.Z.Amount + 2.0, transform.Rotation.Z.Amount);

                        var aX = new_X * org_X;
                        var aY = new_Y * org_Y;
                        var aZ = new_Z * org_Z;

                        NextRotation[index] = new Vector3D(aX, aY, aZ);
                    }
                }
            }
        }

        public override void UpdateOnGameLoop(double period, ITransformModifier transformModifier)
        {
            foreach (var transform in Transforms)
            {
                var index = Transforms.IndexOf(transform);

                if (Easing.IsEnabled)
                {
                    double eased = Easings.Interpolate((float)period, Easing.SelectedEasing);

                    Vector3D lerpedLocation = Utils.Lerp(PreviousLocation[index], NextLocation[index], eased);
                    Vector3D lerpedScale = Utils.Lerp(PreviousScale[index], NextScale[index], eased);
                    Vector3D lerpedRotation = Utils.Lerp(PreviousRotation[index], NextRotation[index], eased);

                    if (RandomizeLocation)
                    {
                        transform.Translate.X.Amount = lerpedLocation.X;
                        transform.Translate.Y.Amount = lerpedLocation.Y;
                        transform.Translate.Z.Amount = lerpedLocation.Z;
                    }

                    if (RandomizeScale)
                    {
                        transform.Scale.X.Amount = lerpedScale.X;
                        transform.Scale.Y.Amount = lerpedScale.Y;
                        transform.Scale.Z.Amount = lerpedScale.Z;
                    }

                    if (RandomizeScale)
                    {
                        transform.Rotation.X.Amount = lerpedRotation.X;
                        transform.Rotation.Y.Amount = lerpedRotation.Y;
                        transform.Rotation.Z.Amount = lerpedRotation.Z;
                    }
                }
                else
                {
                    if (RandomizeLocation)
                    {
                        transform.Translate.X.Amount = NextLocation[index].X;
                        transform.Translate.Y.Amount = NextLocation[index].Y;
                        transform.Translate.Z.Amount = NextLocation[index].Z;
                    }

                    if (RandomizeScale)
                    {
                        transform.Scale.X.Amount = NextScale[index].X;
                        transform.Scale.Y.Amount = NextScale[index].Y;
                        transform.Scale.Z.Amount = NextScale[index].Z;
                    }

                    if (RandomizeScale)
                    {
                        transform.Rotation.X.Amount = NextRotation[index].X;
                        transform.Rotation.Y.Amount = NextRotation[index].Y;
                        transform.Rotation.Z.Amount = NextRotation[index].Z;
                    }
                }
            }
        }
    }
}

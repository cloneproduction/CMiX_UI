using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Tools;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public class RandomXYZ : MessageCommunicator, ITransformModifier, IMessageProcessor
    {
        public RandomXYZ(string name, int id, MasterBeat masterBeat, RandomXYZModel randomXYZModel)
        {
            Counter = new Counter(randomXYZModel.CounterModel);
            Counter.CounterChangeEvent += Counter_CounterChangeEvent;

            Easing = new Easing(randomXYZModel.EasingModel); 
            BeatModifier = new BeatModifier(masterBeat, randomXYZModel.BeatModifierModel);
            Transforms = new ObservableCollection<Transform>();

            SelectedModifierType = ModifierType.OBJECT;

            LocationX = new Slider(nameof(LocationX), randomXYZModel.LocationX);
            LocationY = new Slider(nameof(LocationY), randomXYZModel.LocationY);
            LocationZ = new Slider(nameof(LocationZ), randomXYZModel.LocationZ);

            ScaleX = new Slider(nameof(ScaleX), randomXYZModel.ScaleX);
            ScaleY = new Slider(nameof(ScaleY), randomXYZModel.ScaleY);
            ScaleZ = new Slider(nameof(ScaleZ), randomXYZModel.ScaleZ);

            RotationX = new Slider(nameof(RotationX), randomXYZModel.RotationX);
            RotationY = new Slider(nameof(RotationY), randomXYZModel.RotationY);
            RotationZ = new Slider(nameof(RotationZ), randomXYZModel.RotationZ);

            RandomizeLocation = true;
            RandomizeScale = true;
            RandomizeRotation = true;
        }

        public override void SetModuleReceiver(ModuleMessageDispatcher messageDispatcher)
        {
            messageDispatcher.RegisterMessageProcessor(this);
            LocationX.SetModuleReceiver(messageDispatcher);
        }

        private void Counter_CounterChangeEvent(object sender, CounterEventArgs e)
        {
            Transforms.Clear();
            for (int i = 0; i < e.Value; i++)
            {
                //Transforms.Add(new Transform("Transform" + i, this));
            }
        }

        public string Name { get; set; }
        public ObservableCollection<Transform> Transforms { get; set; }

        public BeatModifier BeatModifier { get; set; }
        public Easing Easing { get; set; }
        public Counter Counter { get; set; }


        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetAndNotify(ref _isExpanded, value);
        }


        private bool _randomizeLocation;
        public bool RandomizeLocation
        {
            get => _randomizeLocation;
            set => SetAndNotify(ref _randomizeLocation, value);
        }

        private bool _randomizeLocationIsExpanded;
        public bool RandomizeLocationIsExpanded
        {
            get => _randomizeLocationIsExpanded;
            set => SetAndNotify(ref _randomizeLocationIsExpanded, value);
        }

        private bool _randomizeScale;
        public bool RandomizeScale
        {
            get => _randomizeScale;
            set => SetAndNotify(ref _randomizeScale, value);
        }

        private bool _randomizeScaleIsExpanded;
        public bool RandomizeScaleIsExpanded
        {
            get => _randomizeScaleIsExpanded;
            set => SetAndNotify(ref _randomizeScaleIsExpanded, value);
        }


        private bool _randomizeRotationIsExpanded;
        public bool RandomizeRotationIsExpanded
        {
            get => _randomizeRotationIsExpanded;
            set => SetAndNotify(ref _randomizeRotationIsExpanded, value);
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

        public Slider LocationX { get; set; }
        public Slider LocationY { get; set; }
        public Slider LocationZ { get; set; }

        public Slider ScaleX { get; set; }
        public Slider ScaleY { get; set; }
        public Slider ScaleZ { get; set; }


        public Slider RotationX { get; set; }
        public Slider RotationY { get; set; }
        public Slider RotationZ { get; set; }


        private Vector3D[] PreviousLocation { get; set; }
        private Vector3D[] PreviousScale { get; set; }
        private Vector3D[] PreviousRotation { get; set; }

        private Vector3D[] NextLocation { get; set; }
        private Vector3D[] NextScale { get; set; }
        private Vector3D[] NextRotation { get; set; }


        public void UpdateOnBeatTick(double period)
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

                        var new_X = RandomNumbers.RandomDouble(-LocationX.Amount + 2.0, LocationX.Amount);
                        var new_Y = RandomNumbers.RandomDouble(-LocationY.Amount + 2.0, LocationY.Amount);
                        var new_Z = RandomNumbers.RandomDouble(-LocationZ.Amount + 2.0, LocationZ.Amount);

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

                        var new_X = RandomNumbers.RandomDouble(-ScaleX.Amount + 2.0, ScaleX.Amount);
                        var new_Y = RandomNumbers.RandomDouble(-ScaleY.Amount + 2.0, ScaleY.Amount);
                        var new_Z = RandomNumbers.RandomDouble(-ScaleZ.Amount + 2.0, ScaleZ.Amount);

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

                        var new_X = RandomNumbers.RandomDouble(-RotationX.Amount + 2.0, RotationX.Amount);
                        var new_Y = RandomNumbers.RandomDouble(-RotationY.Amount + 2.0, RotationY.Amount);
                        var new_Z = RandomNumbers.RandomDouble(-RotationZ.Amount + 2.0, RotationZ.Amount);

                        var aX = new_X * org_X;
                        var aY = new_Y * org_Y;
                        var aZ = new_Z * org_Z;

                        NextRotation[index] = new Vector3D(aX, aY, aZ);
                    }
                }
            }
        }

        public void UpdateOnGameLoop(double period)
        {
            if(Transforms.Count != PreviousLocation.Length)
            {

            }


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


        public override void SetViewModel(IModel model)
        {
            RandomXYZModel randomXYZModel = model as RandomXYZModel;
            this.Name = randomXYZModel.Name;
            this.ID = randomXYZModel.ID;
            this.SetViewModel(model);
        }

        public override IModel GetModel()
        {
            RandomXYZModel model = new RandomXYZModel();
            model.ID = this.ID;
            return model;
        }
    }
}

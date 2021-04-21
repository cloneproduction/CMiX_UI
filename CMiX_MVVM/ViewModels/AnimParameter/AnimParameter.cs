using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : Module, IObserver
    {
        public AnimParameter(string name, double[] defaultParameter, MasterBeat beat, AnimParameterModel animParameterModel) 
        {
            Period = new double[0];
            Range = new Range(animParameterModel.RangeModel);
            Easing = new Easing(animParameterModel.EasingModel);
            Width = new Slider(nameof(Width), animParameterModel.Width);
            BeatModifier = new BeatModifier(beat, animParameterModel.BeatModifierModel);
            Parameters = defaultParameter;
            Name = name;
            SelectedModeType = ModeType.None;
        }

        public override void SetReceiver(IMessageReceiver messageReceiver)
        {
            Width.SetReceiver(messageReceiver);
        }

        public BeatModifier BeatModifier { get; set; }
        public Easing Easing { get; set; }
        public IRange Range { get; set; }
        public Slider Width { get; set; }

        public double[] Parameters { get; set; }
        public double[] Period { get; set; }
        public double DefaultValue { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }

        private ModeType _selectedModeType;
        public ModeType SelectedModeType
        {
            get => _selectedModeType;
            set
            {
                SetAndNotify(ref _selectedModeType, value);
                SetAnimMode();
            }
        }

        private IAnimMode _animMode;
        public IAnimMode AnimMode
        {
            get => _animMode;
            set
            {
                SetAndNotify(ref _animMode, value);

            }
        }

        private void SetAnimMode()
        {
            ParametersToDefault();
            //if (this.AnimMode != null)
            //    this.AnimMode.Dispose();
            this.AnimMode = ModesFactory.CreateMode(SelectedModeType, this);
        }

        public void AnimateOnBeatTick()
        {
            var index = BeatModifier.BeatIndex;
            if (index >= 0 && index < Period.Length)
                this.AnimMode.UpdateOnBeatTick(this.Parameters, Period[BeatModifier.BeatIndex], Range, this.Easing, this.BeatModifier);
        }

        public void AnimateOnGameLoop(double[] parameters)
        {
            this.AnimMode.UpdateOnGameLoop(parameters, Period[BeatModifier.BeatIndex], Range, this.Easing, this.BeatModifier);
        }

        public void Update(int count)
        {
            //this.Parameters = new double[count];
            ParametersToDefault();
            AnimateOnBeatTick();
        }

        private void ParametersToDefault()
        {
            for (int i = 0; i < Parameters.Length; i++)
            {
                this.Parameters[i] = DefaultValue;
            }
        }

        public override void SetViewModel(IModel model)
        {
            AnimParameterModel animParameterModel = model as AnimParameterModel;
            this.SelectedModeType = animParameterModel.SelectedModeType;
            this.Name = animParameterModel.Name;
            this.IsEnabled = animParameterModel.IsEnabled;
            this.Width.SetViewModel(animParameterModel.Width);
            this.Easing.SetViewModel(animParameterModel.EasingModel);
            this.BeatModifier.SetViewModel(animParameterModel.BeatModifierModel);
            this.AnimMode.SetViewModel(animParameterModel.AnimModeModel);
        }

        public override IModel GetModel()
        {
            AnimParameterModel model = new AnimParameterModel();

            model.IsEnabled = this.IsEnabled;
            model.Name = this.Name;
            model.SelectedModeType = this.SelectedModeType;
            model.Width = (SliderModel)this.Width.GetModel();
            model.EasingModel = (EasingModel)this.Easing.GetModel();
            model.BeatModifierModel = (BeatModifierModel)this.BeatModifier.GetModel();
            model.AnimModeModel = (AnimModeModel)this.AnimMode.GetModel();

            return model;
        }
    }
}
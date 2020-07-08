using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : Sendable
    {
        public AnimParameter(string name, Beat beat, bool isEnabled = true)
        {
            Mode = new AnimMode(this);
            Influence = new Slider(nameof(Influence), this);
            Range = new Range(this);
;           BeatModifier = new BeatModifier(beat, this);
            BeatModifier.BeatTap += BeatModifier_BeatTap;
            SelectedModeType = ModeType.Steady;

            Name = name;
            IsEnabled = isEnabled;
        }

        public AnimParameter(string name, Beat beat, bool isEnabled, Sendable parentSendable) : this(name, beat, isEnabled)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as AnimParameterModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        private void BeatModifier_BeatTap(object sender, System.EventArgs e)
        {
            //System.Console.WriteLine("BeatModifier " + this.Name + " TAP");
        }

        public override string GetMessageAddress()
        {
            return $"{this.GetType().Name}/{Name}/";
        }

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
            set
            {
                SetAndNotify(ref _IsEnabled, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private ModeType _selectedModeType;
        public ModeType SelectedModeType
        {
            get => _selectedModeType;
            set
            {
                SetAndNotify(ref _selectedModeType, value);
                SetAnimMode();
                //OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private void SetAnimMode()
        {
            AnimMode = ModesFactory.CreateMode(SelectedModeType);
        }

        public IAnimMode AnimMode { get; set; }
        public Range Range { get; set; }
        public AnimMode Mode { get; set; }
        public Slider Influence { get; set; }
        public BeatModifier BeatModifier { get; set; }
    }
}

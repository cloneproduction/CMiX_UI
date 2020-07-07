using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : Sendable
    {
        public AnimParameter(string name, Beat beat, bool isEnabled = true)
        {
            Name = name;
            Mode = new AnimMode(this);
            Influence = new Slider(nameof(Influence), this);

            BeatModifier = new BeatModifier(beat, this);
            BeatModifier.BeatTap += BeatModifier_BeatTap;
            IsEnabled = isEnabled;
        }

        private void BeatModifier_BeatTap(object sender, System.EventArgs e)
        {
            System.Console.WriteLine("BeatModifier " + this.Name + " TAP");
        }

        public override string GetMessageAddress()
        {
            return $"{this.GetType().Name}/{Name}/";
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

        private double _minimum;
        public double Minimum
        {
            get => _minimum;
            set => SetAndNotify(ref _minimum, value);
        }

        private double _maximum;
        public double Maximum
        {
            get => _maximum;
            set => SetAndNotify(ref _maximum, value);
        }

        public AnimMode Mode { get; set; }
        public Slider Influence { get; set; }
        public BeatModifier BeatModifier { get; set; }
    }
}

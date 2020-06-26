using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : Sendable
    {
        public AnimParameter(string name, Beat beat, bool isEnabled)
        {
            Name = name;
            Influence = new Slider(nameof(Influence), this);
            BeatModifier = new BeatModifier(beat);
          
            Mode = AnimMode.None;
            IsEnabled = isEnabled;


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

        private AnimMode _Mode;
        public AnimMode Mode
        {
            get => _Mode;
            set
            {
                SetAndNotify(ref _Mode, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        public Slider Influence { get; set; }
        public BeatModifier BeatModifier { get; set; }
    }
}

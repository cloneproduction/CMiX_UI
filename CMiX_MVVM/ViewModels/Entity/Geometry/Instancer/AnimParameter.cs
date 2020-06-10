using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System.Windows;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : Sendable
    {
        public AnimParameter(string name, Beat beat, bool isEnabled)
        {
            Mode = AnimMode.None;
            IsEnabled = isEnabled;
            Slider = new Slider(nameof(Slider));
            BeatModifier = new BeatModifier(beat);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as AnimParameterModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
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

        public Slider Slider { get; set; }
        public BeatModifier BeatModifier { get; set; }

        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("AnimParameterModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }
    }
}

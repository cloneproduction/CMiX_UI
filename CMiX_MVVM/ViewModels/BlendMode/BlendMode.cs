using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class BlendMode : Sendable
    {
        public BlendMode()
        {
            Mode = ((BlendModeEnum)0).ToString();
        }

        public BlendMode(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as BlendModeModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set
            {
                //Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _mode, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        public void Reset() => Mode = ((BlendModeEnum)0).ToString();
    }
}

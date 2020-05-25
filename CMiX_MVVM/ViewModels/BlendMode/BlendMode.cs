using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.ViewModels
{
    public class BlendMode : Sendable
    {
        public BlendMode(Beat masterBeat)
        {
            Mode = ((BlendModeEnum)0).ToString();
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

        public void Reset()
        {
            Mode = ((BlendModeEnum)0).ToString();
        }
    }
}

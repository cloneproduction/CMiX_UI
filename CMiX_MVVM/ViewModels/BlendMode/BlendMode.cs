using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class BlendMode : Sendable
    {
        public BlendMode(Beat masterBeat)
        {
            Mode = ((BlendModeEnum)0).ToString();
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (this.GetMessageAddress() == e.MessageAddress)
            {
                this.SetViewModel(e.Model as BlendModeModel);
                Console.WriteLine("BlendMode Updated");
            }
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

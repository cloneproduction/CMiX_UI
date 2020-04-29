using CMiX.MVVM.Services;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class BlendMode : ViewModel, IUndoable
    {
        public BlendMode(Beat masterBeat, string messageAddress, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}{nameof(BlendMode)}";
            Mode = ((MVVM.ViewModels.BlendMode)0).ToString();
        }

        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _mode, value);
                //var blendModeModel = this.GetModel();
                //MessengerService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, blendModeModel);
            }
        }

        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public MessengerService MessengerService { get; set; }

        public void Reset()
        {
            Mode = ((MVVM.ViewModels.BlendMode)0).ToString();
        }
    }
}

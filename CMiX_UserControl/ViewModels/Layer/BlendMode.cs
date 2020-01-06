using CMiX.MVVM.Services;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class BlendMode : ViewModel, ISendable, IUndoable
    {
        public BlendMode(MasterBeat masterBeat, string messageAddress, Sender sender, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}{nameof(BlendMode)}";
            Sender = sender;
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
                BlendModeModel blendModeModel = new BlendModeModel();
                this.CopyModel(blendModeModel);
                Sender.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, blendModeModel);
            }
        }

        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }

        public void CopyModel(IModel model)
        {
            BlendModeModel blendModeModel = model as BlendModeModel;
            blendModeModel.MessageAddress = MessageAddress;
            blendModeModel.Mode = Mode;
        }

        public void PasteModel(IModel model)
        {
            BlendModeModel blendModeModel = model as BlendModeModel;
            MessageAddress = blendModeModel.MessageAddress;
            Mode = blendModeModel.Mode;
        }

        public void Reset()
        {
            Mode = ((MVVM.ViewModels.BlendMode)0).ToString();
        }
    }
}

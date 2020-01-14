using CMiX.MVVM.Services;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class BlendMode : ViewModel, ISendable, IUndoable
    {
        public BlendMode(MasterBeat masterBeat, string messageAddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}{nameof(BlendMode)}";
            MessageService = messageService;
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
                var blendModeModel = GetModel();
                MessageService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, blendModeModel);
            }
        }

        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }

        public BlendModeModel GetModel()
        {
            BlendModeModel blendModeModel = new BlendModeModel();
            blendModeModel.Mode = Mode;
            return blendModeModel;
        }

        //public void CopyModel(BlendModeModel blendModeModel)
        //{
        //    blendModeModel.Mode = Mode;
        //}

        public void PasteModel(BlendModeModel blendModeModel)
        {
            Mode = blendModeModel.Mode;
        }

        public void Reset()
        {
            Mode = ((MVVM.ViewModels.BlendMode)0).ToString();
        }
    }
}

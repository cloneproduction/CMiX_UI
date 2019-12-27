using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using CMiX.ViewModels;
using Memento;

namespace CMiX
{
    public class BlendMode : SendableViewModel
    {
        public BlendMode(MasterBeat masterBeat, string messageAddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor)
        : base(serverValidations, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageAddress, nameof(BlendMode));
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
                this.Copy(blendModeModel);
                SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, blendModeModel);
            }
        }

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }

        public void Copy(BlendModeModel blendModeModel)
        {
            blendModeModel.MessageAddress = MessageAddress;
            blendModeModel.Mode = Mode;
        }

        public void Paste(BlendModeModel blendModeModel)
        {
            MessageAddress = blendModeModel.MessageAddress;
            Mode = blendModeModel.Mode;
        }

        public void Reset()
        {
            Mode = ((MVVM.ViewModels.BlendMode)0).ToString();
        }
    }
}

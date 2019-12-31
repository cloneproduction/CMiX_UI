using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.Services;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiX.ViewModels
{
    public class BlendMode : ViewModel, ISendable, IUndoable
    {
        public BlendMode(MasterBeat masterBeat, string messageAddress, Messenger messenger, Mementor mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageAddress, nameof(BlendMode));
            Messenger = messenger;
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
                Messenger.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, blendModeModel);
            }
        }

        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }

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

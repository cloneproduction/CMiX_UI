using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Engine.ViewModels
{
    public class MasterBeat : ICopyPasteModel, IMessageReceiver
    {
        public MasterBeat()
        {

        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }
        public double Period { get; set; }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public void CopyModel(IModel model)
        {
            MasterBeatModel masterBeatModel = model as MasterBeatModel;
            masterBeatModel.MessageAddress = MessageAddress;
            masterBeatModel.Period = Period;
        }

        public void PasteModel(IModel model)
        {
            MasterBeatModel masterBeatModel = model as MasterBeatModel;
            MessageAddress = masterBeatModel.MessageAddress;
            Period = masterBeatModel.Period;
        }
    }
}

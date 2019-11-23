using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;

namespace CMiX.Engine.ViewModel
{
    public class BlendMode : ViewModel
    {
        public BlendMode(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        : base(netMQClient, messageAddress, serializer)
        {
            Mode = ((MVVM.ViewModels.BlendMode)0).ToString();
        }

        public override void ByteReceived()
        {
            string receivedAddress = NetMQClient.ByteMessage.MessageAddress;

            if (receivedAddress == this.MessageAddress)
            {
                MessageCommand command = NetMQClient.ByteMessage.Command;
                switch (command)
                {
                    case MessageCommand.VIEWMODEL_UPDATE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            BlendModeModel blendModeModel = NetMQClient.ByteMessage.Payload as BlendModeModel;
                            this.PasteData(blendModeModel);
                            System.Console.WriteLine(MessageAddress + " " + Mode);
                        }
                        break;
                }
            }
        }

        public string Mode { get; set; }

        public void PasteData(BlendModeModel blendModeModel)
        {
            MessageAddress = blendModeModel.MessageAddress;
            Mode = blendModeModel.Mode;
        }
    }
}
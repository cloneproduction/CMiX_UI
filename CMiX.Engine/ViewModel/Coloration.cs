using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;

namespace CMiX.Engine.ViewModel
{
    public class Coloration : ViewModel
    {
        public Coloration(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        : base(netMQClient, messageAddress, serializer)
        {
            ColorSelector = new ColorSelector(this.NetMQClient, this.MessageAddress + nameof(ColorSelector), this.Serializer);
        }

        public override void ByteReceived()
        {
            string receivedAddress = NetMQClient.ByteMessage.MessageAddress;

            if (receivedAddress == this.MessageAddress)
            {
                MessageCommand command = NetMQClient.ByteMessage.Command;
                System.Console.WriteLine("receivedAddress : " + receivedAddress);
                System.Console.WriteLine("this.MessageAddress : " + this.MessageAddress);
                switch (command)
                {
                    case MessageCommand.VIEWMODEL_UPDATE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            ColorationModel colorationModel = NetMQClient.ByteMessage.Payload as ColorationModel;
                            this.PasteData(colorationModel);
                        }
                        break;
                }
            }
        }

        public ColorSelector ColorSelector { get; set; }

        public void PasteData(ColorationModel colorationModel)
        {
            MessageAddress = colorationModel.MessageAddress;
            ColorSelector.PasteData(colorationModel.ColorSelectorModel);
        }
    }
}

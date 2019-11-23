using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using System.Windows.Media;

namespace CMiX.Engine.ViewModel
{
    public class ColorPicker : ViewModel
    {
        public ColorPicker(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        : base(netMQClient, messageAddress, serializer)
        {

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
                            ColorPickerModel colorPickerModel = NetMQClient.ByteMessage.Payload as ColorPickerModel;
                            this.PasteData(colorPickerModel);
                            //System.Console.WriteLine(MessageAddress + " " + Mode);
                        }
                        break;
                }
            }
        }

        public Color SelectedColor { get; set; }

        public void PasteData(ColorPickerModel colorPickerModel)
        {
            MessageAddress = colorPickerModel.MessageAddress;
            SelectedColor = Utils.HexStringToColor(colorPickerModel.SelectedColor);
            System.Console.WriteLine(this.MessageAddress + " " + SelectedColor);
        }
    }
}

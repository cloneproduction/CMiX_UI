using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;

namespace CMiX.Engine.ViewModel
{
    public class ColorSelector : ViewModel
    {
        public ColorSelector(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        : base(netMQClient, messageAddress, serializer)
        {
            ColorPicker = new ColorPicker(this.NetMQClient, this.MessageAddress + nameof(ColorPicker), this.Serializer);
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
                            ColorSelectorModel colorSelectorModel = NetMQClient.ByteMessage.Payload as ColorSelectorModel;
                            this.PasteData(colorSelectorModel);
                            //System.Console.WriteLine(MessageAddress + " " + Mode);
                        }
                        break;
                }
            }
        }
        public ColorPicker ColorPicker { get; set; }

        public void PasteData(ColorSelectorModel colorSelectorModel)
        {
            MessageAddress = colorSelectorModel.MessageAddress;
            ColorPicker.PasteData(colorSelectorModel.ColorPickerModel);
        }
    }
}
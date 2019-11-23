using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Engine.ViewModel
{
    public class Content : ViewModel
    {
        public Content(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        : base(netMQClient, messageAddress, serializer)
        {
            Objects = new List<Object>();
            Object obj = new Object(this.NetMQClient, this.MessageAddress + "Object0", this.Serializer);
            obj.Name = "Object 0";
            Objects.Add(obj);
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
                            ContentModel contentModel = NetMQClient.ByteMessage.Payload as ContentModel;
                            this.PasteData(contentModel);
                            //System.Console.WriteLine(MessageAddress + " " + Mode);
                        }
                        break;
                    case MessageCommand.OBJECT_ADD:
                        if(NetMQClient.ByteMessage.Payload != null)
                        {
                            ObjectModel objectModel = NetMQClient.ByteMessage.Payload as ObjectModel;
                            this.AddObject(objectModel);
                        }
                        break;
                    case MessageCommand.OBJECT_DELETE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            int index = (int)NetMQClient.ByteMessage.Payload;
                            Console.WriteLine("Delete Object with Index : " + index.ToString());
                            this.DeleteObject(index);
                        }
                        break;
                }
            }
        }

        public List<Object> Objects { get; set; }

        public void AddObject(ObjectModel objectModel)
        {
            Object obj = new Object(this.NetMQClient, this.MessageAddress + nameof(Object), this.Serializer);
            obj.PasteData(objectModel);
            Objects.Add(obj);
            Console.WriteLine("AddObject messageaddress : " + obj.MessageAddress);
        }

        public void DeleteObject(int index)
        {
            Console.WriteLine("DeleteLayer : " + Objects[index].MessageAddress);
            Objects.RemoveAt(index);
        }

        public void PasteData(ContentModel contentModel)
        {
            MessageAddress = contentModel.MessageAddress;
            Objects.Clear();
            foreach (ObjectModel obj in contentModel.ObjectModels)
            {
                Object newObject = new Object(this.NetMQClient, this.MessageAddress, this.Serializer);
                newObject.PasteData(obj);
            }
        }
    }
}
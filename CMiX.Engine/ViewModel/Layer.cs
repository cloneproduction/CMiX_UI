using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ceras;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;

namespace CMiX.Engine.ViewModel
{
    public class Layer : ViewModel
    {
        public Layer(NetMQClient netMQClient, string topic, CerasSerializer serializer)
            : base(netMQClient, topic, serializer)
        {

        }

        public string Name { get; set; }

        public override void ByteReceived()
        {
            Console.WriteLine("ByteReceived on Layer");
        }

        public void PasteData(LayerModel layerModel)
        {
            Name = layerModel.Name;
        }
    }
}
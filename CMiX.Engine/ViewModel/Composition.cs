using Ceras;
using System;
using CMiX.MVVM.Models;
using CMiX.MVVM.Message;
using CMiX.MVVM.Commands;

using System.Collections.Generic;

namespace CMiX.Engine.ViewModel
{
    public class Composition : ViewModel
    {
        public Composition(NetMQClient netMQClient, string topic, CerasSerializer serializer) 
            : base (netMQClient, topic, serializer)
        {
            Layers = new List<Layer>();
            Name = String.Empty;
            Layers.Add(new Layer(NetMQClient, "/Layer", Serializer));
        }

        public override void ByteReceived()
        {
            string topic = NetMQClient.ByteMessage.Topic;

            if(topic == "/Composition")
            {
                MessageCommand command = NetMQClient.ByteMessage.Command;
                switch (command)
                {
                    case MessageCommand.AddLayer:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            LayerModel layerModel = NetMQClient.ByteMessage.Payload as LayerModel;
                            this.AddLayer(layerModel);
                            Console.WriteLine("LayerCount  " + Layers.Count);
                        }
                        break;
                }
            }
        }

        public void AddLayer(LayerModel layerModel)
        {
            Layer layer = new Layer(NetMQClient, "/Layer", Serializer);
            Layers.Add(layer);
            Console.WriteLine("Added New Layer");
        }

        public List<Layer> Layers { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public void PasteData(CompositionModel compositionModel)
        {
            MessageAddress = compositionModel.MessageAddress;
            Name = compositionModel.Name;

            //ContentFolderName = compositionModel.ContentFolderName;
            //LayerID = compositionModel.layerID;
            //layerNameID = compositionModel.layerNameID;

            Layers.Clear();
            foreach (LayerModel layermodel in compositionModel.LayersModel)
            {
                Layer layer = new Layer(this.NetMQClient, "/Layer", Serializer);
                layer.PasteData(layermodel);
                Layers.Add(layer);
            }
            Console.WriteLine(Layers.Count.ToString());
            //SelectedLayer.Paste(compositionModel.SelectedLayer);
            //MasterBeat.Paste(compositionModel.MasterBeatModel);
            //Camera.Paste(compositionModel.CameraModel);
            //Transition.Paste(compositionModel.TransitionModel);
        }
    }
}
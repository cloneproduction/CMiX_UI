using Ceras;
using System;
using CMiX.MVVM.Models;
using CMiX.MVVM.Message;
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
        }

        public override void ByteReceived()
        {
            object obj = Serializer.Deserialize<object>(NetMQClient.ByteMessage.Message);

            if (obj.GetType() == typeof(CompositionModel))
            {
                PasteData(obj as CompositionModel);
            }
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
using System;
using System.Collections.ObjectModel;
using Ceras;
using CMiX.MVVM.Models;
using CMiX.MVVM.Message;
using CMiX.MVVM.Commands;
using CMiX.MVVM.ViewModels;

namespace CMiX.Engine.ViewModel
{
    public class Composition : ViewModel
    {
        public Composition(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer) 
            : base (netMQClient, messageAddress, serializer)
        {
            MessageAddress = $"{messageAddress}{nameof(Composition)}/";
            Name = String.Empty;
            Layers = new ObservableCollection<Layer>();
            Layers.Add(new Layer(NetMQClient, $"{MessageAddress}{nameof(Layer)}0/", Serializer));
            Camera = new Camera(NetMQClient, MessageAddress, Serializer);
        }

        public event EventHandler MessageReceived;

        public override void ByteReceived()
        {
            string receivedAddress = NetMQClient.ByteMessage.MessageAddress;
            if(receivedAddress == this.MessageAddress)
            {
                MessageCommand command = NetMQClient.ByteMessage.Command;
                switch (command)
                {
                    case MessageCommand.LAYER_ADD:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            LayerModel layerModel = NetMQClient.ByteMessage.Payload as LayerModel;
                            this.AddLayer(layerModel);
                        }
                        break;

                    case MessageCommand.LAYER_DUPLICATE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            LayerModel layerModel = NetMQClient.ByteMessage.Payload as LayerModel;
                            int[] movedIndex = NetMQClient.ByteMessage.Parameter as int[];
                            this.DuplicateLayer(layerModel, movedIndex);
                        }
                        break;

                    case MessageCommand.LAYER_DELETE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            int index = (int)NetMQClient.ByteMessage.Payload;
                            this.DeleteLayer(index);
                        }
                        break;

                    case MessageCommand.LAYER_MOVE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            int[] index = (int[])NetMQClient.ByteMessage.Payload;
                            Layers.Move(index[0], index[1]);
                        }
                        break;
                }
            }
        }

        public void AddLayer(LayerModel layerModel)
        {
            Layer layer = new Layer(NetMQClient, "/Layer", Serializer);
            layer.PasteData(layerModel);
            Layers.Add(layer);
            Console.WriteLine("AddLayer : " + layer.MessageAddress);
        }

        public void DuplicateLayer(LayerModel layerModel, int[] movedIndex)
        {
            Layer layer = new Layer(NetMQClient, "/Layer", Serializer);
            layer.PasteData(layerModel);
            Layers.Add(layer);
            Layers.Move(movedIndex[0], movedIndex[1]);
            Console.WriteLine("DuplicateLayer : " + layer.MessageAddress);
        }

        public void DeleteLayer(int index)
        {
            Console.WriteLine("DeleteLayer : " + Layers[index].MessageAddress);
            Layers.RemoveAt(index);
            foreach (var item in Layers)
            {
                if (item.ID > index)
                    item.ID--;
            }
        }

        public ObservableCollection<Layer> Layers { get; set; }
        public Camera Camera { get; set; }
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
            //SelectedLayer.Paste(compositionModel.SelectedLayer);
            //MasterBeat.Paste(compositionModel.MasterBeatModel);
            //Camera.Paste(compositionModel.CameraModel);
            //Transition.Paste(compositionModel.TransitionModel);
        }
    }
}
using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.Models;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Services;
using CMiX.MVVM;
using CMiX.Engine;

namespace CMiX.Engine.ViewModel
{
    public class Composition : ICopyPasteModel, IMessageReceiver
    {
        public Composition(Receiver receiver, string messageAddress) 
        {
            MessageAddress = $"{messageAddress}{nameof(Composition)}/";
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived; ;

            Layers = new ObservableCollection<Layer>();
            Layers.Add(new Layer(receiver, $"{MessageAddress}{nameof(Layer)}0/"));
            Camera = new Camera(receiver, MessageAddress);
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);

            if (MessageAddress == Receiver.ReceivedAddress && Receiver.ReceivedData != null)
            {
                object data = Receiver.ReceivedData;
                switch (Receiver.ReceivedCommand)
                {
                    case (MessageCommand.LAYER_ADD):
                        LayerModel addLayerModel = data as LayerModel;
                        this.AddLayer(addLayerModel);
                        break;

                    case (MessageCommand.LAYER_DUPLICATE):
                        LayerModel layerModel = data as LayerModel;
                        int[] movedIndex = Receiver.ReceivedParameter as int[];
                        this.DuplicateLayer(layerModel, movedIndex);
                        break;

                    case (MessageCommand.LAYER_DELETE):
                        int deleteIndex = (int)data;
                        this.DeleteLayer(deleteIndex);
                        break;

                    case (MessageCommand.LAYER_MOVE):
                        int[] moveIndexes = (int[])data;
                        Layers.Move(moveIndexes[0], moveIndexes[1]);
                        break;
                }
            }
        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }
        public ObservableCollection<Layer> Layers { get; set; }
        public Camera Camera { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public void AddLayer(LayerModel layerModel)
        {
            Layer layer = new Layer(Receiver, "/Layer");
            layer.PasteModel(layerModel);
            Layers.Add(layer);
            Console.WriteLine("AddLayer : " + layer.MessageAddress);
        }

        public void DuplicateLayer(LayerModel layerModel, int[] movedIndex)
        {
            Layer layer = new Layer(Receiver, "/Layer");
            layer.PasteModel(layerModel);
            Layers.Add(layer);
            Layers.Move(movedIndex[0], movedIndex[1]);
            Console.WriteLine("DuplicateLayer : " + layer.MessageAddress);
        }

        public void DeleteLayer(int index)
        {
            Layers.RemoveAt(index);
            foreach (var item in Layers)
            {
                if (item.ID > index)
                    item.ID--;
            }
            Console.WriteLine("DeleteLayer : " + Layers[index].MessageAddress);
        }

        public void CopyModel(IModel model)
        {
            CompositionModel compositionModel = model as CompositionModel;
            MessageAddress = compositionModel.MessageAddress;
            Name = compositionModel.Name;

            //ContentFolderName = compositionModel.ContentFolderName;
            //LayerID = compositionModel.layerID;
            //layerNameID = compositionModel.layerNameID;

            Layers.Clear();
            foreach (LayerModel layermodel in compositionModel.LayersModel)
            {
                Layer layer = new Layer(Receiver, "/Layer");
                layer.PasteModel(layermodel);
                Layers.Add(layer);
            }
            //SelectedLayer.Paste(compositionModel.SelectedLayer);
            //MasterBeat.Paste(compositionModel.MasterBeatModel);
            //Camera.Paste(compositionModel.CameraModel);
            //Transition.Paste(compositionModel.TransitionModel);
        }

        public void PasteModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
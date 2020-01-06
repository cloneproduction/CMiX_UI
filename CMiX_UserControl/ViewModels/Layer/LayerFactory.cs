using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class LayerFactory
    {
        public LayerFactory()
        {

        }

        int LayerID = 0;

        public Layer CreateLayer(ILayerContext context)
        {
            Layer layer = new Layer(context.MasterBeat, context.MessageAddress, context.Sender, context.Assets, context.Mementor)
            {
                ID = LayerID,
                DisplayName = "Layer " + LayerID
            };
            LayerID++;
            return layer;
        }

        public Layer DuplicateLayer(ILayerContext context, Layer layer)
        {
            LayerModel layerModel = new LayerModel();
            layer.CopyModel(layerModel);

            Layer newLayer = new Layer(context.MasterBeat, context.MessageAddress, context.Sender, context.Assets, context.Mementor);
            newLayer.PasteModel(layerModel);
            newLayer.ID = LayerID;
            newLayer.Name = newLayer.Name + "- Copy";
            context.Layers.Add(newLayer);

            int oldIndex = Layers.IndexOf(SelectedLayer);
            int newIndex = Layers.IndexOf(lyr) + 1;
            Layers.Move(oldIndex, newIndex);
            int[] movedIndex = new int[2] { oldIndex, newIndex };


            LayerID++;

            return newLayer;
        }

        //private string CreateLayerMessageAddress()
        //{
        //    //return $"{this.MessageAddress}Layer{LayerNameID.ToString()}/";
        //}
    }
}
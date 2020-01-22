using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class LayerManager
    {
        public LayerManager(ObservableCollection<Layer> layers, MessageService messageService)
        {
            MessageService = messageService;
            Layers = layers;
        }

        int LayerID = 0;
        public MessageService MessageService { get; set; }
        public ObservableCollection<Layer> Layers { get; set; }

        public Layer CreateLayer(ILayerContext context)
        {
            Layer newLayer = new Layer(context.MasterBeat, context.MessageAddress, LayerID, context.MessageService, context.Assets, context.Mementor);
            context.Layers.Add(newLayer);
            context.SelectedLayer = newLayer;
            MessageService.SendMessages(context.MessageAddress, MessageCommand.LAYER_ADD, null, newLayer.GetModel());

            LayerID++;
            return newLayer;
        }

        public Layer DuplicateLayer(ILayerContext context)
        {
            var SelectedLayer = context.SelectedLayer;
            if (SelectedLayer != null)
            {
                LayerModel layerModel = context.SelectedLayer.GetModel();
                layerModel.ID = LayerID;
                layerModel.Name = SelectedLayer.Name + "- Copy";

                Layer newLayer = new Layer(context.MasterBeat, context.MessageAddress, LayerID, context.MessageService, context.Assets, context.Mementor);
                newLayer.SetViewModel(layerModel);
                MessageService.SendMessages(context.MessageAddress, MessageCommand.LAYER_DUPLICATE, null, layerModel);

                int index = context.Layers.IndexOf(SelectedLayer);
                context.Layers.Insert(index + 1, newLayer);
                context.SelectedLayer = newLayer;

                LayerID++;
                return newLayer;
            }
            else
                return null;
        }

        public Layer DuplicateLayerLink(Composition composition)
        {
            var SelectedLayer = composition.LayerEditor.SelectedLayer;
            if (SelectedLayer != null)
            {
                LayerModel layerModel = SelectedLayer.GetModel();
                layerModel.ID = LayerID;
                layerModel.Name = SelectedLayer.Name + "- Link";

                Layer newLayer = new Layer(composition.MasterBeat, composition.MessageAddress, LayerID, composition.MessageService, composition.Assets, composition.Mementor);
                newLayer.SetViewModel(layerModel);

                newLayer.Entities.Clear();
                foreach (var item in SelectedLayer.Entities)
                {
                    newLayer.Entities.Add(item);
                }

                

                int index = composition.Layers.IndexOf(SelectedLayer);
                composition.Layers.Insert(index + 1, newLayer);
                composition.LayerEditor.SelectedLayer = newLayer;

                MessageService.SendMessages(composition.MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, composition.GetModel());
                LayerID++;
                return newLayer;
            }
            else
                return null;
        }

        public void DeleteLayer(ILayerContext context)
        {
            var SelectedLayer = context.SelectedLayer;
            if (SelectedLayer != null)
            {
                Layer removedlayer = SelectedLayer as Layer;
                int removedLayerIndex = context.Layers.IndexOf(removedlayer);
                context.Layers.Remove(removedlayer);

                if (context.Layers.Count > 0)
                {
                    if (removedLayerIndex > 0)
                        SelectedLayer = context.Layers[removedLayerIndex - 1];
                    else
                        SelectedLayer = context.Layers[0];
                }
                else
                {
                    SelectedLayer = null;
                }

                MessageService.SendMessages(context.MessageAddress, MessageCommand.LAYER_DELETE, null, removedLayerIndex);
            }
        }


        public void PasteLayer(ILayerContext context)
        {

        }
    }
}
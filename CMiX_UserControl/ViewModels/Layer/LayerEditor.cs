using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using GongSolutions.Wpf.DragDrop;
using Memento;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class LayerEditor : ViewModel, ILayerEditor
    {
        public LayerEditor(Sender sender, string messageAddress, MasterBeat masterBeat, Assets assets, Mementor mementor)
        {
            Mementor = mementor;
            LayerFactory = new LayerFactory();
            Layers = new ObservableCollection<Layer>();
            
            Assets = assets;
            MasterBeat = masterBeat;

            MessageAddress = messageAddress;
            Sender = sender;

            DeleteSelectedLayerCommand = new RelayCommand(p => DeleteSelectedLayer());
            DuplicateSelectedLayerCommand = new RelayCommand(p => DuplicateSelectedLayer());
            AddLayerCommand = new RelayCommand(p => AddLayer());
            CopyLayerCommand = new RelayCommand(p => CopyLayer());
            PasteLayerCommand = new RelayCommand(p => PasteLayer());
            ResetLayerCommand = new RelayCommand(p => ResetLayer());
        }

        public ICommand NewLayerCommand { get; }
        public ICommand DeleteSelectedLayerCommand { get; }
        public ICommand DuplicateSelectedLayerCommand { get; }

        public ICommand AddLayerCommand { get; }
        public ICommand CopyLayerCommand { get; }
        public ICommand PasteLayerCommand { get; }
        public ICommand ResetLayerCommand { get; }

        public LayerFactory LayerFactory { get; set; }
        public ObservableCollection<Layer> Layers { get; set; }
        public Layer SelectedLayer { get; set; }
        public MasterBeat MasterBeat { get; set; }
        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }

        #region COPY/PASTE/RESET LAYER
        private void CopyLayer()
        {
            LayerModel layerModel = new LayerModel();
            SelectedLayer.CopyModel(layerModel);
            IDataObject data = new DataObject();
            data.SetData(nameof(LayerModel), layerModel, false);
            Clipboard.SetDataObject(data);
        }

        private void PasteLayer()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(nameof(LayerModel)))
            {
                Mementor.BeginBatch();

                var selectedlayermessageaddress = SelectedLayer.MessageAddress;
                var selectedlayername = SelectedLayer.LayerName;
                var selectedname = SelectedLayer.DisplayName;
                var selectedLayerID = SelectedLayer.ID;

                var layerModel = data.GetData(nameof(LayerModel)) as LayerModel;

                SelectedLayer.PasteModel(layerModel);
                SelectedLayer.UpdateMessageAddress(selectedlayermessageaddress);
                SelectedLayer.LayerName = selectedlayername;
                SelectedLayer.DisplayName = selectedname;
                SelectedLayer.ID = selectedLayerID;
                SelectedLayer.CopyModel(layerModel);

                Sender.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, layerModel);
                Mementor.EndBatch();
            }
        }

        public void ResetLayer()
        {
            SelectedLayer.Reset();
            LayerModel layerModel = new LayerModel();
            SelectedLayer.CopyModel(layerModel);
            //SendMessages("LayerModel", layerModel);
        }
        #endregion

        #region ADD/DUPLICATE/DELETE LAYERS
        public void AddLayer()
        {
            Mementor.BeginBatch();
            Sender.Disable();

            var newLayer = LayerFactory.CreateLayer(this);
            SelectedLayer = newLayer;
            Layers.Add(newLayer);
            Mementor.ElementAdd(Layers, newLayer);
            Sender.Enable();

            LayerModel layerModel = new LayerModel();
            SelectedLayer.CopyModel(layerModel);
            Sender.SendMessages(MessageAddress, MessageCommand.LAYER_ADD, null, layerModel);

            Mementor.EndBatch();
            Console.WriteLine("Add Layer");
        }

        private void DuplicateSelectedLayer()
        {
            Mementor.BeginBatch();
            Sender.Disable();

            var layer = LayerFactory.DuplicateLayer(this, SelectedLayer);
            Mementor.ElementAdd(Layers, layer);

            Sender.Enable();
            Mementor.EndBatch();

            //Sender.SendMessages(MessageAddress, MessageCommand.LAYER_DUPLICATE, movedIndex, layermodel);
        }

        public void UpdateLayersIDOnDelete(Layer deletedlayer)
        {
            foreach (var item in Layers)
            {
                if (item.ID > deletedlayer.ID)
                    item.ID--;
            }
            //LayerID--;
        }

        private void DeleteSelectedLayer()
        {
            if (SelectedLayer != null)
            {
                Mementor.BeginBatch();
                Sender.Disable();

                Layer removedlayer = SelectedLayer as Layer;
                int removedlayerindex = Layers.IndexOf(removedlayer);
                UpdateLayersIDOnDelete(removedlayer);
                Mementor.ElementRemove(Layers, removedlayer);
                Layers.Remove(removedlayer);

                if (Layers.Count > 0)
                {
                    if (removedlayerindex > 0)
                        SelectedLayer = Layers[removedlayerindex - 1];
                    else
                        SelectedLayer = Layers[0];
                }
                else
                {
                    //LayerNameID = 0;
                }

                Sender.Enable();
                Mementor.EndBatch();

                Sender.SendMessages(MessageAddress, MessageCommand.LAYER_DELETE, null, removedlayerindex);
            }
        }


        #endregion

        #region DRAG DROP
        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data.GetType() == typeof(Layer) && Layers.Count > 1)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Copy;
            }
            else if (dropInfo.Data != null && dropInfo.TargetItem != null)
                SelectedLayer = dropInfo.TargetItem as Layer;
        }

        public void Drop(IDropInfo dropInfo)
        {
            Mementor.BeginBatch();
            if (dropInfo.DragInfo != null)
            {
                int sourceindex = dropInfo.DragInfo.SourceIndex;
                int insertindex = dropInfo.InsertIndex;

                if (sourceindex != insertindex)
                {
                    

                    if (insertindex >= Layers.Count - 1)
                    {
                        insertindex -= 1;
                    }

                    Layers.Move(sourceindex, insertindex);
                    Mementor.ElementIndexChange(Layers, Layers[insertindex], sourceindex);
                    SelectedLayer = Layers[insertindex];

                    int[] moveIndex = new int[2] { sourceindex, insertindex };

                    Sender.SendMessages(MessageAddress, MessageCommand.LAYER_MOVE, null, moveIndex); 
                }
            }
            Mementor.EndBatch();
        }
        #endregion

        public List<string> GetLayerNames()
        {
            var layerNames = new List<string>();
            foreach (var layer in Layers)
            {
                layerNames.Add(layer.LayerName);
            }
            layerNames.Sort();
            return layerNames;
        }

        public List<int> GetLayerID()
        {
            var LayerID = new List<int>();
            foreach (var layer in Layers)
            {
                LayerID.Add(layer.ID);
            }
            return LayerID;
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public void PasteModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}

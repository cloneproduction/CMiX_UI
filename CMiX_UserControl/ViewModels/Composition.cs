using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Threading.Tasks;
using System.Linq;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.ViewModels
{
    public class Composition : ViewModel, IDropTarget
    {
        #region CONSTRUCTORS
        public Composition(ObservableCollection<OSCMessenger> oscmessengers)
        {
            Name = string.Empty;
            MessageAddress = "";

            OSCValidation = new ObservableCollection<OSCValidation>();
            CreateOSCValidation(oscmessengers);

            Layers = new ObservableCollection<Layer>();

            Transition = new Slider("/Transition", OSCValidation, Mementor);

            MasterBeat = new MasterBeat(OSCValidation, Mementor);
            Camera = new Camera(OSCValidation, MasterBeat, Mementor);
            Mementor = new Mementor();

            CreateLayer();

            ReloadCompositionCommand = new RelayCommand(p => ReloadComposition(p));
            AddLayerCommand = new RelayCommand(p => AddLayer());
            DeleteLayerCommand = new RelayCommand(p => DeleteLayer(p));
            DuplicateLayerCommand = new RelayCommand(p => DuplicateLayer(p));
            CopyLayerCommand = new RelayCommand(p => CopyLayer());
            PasteLayerCommand = new RelayCommand(p => PasteLayer());
        }
        #endregion


        #region PROPERTIES
        private int layerID = 0;
        private int layerNameID = 0;

        public ICommand ReloadCompositionCommand { get; }
        public ICommand AddLayerCommand { get; }
        public ICommand CopyLayerCommand { get; }
        public ICommand PasteLayerCommand { get; }
        public ICommand SaveCompositionCommand { get; }
        public ICommand OpenCompositionCommand { get; }
        public ICommand DeleteLayerCommand { get; }
        public ICommand DuplicateLayerCommand { get; }

        public MasterBeat MasterBeat { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }

        public ObservableCollection<Layer> Layers { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private string _contentfoldername;
        public string ContentFolderName
        {
            get => _contentfoldername;
            set
            {
                SetAndNotify(ref _contentfoldername, value);
                foreach (var layer in Layers)
                {
                    UpdateLayerContentFolder(layer);
                }
                SendMessages("/ContentFolder", ContentFolderName);
            }
        }

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private Layer _selectedlayer;
        public Layer SelectedLayer
        {
            get => _selectedlayer;
            set => SetAndNotify(ref _selectedlayer, value);
        }
        #endregion

        #region PUBLIC METHODS
        public string CreateLayerName()
        {
            return "/Layer" + layerNameID.ToString() + "/";
        }

        public Layer CreateLayer()
        {
            var layername = CreateLayerName();

            Layer layer = new Layer(MasterBeat, layername, OSCValidation, Mementor);
            layer.ID = layerID;
            Layers.Add(layer);
            SelectedLayer = layer;

            return layer;
        }

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
            var layerID = new List<int>();
            foreach (var layer in Layers)
            {
                layerID.Add(layer.ID);
            }
            return layerID;
        }

        public void QueueLayerNames()
        {
            this.QueueMessages("/LayerNames", GetLayerNames().ToArray());
        }

        public void QueueLayerID()
        {
            this.QueueMessages("/LayerID", GetLayerID().Select(x => x.ToString()).ToArray());
        }

        public void UpdateLayerContentFolder(Layer layer)
        {
            layer.Content.Texture.FileSelector.FolderPath = ContentFolderName;
            layer.Content.Geometry.FileSelector.FolderPath = ContentFolderName;
            layer.Content.Geometry.GeometryFX.FileSelector.FolderPath = ContentFolderName;
            layer.Mask.Texture.FileSelector.FolderPath = ContentFolderName;
            layer.Mask.Geometry.FileSelector.FolderPath = ContentFolderName;
        }
        #endregion

        #region PRIVATE METHODS
        private void CreateOSCValidation(ObservableCollection<OSCMessenger> oscmessengers)
        {
            foreach (var messenger in oscmessengers)
            {
                OSCValidation.Add(new OSCValidation(messenger));
                
            }
        }

        private async void ReloadComposition(object messenger)
        {
            CompositionModel compositionmodel = new CompositionModel();
            this.Copy(compositionmodel);
            OSCMessenger oscmessenger = messenger as OSCMessenger;

            oscmessenger.QueueMessage("/Transition/Amount", Transition.Amount);
            oscmessenger.QueueMessage("/Transition/Start", true);
            oscmessenger.SendQueue();

            oscmessenger.QueueMessage("/CompositionReloaded", true);
            oscmessenger.QueueObject(compositionmodel);
            await Task.Delay(TimeSpan.FromMinutes(Transition.Amount));
            oscmessenger.SendQueue();
        }
        #endregion

        #region COPY/PASTE LAYER
        private void CopyLayer()
        {
            LayerModel layermodel = new LayerModel();
            SelectedLayer.Copy(layermodel);
            IDataObject data = new DataObject();
            data.SetData("Layer", layermodel, false);
            Clipboard.SetDataObject(data);
        }

        private void PasteLayer()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Layer"))
            {
                Mementor.BeginBatch();

                var selectedlayermessageaddress = SelectedLayer.MessageAddress;
                var selectedlayername = SelectedLayer.LayerName;
                var selectedlayerID = SelectedLayer.ID;

                var layermodel = (LayerModel)data.GetData("Layer") as LayerModel;

                SelectedLayer.Paste(layermodel);
                SelectedLayer.UpdateMessageAddress(selectedlayermessageaddress);
                SelectedLayer.LayerName = selectedlayername;
                SelectedLayer.ID = selectedlayerID;
                SelectedLayer.Copy(layermodel);

                QueueLayerID();
                QueueLayerNames();
                QueueObjects(layermodel);
                SendQueues();

                Mementor.EndBatch();
            }
        }
        #endregion

        #region ADD/REMOVE/DUPLICATE/DELETE LAYERS
        public void AddLayer()
        {
            Mementor.BeginBatch();
            DisabledMessages();

            layerID++;
            layerNameID++;
            var layer = CreateLayer();

            Mementor.ElementAdd(Layers, layer);
            UpdateLayerContentFolder(layer);

            LayerModel layermodel = new LayerModel();
            layer.Copy(layermodel);

            EnabledMessages();

            QueueLayerID();
            QueueLayerNames();
            QueueObjects(layermodel);
            SendQueues();

            Mementor.EndBatch();
        }

        public void UpdateLayersIDOnDelete(Layer deletedlayer)
        {
            foreach (var item in Layers)
            {
                if (item.ID > deletedlayer.ID)
                    item.ID--;
            }
            layerID--;
        }

        private void DuplicateLayer(object layer)
        {
            Mementor.BeginBatch();
            DisabledMessages();

            var lyr = layer as Layer;
            LayerModel layermodel = new LayerModel();
            lyr.Copy(layermodel);

            layerID++;
            layerNameID++;
            Layer newlayer = CreateLayer();
            var newlayername = CreateLayerName();
            Mementor.ElementAdd(Layers, newlayer);

            newlayer.Paste(layermodel);
            newlayer.LayerName = newlayername;
            newlayer.ID = layerID;
            newlayer.UpdateMessageAddress(newlayername);
            newlayer.Enabled = false;
            newlayer.Copy(layermodel);

            Layers.Move(Layers.IndexOf(SelectedLayer), Layers.IndexOf(lyr) + 1);

            SelectedLayer = newlayer;

            EnabledMessages();

            this.QueueLayerID();
            this.QueueLayerNames();
            this.QueueObjects(layermodel);
            this.SendQueues();

            Mementor.EndBatch();
        }

        private void DeleteLayer(object layer)
        {
            if(layer != null)
            {
                Mementor.BeginBatch();

                DisabledMessages();

                Layer removedlayer = layer as Layer;

                UpdateLayersIDOnDelete(removedlayer);
                Mementor.ElementRemove(Layers, removedlayer);
                Layers.Remove(removedlayer);

                EnabledMessages();

                this.QueueLayerID();
                this.QueueLayerNames();
                this.QueueMessages("LayerRemoved", removedlayer.LayerName);
                this.SendQueues();

                Mementor.EndBatch();
            }
        }
        #endregion

        #region COPY/PASTE/LOAD/SAVE/OPEN COMPOSITIONS

        public void Copy(CompositionModel compositionmodel)
        {
            compositionmodel.MessageAddress = MessageAddress;
            compositionmodel.Name = Name;
            compositionmodel.ContentFolderName = ContentFolderName;
            compositionmodel.LayerID = GetLayerID();
            compositionmodel.LayerNames = GetLayerNames();

            foreach (Layer lyr in Layers)
            {
                LayerModel layermodel = new LayerModel();
                lyr.Copy(layermodel);
                compositionmodel.LayersModel.Add(layermodel);
            }

            MasterBeat.Copy(compositionmodel.MasterBeatModel);
            Camera.Copy(compositionmodel.CameraModel);
            Transition.Copy(compositionmodel.TransitionModel);
        }

        public void Paste(CompositionModel compositionmodel)
        {
            Layers.Clear();
            layerID = -1;

            DisabledMessages();

            MessageAddress = compositionmodel.MessageAddress;
            Name = compositionmodel.Name;
            ContentFolderName = compositionmodel.ContentFolderName;

            foreach (LayerModel layermodel in compositionmodel.LayersModel)
            {
                Layer layer = new Layer(MasterBeat, CreateLayerName(), OSCValidation, Mementor);
                layer.Paste(layermodel);
                layer.ID = layerID++;
                Layers.Add(layer);
            }

            MasterBeat.Paste(compositionmodel.MasterBeatModel);
            Camera.Paste(compositionmodel.CameraModel);
            Transition.Paste(compositionmodel.TransitionModel);
            EnabledMessages();
        }

        public void Load(CompositionModel compositionmodel)
        {
            Name = compositionmodel.Name;
            ContentFolderName = compositionmodel.ContentFolderName;

            Layers.Clear();
            foreach (LayerModel layermodel in compositionmodel.LayersModel)
            {
                Layer layer = new Layer(MasterBeat, layermodel.LayerName, OSCValidation, Mementor);
                layer.Load(layermodel);
                Layers.Add(layer);
            }

            MasterBeat.Paste(compositionmodel.MasterBeatModel);
            Camera.Paste(compositionmodel.CameraModel);
            Transition.Paste(compositionmodel.TransitionModel);
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
            if (dropInfo.DragInfo != null)
            {
                int sourceindex = dropInfo.DragInfo.SourceIndex;
                int insertindex = dropInfo.InsertIndex;

                if(sourceindex != insertindex)
                {
                    Mementor.BeginBatch();

                    if (insertindex >= Layers.Count - 1)
                    {
                        insertindex -= 1;
                    }

                    Layers.Move(sourceindex, insertindex);
                    Mementor.ElementIndexChange(Layers, Layers[insertindex], sourceindex);
                    SelectedLayer = Layers[insertindex];

                    this.QueueLayerID();
                    this.QueueLayerNames();
                    SendQueues();
                    Mementor.EndBatch();
                }
            }
        }
        #endregion
    }
}
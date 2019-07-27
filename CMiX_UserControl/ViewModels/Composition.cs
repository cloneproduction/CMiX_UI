using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Threading.Tasks;
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
            MessageAddress = "/Layer";

            OSCValidation = new ObservableCollection<OSCValidation>();
            CreateOSCValidation(oscmessengers);

            Layers = new ObservableCollection<Layer>();
            Transition = new Slider("/Transition", OSCValidation, Mementor);
            Transition.Amount = 0.2;

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

        #region PUBLIC METHODS
        public Layer CreateLayer()
        {
            layerID += 1;
            layerNameID += 1;
            Layer layer = new Layer(MasterBeat, string.Format("{0}/", MessageAddress + layerNameID.ToString()), OSCValidation, Mementor);
            Layers.Add(layer);
            SelectedLayer = layer;

            return layer;
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

            List<string> layernames = new List<string>();
            foreach (var layer in Layers)
            {
                layernames.Add(layer.LayerName);
            }

            oscmessenger.QueueMessage("LayerNames", layernames.ToArray());
            oscmessenger.QueueMessage("/CompositionReloaded", true);
            oscmessenger.QueueObject(compositionmodel);
            await Task.Delay(TimeSpan.FromMinutes(Transition.Amount));
            oscmessenger.SendQueue();
        }

        private void QueueLayerNames()
        {
            List<string> layernames = new List<string>();
            foreach (var layer in Layers)
            {
                layernames.Add(layer.LayerName);
            }
            QueueMessages("LayerNames", layernames.ToArray());
        }
        #endregion

        public void UpdateLayerContentFolder(Layer layer)
        {
            layer.Content.Texture.FileSelector.FolderPath = ContentFolderName;
            layer.Content.Geometry.FileSelector.FolderPath = ContentFolderName;
            layer.Content.Geometry.GeometryFX.FileSelector.FolderPath = ContentFolderName;
            layer.Mask.Texture.FileSelector.FolderPath = ContentFolderName;
            layer.Mask.Geometry.FileSelector.FolderPath = ContentFolderName;
        }

        #region PROPERTIES
        private int layerID = -1;
        private int layerNameID = -1;

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

                var layermodel = (LayerModel)data.GetData("Layer") as LayerModel;
                var selectedlayermessageaddress = SelectedLayer.MessageAddress;
                var selectedlayername = SelectedLayer.LayerName;

                SelectedLayer.Paste(layermodel);
                SelectedLayer.UpdateMessageAddress(selectedlayermessageaddress);
                SelectedLayer.LayerName = selectedlayername;
                SelectedLayer.Copy(layermodel);

                Mementor.EndBatch();

                QueueLayerNames();
                QueueObjects(layermodel);
                SendQueues();
            }
        }
        #endregion

        #region ADD/REMOVE/DUPLICATE/DELETE LAYERS
        public void AddLayer()
        {
            Mementor.BeginBatch();
            DisabledMessages();

            var layer = CreateLayer();

            Mementor.ElementAdd(Layers, layer);
            UpdateLayerContentFolder(layer);

            LayerModel layermodel = new LayerModel();
            layer.Copy(layermodel);

            EnabledMessages();
            Mementor.EndBatch();

            QueueLayerNames();
            QueueObjects(layermodel);
            SendQueues();
        }

        private void DuplicateLayer(object layer)
        {
            Mementor.BeginBatch();
            DisabledMessages();

            var lyr = layer as Layer;
            LayerModel layermodel = new LayerModel();
            lyr.Copy(layermodel);

            Layer newlayer = CreateLayer();
            Mementor.ElementAdd(Layers, newlayer);

            newlayer.Paste(layermodel);
            newlayer.LayerName = string.Format("{0}/", MessageAddress + layerNameID.ToString());
            newlayer.UpdateMessageAddress(string.Format("{0}/", MessageAddress + layerNameID.ToString()));
            newlayer.Enabled = false;

            Layers.Move(Layers.IndexOf(SelectedLayer), Layers.IndexOf(lyr) + 1);

            
            SelectedLayer = newlayer;

            newlayer.Copy(layermodel);

            EnabledMessages();
            Mementor.EndBatch();

            QueueLayerNames();
            QueueObjects(layermodel);
            SendQueues();
        }

        private void DeleteLayer(object layer)
        {
            if(layer != null)
            {
                Mementor.BeginBatch();
                DisabledMessages();

                Layer removedlayer = layer as Layer;
                layerID -= 1;

                Mementor.ElementRemove(Layers, removedlayer);
                Layers.Remove(removedlayer);

                Mementor.EndBatch();
                EnabledMessages();

                QueueLayerNames();
                QueueMessages("LayerRemoved", removedlayer.LayerName);
                SendQueues();
            }
        }
        #endregion

        #region COPY/PASTE/LOAD/SAVE/OPEN COMPOSITIONS

        public void Copy(CompositionModel compositionmodel)
        {
            compositionmodel.MessageAddress = MessageAddress;
            compositionmodel.Name = Name;
            compositionmodel.ContentFolderName = ContentFolderName;

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
            DisabledMessages();

            MessageAddress = compositionmodel.MessageAddress;
            Name = compositionmodel.Name;
            ContentFolderName = compositionmodel.ContentFolderName;

            Layers.Clear();
            layerID = -1;

            foreach (LayerModel layermodel in compositionmodel.LayersModel)
            {
                layerID += 1;
                Layer layer = new Layer(MasterBeat, MessageAddress + layerID.ToString(), OSCValidation, Mementor);
                layer.Paste(layermodel);
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

        #region NOTIFYCOLLECTIONCHANGED
        //public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    LayerIndex.Clear();

        //    foreach (Layer lyr in Layers)
        //    {
        //        LayerIndex.Add(lyr.Index);
        //    }
        //}
        #endregion

        #region DRAG DROP
        public void DragOver(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as IDataObject;
            if (dropInfo.Data.GetType() == typeof(Layer) && Layers.Count > 1)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Copy;
            }

            if (dropInfo.Data.GetType() == typeof(FileNameItem) && Layers.Count > 1 && dropInfo.TargetItem != null)
            {
                SelectedLayer = dropInfo.TargetItem as Layer;
            }

            if (dataObject != null)
            {
                if (dataObject.GetDataPresent(DataFormats.FileDrop) && dropInfo.TargetItem != null)
                {
                    SelectedLayer = dropInfo.TargetItem as Layer;
                }
            }
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
                        Layers.Move(sourceindex, insertindex - 1);
                        SelectedLayer = Layers[insertindex - 1];
                        Mementor.ElementIndexChange(Layers, Layers[insertindex - 1], sourceindex);
                    }
                    else
                    {
                        Layers.Move(dropInfo.DragInfo.SourceIndex, dropInfo.InsertIndex);
                        SelectedLayer = Layers[insertindex];
                        Mementor.ElementIndexChange(Layers, Layers[insertindex], sourceindex);
                    }

                    QueueLayerNames();
                    SendQueues();
                    Mementor.EndBatch();
                }
            }
        }
        #endregion
    }
}
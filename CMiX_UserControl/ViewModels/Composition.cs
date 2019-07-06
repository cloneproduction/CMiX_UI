using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Windows;
using System.IO;
using Newtonsoft.Json;
using CMiX.Models;
using Memento;
using GongSolutions.Wpf.DragDrop;
using System.Linq;

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

            LayerNames = new List<string>();
            LayerIndex = new List<int>();
            Layers = new ObservableCollection<Layer>();
            

            MasterBeat = new MasterBeat(OSCValidation, Mementor);
            Camera = new Camera(OSCValidation, MasterBeat, Mementor);
            Mementor = new Mementor();


            // CREATE DEFAULT LAYER
            layerID += 1;
            layerNameID += 1;
            Layer layer = new Layer(MasterBeat, string.Format("{0}/", MessageAddress + layerNameID.ToString()), OSCValidation, layerNameID, Mementor);
            layer.Index = layerID;
            Layers.Add(layer);
            SelectedLayer = layer;
            LayerNames.Add(string.Format("{0}/", MessageAddress + layerNameID.ToString()));
            LayerIndex.Add(layer.Index);
            Layers.CollectionChanged += ContentCollectionChanged;

            ReloadCompositionCommand = new RelayCommand(p => ReloadComposition(p));
            AddLayerCommand = new RelayCommand(p => AddLayer());
            RemoveLayerCommand = new RelayCommand(p => RemoveLayer());
            DeleteLayerCommand = new RelayCommand(p => DeleteLayer(p));
            DuplicateLayerCommand = new RelayCommand(p => DuplicateLayer(p));
            CopyLayerCommand = new RelayCommand(p => CopyLayer());
            PasteLayerCommand = new RelayCommand(p => PasteLayer());
            SaveCompositionCommand = new RelayCommand(p => Save());
            OpenCompositionCommand = new RelayCommand(p => Open());
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

        private void ReloadComposition(object messenger)
        {
            CompositionModel compositionmodel = new CompositionModel();
            this.Copy(compositionmodel);
            OSCMessenger oscmessenger = messenger as OSCMessenger;
            oscmessenger.SendMessage("CompositionReloaded", true);
            oscmessenger.QueueObject(compositionmodel);
            oscmessenger.SendQueue();
        }
        #endregion

        #region PROPERTIES
        private int layerID = -1;
        private int layerNameID = -1;

        public ICommand ReloadCompositionCommand { get; }
        public ICommand AddLayerCommand { get; }
        public ICommand RemoveLayerCommand { get; }
        public ICommand CopyLayerCommand { get; }
        public ICommand PasteLayerCommand { get; }
        public ICommand SaveCompositionCommand { get; }
        public ICommand OpenCompositionCommand { get; }
        public ICommand DeleteLayerCommand { get; }
        public ICommand DuplicateLayerCommand { get; }

        public MasterBeat MasterBeat { get; set; }
        public Camera Camera { get; set; }

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
                    layer.Content.Texture.FileSelector.FolderPath = ContentFolderName;
                    layer.Content.Geometry.FileSelector.FolderPath = ContentFolderName;
                    layer.Mask.Texture.FileSelector.FolderPath = ContentFolderName;
                    layer.Mask.Geometry.FileSelector.FolderPath = ContentFolderName;
                }
                SendMessages("/ContentFolder", ContentFolderName);
            }
        }


        private List<string> _layernames;
        public List<string> LayerNames
        {
            get => _layernames;
            set => SetAndNotify(ref _layernames, value);
        }

        private List<int> _layerindex;
        public List<int> LayerIndex
        {
            get => _layerindex;
            set => SetAndNotify(ref _layerindex, value);
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
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "SelectedLayer");
                SetAndNotify(ref _selectedlayer, value);
            }

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
                var selectedlayerindex = SelectedLayer.Index;

                SelectedLayer.Paste(layermodel);
                SelectedLayer.UpdateMessageAddress(selectedlayermessageaddress);
                SelectedLayer.LayerName = selectedlayername;
                SelectedLayer.Index = selectedlayerindex;
                SelectedLayer.Copy(layermodel);

                Mementor.EndBatch();

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

            layerID += 1;
            layerNameID += 1;

            Layer layer = new Layer(MasterBeat, string.Format("{0}/", MessageAddress + layerNameID.ToString()), OSCValidation, layerNameID, Mementor);
            layer.Index = layerID;
            Layers.Add(layer);
            Mementor.ElementAdd(Layers, layer);
            SelectedLayer = layer;

            LayerNames.Add(string.Format("{0}/", MessageAddress + layerNameID.ToString()));

            LayerIndex.Clear();
            foreach (Layer lyr in Layers)
            {
                LayerIndex.Add(lyr.Index);
            }

            LayerModel layermodel = new LayerModel();
            layer.Copy(layermodel);

            EnabledMessages();
            Mementor.EndBatch();

            QueueMessages("LayerNames", LayerNames.ToArray());
            QueueMessages("LayerIndex", LayerIndex.Select(i => i.ToString()).ToArray());
            QueueObjects(layermodel);
            SendQueues();
        }

        private void DuplicateLayer(object layer)
        {
            Mementor.BeginBatch();
            DisabledMessages();

            layerID += 1;
            layerNameID += 1;
            LayerNames.Add(string.Format("{0}/", MessageAddress + layerNameID.ToString()));

            Layer lyr = layer as Layer;
            LayerModel layermodel = new LayerModel();
            lyr.Copy(layermodel);
            
            Layer newlayer = new Layer(MasterBeat, string.Format("{0}/", MessageAddress + layerNameID.ToString()), OSCValidation, layerNameID, Mementor);
            newlayer.Paste(layermodel);
            newlayer.LayerName = string.Format("{0}/", MessageAddress + layerNameID.ToString());
            newlayer.UpdateMessageAddress(string.Format("{0}/", MessageAddress + layerNameID.ToString()));
            newlayer.Index = layerID;
            newlayer.Enabled = false;
            
            Layers.Insert(Layers.IndexOf(lyr) + 1, newlayer);
            Mementor.ElementAdd(Layers, newlayer);
            SelectedLayer = newlayer;

            LayerIndex.Clear();
            foreach (Layer lay in Layers)
            {
                LayerIndex.Add(lay.Index);
            }

            newlayer.Copy(layermodel);

            EnabledMessages();
            Mementor.EndBatch();

            QueueMessages("LayerNames", this.LayerNames.ToArray());
            QueueMessages("LayerIndex", LayerIndex.Select(i => i.ToString()).ToArray());
            QueueObjects(layermodel);
            SendQueues();
        }

        private void RemoveLayer()
        {
            if(SelectedLayer != null)
            {
                Mementor.BeginBatch();
                DisabledMessages();

                layerID -= 1;
                LayerIndex.Clear();
                string removedlayername = SelectedLayer.LayerName;

                LayerNames.Remove(removedlayername);
                Layers.Remove(SelectedLayer);
                
                foreach (Layer lyr in Layers)
                {
                    if (lyr.Index > Layers.IndexOf(SelectedLayer))
                    {
                        lyr.Index -= 1;
                    }
                    LayerIndex.Add(lyr.Index);
                }

                EnabledMessages();
                Mementor.EndBatch();

                QueueMessages("LayerNames", this.LayerNames.ToArray());
                QueueMessages("LayerIndex", LayerIndex.Select(i => i.ToString()).ToArray());
                QueueMessages("LayerRemoved", removedlayername);
                SendQueues(); 
            }
        }

        private void DeleteLayer(object layer)
        {
            Mementor.BeginBatch();
            DisabledMessages();

            Layer lyr = layer as Layer;
            layerID -= 1;

            Mementor.ElementRemove(Layers, lyr);
            LayerNames.Remove(lyr.LayerName);
            Layers.Remove(lyr);

            LayerIndex.Clear();
            foreach (Layer lay in Layers)
            {
                if (lay.Index > lyr.Index)
                {
                    lay.Index -= 1;
                }
                LayerIndex.Add(lay.Index);
            }

            EnabledMessages();
            Mementor.EndBatch();

            QueueMessages("LayerNames", this.LayerNames.ToArray());
            QueueMessages("LayerIndex", LayerIndex.Select(i => i.ToString()).ToArray());
            QueueMessages("LayerRemoved", lyr.LayerName);
            SendQueues();
        }
        #endregion

        #region COPY/PASTE/LOAD/SAVE/OPEN COMPOSITIONS

        public void Copy(CompositionModel compositionmodel)
        {
            compositionmodel.MessageAddress = MessageAddress;
            compositionmodel.Name = Name;
            compositionmodel.LayerNames = LayerNames;
            compositionmodel.LayerIndex = LayerIndex;
            compositionmodel.ContentFolderName = ContentFolderName;


            foreach (Layer lyr in Layers)
            {
                LayerModel layermodel = new LayerModel();
                lyr.Copy(layermodel);
                compositionmodel.LayersModel.Add(layermodel);
            }

            MasterBeat.Copy(compositionmodel.MasterBeatModel);
            Camera.Copy(compositionmodel.CameraModel);
        }

        public void Paste(CompositionModel compositionmodel)
        {
            DisabledMessages();

            MessageAddress = compositionmodel.MessageAddress;
            Name = compositionmodel.Name;
            ContentFolderName = compositionmodel.ContentFolderName;
            
            LayerNames = compositionmodel.LayerNames;
            LayerIndex = compositionmodel.LayerIndex;
            Layers.Clear();
            layerID = -1;

            foreach (LayerModel layermodel in compositionmodel.LayersModel)
            {
                layerID += 1;
                Layer layer = new Layer(MasterBeat, MessageAddress + layerID.ToString(), OSCValidation, 0, Mementor);
                layer.Paste(layermodel);
                Layers.Add(layer);
            }

            MasterBeat.Paste(compositionmodel.MasterBeatModel);
            Camera.Paste(compositionmodel.CameraModel);

            EnabledMessages();
        }

        public void Load(CompositionModel compositionmodel)
        {
            Name = compositionmodel.Name;
            ContentFolderName = compositionmodel.ContentFolderName;

            LayerNames = compositionmodel.LayerNames;
            LayerIndex = compositionmodel.LayerIndex;
            Layers.Clear();
            foreach (LayerModel layermodel in compositionmodel.LayersModel)
            {
                Layer layer = new Layer(MasterBeat, layermodel.LayerName, OSCValidation, layermodel.Index, Mementor);
                layer.Load(layermodel);
                Layers.Add(layer);
            }

            MasterBeat.Paste(compositionmodel.MasterBeatModel);
            Camera.Paste(compositionmodel.CameraModel);
        }

        private void Save()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();

            if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CompositionModel compositionmodel = new CompositionModel();
                this.Copy(compositionmodel);
                string folderPath = savedialog.FileName;
                string json = JsonConvert.SerializeObject(compositionmodel);
                File.WriteAllText(folderPath, json);
            }
        }

        private void Open()
        {
            System.Windows.Forms.OpenFileDialog opendialog = new System.Windows.Forms.OpenFileDialog();

            opendialog.FileName = "default.json";

            if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = opendialog.FileName;

                if (opendialog.FileName.Trim() != string.Empty) // Check if you really have a file name 
                {
                    using (StreamReader r = new StreamReader(opendialog.FileName))
                    {
                        string json = r.ReadToEnd();
                        CompositionModel compositionmodel = new CompositionModel();
                        compositionmodel = JsonConvert.DeserializeObject<CompositionModel>(json);
                        this.Load(compositionmodel);

                        LayerIndex.Clear();
                        foreach (Layer lyr in this.Layers)
                        {
                            LayerIndex.Add(lyr.Index);
                        }
                    }
                }
            }
        }
        #endregion

        #region NOTIFYCOLLECTIONCHANGED
        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LayerIndex.Clear();

            foreach (Layer lyr in Layers)
            {
                LayerIndex.Add(lyr.Index);
            }

            QueueMessages("LayerNames", this.LayerNames.ToArray());
            QueueMessages("LayerIndex", LayerIndex.Select(i => i.ToString()).ToArray());
            SendQueues();
        }
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
                        Layers.Move(sourceindex, insertindex - 1); //error here on drop
                        SelectedLayer = Layers[insertindex - 1];
                        Mementor.ElementIndexChange(Layers, Layers[insertindex - 1], sourceindex);
                    }
                    else
                    {
                        Layers.Move(dropInfo.DragInfo.SourceIndex, dropInfo.InsertIndex);
                        SelectedLayer = Layers[insertindex];
                        Mementor.ElementIndexChange(Layers, Layers[insertindex], sourceindex);
                    }
                    Mementor.EndBatch();
                }
            }
        }
        #endregion
    }
}
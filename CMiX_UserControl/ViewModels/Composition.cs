using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Windows;
using System.IO;
using Newtonsoft.Json;

using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

using Memento;
using GongSolutions.Wpf.DragDrop;
using System.Linq;
using System.Threading.Tasks;

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

            //LayerNames = new List<string>();
            //LayerIndex = new List<int>();
            Layers = new ObservableCollection<Layer>();

            Transition = new Slider("/Transition", OSCValidation, Mementor);
            Transition.Amount = 0.2;

            MasterBeat = new MasterBeat(OSCValidation, Mementor);
            Camera = new Camera(OSCValidation, MasterBeat, Mementor);
            Mementor = new Mementor();


            // CREATE DEFAULT LAYER
            CreateLayer();

            //Layers.CollectionChanged += ContentCollectionChanged;

            ReloadCompositionCommand = new RelayCommand(p => ReloadComposition(p));
            AddLayerCommand = new RelayCommand(p => AddLayer());
            DeleteLayerCommand = new RelayCommand(p => DeleteLayer(p));
            DuplicateLayerCommand = new RelayCommand(p => DuplicateLayer(p));
            CopyLayerCommand = new RelayCommand(p => CopyLayer());
            PasteLayerCommand = new RelayCommand(p => PasteLayer());
            SaveCompositionCommand = new RelayCommand(p => Save());
            OpenCompositionCommand = new RelayCommand(p => Open());
        }
        #endregion

        public Layer CreateLayer()
        {
            layerID += 1;
            layerNameID += 1;
            Layer layer = new Layer(MasterBeat, string.Format("{0}/", MessageAddress + layerNameID.ToString()), OSCValidation, layerID, Mementor);
            Layers.Add(layer);
            SelectedLayer = layer;

            return layer;
        }

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
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "SelectedLayer");
                SetAndNotify(ref _selectedlayer, value);
            }

        }
        #endregion


        public void QueueLayerNames()
        {
            List<string> layernames = new List<string>();
            foreach (var layer in Layers)
            {
                layernames.Add(layer.LayerName);
            }
            QueueMessages("LayerNames", layernames.ToArray());
        }

        public void QueueLayerIndex()
        {
            List<int> layerindex = new List<int>();
            foreach (var layer in Layers)
            {
                layerindex.Add(layer.Index);
                Console.WriteLine(layer.Index);
            }
            QueueMessages("LayerIndex", layerindex.Select(i => i.ToString()).ToArray());
        }


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

            var layer = CreateLayer();

            Mementor.ElementAdd(Layers, layer);
            UpdateLayerContentFolder(layer);

            LayerModel layermodel = new LayerModel();
            layer.Copy(layermodel);

            EnabledMessages();
            Mementor.EndBatch();

            QueueLayerNames();
            QueueLayerIndex();
            QueueObjects(layermodel);
            SendQueues();
        }

        private void DuplicateLayer(object layer)
        {
            Mementor.BeginBatch();
            DisabledMessages();

            layerID += 1;
            layerNameID += 1;
            //LayerNames.Add(string.Format("{0}/", MessageAddress + layerNameID.ToString()));

            Layer lyr = layer as Layer;
            LayerModel layermodel = new LayerModel();
            lyr.Copy(layermodel);

            //layerID += 1;
            //layerNameID += 1;
            //Layer layer = new Layer(MasterBeat, string.Format("{0}/", MessageAddress + layerNameID.ToString()), OSCValidation, layerID, Mementor);
            //Layers.Add(layer);
            //SelectedLayer = layer;
            

            Layer newlayer = new Layer(MasterBeat, string.Format("{0}/", MessageAddress + layerNameID.ToString()), OSCValidation, layerNameID, Mementor);

            //lyr = newlayer.Clone() as Layer;
            newlayer.Paste(layermodel);
            newlayer.LayerName = string.Format("{0}/", MessageAddress + layerNameID.ToString());
            newlayer.UpdateMessageAddress(string.Format("{0}/", MessageAddress + layerNameID.ToString()));
            newlayer.Index = layerID;
            newlayer.Enabled = false;
            
            Layers.Insert(Layers.IndexOf(lyr) + 1, newlayer);

            Mementor.ElementAdd(Layers, newlayer);
            SelectedLayer = newlayer;

            newlayer.Copy(layermodel);

            EnabledMessages();
            Mementor.EndBatch();

            QueueLayerNames();
            QueueLayerIndex();

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

                foreach (Layer lay in Layers)
                {
                    if (lay.Index > removedlayer.Index)
                    {
                        lay.Index -= 1;
                    }
                }

                Mementor.ElementRemove(Layers, removedlayer);
                Layers.Remove(removedlayer);

                Mementor.EndBatch();
                EnabledMessages();

                QueueLayerNames();
                QueueLayerIndex();
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
                Layer layer = new Layer(MasterBeat, MessageAddress + layerID.ToString(), OSCValidation, 0, Mementor);
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
                Layer layer = new Layer(MasterBeat, layermodel.LayerName, OSCValidation, layermodel.Index, Mementor);
                layer.Load(layermodel);
                Layers.Add(layer);
            }

            MasterBeat.Paste(compositionmodel.MasterBeatModel);
            Camera.Paste(compositionmodel.CameraModel);
            Transition.Paste(compositionmodel.TransitionModel);
        }

        //private void Save()
        //{
        //    System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();

        //    if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        CompositionModel compositionmodel = new CompositionModel();
        //        this.Copy(compositionmodel);
        //        string folderPath = savedialog.FileName;
        //        string json = JsonConvert.SerializeObject(compositionmodel);
        //        File.WriteAllText(folderPath, json);
        //    }
        //}

        //private void Open()
        //{
        //    System.Windows.Forms.OpenFileDialog opendialog = new System.Windows.Forms.OpenFileDialog();

        //    opendialog.FileName = "default.json";

        //    if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        string folderPath = opendialog.FileName;

        //        if (opendialog.FileName.Trim() != string.Empty) // Check if you really have a file name 
        //        {
        //            using (StreamReader r = new StreamReader(opendialog.FileName))
        //            {
        //                string json = r.ReadToEnd();
        //                CompositionModel compositionmodel = new CompositionModel();
        //                compositionmodel = JsonConvert.DeserializeObject<CompositionModel>(json);
        //                this.Load(compositionmodel);
        //            }
        //        }
        //    }
        //}
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
                    QueueLayerIndex();

                    SendQueues();
                    Mementor.EndBatch();
                }
            }
        }
        #endregion
    }
}
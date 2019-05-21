using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Windows;
using System.IO;
using Newtonsoft.Json;
using CMiX.Services;
using CMiX.Models;
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
            Messengers = oscmessengers;
            MessageAddress = String.Empty;

            LayerNames = new List<string>();
            Layers = new ObservableCollection<Layer>();
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
            
            MasterBeat = new MasterBeat(Messengers, Mementor);
            Camera = new Camera(Messengers, MasterBeat, Mementor);
            Mementor = new Mementor();
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

        private List<string> _layernames;
        public List<string> LayerNames
        {
            get => _layernames;
            set => SetAndNotify(ref _layernames, value);
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

        private void ReloadComposition(object messenger)
        {
            OSCMessenger message = messenger as OSCMessenger;
            message.QueueObject(this);
            message.SendQueue();
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
                var layermodel = (LayerModel)data.GetData("Layer") as LayerModel;
                SelectedLayer.Paste(layermodel);
                QueueObjects(layermodel);
                SendQueues();
            }
        }
        #endregion

        #region ADD/REMOVE/DUPLICATE/DELETE LAYERS
        public void AddLayer()
        {
            Mementor.BeginBatch();

            layerID += 1;
            layerNameID += 1;

            Layer layer = new Layer(MasterBeat, "/Layer" + layerNameID.ToString(), Messengers, layerNameID, Mementor);
            layer.Index = layerID;
            Layers.Add(layer);
            Mementor.ElementAdd(Layers, layer);
            SelectedLayer = layer;

            LayerNames.Add("/Layer" + layerNameID.ToString());

            List<string> layerindex = new List<string>();
            foreach (Layer lyr in Layers)
            {
                layerindex.Add(lyr.Index.ToString());
            }

            LayerModel layermodel = new LayerModel();
            layer.Copy(layermodel);

            QueueMessages("/LayerNames", LayerNames.ToArray());
            QueueMessages("/LayerIndex", layerindex.ToArray());
            QueueObjects(layermodel);
            SendQueues();

            Mementor.EndBatch();
        }

        private void DuplicateLayer(object layer)
        {
            Mementor.BeginBatch();

            layerID += 1;
            layerNameID += 1;
            LayerNames.Add("/Layer" + layerNameID.ToString());

            Layer lyr = layer as Layer;
            LayerModel layermodel = new LayerModel();
            lyr.Copy(layermodel);

            Layer newlayer = new Layer(MasterBeat, "/Layer" + layerNameID.ToString(), Messengers, layerNameID, Mementor);
            newlayer.Paste(layermodel);
            newlayer.LayerName = "/Layer" + layerNameID.ToString();
            newlayer.Index = layerID;
            newlayer.Enabled = false;

            int index = Layers.IndexOf(lyr) + 1;
            Layers.Insert(index, newlayer);
            Mementor.ElementAdd(Layers, newlayer);
            SelectedLayer = newlayer;

            List<string> layerindex = new List<string>();
            foreach (Layer lay in Layers)
            {
                layerindex.Add(lay.Index.ToString());
            }


            QueueMessages("/LayerNames", this.LayerNames.ToArray());
            QueueMessages("/LayerIndex", layerindex.ToArray());
            QueueObjects(newlayer);
            SendQueues();

            Mementor.EndBatch();
        }

        private void RemoveLayer()
        {
            if(SelectedLayer != null)
            {
                Mementor.BeginBatch();

                layerID -= 1;
                string removedlayername = string.Empty;
                List<string> layerindex = new List<string>();
                removedlayername = SelectedLayer.LayerName;
                LayerNames.Remove(SelectedLayer.LayerName);
                Layers.Remove(SelectedLayer);

                foreach (Layer lyr in Layers)
                {
                    if (lyr.Index > Layers.IndexOf(SelectedLayer))
                    {
                        lyr.Index -= 1;
                    }
                    layerindex.Add(lyr.Index.ToString());
                }

                QueueMessages("/LayerNames", this.LayerNames.ToArray());
                QueueMessages("/LayerIndex", layerindex.ToArray());
                QueueMessages("/LayerRemoved", removedlayername);
                SendQueues();

                Mementor.EndBatch();
            }
        }

        private void DeleteLayer(object layer)
        {
            Mementor.BeginBatch();

            Layer lyr = layer as Layer;
            layerID -= 1;

            Mementor.ElementRemove(Layers, lyr);
            LayerNames.Remove(lyr.LayerName);
            Layers.Remove(lyr);
            

            List<string> layerindex = new List<string>();
            foreach (Layer lay in Layers)
            {
                if (lay.Index > lyr.Index)
                {
                    lay.Index -= 1;
                }
                layerindex.Add(lay.Index.ToString());
            }

            QueueMessages("/LayerNames", this.LayerNames.ToArray());
            QueueMessages("/LayerIndex", layerindex.ToArray());
            QueueMessages("/LayerRemoved", lyr.LayerName);
            SendQueues();

            Mementor.EndBatch();
        }
        #endregion

        #region COPY/PASTE/LOAD/SAVE/OPEN COMPOSITIONS

        public void Copy(CompositionModel compositionmodel)
        {
            compositionmodel.Name = Name;
            compositionmodel.LayerNames = LayerNames;

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
            Name = compositionmodel.Name;
            LayerNames = compositionmodel.LayerNames;

            Layers.Clear();
            layerID = -1;

            foreach (LayerModel layermodel in compositionmodel.LayersModel)
            {
                layerID += 1;
                Layer layer = new Layer(MasterBeat, "/Layer" + layerID.ToString(), Messengers, 0, Mementor);
                layer.Paste(layermodel);
                Layers.Add(layer);
            }

            MasterBeat.Paste(compositionmodel.MasterBeatModel);
            Camera.Paste(compositionmodel.CameraModel);
        }

        public void Load(CompositionModel compositionmodel)
        {
            Name = compositionmodel.Name;
            LayerNames = compositionmodel.LayerNames;

            Layers.Clear();
            foreach (LayerModel layermodel in compositionmodel.LayersModel)
            {
                Layer layer = new Layer(MasterBeat, layermodel.LayerName, Messengers, layermodel.Index, Mementor);
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

                        List<string> layerindex = new List<string>();
                        foreach (Layer lyr in this.Layers)
                        {
                            layerindex.Add(lyr.Index.ToString());
                        }

                        QueueObjects(this);
                        QueueMessages("/LayerNames", this.LayerNames.ToArray());
                        QueueMessages("/LayerIndex", layerindex.ToArray());
                        SendQueues();
                    }
                }
            }
        }
        #endregion

        #region NOTIFYCOLLECTIONCHANGED
        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<string> layerindex = new List<string>();

            foreach (Layer lyr in Layers)
            {
                layerindex.Add(lyr.Index.ToString());
            }

            QueueMessages("/LayerNames", this.LayerNames.ToArray());
            QueueMessages("/LayerIndex", layerindex.ToArray());
            SendQueues();
        }
        #endregion

        #region DRAG DROP
        public void DragOver(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as IDataObject;
            if (dropInfo.Data.GetType() == typeof(Layer))
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Copy;
            }

            if (dataObject != null)
            {
                if (dataObject.GetDataPresent(DataFormats.FileDrop) || dropInfo.Data.GetType() == typeof(FileNameItem))
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
        #endregion
    }
}
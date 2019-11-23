using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Commands;
using Memento;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.ViewModels
{
    public class Composition : ViewModel, IDropTarget
    {

        #region CONSTRUCTORS
        public Composition(ObservableCollection<Server> servers, Assets assets)
        {
            Name = string.Empty;
            MessageAddress = "/Composition";

            ServerValidation = new ObservableCollection<ServerValidation>();
            CreateServerValidation(servers);

            Assets = assets;
            Layers = new ObservableCollection<Layer>();
            Mementor = new Mementor();

            Transition = new Slider("/Transition", ServerValidation, Mementor);
            ContentFolderName = string.Empty;
            MasterBeat = new MasterBeat(ServerValidation, Mementor);
            Camera = new Camera(ServerValidation, MasterBeat, Mementor);

            string messageAddress = CreateLayerMessageAddress();
            CreateLayer(messageAddress, LayerID);

            ReloadCompositionCommand = new RelayCommand(p => ReloadComposition(p));
            ReloadAllOSCCommand = new RelayCommand(p => ReloadAllOSC());
            ResetAllOSCCommand = new RelayCommand(p => ResetAllOSC());
            AddLayerCommand = new RelayCommand(p => AddLayer());
            DeleteLayerCommand = new RelayCommand(p => DeleteLayer());
            DuplicateLayerCommand = new RelayCommand(p => DuplicateLayer());
            CopyLayerCommand = new RelayCommand(p => CopyLayer());
            PasteLayerCommand = new RelayCommand(p => PasteLayer());
            ResetLayerCommand = new RelayCommand(p => ResetLayer());
        }
        #endregion


        #region PROPERTIES

        #region COMMANDS
        public ICommand ReloadCompositionCommand { get; }
        public ICommand ResetAllOSCCommand { get; }
        public ICommand ReloadAllOSCCommand { get; }

        public ICommand AddLayerCommand { get; }
        public ICommand CopyLayerCommand { get; }
        public ICommand PasteLayerCommand { get; }
        public ICommand ResetLayerCommand { get; }

        public ICommand SaveCompositionCommand { get; }
        public ICommand OpenCompositionCommand { get; }
        public ICommand DeleteLayerCommand { get; }
        public ICommand DuplicateLayerCommand { get; }
        #endregion

        public MasterBeat MasterBeat { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }

        public ObservableCollection<Layer> Layers { get; }

        private int _LayerID = 0;
        public int LayerID
        {
            get { return _LayerID; }
            set { _LayerID = value; }
        }

        private int _layerNameID = 0;
        public int layerNameID
        {
            get { return _layerNameID; }
            set { _layerNameID = value; }
        }

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
                //SendMessages("/ContentFolder", ContentFolderName);
            }
        }

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public Assets Assets { get; set; }

        private Layer _selectedlayer;
        public Layer SelectedLayer
        {
            get => _selectedlayer;
            set => SetAndNotify(ref _selectedlayer, value);
        }
        #endregion

        #region PUBLIC METHODS
        public Layer CreateLayer(string messageAddress, int layerID)
        {
            Layer layer = new Layer(MasterBeat, messageAddress, ServerValidation, Mementor);
            layer.ID = layerID;
            layer.Name = "Layer " + layerID;
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
            var LayerID = new List<int>();
            foreach (var layer in Layers)
            {
                LayerID.Add(layer.ID);
            }
            return LayerID;
        }

        public void QueueLayerNames()
        {
            //this.QueueMessages("/LayerNames", GetLayerNames().ToArray());
        }

        public void QueueLayerID()
        {
            //this.QueueMessages("/LayerID", GetLayerID().Select(x => x.ToString()).ToArray());
        }

        public void UpdateLayerContentFolder(Layer layer)
        {
            //layer.Content.Texture.FileSelector.FolderPath = ContentFolderName;
            //layer.Content.Geometry.FileSelector.FolderPath = ContentFolderName;
            //layer.Content.Geometry.GeometryFX.FileSelector.FolderPath = ContentFolderName;
            layer.Mask.Texture.FileSelector.FolderPath = ContentFolderName;
            layer.Mask.Geometry.FileSelector.FolderPath = ContentFolderName;
        }
        #endregion

        #region PRIVATE METHODS
        private string CreateLayerMessageAddress()
        {
            return "/Layer" + layerNameID.ToString() + "/";
        }

        private void CreateServerValidation(ObservableCollection<Server> servers)
        {
            foreach (var server in servers)
            {
                ServerValidation.Add(new ServerValidation(server));
            }
        }

        private async void ReloadComposition(object messenger)
        {
            //CompositionModel compositionmodel = new CompositionModel();
            //this.Copy(compositionmodel);
            //OSCMessenger oscmessenger = messenger as OSCMessenger;

            //oscmessenger.QueueMessage("/Transition/Amount", Transition.Amount);
            //oscmessenger.QueueMessage("/Transition/Start", true);
            //oscmessenger.SendQueue();

            //oscmessenger.QueueMessage("/CompositionReloaded", true);
            //oscmessenger.QueueObject(compositionmodel);
            //await Task.Delay(TimeSpan.FromMinutes(Transition.Amount));
            //oscmessenger.SendQueue();


        }

        private void ReloadAllOSC()
        {
            foreach (var serverValidation in ServerValidation)
                ReloadComposition(serverValidation.Server);

        }

        private void ResetAllOSC()
        {
            //foreach (var serverValidation in ServerValidation)
                //serverValidation.Server.Send("/CompositionReloaded", true);
        }
        #endregion

        #region COPY/PASTE/RESET LAYER
        private void CopyLayer()
        {
            LayerModel layermodel = new LayerModel();
            SelectedLayer.Copy(layermodel);
            IDataObject data = new DataObject();
            data.SetData("LayerModel", layermodel, false);
            Clipboard.SetDataObject(data);
        }

        private void PasteLayer()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("LayerModel"))
            {
                Mementor.BeginBatch();

                var selectedlayermessageaddress = SelectedLayer.MessageAddress;
                var selectedlayername = SelectedLayer.LayerName;
                var selectedname = SelectedLayer.Name;
                var selectedLayerID = SelectedLayer.ID;

                var layermodel = data.GetData("LayerModel") as LayerModel;

                SelectedLayer.Paste(layermodel);
                SelectedLayer.UpdateMessageAddress(selectedlayermessageaddress);
                SelectedLayer.LayerName = selectedlayername;
                SelectedLayer.Name = selectedname;
                SelectedLayer.ID = selectedLayerID;
                SelectedLayer.Copy(layermodel);

                //SendMessages("LayerModel", layermodel);

                Mementor.EndBatch();
            }
        }

        public void ResetLayer()
        {
            SelectedLayer.Reset();
            LayerModel layerModel = new LayerModel();
            SelectedLayer.Copy(layerModel);
            //SendMessages("LayerModel", layerModel);
        }
        #endregion

        #region ADD/DUPLICATE/DELETE LAYERS

        public void AddLayer()
        {
            Mementor.BeginBatch();
            DisabledMessages();

            LayerID++;
            layerNameID++;
            
            string messageAddress = CreateLayerMessageAddress();
            var layer = CreateLayer(messageAddress, LayerID);

            Mementor.ElementAdd(Layers, layer);
            UpdateLayerContentFolder(layer);
            EnabledMessages();

            LayerModel layerModel = new LayerModel();
            layer.Copy(layerModel);
            SendMessages(MessageAddress, MessageCommand.LAYER_ADD, null, layerModel);

            Mementor.EndBatch();
        }



        private void DuplicateLayer()
        {
            if(SelectedLayer != null)
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var lyr = SelectedLayer;
                LayerModel layermodel = new LayerModel();
                lyr.Copy(layermodel);

                LayerID++;
                layerNameID++;
                var messageAddress = CreateLayerMessageAddress();
                Layer newlayer = CreateLayer(messageAddress, LayerID);
                
                Mementor.ElementAdd(Layers, newlayer);

                newlayer.Paste(layermodel);
                newlayer.LayerName = messageAddress;
                newlayer.Name += " - Copy";
                newlayer.ID = LayerID;
                newlayer.UpdateMessageAddress(messageAddress);
                newlayer.Enabled = false;
                newlayer.Copy(layermodel);

                int oldIndex = Layers.IndexOf(SelectedLayer);
                int newIndex = Layers.IndexOf(lyr) + 1;
                Layers.Move(oldIndex, newIndex);
                int[] movedIndex = new int[2] { oldIndex, newIndex };

                SelectedLayer = newlayer;
                EnabledMessages();

                SendMessages(MessageAddress, MessageCommand.LAYER_DUPLICATE, movedIndex, layermodel);

                Mementor.EndBatch();
            }
        }

        private void DeleteLayer()
        {
            if(SelectedLayer != null)
            {
                Mementor.BeginBatch();

                DisabledMessages();

                Layer removedlayer = SelectedLayer as Layer;
                int removedlayerindex = Layers.IndexOf(removedlayer);
                UpdateLayersIDOnDelete(removedlayer);
                Mementor.ElementRemove(Layers, removedlayer);
                Layers.Remove(removedlayer);

                if (Layers.Count == 0)
                    layerNameID = 0;

                if (Layers.Count > 0)
                {
                    if(removedlayerindex > 0)
                        SelectedLayer = Layers[removedlayerindex - 1];
                    else
                        SelectedLayer = Layers[0];
                }

                EnabledMessages();
                SendMessages(MessageAddress, MessageCommand.LAYER_DELETE, null, removedlayerindex);

                Mementor.EndBatch();
            }
        }

        public void UpdateLayersIDOnDelete(Layer deletedlayer)
        {
            foreach (var item in Layers)
            {
                if (item.ID > deletedlayer.ID)
                    item.ID--;
            }
            LayerID--;
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
            compositionmodel.layerID = LayerID;
            compositionmodel.layerNameID = layerNameID;

            foreach (Layer lyr in Layers)
            {
                LayerModel layermodel = new LayerModel();
                lyr.Copy(layermodel);
                compositionmodel.LayersModel.Add(layermodel);
            }

            SelectedLayer.Copy(compositionmodel.SelectedLayer);
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

            LayerID = compositionmodel.layerID;
            layerNameID = compositionmodel.layerNameID;

            Layers.Clear();
            foreach (LayerModel layermodel in compositionmodel.LayersModel)
            {
                Layer layer = new Layer(MasterBeat, CreateLayerMessageAddress(), ServerValidation, Mementor);
                layer.Paste(layermodel);
                Layers.Add(layer);
            }

            SelectedLayer.Paste(compositionmodel.SelectedLayer);
            MasterBeat.Paste(compositionmodel.MasterBeatModel);
            Camera.Paste(compositionmodel.CameraModel);
            Transition.Paste(compositionmodel.TransitionModel);

            EnabledMessages();
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

                    int[] moveIndex = new int[2] { sourceindex, insertindex };

                    SendMessages(MessageAddress, MessageCommand.LAYER_MOVE, null, moveIndex);

                    Mementor.EndBatch();
                }
            }
        }
        #endregion
    }
}
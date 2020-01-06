using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Services;
using CMiX.MVVM;
using Memento;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.Studio.ViewModels
{
    public class Composition : ViewModel, ICopyPasteModel, ISendable, IUndoable, IDropTarget
    {

        #region CONSTRUCTORS
        public Composition(Sender sender, string messageAddress, EntityFactory entityFactory, Assets assets, Mementor mementor)
        {
            Name = string.Empty;

            MessageAddress = $"{messageAddress}{nameof(Composition)}/";
            Sender = sender;
            EntityFactory = entityFactory;
            Assets = assets;
            Mementor = mementor;

            Layers = new ObservableCollection<Layer>();

            Transition = new Slider("/Transition", sender, Mementor);
            MasterBeat = new MasterBeat(sender);
            Camera = new Camera(sender, MessageAddress, MasterBeat, Mementor);

            string layerMessageAddress = CreateLayerMessageAddress();
            CreateLayer(layerMessageAddress, LayerID);

            ReloadCompositionCommand = new RelayCommand(p => ReloadComposition(p));

            AddLayerCommand = new RelayCommand(p => AddLayer());
            DeleteLayerCommand = new RelayCommand(p => DeleteLayer());
            DuplicateLayerCommand = new RelayCommand(p => DuplicateLayer());
            CopyLayerCommand = new RelayCommand(p => CopyLayer());
            PasteLayerCommand = new RelayCommand(p => PasteLayer());
            ResetLayerCommand = new RelayCommand(p => ResetLayer());

            AddEntityCommand = new RelayCommand(p => AddEntity(p));
            DeleteEntityCommand = new RelayCommand(p => DeleteEntity(p));
        }
        #endregion

        #region PROPERTIES
        public ICommand ReloadCompositionCommand { get; }

        public ICommand AddLayerCommand { get; }
        public ICommand CopyLayerCommand { get; }
        public ICommand PasteLayerCommand { get; }
        public ICommand ResetLayerCommand { get; }

        public ICommand SaveCompositionCommand { get; }
        public ICommand OpenCompositionCommand { get; }
        public ICommand DeleteLayerCommand { get; }
        public ICommand DuplicateLayerCommand { get; }

        public ICommand AddEntityCommand { get; set; }
        public ICommand DeleteEntityCommand { get; set; }


        public MessageService MessageService { get; set; }

        public EntityFactory EntityFactory { get; set; }
        public MasterBeat MasterBeat { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }

        public ObservableCollection<Layer> Layers { get; }

        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }

        private int _LayerID = 0;
        public int LayerID
        {
            get { return _LayerID; }
            set { _LayerID = value; }
        }

        private int _layerNameID = 0;
        public int LayerNameID
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

        private Layer _selectedlayer;
        public Layer SelectedLayer
        {
            get => _selectedlayer;
            set => SetAndNotify(ref _selectedlayer, value);
        }
        #endregion

        #region PUBLIC METHODS
        public void AddEntity(object layerControl)
        {
            if (layerControl != null)
            {
                var lc = layerControl as ISendableEntityContext;
                BeatModifier bm = new BeatModifier(lc.MessageAddress, this.MasterBeat, Sender, Mementor);
                var entity = EntityFactory.CreateEntity(bm, lc.MessageAddress, Sender, Mementor);
                lc.Entities.Add(entity);
                lc.SelectedEntity = entity;
            }
        }

        public void DeleteEntity(object layerControl)
        {
            if (layerControl != null)
            {
                var lc = layerControl as ISendableEntityContext;
                lc.Entities.Remove(lc.SelectedEntity);
                if (lc.Entities.Count > 0)
                    lc.SelectedEntity = lc.Entities[lc.Entities.Count - 1];
                else
                    lc.SelectedEntity = null;
            }
        }

        public Layer CreateLayer(string messageAddress, int layerID)
        {
            Layer layer = new Layer(MasterBeat, messageAddress, Sender, Mementor)
            {
                ID = layerID,
                DisplayName = "Layer " + layerID
            };
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

        #endregion

        #region PRIVATE METHODS
        private string CreateLayerMessageAddress()
        {
            return $"{this.MessageAddress}Layer{LayerNameID.ToString()}/";
        }

        private void ReloadComposition(object sender)
        {
            CompositionModel compositionModel = new CompositionModel();
            this.CopyModel(compositionModel);
            Sender.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, compositionModel);
        }
        #endregion

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

            LayerID++;
            LayerNameID++;
            
            string messageAddress = CreateLayerMessageAddress();
            var layer = CreateLayer(messageAddress, LayerID);

            Mementor.ElementAdd(Layers, layer);
            Sender.Enable();

            LayerModel layerModel = new LayerModel();
            layer.CopyModel(layerModel);

            Sender.SendMessages(MessageAddress, MessageCommand.LAYER_ADD, null, layerModel);

            Mementor.EndBatch();
        }

        private void DuplicateLayer()
        {
            if(SelectedLayer != null)
            {
                Mementor.BeginBatch();
                Sender.Disable();

                var lyr = SelectedLayer;
                LayerModel layermodel = new LayerModel();
                lyr.CopyModel(layermodel);

                LayerID++;
                LayerNameID++;
                var messageAddress = CreateLayerMessageAddress();

                Layer newlayer = CreateLayer(messageAddress, LayerID);
                newlayer.PasteModel(layermodel);
                newlayer.LayerName = messageAddress;
                newlayer.DisplayName += " - Copy";
                newlayer.ID = LayerID;
                newlayer.UpdateMessageAddress(messageAddress);
                newlayer.Enabled = false;
                newlayer.CopyModel(layermodel);

                SelectedLayer = newlayer;
                Mementor.ElementAdd(Layers, newlayer);

                int oldIndex = Layers.IndexOf(SelectedLayer);
                int newIndex = Layers.IndexOf(lyr) + 1;
                Layers.Move(oldIndex, newIndex);
                int[] movedIndex = new int[2] { oldIndex, newIndex };

                Sender.Enable();
                Mementor.EndBatch();

                Sender.SendMessages(MessageAddress, MessageCommand.LAYER_DUPLICATE, movedIndex, layermodel);
            }
        }

        private void DeleteLayer()
        {
            if(SelectedLayer != null)
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
                    if(removedlayerindex > 0)
                        SelectedLayer = Layers[removedlayerindex - 1];
                    else
                        SelectedLayer = Layers[0];
                }
                else
                {
                    LayerNameID = 0;
                }

                Sender.Enable();
                Mementor.EndBatch();

                Sender.SendMessages(MessageAddress, MessageCommand.LAYER_DELETE, null, removedlayerindex);
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

        public void CopyModel(IModel model)
        {
            CompositionModel compositionModel = model as CompositionModel;
            compositionModel.MessageAddress = MessageAddress;
            compositionModel.Name = Name;
            //compositionmodel.ContentFolderName = ContentFolderName;

            compositionModel.LayerID = GetLayerID();
            compositionModel.LayerNames = GetLayerNames();
            compositionModel.layerID = LayerID;
            compositionModel.layerNameID = LayerNameID;

            foreach (Layer lyr in Layers)
            {
                LayerModel layermodel = new LayerModel();
                lyr.CopyModel(layermodel);
                compositionModel.LayersModel.Add(layermodel);
            }

            SelectedLayer.CopyModel(compositionModel.SelectedLayer);
            MasterBeat.CopyModel(compositionModel.MasterBeatModel);
            Camera.CopyModel(compositionModel.CameraModel);
            Transition.CopyModel(compositionModel.TransitionModel);
        }

        public void PasteModel(IModel model)
        {
            CompositionModel compositionModel = model as CompositionModel;
            Sender.Disable();

            MessageAddress = compositionModel.MessageAddress;
            Name = compositionModel.Name;
            //ContentFolderName = compositionmodel.ContentFolderName;

            LayerID = compositionModel.layerID;
            LayerNameID = compositionModel.layerNameID;

            Layers.Clear();
            foreach (LayerModel layermodel in compositionModel.LayersModel)
            {
                Layer layer = new Layer(MasterBeat, CreateLayerMessageAddress(), Sender, Mementor);
                layer.PasteModel(layermodel);
                Layers.Add(layer);
            }

            SelectedLayer.PasteModel(compositionModel.SelectedLayer);
            MasterBeat.PasteModel(compositionModel.MasterBeatModel);
            Camera.PasteModel(compositionModel.CameraModel);
            Transition.PasteModel(compositionModel.TransitionModel);

            Sender.Enable();
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

                    Sender.SendMessages(MessageAddress, MessageCommand.LAYER_MOVE, null, moveIndex);

                    Mementor.EndBatch();
                }
            }
        }
        #endregion
    }
}
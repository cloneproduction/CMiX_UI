using System;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class Geometry : ViewModel, ISendable, IUndoable, IGetSet<GeometryModel>
    {
        #region CONSTRUCTORS
        public Geometry(string messageaddress, MessageService messageService, Mementor mementor, Beat beat) 
        {
            MessageAddress = $"{messageaddress}{nameof(Geometry)}/";
            MessageService = messageService;
            Mementor = mementor;

            Instancer = new Instancer(MessageAddress, messageService, mementor, beat);
            Transform = new Transform(MessageAddress, messageService, mementor);
            GeometryFX = new GeometryFX(MessageAddress, messageService, mementor);
            AssetSelector = new AssetSelector(MessageAddress, messageService, mementor);
           
            CopyGeometryCommand = new RelayCommand(p => CopyGeometry());
            PasteGeometryCommand = new RelayCommand(p => PasteGeometry());
            ResetGeometryCommand = new RelayCommand(p => ResetGeometry());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyGeometryCommand { get; }
        public ICommand PasteGeometryCommand { get; }
        public ICommand ResetGeometryCommand { get; }

        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }

        public AssetSelector AssetSelector { get; set; }
        public Transform Transform { get; }
        public Instancer Instancer { get;  }
        public GeometryFX GeometryFX { get; }

        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("GeometryModel", GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("GeometryModel"))
            {
                Mementor.BeginBatch();
                MessageService.Disable();

                var geometrymodel = data.GetData("GeometryModel") as GeometryModel;
                var geometrymessageaddress = MessageAddress;
                this.Paste(geometrymodel);

                MessageService.Enable();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, geometrymodel);
            }
        }

        public void ResetGeometry()
        {
            GeometryModel geometrymodel = GetModel();
            this.Reset();
            //SendMessages(MessageAddress, geometrymodel);
        }

        public void Paste(GeometryModel geometryModel)
        {
            MessageService.Disable();

            Transform.Paste(geometryModel.TransformModel);
            GeometryFX.Paste(geometryModel.GeometryFXModel);
            Instancer.Paste(geometryModel.InstancerModel);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();
            //Mementor.BeginBatch();
            Transform.Reset();
            GeometryFX.Reset();
            Instancer.Reset();
            //Mementor.EndBatch();
            MessageService.Enable();

            //SendMessages(MessageAddress, GetModel());
        }

        public GeometryModel GetModel()
        {
            GeometryModel model = new GeometryModel();

            model.TransformModel = Transform.GetModel();
            model.GeometryFXModel = GeometryFX.GetModel();
            model.InstancerModel = Instancer.GetModel();
            model.AssetSelectorModel = AssetSelector.GetModel();

            return model;
        }

        public void SetViewModel(GeometryModel model)
        {
            Transform.SetViewModel(model.TransformModel);
            GeometryFX.SetViewModel(model.GeometryFXModel);
            Instancer.SetViewModel(model.InstancerModel);
            AssetSelector.SetViewModel(model.AssetSelectorModel);
        }
        #endregion
    }
}
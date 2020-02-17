using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Geometry : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Geometry(string messageaddress, MessageService messageService, Mementor mementor, Assets assets, Beat beat) 
        {
            MessageAddress = $"{messageaddress}{nameof(Geometry)}/";
            MessageService = messageService;
            Instancer = new Instancer(MessageAddress, messageService, mementor, beat);
            Transform = new Transform(MessageAddress, messageService, mementor);

            AssetSelector = new AssetSelector<GeometryItem>(MessageAddress, assets, messageService, mementor);
            GeometryFX = new GeometryFX(MessageAddress, messageService, mementor);

            CopyGeometryCommand = new RelayCommand(p => CopyGeometry());
            PasteGeometryCommand = new RelayCommand(p => PasteGeometry());
            ResetGeometryCommand = new RelayCommand(p => ResetGeometry());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyGeometryCommand { get; }
        public ICommand PasteGeometryCommand { get; }
        public ICommand ResetGeometryCommand { get; }

        public AssetSelector<GeometryItem> AssetSelector { get; set; }
        public Transform Transform { get; }
        public Instancer Instancer { get;  }
        public GeometryFX GeometryFX { get; }
        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }
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

        public GeometryModel GetModel()
        {
            GeometryModel geometryModel = new GeometryModel();

            geometryModel.Transform = Transform.GetModel();
            geometryModel.GeometryFX = GeometryFX.GetModel();
            geometryModel.Instancer = Instancer.GetModel();
            return geometryModel;
        }


        public void Paste(GeometryModel geometryModel)
        {
            MessageService.Disable();

            Transform.Paste(geometryModel.Transform);
            GeometryFX.Paste(geometryModel.GeometryFX);
            Instancer.Paste(geometryModel.Instancer);

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
        #endregion
    }
}
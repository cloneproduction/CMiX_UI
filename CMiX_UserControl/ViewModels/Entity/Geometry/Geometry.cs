using System;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class Geometry : ViewModel, IUndoable
    {
        #region CONSTRUCTORS
        public Geometry(string messageaddress, Mementor mementor, Beat beat) 
        {
            MessageAddress = $"{messageaddress}{nameof(Geometry)}/";
            //MessengerService = messengerService;
            Mementor = mementor;

            Instancer = new Instancer(MessageAddress, mementor, beat);
            Transform = new Transform(MessageAddress, mementor);
            GeometryFX = new GeometryFX(MessageAddress, mementor);

            AssetPathSelector = new AssetPathSelector<AssetGeometry>(MessageAddress, mementor);

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
        public MessengerService MessengerService { get; set; }

        public AssetPathSelector<AssetGeometry> AssetPathSelector { get; set; }

        public Transform Transform { get; set; }
        public Instancer Instancer { get; set; }
        public GeometryFX GeometryFX { get; set; }

        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("GeometryModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("GeometryModel"))
            {
                Mementor.BeginBatch();
                MessengerService.Disable();

                var geometrymodel = data.GetData("GeometryModel") as GeometryModel;
                var geometrymessageaddress = MessageAddress;
                this.SetViewModel(geometrymodel);

                MessengerService.Enable();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, geometrymodel);
            }
        }

        public void ResetGeometry()
        {
            GeometryModel geometrymodel = this.GetModel();
            this.Reset();
            //SendMessages(MessageAddress, geometrymodel);
        }


        public void Reset()
        {
            MessengerService.Disable();
            //Mementor.BeginBatch();
            Transform.Reset();
            GeometryFX.Reset();
            Instancer.Reset();
            //Mementor.EndBatch();
            MessengerService.Enable();

            //SendMessages(MessageAddress, GetModel());
        }

        public void SetViewModel(GeometryModel model)
        {
            Transform.SetViewModel(model.TransformModel);
            GeometryFX.SetViewModel(model.GeometryFXModel);
            Instancer.SetViewModel(model.InstancerModel);
            AssetPathSelector.SetViewModel(model.AssetPathSelectorModel);
        }
        #endregion
    }
}
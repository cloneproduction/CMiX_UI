using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class Geometry : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Geometry(string messageaddress, Messenger messenger, Mementor mementor, Beat beat) 
        {
            MessageAddress = $"{messageaddress}{nameof(Geometry)}/";
            Messenger = messenger;
            Instancer = new Instancer(MessageAddress, messenger, mementor, beat);
            Transform = new Transform(MessageAddress, messenger, mementor);

            FileSelector = new FileSelector(MessageAddress, "Single", new List<string> { ".FBX", ".OBJ" }, messenger, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(string.Empty, FileSelector.MessageAddress, messenger) { FileIsSelected = true, FileName = "Quad (default)" });
            FileSelector.SelectedFileNameItem = new FileNameItem(string.Empty, FileSelector.MessageAddress, messenger) { FileIsSelected = true, FileName = "Quad (default)" };

            GeometryFX = new GeometryFX(MessageAddress, messenger, mementor);

            CopyGeometryCommand = new RelayCommand(p => CopyGeometry());
            PasteGeometryCommand = new RelayCommand(p => PasteGeometry());
            ResetGeometryCommand = new RelayCommand(p => ResetGeometry());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            FileSelector.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(FileSelector)));
            GeometryFX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(GeometryFX)));
            Instancer.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Instancer)));
            Transform.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Transform)));
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyGeometryCommand { get; }
        public ICommand PasteGeometryCommand { get; }
        public ICommand ResetGeometryCommand { get; }

        public FileSelector FileSelector { get; }

        public Transform Transform { get; }
        public Instancer Instancer { get;  }
        public GeometryFX GeometryFX { get; }
        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            GeometryModel geometrymodel = new GeometryModel();
            this.Copy(geometrymodel);
            IDataObject data = new DataObject();
            data.SetData("GeometryModel", geometrymodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("GeometryModel"))
            {
                Mementor.BeginBatch();
                Messenger.Disable();

                var geometrymodel = data.GetData("GeometryModel") as GeometryModel;
                var geometrymessageaddress = MessageAddress;
                this.Paste(geometrymodel);
                UpdateMessageAddress(geometrymessageaddress);
                this.Copy(geometrymodel);

                Messenger.Enable();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, geometrymodel);
            }
        }

        public void ResetGeometry()
        {
            GeometryModel geometrymodel = new GeometryModel();
            this.Reset();
            this.Copy(geometrymodel);
            //SendMessages(MessageAddress, geometrymodel);
        }

        public void Copy(GeometryModel geometrymodel)
        {
            geometrymodel.MessageAddress = MessageAddress;
            FileSelector.CopyModel(geometrymodel.FileSelector);
            Transform.Copy(geometrymodel.Transform);
            GeometryFX.Copy(geometrymodel.GeometryFX);
            Instancer.Copy(geometrymodel.Instancer);
        }

        public void Paste(GeometryModel geometrymodel)
        {
            Messenger.Disable();

            MessageAddress = geometrymodel.MessageAddress;
            FileSelector.PasteModel(geometrymodel.FileSelector);
            Transform.Paste(geometrymodel.Transform);
            GeometryFX.Paste(geometrymodel.GeometryFX);
            Instancer.Paste(geometrymodel.Instancer);
            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();
            //Mementor.BeginBatch();

            FileSelector.Reset();
            Transform.Reset();
            GeometryFX.Reset();
            Instancer.Reset();
            //Mementor.EndBatch();
            Messenger.Enable();

            GeometryModel geometrymodel = new GeometryModel();
            this.Copy(geometrymodel);
            //SendMessages(MessageAddress, geometrymodel);
        }
        #endregion
    }
}
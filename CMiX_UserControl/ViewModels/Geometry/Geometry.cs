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
        public Geometry(string messageaddress, Sender sender, Mementor mementor, Beat beat) 
        {
            MessageAddress = $"{messageaddress}{nameof(Geometry)}/";
            Sender = sender;
            Instancer = new Instancer(MessageAddress, sender, mementor, beat);
            Transform = new Transform(MessageAddress, sender, mementor);

            FileSelector = new FileSelector(MessageAddress, "Single", new List<string> { ".FBX", ".OBJ" }, sender, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(string.Empty, FileSelector.MessageAddress, sender) { FileIsSelected = true, FileName = "Quad (default)" });
            FileSelector.SelectedFileNameItem = new FileNameItem(string.Empty, FileSelector.MessageAddress, sender) { FileIsSelected = true, FileName = "Quad (default)" };

            GeometryFX = new GeometryFX(MessageAddress, sender, mementor);

            CopyGeometryCommand = new RelayCommand(p => CopyGeometry());
            PasteGeometryCommand = new RelayCommand(p => PasteGeometry());
            ResetGeometryCommand = new RelayCommand(p => ResetGeometry());
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
        public Sender Sender { get; set; }
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
                Sender.Disable();

                var geometrymodel = data.GetData("GeometryModel") as GeometryModel;
                var geometrymessageaddress = MessageAddress;
                this.Paste(geometrymodel);
                //UpdateMessageAddress(geometrymessageaddress);
                this.Copy(geometrymodel);

                Sender.Enable();
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

        public void Copy(GeometryModel geometryModel)
        {
            FileSelector.CopyModel(geometryModel.FileSelector);
            Transform.Copy(geometryModel.Transform);
            GeometryFX.Copy(geometryModel.GeometryFX);
            Instancer.Copy(geometryModel.Instancer);
        }

        public void Paste(GeometryModel geometryModel)
        {
            Sender.Disable();

            FileSelector.PasteModel(geometryModel.FileSelector);
            Transform.Paste(geometryModel.Transform);
            GeometryFX.Paste(geometryModel.GeometryFX);
            Instancer.Paste(geometryModel.Instancer);

            Sender.Enable();
        }

        public void Reset()
        {
            Sender.Disable();
            //Mementor.BeginBatch();

            FileSelector.Reset();
            Transform.Reset();
            GeometryFX.Reset();
            Instancer.Reset();
            //Mementor.EndBatch();
            Sender.Enable();

            GeometryModel geometrymodel = new GeometryModel();
            this.Copy(geometrymodel);
            //SendMessages(MessageAddress, geometrymodel);
        }
        #endregion
    }
}
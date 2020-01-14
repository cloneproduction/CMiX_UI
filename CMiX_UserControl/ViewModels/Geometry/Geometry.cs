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
        public Geometry(string messageaddress, MessageService messageService, Mementor mementor, Beat beat) 
        {
            MessageAddress = $"{messageaddress}{nameof(Geometry)}/";
            MessageService = messageService;
            Instancer = new Instancer(MessageAddress, messageService, mementor, beat);
            Transform = new Transform(MessageAddress, messageService, mementor);

            FileSelector = new FileSelector(MessageAddress, "Single", new List<string> { ".FBX", ".OBJ" }, messageService, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(string.Empty, FileSelector.MessageAddress, messageService) { FileIsSelected = true, FileName = "Quad (default)" });
            FileSelector.SelectedFileNameItem = new FileNameItem(string.Empty, FileSelector.MessageAddress, messageService) { FileIsSelected = true, FileName = "Quad (default)" };

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

        public FileSelector FileSelector { get; }
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
            geometryModel.FileSelector = FileSelector.GetModel();
            geometryModel.Transform = Transform.GetModel();
            geometryModel.GeometryFX = GeometryFX.GetModel();
            geometryModel.Instancer = Instancer.GetModel();
            return geometryModel;
        }

        //public void Copy(GeometryModel geometryModel)
        //{
        //    FileSelector.CopyModel(geometryModel.FileSelector);
        //    Transform.Copy(geometryModel.Transform);
        //    GeometryFX.Copy(geometryModel.GeometryFX);
        //    Instancer.Copy(geometryModel.Instancer);
        //}

        public void Paste(GeometryModel geometryModel)
        {
            MessageService.Disable();

            FileSelector.PasteModel(geometryModel.FileSelector);
            Transform.Paste(geometryModel.Transform);
            GeometryFX.Paste(geometryModel.GeometryFX);
            Instancer.Paste(geometryModel.Instancer);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();
            //Mementor.BeginBatch();

            FileSelector.Reset();
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
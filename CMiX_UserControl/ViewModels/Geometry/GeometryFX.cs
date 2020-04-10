using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class GeometryFX : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public GeometryFX(string messageaddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageaddress}{nameof(GeometryFX)}/";
            MessageService = messageService;
            Explode = new Slider(MessageAddress + nameof(Explode), messageService, mementor);
            FileSelector = new FileSelector(MessageAddress, "Single", new List<string> { ".PNG", ".JPG", ".MOV", ".TXT" }, messageService, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(string.Empty, FileSelector.MessageAddress, messageService) { FileIsSelected = true, FileName = "Black (default).png" });
        }
        #endregion

        #region PROPERTIES
        public Slider Explode { get; }
        public FileSelector FileSelector { get; }
        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }
        #endregion

        #region COPY/PASTE/RESET

        public void SetViewModel(GeometryFXModel model)
        {
           
        }

        //public void Copy(GeometryFXModel geometryFXdto)
        //{
        //    Explode.CopyModel(geometryFXdto.Explode);
        //    FileSelector.CopyModel(geometryFXdto.FileSelector);
        //}

        public void Paste(GeometryFXModel geometryFXdto)
        {
            MessageService.Disable();

            Explode.SetViewModel(geometryFXdto.Explode);
            FileSelector.SetViewModel(geometryFXdto.FileSelector);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();;

            Explode.Reset();
            FileSelector.Reset();

            MessageService.Enable();
        }


        #endregion
    }
}
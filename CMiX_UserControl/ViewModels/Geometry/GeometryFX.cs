using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class GeometryFX : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public GeometryFX(string messageaddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(GeometryFX));
            MessageService = messageService;
            Explode = new Slider(MessageAddress + nameof(Explode), messageService, mementor);
            FileSelector = new FileSelector(MessageAddress, "Single", new List<string> { ".PNG", ".JPG", ".MOV", ".TXT" }, messageService, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(string.Empty, FileSelector.MessageAddress, messageService) { FileIsSelected = true, FileName = "Black (default).png" });
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(GeometryFX));
            Explode.UpdateMessageAddress(MessageAddress + nameof(Explode));
            FileSelector.UpdateMessageAddress(MessageAddress + nameof(FileSelector));
        }
        #endregion

        #region PROPERTIES
        public Slider Explode { get; }
        public FileSelector FileSelector { get; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(GeometryFXModel geometryFXdto)
        {
            Explode.Copy(geometryFXdto.Explode);
            FileSelector.Copy(geometryFXdto.FileSelector);
        }

        public void Paste(GeometryFXModel geometryFXdto)
        {
            MessageService.DisabledMessages();

            Explode.Paste(geometryFXdto.Explode);
            FileSelector.Paste(geometryFXdto.FileSelector);

            MessageService.EnabledMessages();
        }

        public void Reset()
        {
            MessageService.DisabledMessages();

            Explode.Reset();
            FileSelector.Reset();

            MessageService.EnabledMessages();
        }
        #endregion
    }
}
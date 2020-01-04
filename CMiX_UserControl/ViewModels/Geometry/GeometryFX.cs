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
        public GeometryFX(string messageaddress, Messenger messenger, Mementor mementor)
        {
            MessageAddress = $"{messageaddress}{nameof(GeometryFX)}/";
            Messenger = messenger;
            Explode = new Slider(MessageAddress + nameof(Explode), messenger, mementor);
            FileSelector = new FileSelector(MessageAddress, "Single", new List<string> { ".PNG", ".JPG", ".MOV", ".TXT" }, messenger, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(string.Empty, FileSelector.MessageAddress, messenger) { FileIsSelected = true, FileName = "Black (default).png" });
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
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(GeometryFXModel geometryFXdto)
        {
            Explode.CopyModel(geometryFXdto.Explode);
            FileSelector.Copy(geometryFXdto.FileSelector);
        }

        public void Paste(GeometryFXModel geometryFXdto)
        {
            Messenger.Disable();

            Explode.PasteModel(geometryFXdto.Explode);
            FileSelector.Paste(geometryFXdto.FileSelector);

            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();;

            Explode.Reset();
            FileSelector.Reset();

            Messenger.Enable();
        }
        #endregion
    }
}
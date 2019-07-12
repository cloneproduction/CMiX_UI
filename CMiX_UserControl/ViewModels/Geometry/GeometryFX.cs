using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class GeometryFX : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryFX(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(GeometryFX));
            Explode = new Slider(MessageAddress + nameof(Explode), oscvalidation, mementor);
            FileSelector = new FileSelector(MessageAddress, "Single", new List<string> { ".PNG", ".JPG", ".MOV", ".TXT" }, oscvalidation, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(FileSelector.MessageAddress, oscvalidation, mementor) { FileIsSelected = true, FileName = "Black (default).png" });
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
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(GeometryFXModel geometryFXdto)
        {
            Explode.Copy(geometryFXdto.Explode);
            FileSelector.Copy(geometryFXdto.FileSelector);
        }

        public void Paste(GeometryFXModel geometryFXdto)
        {
            DisabledMessages();

            Explode.Paste(geometryFXdto.Explode);
            FileSelector.Paste(geometryFXdto.FileSelector);

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();

            Explode.Reset();
            FileSelector.Reset();

            EnabledMessages();
        }
        #endregion
    }
}
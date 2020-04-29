using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class GeometryFX : ViewModel, IUndoable
    {
        #region CONSTRUCTORS
        public GeometryFX(string messageaddress, Mementor mementor)
        {
            MessageAddress = $"{messageaddress}{nameof(GeometryFX)}/";
            Explode = new Slider(MessageAddress + nameof(Explode), mementor);
        }
        #endregion

        #region PROPERTIES
        public Slider Explode { get; }
        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public MessengerService MessengerService { get; set; }
        #endregion

        #region COPY/PASTE/RESET

        public void Paste(GeometryFXModel geometryFXdto)
        {
            MessengerService.Disable();

            Explode.SetViewModel(geometryFXdto.Explode);
            

            MessengerService.Enable();
        }

        public void Reset()
        {
            MessengerService.Disable();;

            Explode.Reset();

            MessengerService.Enable();
        }


        #endregion
    }
}
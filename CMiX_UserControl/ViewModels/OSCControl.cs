using CMiX.Services;
using GuiLabs.Undo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CMiX.ViewModels
{
    public class OSCControl : ViewModel
    {
        #region CONSTRUCTORS
        public OSCControl(ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            oscmessenger: new OSCMessenger()
        )
        { } 

        public OSCControl
            (
                ActionManager actionmanager,
                OSCMessenger oscmessenger
            )
            : base(actionmanager, oscmessenger)
        {
            OSCMessenger = oscmessenger ?? throw new ArgumentNullException(nameof(oscmessenger));
        }
        #endregion

        #region PROPERTIES
        private OSCMessenger _oscmessenger;
        public OSCMessenger OSCMessenger
        {
            get { return _oscmessenger; }
            set => SetAndNotify(ref _oscmessenger, value);
        }

        public ObservableCollection<OSCMessenger> Messengers;

        #endregion
    }
}

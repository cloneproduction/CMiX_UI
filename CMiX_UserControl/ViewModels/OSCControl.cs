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
            messengers: new ObservableCollection<OSCMessenger>()
        )
        { } 

        public OSCControl
            (
                ActionManager actionmanager,
                ObservableCollection<OSCMessenger> messengers
            )
            : base(actionmanager, messengers)
        {
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
        }
        #endregion

        #region PROPERTIES
        private OSCMessenger _oscmessenger;
        public OSCMessenger OSCMessenger
        {
            get { return _oscmessenger; }
            set => SetAndNotify(ref _oscmessenger, value);
        }

        #endregion
    }
}

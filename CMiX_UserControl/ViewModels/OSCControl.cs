using CMiX.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class OSCControl : ViewModel
    {
        #region CONSTRUCTORS
        public OSCControl()
        : this
        (
            messengers: new ObservableCollection<OSCMessenger>()
        )
        { }

        public OSCControl
        (
            ObservableCollection<OSCMessenger> messengers
        )
        : base(messengers)
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

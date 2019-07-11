using System;

namespace CMiX.MVVM.ViewModels
{
    public class OSCValidation : ViewModel
    {
        public OSCValidation(OSCMessenger oscmessenger)
        {
            OSCMessenger = oscmessenger;
            SendEnabled = false;
        }

        private OSCMessenger _oscmessenger;
        public OSCMessenger OSCMessenger
        {
            get { return _oscmessenger; }
            set => SetAndNotify(ref _oscmessenger, value);
        }


        private bool _sendenabled;
        public bool SendEnabled
        {
            get { return _sendenabled; }
            set => SetAndNotify(ref _sendenabled, value);
        }
    }
}
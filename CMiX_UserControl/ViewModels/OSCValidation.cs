using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using SharpOSC;

using System.Windows.Input;
using CMiX.Models;

namespace CMiX.ViewModels
{
    public class OSCValidation : ViewModel
    {
        public OSCValidation(OSCMessenger oscmessenger)
        {
            OSCMessenger = oscmessenger;
        }

        private OSCMessenger oscmessenger;
        public OSCMessenger OSCMessenger
        {
            get { return oscmessenger; }
            set { oscmessenger = value; }
        }


        private bool _sendenabled = true;
        public bool SendEnabled
        {
            get { return _sendenabled; }
            set => SetAndNotify(ref _sendenabled, value);
        }
    }
}
using CMiX.MVVM.ViewModels;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class MessengerSettings : ViewModel, IModalDialogViewModel
    {
        public MessengerSettings()
        {

        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public bool? DialogResult => throw new NotImplementedException();
    }
}

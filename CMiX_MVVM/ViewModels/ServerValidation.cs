using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public class ServerValidation : ViewModel
    {
        public ServerValidation(Server server)
        {
            Server = server;
            SendEnabled = true;
        }

        private Server _server;
        public Server Server
        {
            get => _server;
            set => SetAndNotify(ref _server, value);
        }

        private bool _sendenabled;
        public bool SendEnabled
        {
            get => _sendenabled;
            set => SetAndNotify(ref _sendenabled, value);
        }
    }
}

// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Core.Presentation.ViewModels.Network
{
    public class ConnectedClient : ViewModel
    {
        public ConnectedClient(string ipPort)
        {
            IP = ipPort.Split(':')[0];
            Port = ipPort.Split(':')[1];
        }

        private bool _unSync;
        public bool UnSync
        {
            get => _unSync;
            set => SetAndNotify(ref _unSync, value);
        }

        private string _port;
        public string Port
        {
            get => _port;
            set => SetAndNotify(ref _port, value);
        }

        private string _ip;
        public string IP
        {
            get => _ip;
            set => SetAndNotify(ref _ip, value);
        }
    }
}

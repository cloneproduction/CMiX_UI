﻿using Ceras;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System.Collections.Generic;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class Messenger : ViewModel
    {
        public Messenger(int id, List<string> addresses)
        {
            Serializer = new CerasSerializer();
            Addresses = addresses;
            Server = new Server("Server Name", "127.0.0.1", 1111 + id, "Device" + id);
        }

        public List<string> Addresses { get; set; }
        public CerasSerializer Serializer { get; set; }

        private Server _server;
        public Server Server
        {
            get => _server;
            set => SetAndNotify(ref _server, value);
        }

        private Component _selectedComponent;
        public Component SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                if(_selectedComponent != null)
                    _selectedComponent.SendChangeEvent -= Value_SendChangeEvent;

                SetAndNotify(ref _selectedComponent, value);

                if (SelectedComponent != null)
                    SelectedComponent.SendChangeEvent += Value_SendChangeEvent;
            }
        }

        private void Value_SendChangeEvent(object sender, ModelEventArgs e)
        {
            Server.Send(e.MessageAddress, Serializer.Serialize(e.Model));
        }
    }
}
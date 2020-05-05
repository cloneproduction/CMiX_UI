using System;

namespace CMiX.Studio.ViewModels.MessageService
{
    public class SettingsEventArgs : EventArgs
    {
        public SettingsEventArgs(string deviceName, string ip, int port)
        {
            DeviceName = deviceName;
            IP = ip;
            Port = port;
        }

        public string Address
        {
            get { return String.Format("tcp://{0}:{1}", IP, Port); }
        }

        private string _deviceName;
        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        private string _ip;
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
    }
}
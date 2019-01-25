using CMiX.Services;
using GuiLabs.Undo;
using System;
using SharpOSC;
namespace CMiX.ViewModels
{
    public class OSCControl : ViewModel
    {
        #region CONSTRUCTORS
        public OSCControl(IMessenger messenger, ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            //messenger: messenger,
            messageaddress: String.Empty,
            updsender: new UDPSender("127.0.0.1", 55555),
            messageEnabled: true
        )
        { }

        public OSCControl
            (
                UDPSender updsender,
                ActionManager actionmanager,
                //IMessenger messenger,
                string messageaddress,
                bool messageEnabled
            )
            : base(actionmanager)
        {
            UDPSender = updsender;
            OSCMessenger = new OSCMessenger(UDPSender);
            //Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
        }
        #endregion

        #region PROPERTIES
        private IMessenger Messenger { get; }
        public string MessageAddress { get; set; }
        public bool MessageEnabled { get; set; }

        private UDPSender _udpsender;
        public UDPSender UDPSender
        {
            get { return _udpsender; }
            set { _udpsender = value; }
        }

        private OSCMessenger _oscmessenger;
        public OSCMessenger OSCMessenger
        {
            get { return _oscmessenger; }
            set { _oscmessenger = value; }
        }


        private int _port = 55555;
        [OSC]
        public int Port
        {
            get => _port;
            set
            {
                SetAndNotify(ref _port, value);
                UpdateUDPSender();
            }
        }

        private string _ip = "127.0.0.1";
        [OSC]
        public string IP
        {
            get => _ip;
            set
            {
                SetAndNotify(ref _ip, value);
                UpdateUDPSender();
            }
        }
        #endregion

        public void UpdateUDPSender()
        {
            UDPSender = new UDPSender(IP, Port);
            OSCMessenger.Sender = UDPSender;
        }
    }
}

using Ceras;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels.MessageService
{
    public class Messenger : ViewModel, IModalDialogViewModel
    {
        public Messenger(int id)
        {
            Serializer = new CerasSerializer();
            Server = new Server();
            Settings = new Settings();
            Name = $"Messenger ({id})";
            CanApplySettings = false;

            StartServerCommand = new RelayCommand(p => StartServer());
            StopServerCommand = new RelayCommand(p => StopServer());
            RestartServerCommand = new RelayCommand(p => RestartServer());

            OkCommand = new RelayCommand(p => Ok(p as Window));
            CancelCommand = new RelayCommand(p => Cancel(p as Window));
            ApplyCommand = new RelayCommand(p => Apply());
            CloseWindowCommand = new RelayCommand(p => Cancel(p as Window));
        }

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public void StartServer()
        {
            Server.Start();
        }

        public void StopServer()
        {
            Server.Stop();
        }

        public void RestartServer()
        {
            Server.Restart();
        }

        public ICommand StartServerCommand { get; }
        public ICommand StopServerCommand { get; }
        public ICommand RestartServerCommand { get; }


        public List<string> Addresses { get; set; }
        public CerasSerializer Serializer { get; set; }

        public Settings Settings {get; set;}

        public bool CanConnect(string host, int port)
        {
            bool result = true;
            IPAddress ipa = Dns.GetHostAddresses(host)[0];
            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(ipa, port);
                if (sock.Connected == true)  // Port is in use and connection is successful
                {
                    //ErrorMessage = "Port already in use";//MessageBox.Show("Port is Closed");
                    ErrorMessage = "Port already in use";
                    result = false;
                }
                sock.Close();

            }
            catch(SocketException ex)
            {
                if (ex.ErrorCode == 10061)  // Port is unused and could not establish connection 
                {
                    ErrorMessage = String.Empty;
                    result = true;
                }
                else
                    ErrorMessage = ex.Message;
            }
            if (port == 0)
                result = false;
            return result;
        }

        public bool ValidateIPv4(string ipString)
        {
            ErrorMessage = String.Empty;
            if (String.IsNullOrWhiteSpace(ipString))
            {
                ErrorMessage = "IP Address is not valid";
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                ErrorMessage = "IP Address is not valid";
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }


        public bool? DialogResult { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public string Address
        {
            get { return String.Format("tcp://{0}:{1}", IP, Port); }
        }

        private string _deviceName;
        public string DeviceName
        {
            get => _deviceName;
            set
            {
                SetAndNotify(ref _deviceName, value);
                CanApplySettings = true;
            }
        }

        private string _ip;
        public string IP
        {
            get => _ip;
            set
            {
                SetAndNotify(ref _ip, value);
                CanApplySettings = true;
            }
        }

        private int _port;
        public int Port
        {
            get => _port;
            set
            {
                SetAndNotify(ref _port, value);
                CanApplySettings = true;
            }
        }

        private bool _canApplySettings;
        public bool CanApplySettings
        {
            get => _canApplySettings;
            set => SetAndNotify(ref _canApplySettings, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetAndNotify(ref _errorMessage, value);
        }

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

        public void ApplySettingToServer()
        {
            //Server.Port = settings.Port;
            //Server.IP = settings.IP;
            //Server.Topic = settings.DeviceName;
            Console.WriteLine("ApplySettingToServer");
        }

        public void Ok(Window window)
        {
            DialogResult = true;
            window.Close();
        }

        public void Cancel(Window window)
        {
            DialogResult = false;
            window.Close();
        }

        public void Apply()
        {
            
            Console.WriteLine("ApplyPressed");
            if (ValidateIPv4(IP) && CanConnect(IP, Port))
            {
                ErrorMessage = "Settings applied succefully !";
                Server.IP = IP;
                Server.Port = Port;
                Server.Topic = DeviceName;
                CanApplySettings = false;
            }

            //else if (!CanConnect(Settings.IP, Settings.Port))

            //else

            //OnSettingApplied();
            //Address = String.Format("tcp://{0}:{1}", IP, Port);

            //if (Messenger.CheckAddresses(Address))
            //{
            //    Messenger.Server.IP = IP;
            //    Messenger.Server.Port = Port;
            //    Messenger.Server.Topic = DeviceName;
            //    Messenger.Addresses.Add(Address);
            //}


        }
    }
}

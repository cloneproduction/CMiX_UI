using MvvmDialogs;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class Settings : ViewModel, IModalDialogViewModel
    {
        public Settings(string name, string topic, string ip, int port)
        {
            Name = name;
            Topic = topic;
            IP = ip;
            Port = port;

            CanApply = false;
            ApplyCommand = new RelayCommand(p => Apply());

            OkCommand = new RelayCommand(p => Ok(p as Window));
            CancelCommand = new RelayCommand(p => Cancel(p as Window));
            
            CloseWindowCommand = new RelayCommand(p => Cancel(p as Window));
        }

        private bool CanApplySettings()
        {
            Console.WriteLine("CanApply = " + CanApply);
            return CanApply;
        }


        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }


        public bool? DialogResult { get; set; }


        private bool _okIsFocused;
        public bool OkIsFocused
        {
            get => _okIsFocused;
            set => SetAndNotify(ref _okIsFocused, value);
        }

        private bool _canApply;
        public bool CanApply
        {
            get => _canApply;
            set => SetAndNotify(ref _canApply, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetAndNotify(ref _name, value);
                CanApply = true;
            }
        }

        private string _topic;
        public string Topic
        {
            get => _topic;
            set
            {
                SetAndNotify(ref _topic, value);
                CanApply = true;
            }
        }

        private string _ip;
        public string IP
        {
            get => _ip;
            set
            {
                SetAndNotify(ref _ip, value);
                CanApply = true;
            }
        }

        private int _port;
        public int Port
        {
            get => _port;
            set
            {
                SetAndNotify(ref _port, value);
                CanApply = true;
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetAndNotify(ref _errorMessage, value);
        }

        public void Ok(Window window)
        {
            window.Close();
        }

        public void Cancel(Window window)
        {
            DialogResult = false;
            window.Close();
        }

        public void Apply()
        {
            if (ValidateIPv4(IP) && ValidatePort(IP, Port))
            {
                ErrorMessage = "Settings applied succefully !";
                CanApply = false;
                DialogResult = true;
                OkIsFocused = true;
            }
        }


        public bool ValidatePort(string host, int port)
        {
            IPAddress ipa = Dns.GetHostAddresses(host)[0];
            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(ipa, port);
                if (sock.Connected == true)  // Port is in use and connection is successful
                {
                    ErrorMessage = "Port already in use";
                    return false;
                }
                sock.Close();

            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10061)  // Port is unused and could not establish connection 
                {
                    ErrorMessage = String.Empty;
                    return true;
                }
                else
                    ErrorMessage = ex.Message;
            }
            if (port == 0)
                return false;

            return false;
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
    }
}
using CMiX.MVVM.ViewModels;
using MvvmDialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels.MessageService
{
    public class Settings : ViewModel, IModalDialogViewModel
    {
        public Settings()
        {
            //Messenger = messenger;
            //DeviceName = messenger.Server.Topic;
            //Port = messenger.Server.Port;
            //IP = messenger.Server.IP;

            CanApply = false;

            OkCommand = new RelayCommand(p => Ok(p as Window));
            CancelCommand = new RelayCommand(p => Cancel(p as Window));
            ApplyCommand = new RelayCommand(p => Apply());
            CloseWindowCommand = new RelayCommand(p => Cancel(p as Window));
        }

        public bool? DialogResult { get; set; }

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
            Address = String.Format("tcp://{0}:{1}", IP, Port);

            //if (Messenger.CheckAddresses(Address))
            //{
            //    Messenger.Server.IP = IP;
            //    Messenger.Server.Port = Port;
            //    Messenger.Server.Topic = DeviceName;
            //    Messenger.Addresses.Add(Address);
            //}
                
            CanApply = false;
        }

        //private string Address
        //{
        //    get { return String.Format("tcp://{0}:{1}", IP, Port); }
        //}
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public Messenger Messenger { get; set; }

        private bool _canApply;
        public bool CanApply
        {
            get => _canApply;
            set => SetAndNotify(ref _canApply, value);
        }


        private string _deviceName;
        public string DeviceName
        {
            get => _deviceName;
            set
            {
                SetAndNotify(ref _deviceName, value);
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

        private string _address;
        public string Address
        {
            get => _address;
            set => SetAndNotify(ref _address, value);
        }
    }
}

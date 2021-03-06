﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using SharpOSC;

using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class OSCMessenger : ViewModel
    {
        public OSCMessenger(string address, int port)
        {
            Enabled = true;
            Address = address;
            Port = port;
            Sender = new UDPSender(address, port);
            messages = new List<OscMessage>();
            ReloadCommand = new RelayCommand(p => Reload(p));
        }

        #region PROPERTIES
        public UDPSender Sender { get; set; }

        public readonly List<OscMessage> messages;

        public ICommand ReloadCommand { get; }

        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set => SetAndNotify(ref _enabled, value);
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set => SetAndNotify(ref _name, value);
        }


        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                SetAndNotify(ref _address, value);
                UpdateUDPSender();
            }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set
            {
                SetAndNotify(ref _port, value);
                UpdateUDPSender();
            }
        }
        #endregion

        public void Reload(object obj)
        {
            if (Enabled)
            {
                QueueObject(obj);
                SendQueue();
            }
        }

        private void UpdateUDPSender()
        {
            Sender = new UDPSender(Address, Port);
        }

        public OscMessage CreateOscMessage(string address, params object[] args)
        {
            return new OscMessage(address, args.Select(a => a?.ToString()).ToArray());
        }

        public void QueueMessage(string address, params object[] args)
        {
            if (Enabled)
            {
                var message = CreateOscMessage(address, args);
                messages.Add(message);
            }
        }

        public void SendMessage(string address, params object[] args)
        {
            if (Enabled)
            {
                var message = CreateOscMessage(address, args);
                Sender.Send(message);
            }
        }

        public void SendQueue()
        {
            if (Enabled)
            {
                var bundle = new OscBundle(0, messages.ToArray());
                Sender.Send(bundle);
                messages.Clear();
            }
        }

        public void QueueObject(object obj)
        {
            if (obj == null) return;

            if (obj is Model && Enabled)
            {
                var model = obj as Model;
                var properties = obj.GetType().GetProperties().Where(p => !p.GetIndexParameters().Any());

                foreach (PropertyInfo property in properties)
                {
                    object propValue = property.GetValue(obj, null);

                    //var IsOSC = Attribute.IsDefined(property, typeof(OSCAttribute));

                    if (propValue != null)
                    {
                        string propertyname = property.Name;
                        Type type = propValue.GetType();


                        if (typeof(Model).IsAssignableFrom(propValue.GetType()))
                        {
                            QueueObject(propValue);
                        }

                        else if(propertyname is "LayerNames")
                        {
                            var list = propValue as List<string>;
                            list.Sort();
                            QueueMessage("/LayerNames", list.ToArray());
                        }
                        else if (propertyname is "LayerID")
                        {
                            var list = propValue as List<int>;
                            QueueMessage("/LayerID", list.Select(i => i.ToString()).ToArray());
                            foreach (var item in list)
                            {
                                Console.WriteLine("LayerID" + item);
                            }
                        }
                        else if (propValue is List<Model>)
                        {
                            var modellist = propValue as List<Model>;
                            foreach (var item in modellist)
                            {
                                QueueObject(item);
                                Console.WriteLine(item.ToString());
                            }
                        }
                        else if (propValue is List<FileNameItemModel>)
                        {
                            var filenameitemlist = propValue as List<FileNameItemModel>;
                            foreach (var item in filenameitemlist)
                            {
                                if (item.FileIsSelected)
                                {
                                    string address = item.MessageAddress;
                                    QueueMessage(address, item.FileName);
                                }
                            }
                        }
                        else
                        {
                            if (propertyname != "MessageAddress")
                            {
                                string address = model.MessageAddress;
                                string propdata = property.GetValue(obj, null).ToString();
                                if (address != null)
                                {
                                    QueueMessage(address + propertyname, propdata);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using SharpOSC;
using CMiX.ViewModels;
using System.Windows.Input;
using CMiX.Models;

namespace CMiX.Services
{
    public class OSCMessenger : ViewModel
    {
        public OSCMessenger(string address, int port)
        {
            Address = address;
            Port = port;
            Sender = new UDPSender(address, port);
            messages = new List<OscMessage>();
            ReloadCommand = new RelayCommand(p => Reload(p));
        }

        public UDPSender Sender { get; set; }

        private readonly List<OscMessage> messages;

        public ICommand ReloadCommand { get; }

        private bool _sendenabled = true;
        public bool SendEnabled
        {
            get { return _sendenabled; }
            set => SetAndNotify(ref _sendenabled, value);
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

        public void Reload(object obj)
        {
            QueueObject(obj);
            SendQueue();
        }

        private void UpdateUDPSender()
        {
            Sender = new UDPSender(Address, Port);
        }

        private OscMessage CreateOscMessage(string address, params object[] args) => new OscMessage(address, args.Select(a => a?.ToString()).ToArray());

        public void QueueMessage(string address, params object[] args)
        {
            var message = CreateOscMessage(address, args);
            messages.Add(message);
        }

        public void SendMessage(string address, params object[] args)
        {
            if (SendEnabled)
            {
                var message = CreateOscMessage(address, args);
                Sender.Send(message);
            }
        }

        public void SendQueue()
        {
            if (SendEnabled)
            {
                var bundle = new OscBundle(0, messages.ToArray());
                Sender.Send(bundle);
                messages.Clear();
            }
        }

        public void QueueObject(object obj)
        {
            if (obj == null) return;

            if (obj is Model)
            {
                var model = obj as Model;
                var properties = obj.GetType().GetProperties().Where(p => !p.GetIndexParameters().Any());

                foreach (PropertyInfo property in properties)
                {
                    object propValue = property.GetValue(obj, null);

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
                            QueueMessage(propertyname, list.ToArray());
                        }
                        else if (propertyname is "LayerIndex")
                        {
                            var list = propValue as List<int>;
                            QueueMessage(propertyname, list.Select(i => i.ToString()).ToArray());
                        }
                        else if (propValue is List<Model>)
                        {
                            var modellist = propValue as List<Model>;
                            foreach (var item in modellist)
                            {
                                QueueObject(item);
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

//public void QueueObject(object obj)
//{
//    string address = string.Empty;
//    string propdata = string.Empty;
//    string propertyname = string.Empty;

//    if (obj == null) return;

//    PropertyInfo propinfo = obj.GetType().GetProperty("MessageAddress");

//    if (propinfo != null)
//    {
//        if (propinfo.GetValue(obj, null) != null) 
//        {
//            address = obj.GetType().GetProperty("MessageAddress").GetValue(obj, null).ToString();
//        }
//    }

//    Type objType = obj.GetType();
//    var properties = objType.GetProperties().Where(p => !p.GetIndexParameters().Any());

//    foreach (PropertyInfo property in properties)
//    {
//        object propValue = property.GetValue(obj, null);

//        object[] attrs = property.GetCustomAttributes(true);
//        foreach (object attr in attrs)
//        {
//            OSCAttribute oscdata = attr as OSCAttribute;
//            if (oscdata != null)
//            {
//                propertyname = property.Name;

//                if (propValue is FileNameItemModel)
//                {
//                    var item = propValue as FileNameItemModel;
//                    if(item.FileIsSelected)
//                        QueueMessage(address + propertyname, item.FileName);
//                }

//                else
//                {
//                    propdata = property.GetValue(obj, null).ToString();
//                    QueueMessage(address + propertyname, propdata);
//                }
//            }
//        }

//        var elems = propValue as IList;
//        if ((elems != null) && !(elems is string[]))
//        {
//            foreach (var item in elems)
//            {
//                QueueObject(item);
//            }
//        }
//        else
//        {
//            // This will not cut-off System.Collections because of the first check
//            if (property.PropertyType.Assembly == objType.Assembly)
//            {
//                QueueObject(propValue);
//            }
//            /*else
//            {
//                if (propValue is string[])
//                {
//                    var str = new StringBuilder();
//                    foreach (string item in (string[])propValue)
//                    {
//                        str.AppendFormat("{0}; ", item);
//                    }
//                    propValue = str.ToString();
//                    str.Clear();
//                }
//            }*/
//        }
//    }
//}
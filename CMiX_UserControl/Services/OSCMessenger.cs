using CMiX.Controls;
using SharpOSC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CMiX.Services
{
    public class OSCMessenger : IMessenger
    {
        public UDPSender Sender { get; }

        private readonly List<OscMessage> messages;

        public OSCMessenger(UDPSender sender)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            messages = new List<OscMessage>();
        }

        private OscMessage CreateOscMessage(string address, params object[] args) => new OscMessage(address, args.Select(a => a?.ToString()).ToArray());

        public void QueueMessage(string address, params object[] args)
        {
            var message = CreateOscMessage(address, args);
            messages.Add(message);
        }

        public void SendMessage(string address, params object[] args)
        {
            var message = CreateOscMessage(address, args);
            Sender.Send(message);
        }

        public void SendQueue()
        {
            var bundle = new OscBundle(0, messages.ToArray());
            Sender.Send(bundle);
            messages.Clear();
        }

        public void QueueObject(object obj)
        {
            string address = string.Empty;
            string propdata = string.Empty;
            string propertyname = string.Empty;

            if (obj == null) return;

            if (obj.GetType().GetProperty("MessageAddress") != null)
            {
                address = obj.GetType().GetProperty("MessageAddress").GetValue(obj, null).ToString();
            }

            Type objType = obj.GetType();
            var properties = objType.GetProperties().Where(p => !p.GetIndexParameters().Any());

            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(obj, null);

                object[] attrs = property.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    OSCAttribute oscdata = attr as OSCAttribute;
                    if (oscdata != null)
                    {
                        propertyname = property.Name;

                        if (propValue is ObservableCollection<ListBoxFileName>)
                        {
                            List<string> filenames = new List<string>();
                            foreach (ListBoxFileName lbfn in (ObservableCollection<ListBoxFileName>)propValue)
                            {
                                if (lbfn.FileIsSelected == true)
                                {
                                    filenames.Add(lbfn.FileName);
                                }
                            }
                            QueueMessage(address + propertyname, filenames.ToArray());
                        }
                        else
                        {
                            propdata = property.GetValue(obj, null).ToString();
                            QueueMessage(address + propertyname, propdata);
                        }
                    }
                }

                var elems = propValue as IList;
                if ((elems != null) && !(elems is string[]))
                {
                    foreach (var item in elems)
                    {
                        QueueObject(item);
                    }
                }
                else
                {
                    // This will not cut-off System.Collections because of the first check
                    if (property.PropertyType.Assembly == objType.Assembly)
                    {
                        QueueObject(propValue);
                    }
                    /*else
                    {
                        if (propValue is string[])
                        {
                            var str = new StringBuilder();
                            foreach (string item in (string[])propValue)
                            {
                                str.AppendFormat("{0}; ", item);
                            }
                            propValue = str.ToString();
                            str.Clear();
                        }
                    }*/
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SharpOSC;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Media;

namespace CMiX
{
    class Messenger
    {
        #region Properties
        private bool _EnabledSend = false;
        public bool EnabledSend
        {
            get { return _EnabledSend; }
            set { _EnabledSend = value; }
        }

        private int _Port = 55555;
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        private string _Address = "127.0.0.1";
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private UDPSender _Sender = new UDPSender("127.0.0.1", 55555);
        public UDPSender Sender
        {
            get { return _Sender; }
            set { _Sender = value; }
        }
        #endregion

        #region Functions
        public void SendAll(CMiXData datacontext)
        {
            Type type = datacontext.GetType();
            PropertyInfo[] properties = type.GetProperties();

            List<OscMessage> messagelist = new List<OscMessage>();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(ObservableCollection<string>))
                {
                    ObservableCollection<string> ob = (ObservableCollection<string>)property.GetValue(datacontext, null);
                    OscMessage message = new OscMessage("/" + property.Name, ob.ToArray());
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(ObservableCollection<bool>))
                {
                    ObservableCollection<bool> ob = (ObservableCollection<bool>)property.GetValue(datacontext, null);
                    List<string> list = ob.Select(e => e.ToString()).ToList();
                    OscMessage message = new OscMessage("/" + property.Name, list.ToArray());
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(ObservableCollection<double>))
                {
                    ObservableCollection<double> ob = (ObservableCollection<double>)property.GetValue(datacontext, null);
                    List<string> list = ob.Select(e => e.ToString("0.00")).ToList();
                    OscMessage message = new OscMessage("/" + property.Name, list.ToArray());
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(ObservableCollection<Color>))
                {
                    ObservableCollection<Color> ob = (ObservableCollection<Color>)property.GetValue(datacontext, null);
                    List<string> list = new List<string>();
                    foreach (Color col in ob)
                    {
                        var drawingcolor = System.Drawing.Color.FromArgb(col.A, col.R, col.G, col.B);
                        string hex = "#" + Utils.ColorToHexString(col);
                        list.Add(hex);
                    }
                    OscMessage message = new OscMessage("/" + property.Name, list.ToArray());
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(ObservableCollection<ObservableCollection<ListBoxFileName>>))
                {
                    ObservableCollection<ObservableCollection<ListBoxFileName>> ob = (ObservableCollection<ObservableCollection<ListBoxFileName>>)property.GetValue(datacontext, null);
                    List<string> count = new List<string>();
                    List<string> list = new List<string>();
                    foreach (ObservableCollection<ListBoxFileName> fn in ob)
                    {
                        int i = 0;
                        foreach (ListBoxFileName lb in fn)
                        {
                            if (lb.FileIsSelected == true)
                            {
                                i += 1;
                                list.Add(lb.FileName);
                            }
                        }
                        count.Add(i.ToString());
                    }
                    OscMessage message = new OscMessage("/" + property.Name, list.ToArray());
                    OscMessage messagecount = new OscMessage("/" + property.Name + "Count", count.ToArray());
                    messagelist.Add(message);
                    messagelist.Add(messagecount);
                }
            }
            OscBundle bundle = new OscBundle(0, messagelist.ToArray());
            Sender.Send(bundle);
        }

        public void Send(CMiXData datacontext, FrameworkElement uielement)
        {
            if (datacontext != null)
            {
                List<string> propdata = new List<string>();

                PropertyInfo property = datacontext.GetType().GetProperty("Channel" + uielement.Name);
                Type type = property.PropertyType;
                var list = property.GetValue(datacontext, null);

                if (type == typeof(ObservableCollection<string>))
                {
                    foreach (string st in (ObservableCollection<string>)list)
                    {
                        propdata.Add(st);
                    }
                    this.Sender.Send(new OscMessage("/" + "Channel" + uielement.Name, propdata.ToArray()));
                }

                else if (type == typeof(ObservableCollection<bool>))
                {
                    foreach (bool st in (ObservableCollection<bool>)list)
                    {
                        propdata.Add(st.ToString());
                    }
                    this.Sender.Send(new OscMessage("/" + "Channel" + uielement.Name, propdata.ToArray()));
                }

                else if (type == typeof(ObservableCollection<double>))
                {
                    foreach (double st in (ObservableCollection<double>)list)
                    {
                        propdata.Add(st.ToString());
                    }
                    this.Sender.Send(new OscMessage("/" + "Channel" + uielement.Name, propdata.ToArray()));
                }

                else if (type == typeof(ObservableCollection<Color>))
                {
                    foreach (Color col in (ObservableCollection<Color>)list)
                    {
                        var drawingcolor = System.Drawing.Color.FromArgb(col.A, col.R, col.G, col.B);
                        string hex = "#" + Utils.ColorToHexString(col);
                        propdata.Add(hex);
                    }
                    this.Sender.Send(new OscMessage("/" + "Channel" + uielement.Name, propdata.ToArray()));
                }

                else if (type == typeof(ObservableCollection<ObservableCollection<ListBoxFileName>>))
                {
                    List<string> count = new List<string>();
                    foreach (ObservableCollection<ListBoxFileName> fn in (ObservableCollection<ObservableCollection<ListBoxFileName>>)list)
                    {
                        int i = 0;
                        foreach (ListBoxFileName lb in (ObservableCollection<ListBoxFileName>)fn)
                        {
                            if (lb.FileIsSelected == true)
                            {
                                i += 1;
                                propdata.Add(lb.FileName);
                            }
                        }
                        count.Add(i.ToString());
                    }

                    List<OscMessage> messagelist = new List<OscMessage>
                    {
                      new OscMessage("/" + "Channel" + uielement.Name, propdata.ToArray()),
                      new OscMessage("/" + "Channel" + uielement.Name + "Count", count.ToArray())
                    };
                    OscBundle bundle = new OscBundle(1, messagelist.ToArray());
                    this.Sender.Send(bundle);
                } 
            }
        }
        #endregion
    }
}



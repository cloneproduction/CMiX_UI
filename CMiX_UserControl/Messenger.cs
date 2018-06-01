using System;
using System.Collections.Generic;
using System.Linq;
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

        public void SendOSC(string Name, string Value)
        {
            this.Sender.Send(new OscMessage("/" + Name, Value));                   
        }

        public void SendOSCList(string Name, List<string> Value)
        {
            this.Sender.Send(new OscMessage("/" + Name, Value.ToArray()));
        }


        public List<OscMessage> ObjectToOscList(object obj, string objectname)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            List<OscMessage> messagelist = new List<OscMessage>();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    string ob = (string)property.GetValue(obj, null);
                    OscMessage message = new OscMessage("/" + objectname + "/" + property.Name, ob);
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(double))
                {
                    double ob = (double)property.GetValue(obj, null);
                    OscMessage message = new OscMessage("/" + objectname + "/" + property.Name, ob.ToString());
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(bool))
                {
                    bool ob = (bool)property.GetValue(obj, null);
                    OscMessage message = new OscMessage("/" + objectname + "/" + property.Name, ob.ToString());
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(Color))
                {
                    Color ob = (Color)property.GetValue(obj, null);
                    string hex = "#" + Utils.ColorToHexString(ob);
                    OscMessage message = new OscMessage("/" + objectname + "/" + property.Name, hex);
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(ObservableCollection<ListBoxFileName>))
                {
                    List<string> FileNameList = new List<string>();
                    ObservableCollection<ListBoxFileName> ob = (ObservableCollection<ListBoxFileName>)property.GetValue(obj, null);
                    foreach (ListBoxFileName filename in ob)
                    {
                        if (filename.FileIsSelected == true)
                        {
                            FileNameList.Add(filename.FileName);
                        }
                    }
                    OscMessage message = new OscMessage("/" + objectname + "/" + property.Name, FileNameList.ToArray());
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(ObservableCollection<string>))
                {
                    List<string> StringList = new List<string>();
                    ObservableCollection<string> ob = (ObservableCollection<string>)property.GetValue(obj, null);
                    foreach (string st in ob)
                    {
                        StringList.Add(st);
                    }
                    OscMessage message = new OscMessage("/" + objectname + "/" + property.Name, StringList.ToArray());
                    messagelist.Add(message);
                }
            }
            return messagelist;
        }

        public void SendAll(object datacontext, string ucName)
        {
            Type type = datacontext.GetType();
            PropertyInfo[] properties = type.GetProperties();

            List<OscMessage> messagelist = new List<OscMessage>();
            foreach (PropertyInfo property in properties)
            {
                if(property.PropertyType == typeof(string))
                {
                    string ob = (string)property.GetValue(datacontext, null);
                    OscMessage message = new OscMessage("/" + ucName + "/" + property.Name, ob);
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(double))
                {
                    double ob = (double)property.GetValue(datacontext, null);
                    OscMessage message = new OscMessage("/" + ucName + "/" + property.Name, ob.ToString());
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(bool))
                {
                    bool ob = (bool)property.GetValue(datacontext, null);
                    OscMessage message = new OscMessage("/" + ucName + "/" + property.Name, ob.ToString());
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(Color))
                {
                    Color ob = (Color)property.GetValue(datacontext, null);
                    string hex = "#" + Utils.ColorToHexString(ob);
                    OscMessage message = new OscMessage("/" + ucName + "/" + property.Name, hex);
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(ObservableCollection<ListBoxFileName>))
                {
                    List<string> FileNameList = new List<string>();
                    ObservableCollection<ListBoxFileName> ob = (ObservableCollection<ListBoxFileName>)property.GetValue(datacontext, null);
                    foreach(ListBoxFileName filename in ob)
                    {
                        if (filename.FileIsSelected == true)
                        {
                            FileNameList.Add(filename.FileName);
                        }
                    }
                    OscMessage message = new OscMessage("/" + ucName + "/" + property.Name, FileNameList.ToArray());
                    messagelist.Add(message);
                }
                else if (property.PropertyType == typeof(ObservableCollection<string>))
                {
                    List<string> StringList = new List<string>();
                    ObservableCollection<string> ob = (ObservableCollection<string>)property.GetValue(datacontext, null);
                    foreach (string st in ob)
                    {
                        StringList.Add(st);
                    }
                    OscMessage message = new OscMessage("/" + ucName + "/" + property.Name, StringList.ToArray());
                    messagelist.Add(message);
                }

            }
            OscBundle bundle = new OscBundle(0, messagelist.ToArray());
            Sender.Send(bundle);
        }

        public void SendBundle(List<OscMessage> messagelist)
        {
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



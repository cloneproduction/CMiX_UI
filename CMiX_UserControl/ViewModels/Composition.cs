﻿using CMiX.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Reflection;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Linq;
using CMiX;
using CMiX.Controls;

namespace CMiX.ViewModels
{
    public class Composition : ViewModel
    {
        public Composition()
        {
            Name = string.Empty;

            var messenger = new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555));
            Messenger = messenger;

            MasterBeat = new MasterBeat(messenger);
            Camera = new Camera(messenger, MasterBeat);
            AddLayerCommand = new RelayCommand(p => AddLayer());
            RemoveLayerCommand = new RelayCommand(p => RemoveLayer());
            //DeleteLayerCommand = new RelayCommand(p => DeleteLayer(p));

            LayerNames = new List<string>();
            Layers = new ObservableCollection<Layer> ();
            Layers.CollectionChanged += ContentCollectionChanged;
        }

        public Composition(string name, Camera camera, MasterBeat masterBeat, IEnumerable<Layer> layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException(nameof(layers));
            }

            var messenger = new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555));
            Messenger = messenger;

            Name = name;
            Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            MasterBeat = masterBeat ?? throw new ArgumentNullException(nameof(masterBeat));
            Layers = new ObservableCollection<Layer>(layers);
            Layers.CollectionChanged += ContentCollectionChanged;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private List<string> _layernames;
        public List<string> LayerNames 
        {
            get => _layernames;
            set => SetAndNotify(ref _layernames, value);
        }

        private IMessenger Messenger { get; }

        public MasterBeat MasterBeat { get; }

        public Camera Camera { get; }

        public ObservableCollection<Layer> Layers { get; }

        public ICommand AddLayerCommand { get; }
        public ICommand RemoveLayerCommand { get; }
        //public ICommand DeleteLayerCommand { get; }

        private int layerID = -1;

        private void AddLayer()
        {
            layerID += 1;
            Layer layer = new Layer(MasterBeat, "/Layer" + layerID.ToString(), Messenger, 0);

            this.LayerNames.Add("/Layer" + layerID.ToString());

            Layers.Add(layer);
            layer.Index = Layers.IndexOf(layer);

            List<string> layerindex = new List<string>();
            foreach (Layer lyr in this.Layers)
            {
                layerindex.Add(lyr.Index.ToString());
            }

            Messenger.QueueMessage("/LayerNames", this.LayerNames.ToArray());
            Messenger.QueueMessage("/LayerIndex", layerindex.ToArray());

            PrintProperties(this);
            Messenger.SendQueue();
        }


        private void RemoveLayer()
        {
            if(Layers.Count > 0)
            {
                int removeindex = Layers.Count - 1;
                int removeitem = Layers[removeindex].Index;
                string removedlayername = Layers[removeindex].LayerName;
                this.LayerNames.Remove(Layers[removeindex].LayerName);
                Layers.RemoveAt(removeindex);

                List<string> layerindex = new List<string>();
                foreach (Layer lyr in this.Layers)
                {
                    if(lyr.Index > removeitem)
                    {
                        lyr.Index -= 1;
                    }
                    layerindex.Add(lyr.Index.ToString());
                }

                Messenger.QueueMessage("/LayerNames", this.LayerNames.ToArray());
                Messenger.QueueMessage("/LayerIndex", layerindex.ToArray());
                Messenger.QueueMessage("/LayerRemoved", removedlayername);
                Messenger.SendQueue();
            }
            else if (Layers.Count == 0)
            {
                layerID = -1;
            }
        }

        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<string> layerindex = new List<string>();
            foreach (Layer lyr in this.Layers)
            {
                layerindex.Add(lyr.Index.ToString());
            }

            Messenger.QueueMessage("/LayerNames", this.LayerNames.ToArray());
            Messenger.QueueMessage("/LayerIndex", layerindex.ToArray());
            Messenger.SendQueue();
        }

        private void PrintProperties(object obj)
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
                            filenames.Add("FileNameTest");
                            filenames.Add("FileNameHello");
                            Messenger.QueueMessage(address + propertyname, filenames.ToArray());
                        }
                        else
                        {
                            propdata = property.GetValue(obj, null).ToString();
                            Messenger.QueueMessage(address + propertyname, propdata);
                        }
                    }
                }
                

                var elems = propValue as IList;
                if ((elems != null) && !(elems is string[]))
                {
                    foreach (var item in elems)
                    {
                        PrintProperties(item);
                    }
                }
                else
                {
                    // This will not cut-off System.Collections because of the first check
                    if (property.PropertyType.Assembly == objType.Assembly)
                    {
                        PrintProperties(propValue);
                    }
                    else
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
                    }
                }
            }
        }
    }
}

/*if (e.Action == NotifyCollectionChangedAction.Remove)
{
    foreach (Layer item in e.OldItems)
    {
        //Removed items
        item.PropertyChanged -= EntityViewModelPropertyChanged;
    }
}
else if (e.Action == NotifyCollectionChangedAction.Add)
{
    foreach (Layer item in e.NewItems)
    {
        //Added items
        item.PropertyChanged += EntityViewModelPropertyChanged;
    }
}*/

/*public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
{
List<string> filename = new List<string>();
foreach (Layer layer in Layers)
{
    filename.Add(layer.LayerName);
}
Messenger.SendMessage("POUETPOUET" + nameof(Layer), filename.ToArray());
}*/

using CMiX.Services;
using CMiX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using AutoMapper;

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

            CopyLayerCommand = new RelayCommand(p => CopyLayer());
            PasteLayerCommand = new RelayCommand(p => PasteLayer());

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
        private IMessenger Messenger { get; }
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



        public MasterBeat MasterBeat { get; }

        public Camera Camera { get; }

        public ObservableCollection<Layer> Layers { get; }

        public ICommand AddLayerCommand { get; }
        public ICommand RemoveLayerCommand { get; }
        public ICommand CopyLayerCommand { get; }
        public ICommand PasteLayerCommand { get; }

        private void CopyLayer()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Layer, LayerModel>());
            var mapper = config.CreateMapper();

            foreach (Layer lyr in Layers)
            {
                if (lyr.Enabled)
                {
                    LayerModel lm = mapper.Map<LayerModel>(lyr);
                       

                    IDataObject data = new DataObject();
                    data.SetData("Layer", lm, false);
                    Clipboard.SetDataObject(data);
                    continue;
                }
            }
        }

        private void PasteLayer()
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<LayerModel, Layer>().ConstructUsing((Func<LayerModel, Layer>)(x => new Layer())));
            var mapper = config.CreateMapper();

            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Layer"))
            {
                for (int i = 0; i < Layers.Count; i++)
                {
                    if (Layers[i].Enabled)
                    {
                        LayerModel lm = (LayerModel)data.GetData("Layer");

                        //MessageBox.Show(lm.Fade.ToString());
                        Layers[i].Fade = lm.Fade;
                        Layers[i].BlendMode = lm.BlendMode;
                        //Layers[i] = (Layer)data.GetData("Layer") as Layer;
                    }
                }
            }
        }

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

            //Messenger.QueueObject(this);
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

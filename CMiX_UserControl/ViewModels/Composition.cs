using CMiX.Services;
using CMiX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Windows;
using Newtonsoft.Json;
using System.IO;

namespace CMiX.ViewModels
{
    public class Composition : ViewModel, IMessengerData
    {
        public Composition()
        {
            Name = string.Empty;
            Messenger = new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555));
            MessageEnabled = true;
            MessageAddress = String.Empty;

            MasterBeat = new MasterBeat(Messenger);
            Camera = new Camera(Messenger, MasterBeat);
            AddLayerCommand = new RelayCommand(p => AddLayer());
            RemoveLayerCommand = new RelayCommand(p => RemoveLayer());

            CopyLayerCommand = new RelayCommand(p => CopyLayer());
            PasteLayerCommand = new RelayCommand(p => PasteLayer());

            SaveCompositionCommand = new RelayCommand(p => Save());
            OpenCompositionCommand = new RelayCommand(p => Open());

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

            Messenger = new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555));
            MessageEnabled = true;
            MessageAddress = String.Empty;
            Name = name;
            Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            MasterBeat = masterBeat ?? throw new ArgumentNullException(nameof(masterBeat));
            Layers = new ObservableCollection<Layer>(layers);
            Layers.CollectionChanged += ContentCollectionChanged;
        }
        private int layerID = -1;


        private IMessenger Messenger { get; }

        public string MessageAddress { get; set; } //NOT USED HERE..

        public bool MessageEnabled { get; set; }

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

        public MasterBeat MasterBeat { get; set; }

        public Camera Camera { get; set; }

        [OSC]
        public ObservableCollection<Layer> Layers { get; }

        public ICommand AddLayerCommand { get; }
        public ICommand RemoveLayerCommand { get; }
        public ICommand CopyLayerCommand { get; }
        public ICommand PasteLayerCommand { get; }
        public ICommand SaveCompositionCommand { get; }
        public ICommand OpenCompositionCommand { get; }


        public void Copy(CompositionDTO compositiondto)
        {
            compositiondto.Name = Name;
            compositiondto.LayerNames = LayerNames;

            foreach(Layer lyr in Layers)
            {
                LayerDTO layerdto = new LayerDTO();
                lyr.Copy(layerdto);
                compositiondto.LayersDTO.Add(layerdto);
            }

            Camera.Copy(compositiondto.CameraDTO);
        }


        public void Paste(CompositionDTO compositiondto)
        {
            MessageEnabled = false;

            Name = compositiondto.Name;
            LayerNames = compositiondto.LayerNames;

            Layers.Clear();
            layerID = -1;
            foreach (LayerDTO layerdto in compositiondto.LayersDTO)
            {
                layerID += 1;
                Layer layer = new Layer(MasterBeat, "/Layer" + layerID.ToString(), Messenger, 0);
                layer.Paste(layerdto);
                Layers.Add(layer);
            }

            Camera.Copy(compositiondto.CameraDTO);

            MessageEnabled = true;
        }

        public void Load(CompositionDTO compositiondto)
        {
            MessageEnabled = false;

            Name = compositiondto.Name;
            LayerNames = compositiondto.LayerNames;

            Layers.Clear();
            foreach (LayerDTO layerdto in compositiondto.LayersDTO)
            {
                Layer layer = new Layer(MasterBeat, layerdto.LayerName, Messenger, layerdto.Index);
                layer.Load(layerdto);
                Layers.Add(layer);
            }

            Camera.Copy(compositiondto.CameraDTO);

            MessageEnabled = true;
        }

        private void Save()
        {
            CompositionDTO compositiondto = new CompositionDTO();
            this.Copy(compositiondto);

            string folderPath = string.Empty;

            using (System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog())
            {
                if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    folderPath = savedialog.FileName;

                    string json = JsonConvert.SerializeObject(compositiondto);
                    System.IO.File.WriteAllText(folderPath, json);
                }
            }
        }

        private void Open()
        {
            using (System.Windows.Forms.OpenFileDialog opendialog = new System.Windows.Forms.OpenFileDialog())
            {
                opendialog.FileName = "default.json";

                string folderPath = string.Empty;

                if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    folderPath = opendialog.FileName;

                    // Check if you really have a file name 
                    if (opendialog.FileName.Trim() != string.Empty)
                    {
                        using (StreamReader r = new StreamReader(opendialog.FileName))
                        {
                            string json = r.ReadToEnd();
                            CompositionDTO compositiondto = new CompositionDTO();
                            compositiondto = JsonConvert.DeserializeObject<CompositionDTO>(json);
                            this.Load(compositiondto);

                            List<string> layerindex = new List<string>();
                            foreach (Layer lyr in this.Layers)
                            {
                                layerindex.Add(lyr.Index.ToString());
                            }

                            Messenger.QueueObject(this);
                            Messenger.QueueMessage("/LayerNames", this.LayerNames.ToArray());
                            Messenger.QueueMessage("/LayerIndex", layerindex.ToArray());
                            Messenger.SendQueue();
                        }
                    }
                }
            }
        }


        private void CopyLayer()
        {
            foreach (Layer lyr in Layers)
            {
                if (lyr.Enabled)
                {
                    LayerDTO layerdto = new LayerDTO();
                    lyr.Copy(layerdto);

                    IDataObject data = new DataObject();
                    data.SetData("Layer", layerdto, false);
                    Clipboard.SetDataObject(data);
                    continue;
                }
            }
        }

        private void PasteLayer()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Layer"))
            {
                for (int i = 0; i < Layers.Count; i++)
                {
                    if (Layers[i].Enabled)
                    {
                        var layerdto = (LayerDTO)data.GetData("Layer") as LayerDTO;
                        Layers[i].Paste(layerdto);

                        Messenger.QueueObject(Layers[i]);
                        Messenger.SendQueue();
                    }
                }
            }
        }


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
            Messenger.QueueObject(layer);
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

            if (MessageEnabled)
            {
                Messenger.QueueMessage("/LayerNames", this.LayerNames.ToArray());
                Messenger.QueueMessage("/LayerIndex", layerindex.ToArray());
                Messenger.SendQueue();
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

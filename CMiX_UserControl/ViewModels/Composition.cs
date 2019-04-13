using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Windows;
using System.IO;
using Newtonsoft.Json;
using CMiX.Services;
using CMiX.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Composition : ViewModel
    {
        #region CONSTRUCTORS
        public Composition()
            : base(new ObservableCollection<OSCMessenger>())
        {
            Name = string.Empty;
            Messengers = new ObservableCollection<OSCMessenger>();
            MessageAddress = String.Empty;        
            MasterBeat = new MasterBeat(Messengers, Mementor);
            Camera = new Camera(Messengers, MasterBeat, Mementor);
            LayerNames = new List<string>();
            Layers = new ObservableCollection<Layer>();
            Layers.CollectionChanged += ContentCollectionChanged;

            AddLayerCommand = new RelayCommand(p => AddLayer());
            RemoveLayerCommand = new RelayCommand(p => RemoveLayer());
            DeleteLayerCommand = new RelayCommand(p => DeleteLayer(p));
            DuplicateLayerCommand = new RelayCommand(p => DuplicateLayer(p));
            CopyLayerCommand = new RelayCommand(p => CopyLayer());
            PasteLayerCommand = new RelayCommand(p => PasteLayer());
            SaveCompositionCommand = new RelayCommand(p => Save());
            OpenCompositionCommand = new RelayCommand(p => Open());
            AddOSCCommand = new RelayCommand(p => AddOSC());
            RemoveSelectedOSCCommand = new RelayCommand(p => RemoveSelectedOSC());
            DeleteOSCCommand = new RelayCommand(p => DeleteOSC(p));

            Mementor = new Mementor();
        }

        public Composition(string name, Camera camera, MasterBeat masterBeat, IEnumerable<Layer> layers, Mementor mementor)
            : base(new ObservableCollection<OSCMessenger>())
        {
            if (layers == null)
            {
                throw new ArgumentNullException(nameof(layers));
            }

            Messengers = new ObservableCollection<OSCMessenger>();
            MessageAddress = String.Empty;
            Name = name;
            Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            MasterBeat = masterBeat ?? throw new ArgumentNullException(nameof(masterBeat));
            Layers = new ObservableCollection<Layer>(layers);
            Layers.CollectionChanged += ContentCollectionChanged;

            Mementor = mementor;
        }
        #endregion

        #region PROPERTIES
        private int layerID = -1;
        private int layerNameID = -1;

        public ICommand AddLayerCommand { get; }
        public ICommand RemoveLayerCommand { get; }
        public ICommand CopyLayerCommand { get; }
        public ICommand PasteLayerCommand { get; }
        public ICommand SaveCompositionCommand { get; }
        public ICommand OpenCompositionCommand { get; }
        public ICommand DeleteLayerCommand { get; }
        public ICommand DuplicateLayerCommand { get; }
        public ICommand AddOSCCommand { get; set; }
        public ICommand RemoveSelectedOSCCommand { get; set; }
        public ICommand DeleteOSCCommand { get; set; }

        public MasterBeat MasterBeat { get; set; }
        public Camera Camera { get; set; }

        [OSC]
        public ObservableCollection<Layer> Layers { get; }

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

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }


        private Layer _selectedlayer;
        public Layer SelectedLayer
        {
            get => _selectedlayer;
            set => SetAndNotify(ref _selectedlayer, value);
        }
        #endregion


        #region COPY/PASTE LAYER
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

                        QueueObjects(Layers[i]);
                        SendQueues();
                    }
                }
            }
        }
        #endregion

        #region ADD/REMOVE/DELETE OSC
        private void DeleteOSC(object oscmessenger)
        {
            OSCMessenger messenger = oscmessenger as OSCMessenger;
            Messengers.Remove(messenger);
        }

        private void RemoveSelectedOSC()
        {
            Console.WriteLine("Remove OSC");
        }

        private void AddOSC()
        {
            Messengers.Add(new OSCMessenger { Port = 55555, Address = "127.0.0.1", SendEnabled = true });
        }
        #endregion

        #region ADD/REMOVE/DUPLICATE/DELETE LAYERS
        private void RemoveLayer()
        {
            layerID -= 1;
            string removedlayername = string.Empty;
            List<string> layerindex = new List<string>();
            removedlayername = SelectedLayer.LayerName;
            LayerNames.Remove(SelectedLayer.LayerName);
            Layers.Remove(SelectedLayer);

            foreach (Layer lyr in Layers)
            {
                if (lyr.Index > Layers.IndexOf(SelectedLayer))
                {
                    lyr.Index -= 1;
                }
                layerindex.Add(lyr.Index.ToString());
            }

            QueueMessages("/LayerNames", this.LayerNames.ToArray());
            QueueMessages("/LayerIndex", layerindex.ToArray());
            QueueMessages("/LayerRemoved", removedlayername);
            SendQueues();
        }

        private void DeleteLayer(object layer)
        {
            Layer lyr = layer as Layer;
            layerID -= 1;

            Mementor.ElementRemove(Layers, lyr);
            LayerNames.Remove(lyr.LayerName);
            Layers.Remove(lyr);
            

            List<string> layerindex = new List<string>();
            foreach (Layer lay in Layers)
            {
                if (lay.Index > lyr.Index)
                {
                    lay.Index -= 1;
                }
                layerindex.Add(lay.Index.ToString());
            }

            QueueMessages("/LayerNames", this.LayerNames.ToArray());
            QueueMessages("/LayerIndex", layerindex.ToArray());
            QueueMessages("/LayerRemoved", lyr.LayerName);
            SendQueues();
        }

        private void DuplicateLayer(object layer)
        {
            layerID += 1;
            layerNameID += 1;
            LayerNames.Add("/Layer" + layerNameID.ToString());

            Layer lyr = layer as Layer;
            LayerDTO layerdto = new LayerDTO();
            lyr.Copy(layerdto);

            Layer newlayer = new Layer(MasterBeat, "/Layer" + layerNameID.ToString(), Messengers, layerNameID, Mementor);
            newlayer.Paste(layerdto);
            newlayer.LayerName = "/Layer" + layerNameID.ToString();
            newlayer.Index = layerID;
            newlayer.Enabled = false;

            int index = Layers.IndexOf(lyr) + 1;
            Layers.Insert(index, newlayer);

            List<string> layerindex = new List<string>();
            foreach (Layer lay in Layers)
            {
                layerindex.Add(lay.Index.ToString());
            }

            Mementor.ElementAdd(Layers, newlayer);
            QueueMessages("/LayerNames", this.LayerNames.ToArray());
            QueueMessages("/LayerIndex", layerindex.ToArray());
            QueueObjects(newlayer);
            SendQueues();
        }

        public void AddLayer()
        {
            layerID += 1;
            layerNameID += 1;

            Layer layer = new Layer(MasterBeat, "/Layer" + layerNameID.ToString(), Messengers, layerNameID, Mementor);
            layer.Index = layerID;
            Mementor.ElementAdd(Layers, layer);
            Layers.Add(layer);
            SelectedLayer = layer;
            LayerNames.Add("/Layer" + layerNameID.ToString());

            List<string> layerindex = new List<string>();
            foreach (Layer lyr in Layers)
            {
                layerindex.Add(lyr.Index.ToString());
            }

            QueueMessages("/LayerNames", this.LayerNames.ToArray());
            QueueMessages("/LayerIndex", layerindex.ToArray());
            QueueObjects(layer);
            SendQueues();
        }
 
        #endregion

        #region COPY/PASTE/LOAD/SAVE/OPEN COMPOSITIONS

        public void Copy(CompositionDTO compositiondto)
        {
            compositiondto.Name = Name;
            compositiondto.LayerNames = LayerNames;

            foreach (Layer lyr in Layers)
            {
                LayerDTO layerdto = new LayerDTO();
                lyr.Copy(layerdto);
                compositiondto.LayersDTO.Add(layerdto);
            }
            MasterBeat.Copy(compositiondto.MasterBeatDTO);
            Camera.Copy(compositiondto.CameraDTO);
        }

        public void Paste(CompositionDTO compositiondto)
        {
            //OSCControl.OSCMessenger.SendEnabled = false;

            Name = compositiondto.Name;
            LayerNames = compositiondto.LayerNames;

            Layers.Clear();
            layerID = -1;
            foreach (LayerDTO layerdto in compositiondto.LayersDTO)
            {
                layerID += 1;
                Layer layer = new Layer(MasterBeat, "/Layer" + layerID.ToString(), Messengers, 0, Mementor);
                layer.Paste(layerdto);
                Layers.Add(layer);
            }

            MasterBeat.Paste(compositiondto.MasterBeatDTO);
            Camera.Paste(compositiondto.CameraDTO);

            //OSCControl.OSCMessenger.SendEnabled = false;
        }

        public void Load(CompositionDTO compositiondto)
        {
            //OSCControl.OSCMessenger.SendEnabled = false;

            Name = compositiondto.Name;
            LayerNames = compositiondto.LayerNames;

            Layers.Clear();
            foreach (LayerDTO layerdto in compositiondto.LayersDTO)
            {
                Layer layer = new Layer(MasterBeat, layerdto.LayerName, Messengers, layerdto.Index, Mementor);
                layer.Load(layerdto);
                Layers.Add(layer);
            }

            MasterBeat.Paste(compositiondto.MasterBeatDTO);
            Camera.Paste(compositiondto.CameraDTO);

            //OSCControl.OSCMessenger.SendEnabled = false;
        }

        private void Save()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();

            if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CompositionDTO compositiondto = new CompositionDTO();
                this.Copy(compositiondto);
                string folderPath = savedialog.FileName;
                string json = JsonConvert.SerializeObject(compositiondto);
                File.WriteAllText(folderPath, json);
            }
        }


        private void Open()
        {
            System.Windows.Forms.OpenFileDialog opendialog = new System.Windows.Forms.OpenFileDialog();

            opendialog.FileName = "default.json";

            if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = opendialog.FileName;

                if (opendialog.FileName.Trim() != string.Empty) // Check if you really have a file name 
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

                        QueueObjects(this);
                        QueueMessages("/LayerNames", this.LayerNames.ToArray());
                        QueueMessages("/LayerIndex", layerindex.ToArray());
                        SendQueues();
                    }
                }
            }
        }
        #endregion

        #region NOTIFYCOLLECTIONCHANGED
        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            List<string> layerindex = new List<string>();

            foreach (Layer lyr in Layers)
            {
                layerindex.Add(lyr.Index.ToString());
            }

            QueueMessages("/LayerNames", this.LayerNames.ToArray());
            QueueMessages("/LayerIndex", layerindex.ToArray());
            SendQueues();
        }
        #endregion
    }
}
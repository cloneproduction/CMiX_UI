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
using MonitoredUndo;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;

namespace CMiX.ViewModels
{
    public class Composition : ViewModel, IMessengerData, ISupportsUndo
    {
        #region UNDO/REDO
        private CommandBindingCollection _commandBindings = new CommandBindingCollection();
        public CommandBindingCollection RegisterCommandBindings
        {
            get
            {
                return _commandBindings;
            }
        }

        private ICommand _windowLoadedCommand;
        public ICommand WindowLoadedCommand
        {
            get
            {
                return _windowLoadedCommand ?? (_windowLoadedCommand = new RelayCommand<EventArgs>(OnWindowLoaded));
            }
        }

        private ICommand _sliderMouseDownCommand;
        public ICommand SliderMouseDownCommand
        {
            get
            {
                return _sliderMouseDownCommand ?? (_sliderMouseDownCommand = new RelayCommand<MouseButtonEventArgs>(OnSliderMouseDown));
            }
        }

        private ICommand _sliderLostMouseCapture;
        public ICommand SliderLostMouseCapture
        {
            get
            {
                return _sliderLostMouseCapture ?? (_sliderLostMouseCapture = new RelayCommand<MouseEventArgs>(OnSliderLostMouseCapture));
            }
        }

        private void OnSliderLostMouseCapture(MouseEventArgs e)
        {
            //if (!BatchAgeChanges)
                //return;

            UndoService.Current[this].EndChangeSetBatch();

            e.Handled = false;
        }

        private void OnSliderMouseDown(MouseButtonEventArgs e)
        {
            //if (!BatchAgeChanges)
            //return;

            // Start a batch to collect all subsequent undo events (for this root)
            // into a single changeset.
            // 
            // Passing "false" for the last parameter tells the system to keep
            // each individual change that is made. If desired, pass "true" to
            // de-dupe these changes and reduce the memory requirements of the
            // changeset.
            UndoService.Current[this].BeginChangeSetBatch("Age Changed", false);

            e.Handled = false;
        }

        private void OnWindowLoaded(EventArgs e)
        {
            var root = UndoService.Current[this];
            root.UndoStackChanged += new EventHandler(OnUndoStackChanged);
            root.RedoStackChanged += new EventHandler(OnRedoStackChanged);
        }

        void OnUndoStackChanged(object sender, EventArgs e)
        {
            RefreshUndoStackList();
        }

        void OnRedoStackChanged(object sender, EventArgs e)
        {
            RefreshUndoStackList();
        }


        #region Properties
        public IEnumerable<ChangeSet> UndoStack
        {
            get
            {
                return UndoService.Current[this].UndoStack;
            }
        }

        public IEnumerable<ChangeSet> RedoStack
        {
            get
            {
                return UndoService.Current[this].RedoStack;
            }
        }
        #endregion

        #region Internal Methods

        private void RefreshUndoStackList()
        {
            var cv = CollectionViewSource.GetDefaultView(UndoStack);
            cv.Refresh();

            cv = CollectionViewSource.GetDefaultView(RedoStack);
            cv.Refresh();
        }

        #endregion

        private void InitialiseCommandBindings()
        {
            // create command binding for undo command
            var undoBinding = new CommandBinding(ApplicationCommands.Undo, UndoExecuted, UndoCanExecute);
            var redoBinding = new CommandBinding(ApplicationCommands.Redo, RedoExecuted, RedoCanExecute);

            // register the binding to the class
            CommandManager.RegisterClassCommandBinding(typeof(Layer), undoBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Layer), redoBinding);

            CommandBindings.Add(undoBinding);
            CommandBindings.Add(redoBinding);
        }

        private void RedoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // A shorthand version of the above call to Undo, except 
            // that this calls Redo.
            UndoService.Current[this].Redo();
        }

        private void RedoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Tell the UI whether Redo is available.
            e.CanExecute = UndoService.Current[this].CanRedo;
        }

        private void UndoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // Get the document root. In this case, we pass in "this", which 
            // implements ISupportsUndo. The ISupportsUndo interface is used
            // by the UndoService to locate the appropriate root node of an 
            // undoable document.
            // In this case, we are treating the window as the root of the undoable
            // document, but in a larger system the root would probably be your
            // domain model.
            var undoRoot = UndoService.Current[this];
            undoRoot.Undo();
        }

        private void UndoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Tell the UI whether Undo is available.
            e.CanExecute = UndoService.Current[this].CanUndo;
        }

        public CommandBindingCollection CommandBindings
        {
            get
            {
                return _commandBindings;
            }
        }
        #endregion

        #region CONSTRUCTORS
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

            InitialiseCommandBindings();
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

            InitialiseCommandBindings();
        }
        #endregion

        #region PROPERTIES
        private int layerID = -1;
        private int layerNameID = -1;

        private IMessenger Messenger { get; } 

        public string MessageAddress { get; set; } //NOT USED HERE..

        public bool MessageEnabled { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private string _IP = "127.0.0.1";
        public string IP
        {
            get => _IP;
            set
            {
                //MessageBox.Show("POUET");
                //Messenger = new OSCMessenger( new UDPSender( IP, Convert.ToInt32(Port)));
                SetAndNotify(ref _IP, value);
            }
        }

        private string _port = "55555";
        public string Port
        {
            get => _port;
            set
            {
                //MessageBox.Show("POUET");
                //Messenger = new OSCMessenger(new UDPSender(IP, Convert.ToInt32(Port)));
                SetAndNotify(ref _port, value);
            }
        }

        private List<string> _layernames;
        public List<string> LayerNames
        {
            get => _layernames;
            set => SetAndNotify(ref _layernames, value);
        }

        private Layer _selectedlayer;
        public Layer SelectedLayer
        {
            get => _selectedlayer;
            set => SetAndNotify(ref _selectedlayer, value);
        }

        public MasterBeat MasterBeat { get; set; }

        public Camera Camera { get; set; }

        [OSC]
        public ObservableCollection<Layer> Layers { get; }
        #endregion

        #region COMMANDS
        public ICommand AddLayerCommand { get; }
        public ICommand RemoveLayerCommand { get; }
        public ICommand CopyLayerCommand { get; }
        public ICommand PasteLayerCommand { get; }
        public ICommand SaveCompositionCommand { get; }
        public ICommand OpenCompositionCommand { get; }
        #endregion

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
            MasterBeat.Copy(compositiondto.MasterBeatDTO);
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

            MasterBeat.Paste(compositiondto.MasterBeatDTO);
            Camera.Paste(compositiondto.CameraDTO);

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

            MasterBeat.Paste(compositiondto.MasterBeatDTO);
            Camera.Paste(compositiondto.CameraDTO);

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
            layerNameID += 1;

            Layer layer = new Layer(MasterBeat, "/Layer" + layerNameID.ToString(), Messenger, layerNameID);
            
            layer.Index = layerID;
            Layers.Add(layer);

            this.LayerNames.Add("/Layer" + layerNameID.ToString());

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
            int removeindex;
            string removedlayername = string.Empty;
            List<string> layerindex = new List<string>();

            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                if (Layers[i].Enabled)
                {
                    layerID -= 1;

                    removeindex = Layers[i].Index;
                    removedlayername = Layers[i].LayerName;

                    LayerNames.Remove(Layers[i].LayerName);
                    Layers.Remove(Layers[i]);

                    foreach (Layer lyr in Layers)
                    {
                        if (lyr.Index > removeindex)
                        {
                            lyr.Index -= 1;
                        }
                        layerindex.Add(lyr.Index.ToString());

                    }
                    break;
                }
            }

            Messenger.QueueMessage("/LayerNames", this.LayerNames.ToArray());
            Messenger.QueueMessage("/LayerIndex", layerindex.ToArray());
            Messenger.QueueMessage("/LayerRemoved", removedlayername);
            Messenger.SendQueue();
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

            DefaultChangeFactory.Current.OnCollectionChanged(this, "Layers", Layers, e);
        }

        public object GetUndoRoot()
        {
            return this;
        }
    }
}
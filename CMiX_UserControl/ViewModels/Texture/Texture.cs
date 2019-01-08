using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

using CMiX.Services;
using CMiX.Controls;
using CMiX.Models;
using GalaSoft.MvvmLight.Command;
using MonitoredUndo;
using System.Windows.Data;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel, IMessengerData, ISupportsUndo
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

        public Texture(string layername, IMessenger messenger)
            : this(
                  texturePaths: new ObservableCollection<ListBoxFileName>(),
                  brightness: 0.0,
                  contrast: 0.0,
                  saturation: 0.0,
                  keying: 0.0,
                  invert: 0.0,
                  invertMode: ((TextureInvertMode)0).ToString(),

                  messenger: messenger,
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(Texture)),
                  messageEnabled: true
                  )
        {
            TexturePaths = new ObservableCollection<ListBoxFileName>();
            TexturePaths.CollectionChanged += ContentCollectionChanged;

            InitialiseCommandBindings();
        }

        public Texture(
            IEnumerable<ListBoxFileName> texturePaths,
            double brightness,
            double contrast,
            double saturation,
            double keying,
            double invert,
            string invertMode,
            IMessenger messenger,
            string messageaddress,
            bool messageEnabled
            )
        {
            TexturePaths = new ObservableCollection<ListBoxFileName>();
            TexturePaths.CollectionChanged += ContentCollectionChanged;

            AssertNotNegative(() => brightness);
            AssertNotNegative(() => contrast);
            AssertNotNegative(() => saturation);
            AssertNotNegative(() => keying);

            AssertNotNegative(() => invert);
            Brightness = brightness;
            Contrast = contrast;
            Saturation = saturation;
            Keying = keying;
            Invert = invert;
            InvertMode = invertMode;

            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;

            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());

            InitialiseCommandBindings();
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

        [OSC]
        public ObservableCollection<ListBoxFileName> TexturePaths { get; }


        private double _brightness;
        [OSC]
        public double Brightness
        {
            get => _brightness;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Brightness", _brightness, value, "Brightness Changed");
                SetAndNotify(ref _brightness, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Brightness), Brightness);
            }
        }

        private double _contrast;
        [OSC]
        public double Contrast
        {
            get => _contrast;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Contrast", _contrast, value, "Contrast Changed");
                SetAndNotify(ref _contrast, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Contrast), Contrast);
            }
        }

        private double _invert;
        [OSC]
        public double Invert
        {
            get => _invert;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Invert", _invert, value, "Invert Changed");
                SetAndNotify(ref _invert, CoerceNotNegative(value));
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Invert), Invert);
            }
        }

        private string _invertMode;
        [OSC]
        public string InvertMode
        {
            get => _invertMode;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "InvertMode", _invertMode, value, "InvertMode Changed");
                SetAndNotify(ref _invertMode, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(InvertMode), InvertMode);
            }
        }


        private double _hue;
        [OSC]
        public double Hue
        {
            get => _hue;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Hue", _hue, value, "Hue Changed");
                SetAndNotify(ref _hue, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Hue), Hue);
            }
        }

        private double _saturation;
        [OSC]
        public double Saturation
        {
            get => _saturation;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Saturation", _saturation, value, "Saturation Changed");
                SetAndNotify(ref _saturation, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Saturation), Saturation);
            }
        }

        private double _luminosity;
        [OSC]
        public double Luminosity
        {
            get => _luminosity;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Luminosity", _luminosity, value, "Luminosity Changed");
                SetAndNotify(ref _luminosity, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Luminosity), Luminosity);
            }
        }


        private double _keying;
        [OSC]
        public double Keying
        {
            get => _keying;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Keying", _keying, value, "Keying Changed");
                SetAndNotify(ref _keying, CoerceNotNegative(value));
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Keying), Keying);
            }
        }


        private double _pan;
        [OSC]
        public double Pan
        {
            get => _pan;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Pan", _pan, value, "Pan Changed");
                SetAndNotify(ref _pan, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Pan), Pan);
            }
        }

        private double _tilt;
        [OSC]
        public double Tilt
        {
            get => _tilt;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Tilt", _tilt, value, "Tilt Changed");
                SetAndNotify(ref _tilt, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Tilt), Tilt);
            }
        }

        private double _scale;
        [OSC]
        public double Scale
        {
            get => _scale;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Scale", _scale, value, "Scale Changed");
                SetAndNotify(ref _scale, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Scale), Scale);
            }
        }

        private double _rotate;
        [OSC]
        public double Rotate
        {
            get => _rotate;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Rotate", _rotate, value, "Rotate Changed");
                SetAndNotify(ref _rotate, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Rotate), Rotate);
            }
        }


        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ListBoxFileName item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ListBoxFileName item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }

            List<string> filename = new List<string>();
            foreach (ListBoxFileName lb in TexturePaths)
            {
                if (lb.FileIsSelected == true)
                {
                    filename.Add(lb.FileName);
                }
            }
            if (MessageEnabled)
                Messenger.SendMessage(MessageAddress + nameof(TexturePaths), filename.ToArray());
        }

        public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<string> filename = new List<string>();
            foreach (ListBoxFileName lb in TexturePaths)
            {
                if (lb.FileIsSelected == true)
                {
                    filename.Add(lb.FileName);
                }
            }
            if (MessageEnabled)
                Messenger.SendMessage(MessageAddress + nameof(TexturePaths), filename.ToArray());
        }

        public void Copy(TextureDTO texturedto)
        {
            foreach (ListBoxFileName lbfn in TexturePaths)
            {
                texturedto.TexturePaths.Add(lbfn);
            }

            texturedto.Brightness = Brightness;
            texturedto.Contrast = Contrast;
            texturedto.Saturation = Saturation;

            texturedto.Pan = Pan;
            texturedto.Tilt = Tilt;
            texturedto.Scale = Scale;
            texturedto.Rotate = Rotate;

            texturedto.Keying = Keying;
            texturedto.Invert = Invert;
            texturedto.InvertMode = InvertMode;
        }

        public void Paste(TextureDTO texturedto)
        {
            MessageEnabled = false;

            TexturePaths.Clear();
            foreach (ListBoxFileName lbfn in texturedto.TexturePaths)
            {
                TexturePaths.Add(lbfn);
            }

            Brightness = texturedto.Brightness;
            Contrast = texturedto.Contrast;
            Saturation = texturedto.Saturation;

            Pan = texturedto.Pan;
            Tilt = texturedto.Tilt;
            Scale = texturedto.Scale;
            Rotate = texturedto.Rotate;

            Keying = texturedto.Keying;
            Invert = texturedto.Invert;
            InvertMode = texturedto.InvertMode;

            MessageEnabled = true;
        }

        public void CopySelf()
        {
            TextureDTO texturedto = new TextureDTO();
            this.Copy(texturedto);
            IDataObject data = new DataObject();
            data.SetData("Texture", texturedto, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Texture"))
            {
                var texturedto = (TextureDTO)data.GetData("Texture") as TextureDTO;
                this.Paste(texturedto);

                Messenger.QueueObject(this);
                Messenger.SendQueue();
            }
        }

        public void ResetSelf()
        {
            TextureDTO texturedto = new TextureDTO();
            this.Paste(texturedto);
        }

        public object GetUndoRoot()
        {
            return this;
        }
    }
}
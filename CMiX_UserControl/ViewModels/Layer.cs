using CMiX.Models;
using CMiX.Services;
using System;
using System.Windows.Input;
using MonitoredUndo;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;
using System.Collections.Generic;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Layer : ViewModel, IMessengerData
    {
        #region

        private CommandBindingCollection _commandBindings = new CommandBindingCollection();

        private ICommand _windowLoadedCommand;
        private ICommand _sliderMouseDownCommand;
        private ICommand _sliderLostMouseCapture;

        public CommandBindingCollection RegisterCommandBindings
        {
            get
            {
                return _commandBindings;
            }
        }

        public ICommand WindowLoadedCommand
        {
            get
            {
                return _windowLoadedCommand ?? (_windowLoadedCommand = new RelayCommand(OnWindowLoaded));
            }
        }

        public ICommand SliderMouseDownCommand
        {
            get
            {
                return _sliderMouseDownCommand ?? (_sliderMouseDownCommand = new RelayCommand<MouseButtonEventArgs>(OnSliderMouseDown));
            }
        }

        public ICommand SliderLostMouseCapture
        {
            get
            {
                return _sliderLostMouseCapture ?? (_sliderLostMouseCapture = new RelayCommand<MouseEventArgs>(OnSliderLostMouseCapture));
            }
        }

        private void OnSliderLostMouseCapture(MouseEventArgs e)
        {
            UndoService.Current[this].EndChangeSetBatch();

            e.Handled = false;
        }

        private void OnSliderMouseDown(MouseButtonEventArgs e)
        {
            UndoService.Current[this].BeginChangeSetBatch("Age Changed", false);

            e.Handled = false;
        }

        private void OnWindowLoaded()
        {
            // The undo / redo stack collections are not "Observable", so we 
            // need to manually refresh the UI when they change.
            var root = UndoService.Current[this];
            root.UndoStackChanged += new EventHandler(OnUndoStackChanged);
            root.RedoStackChanged += new EventHandler(OnRedoStackChanged);
            //FirstNameTextbox.Focus();
        }

        void OnUndoStackChanged(object sender, EventArgs e)
        {
            RefreshUndoStackList();
        }

        void OnRedoStackChanged(object sender, EventArgs e)
        {
            RefreshUndoStackList();
        }

        #region Internal Methods


        private void RefreshUndoStackList()
        {
            var cv = CollectionViewSource.GetDefaultView(UndoStack);
            cv.Refresh();

            cv = CollectionViewSource.GetDefaultView(RedoStack);
            cv.Refresh();
        }

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

        private void InitialiseCommandBindings()
        {
            var undoBinding = new CommandBinding(ApplicationCommands.Undo, UndoExecuted, UndoCanExecute);
            var redoBinding = new CommandBinding(ApplicationCommands.Redo, RedoExecuted, RedoCanExecute);

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
            var undoRoot = UndoService.Current[this];
            undoRoot.Undo();
        }

        private void UndoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
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
        


        public Layer(MasterBeat masterBeat, string layername, IMessenger messenger, int index)
        {
            InitialiseCommandBindings();

            Messenger = messenger;
            MessageAddress = String.Format("{0}/", layername);
            MessageEnabled = false;

            Index = index;
            LayerName = layername;
            Fade = 0.0;
            BlendMode = ((BlendMode)0).ToString();
            Index = 0;
            Enabled = false;
            BeatModifier = new BeatModifier(layername, messenger, masterBeat);
            Content = new Content(BeatModifier, layername, messenger);
            Mask = new Mask(BeatModifier, layername, messenger);
            Coloration = new Coloration(BeatModifier, layername, messenger);
            LayerFX = new LayerFX(BeatModifier, layername, messenger);

            MessageEnabled = true;
        }

        public Layer(
            IMessenger messenger,
            string messageaddress,

            string layername,
            bool enabled,
            int index,
            double fade,
            string blendMode,
            BeatModifier beatModifier,
            Content content,
            Mask mask,
            Coloration coloration,
            LayerFX layerfx,

            bool messageEnabled
            )
        {
            LayerName = layername;
            Index = index;
            Enabled = enabled;
            Fade = fade;
            BlendMode = blendMode;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Mask = mask ?? throw new ArgumentNullException(nameof(mask));
            Coloration = coloration ?? throw new ArgumentNullException(nameof(coloration));
            LayerFX = layerfx ?? throw new ArgumentNullException(nameof(layerfx));
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
        }

        #region PROPERTY
        public IMessenger Messenger { get; }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        private string _layername;
        [OSC]
        public string LayerName
        {
            get => _layername;
            set => SetAndNotify(ref _layername, value);
        }

        private bool _enabled;
        [OSC]
        public bool Enabled
        {
            get => _enabled;
            set => SetAndNotify(ref _enabled, value);
        }

        private int _index;
        [OSC]
        public int Index
        {
            get => _index;
            set => SetAndNotify(ref _index, value);
        }

        private double _fade;
        [OSC]
        public double Fade
        {
            get => _fade;
            set
            {
                SetAndNotify(ref _fade, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Fade), Fade);
            }
        }

        private bool _out;
        [OSC]
        public bool Out
        {
            get => _out;
            set
            {
                SetAndNotify(ref _out, value);
                if (MessageEnabled && Out)
                    Messenger.SendMessage(MessageAddress + nameof(Out), Out);
            }
        }

        private string _blendMode;
        [OSC]
        public string BlendMode
        {
            get => _blendMode;
            set
            {
                SetAndNotify(ref _blendMode, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(BlendMode), BlendMode);
            }
        }

        public BeatModifier BeatModifier { get; }

        public Content Content { get; }

        public Mask Mask { get; }

        public Coloration Coloration { get; }

        public LayerFX LayerFX{ get; }

        #endregion

        #region COPY/PASTE/LOAD
        public void Copy(LayerDTO layerdto)
        {
            layerdto.BlendMode = BlendMode;
            layerdto.Fade = Fade;
            layerdto.LayerName = LayerName;
            layerdto.Index = Index;

            BeatModifier.Copy(layerdto.BeatModifierDTO);
            Content.Copy(layerdto.ContentDTO);
            Mask.Copy(layerdto.MaskDTO);
            Coloration.Copy(layerdto.ColorationDTO);
            LayerFX.Copy(layerdto.LayerFXDTO);
        }

        public void Paste(LayerDTO layerdto)
        {
            MessageEnabled = false;

            BlendMode = layerdto.BlendMode;
            Fade = layerdto.Fade;
            Out = layerdto.Out;

            BeatModifier.Paste(layerdto.BeatModifierDTO);
            Content.Paste(layerdto.ContentDTO);
            Mask.Paste(layerdto.MaskDTO);
            Coloration.Paste(layerdto.ColorationDTO);
            LayerFX.Paste(layerdto.LayerFXDTO);

            MessageEnabled = true;
        }

        public void Load(LayerDTO layerdto)
        {
            MessageEnabled = false;

            BlendMode = layerdto.BlendMode;
            Fade = layerdto.Fade;
            LayerName = layerdto.LayerName;
            Index = layerdto.Index;
            Out = layerdto.Out;

            BeatModifier.Paste(layerdto.BeatModifierDTO);
            Content.Paste(layerdto.ContentDTO);
            Mask.Paste(layerdto.MaskDTO);
            Coloration.Paste(layerdto.ColorationDTO);
            LayerFX.Paste(layerdto.LayerFXDTO);

            MessageEnabled = true;
        }
        #endregion
    }
}
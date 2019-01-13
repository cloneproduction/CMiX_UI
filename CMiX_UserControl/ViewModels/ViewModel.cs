using GalaSoft.MvvmLight.Command;
using MonitoredUndo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class ViewModel : INotifyPropertyChanged, ICloneable, ISupportsUndo
    {

        #region INOTIFYPROPERTYCHANGED
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetAndNotify<TRet>(ref TRet backingField, TRet newValue, [CallerMemberName] string propertyName = null)
        {
            backingField = newValue;
            Notify(propertyName);
        }

        public static void AssertNotNegative(double value, string parameterName)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value must not be negative.", parameterName);
            }
        }

        public static void AssertNotNegative(Expression<Func<double>> parameterExpression)
        {
            double value = parameterExpression.Compile().Invoke();

            if (value < 0)
            {
                throw new ArgumentException("Value must not be negative.", ((MemberExpression)parameterExpression.Body).Member.Name);
            }
        }

        public static double CoerceNotNegative(double value)
        {
            return value < 0 ? 0 : value;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

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
            CommandManager.RegisterClassCommandBinding(typeof(Composition), undoBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Composition), redoBinding);

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

        public object GetUndoRoot()
        {
            return this;
        }
        #endregion

        public ViewModel()
        {
            InitialiseCommandBindings();
        }
    }
}
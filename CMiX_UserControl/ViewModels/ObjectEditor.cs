using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class ObjectEditor : ViewModel
    {
        public ObjectEditor()
        {
            Editables = new ObservableCollection<IEditable>();
            AddEditableCommand = new RelayCommand(p => AddEditable(p));
            RemoveEditableCommand = new RelayCommand(p => RemoveEditable(p));
        }

        public ICommand AddEditableCommand { get; set; }
        public ICommand RemoveEditableCommand { get; set; }

        public ObservableCollection<IEditable> Editables { get; set; }

        private IEditable _selectedEditable;
        public IEditable SelectedEditable
        {
            get => _selectedEditable;
            set => SetAndNotify(ref _selectedEditable, value);
        }

        public void RemoveEditable(object obj)
        {
            var editable = obj as IEditable;
            Editables.Remove(editable);
        }

        public void AddEditable(object obj)
        {
            var editable = obj as IEditable;
            SelectedEditable = editable;
            if (!Editables.Contains(editable))
                Editables.Add(editable);
        }
    }
}

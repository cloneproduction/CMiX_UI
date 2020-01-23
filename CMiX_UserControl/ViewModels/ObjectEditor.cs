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
            RenameCommand = new RelayCommand(p => Rename(p));
        }

        public ICommand AddEditableCommand { get; set; }
        public ICommand RemoveEditableCommand { get; set; }
        public ICommand RenameCommand { get; set; }
        public ObservableCollection<IEditable> Editables { get; set; }

        private IEditable _selectedEditable;
        public IEditable SelectedEditable
        {
            get => _selectedEditable;
            set => SetAndNotify(ref _selectedEditable, value);
        }

        public void RemoveEditable(object obj)
        {
            System.Console.WriteLine("RemoveEditableReached");
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

        public void Rename(object obj)
        {
            var editable = obj as IEditable;
            editable.IsRenaming = true;
        }
    }
}

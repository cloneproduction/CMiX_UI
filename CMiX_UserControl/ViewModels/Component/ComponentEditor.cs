using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class ComponentEditor : ViewModel
    {
        public ComponentEditor()
        {
            Editables = new ObservableCollection<IComponent>();
            EditComponentCommand = new RelayCommand(p => EditComponent(p));
            RemoveComponentCommand = new RelayCommand(p => RemoveComponent(p));
            RenameComponentCommand = new RelayCommand(p => Rename(p));
        }

        public ICommand EditComponentCommand { get; set; }
        public ICommand RemoveComponentCommand { get; set; }
        public ICommand RenameComponentCommand { get; set; }

        public ObservableCollection<IComponent> Editables { get; set; }

        private IComponent _selectedEditable;
        public IComponent SelectedEditable
        {
            get => _selectedEditable;
            set => SetAndNotify(ref _selectedEditable, value);
        }


        public void RemoveComponent(object obj)
        {
            var editable = obj as IComponent;
            Editables.Remove(editable);
        }

        public void EditComponent(object obj)
        {
            var editable = obj as IComponent;
            if (!Editables.Contains(editable))
            {
                Editables.Add(editable);
                SelectedEditable = editable;
            }
        }

        public void Rename(object obj)
        {
            var editable = obj as IComponent;
            editable.IsRenaming = true;
        }
    }
}

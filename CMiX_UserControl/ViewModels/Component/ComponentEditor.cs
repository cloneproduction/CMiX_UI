using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class ComponentEditor : ViewModel
    {
        public ComponentEditor()
        {
            Editables = new ObservableCollection<Component>();
            EditComponentCommand = new RelayCommand(p => EditComponent(p as Component));
            RemoveComponentCommand = new RelayCommand(p => RemoveComponent(p));
            RenameComponentCommand = new RelayCommand(p => Rename(p));
        }

        public void ComponentManager_ComponentDeleted(object sender, ComponentEventArgs e)
        {
            Editables.Remove(e.Component);
        }

        public ICommand EditComponentCommand { get; set; }
        public ICommand RemoveComponentCommand { get; set; }
        public ICommand RenameComponentCommand { get; set; }

        public ObservableCollection<Component> Editables { get; set; }

        private Component _selectedEditable;
        public Component SelectedEditable
        {
            get => _selectedEditable;
            set => SetAndNotify(ref _selectedEditable, value);
        }

        public void RemoveComponent(object obj)
        {
            Editables.Remove(obj as Component);
        }

        public void EditComponent(Component component)
        {
            if (!Editables.Contains(component))
                Editables.Insert(0, component);
            else
                Editables.Move(Editables.IndexOf(component), 0);

            SelectedEditable = component;
        }

        public void Rename(object obj)
        {
            ((Component)obj).IsRenaming = true;
        }
    }
}

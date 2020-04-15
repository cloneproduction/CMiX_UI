using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class ComponentEditor : ViewModel
    {
        public ComponentEditor(ObservableCollection<Component> componentsInEditing)
        {
            ComponentsInEditing = componentsInEditing;
            EditComponentCommand = new RelayCommand(p => EditComponent(p as Component));
            RemoveComponentCommand = new RelayCommand(p => RemoveComponentFromEditing(p as Component));
        }

        public ICommand EditComponentCommand { get; set; }
        public ICommand RemoveComponentCommand { get; set; }

        public ObservableCollection<Component> ComponentsInEditing { get; set; }

        private Component _selectedEditable;
        public Component SelectedEditable
        {
            get => _selectedEditable;
            set => SetAndNotify(ref _selectedEditable, value);
        }

        public void RemoveComponentFromEditing(Component component)
        {
            ComponentsInEditing.Remove(component);
        }

        public void EditComponent(Component component)
        {
            if (!ComponentsInEditing.Contains(component))
                ComponentsInEditing.Insert(0, component);
            else
                ComponentsInEditing.Move(ComponentsInEditing.IndexOf(component), 0);

            SelectedEditable = component;
        }
    }
}

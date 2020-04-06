using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public abstract class Component : ViewModel, IUndoable, ISendable
    {
        public int ID { get; set; }
        public string MessageAddress { get; set; }

        public Assets Assets { get; set; }
        public Mementor Mementor { get; set; }
        public Beat Beat { get; set; }
        public MessageService MessageService { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }

        private bool _isVisible = false;
        public bool IsVisible
        {
            get
            {
                if (!ParentIsVisible)
                    return _isVisible;
                else
                    return true;
            }
            set
            {
                if (!ParentIsVisible)
                    SetAndNotify(ref _isVisible, value);
                SetVisibility();
            }
        }

        private bool _parentIsVisible = false;
        public bool ParentIsVisible
        {
            get => _parentIsVisible;
            set
            {
                SetAndNotify(ref _parentIsVisible, value);
                Notify(nameof(IsVisible));
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetAndNotify(ref _isExpanded, value);
        }

        private ObservableCollection<Component> _components;
        public ObservableCollection<Component> Components
        {
            get => _components;
            set => SetAndNotify(ref _components, value);
        }

        private Component _selectedComponent;
        public Component SelectedComponent
        {
            get => _selectedComponent;
            set => SetAndNotify(ref _selectedComponent, value);
        }


        public void AddComponent(Component component)
        {
            Components.Add(component);
            IsExpanded = true;
        }

        public void RemoveComponent(Component component)
        {
            Components.Remove(component);
        }

        public void SetVisibility()
        {
            foreach (var item in Components)
            {
                item.ParentIsVisible = IsVisible;
                item.SetVisibility();
            } 
        }

        public void Rename()
        {
            IsRenaming = true;
        }

        //public ComponentModel GetModel()
        //{
        //    ComponentModel componentModel = new ComponentModel();

        //    componentModel.IsExpanded = IsExpanded;
        //    componentModel.IsSelected = IsSelected;
        //    componentModel.Name = Name;
        //    componentModel.ParentIsVisible = ParentIsVisible;
        //    componentModel.SelectedComponent = SelectedComponent.GetModel();

        //    foreach (var component in Components)
        //    {
        //        componentModel.ComponentModels.Add(component.GetModel());
        //    }
            
        //    return componentModel;
        //}

        //public void SetViewModel(ComponentModel model)
        //{
        //    IsExpanded = model.IsExpanded;
        //    IsSelected = model.IsSelected;
        //    Name = model.Name;
        //    ParentIsVisible = model.ParentIsVisible;
        //    SelectedComponent.SetViewModel(model.SelectedComponent);;

        //    Components.Clear();
        //    foreach (var componentModel in model.ComponentModels)
        //    {
        //        Components.Add()
        //    }
        //}
    }
}
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Component : Sendable
    {
        public Component(int id, Beat beat)
        {
            ID = id;
            Beat = beat;
            Name = $"{GetType().Name}  {ID}";

            IsExpanded = true;
            Components = new ObservableCollection<Component>();
            TreeViewItemRigthClickCommand = new RelayCommand(p => RightClick());
        }

        private void RightClick()
        {
            this.IsSelected = true;
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as IComponentModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        ICommand TreeViewItemRigthClickCommand { get; set; }
        public Beat Beat { get; set; }

        public override string GetMessageAddress()
        {
            return $"{this.GetType().Name}/{ID}/";
        }


        private int _id;
        public int ID
        {
            get => _id;
            set => SetAndNotify(ref _id, value);
        }

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

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetAndNotify(ref _isEditing, value);
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
            OnSendChange(this.GetModel(), this.GetMessageAddress());
        }

        public void RemoveComponent(Component component)
        {
            Components.Remove(component);
            OnSendChange(this.GetModel(), this.GetMessageAddress());
        }

        private void SetVisibility()
        {
            foreach (var item in Components)
            {
                item.ParentIsVisible = IsVisible;
                item.SetVisibility();
            } 
        }
    }
}
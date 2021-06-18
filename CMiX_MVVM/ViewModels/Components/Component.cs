using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.ViewModels.Components.Factories;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public abstract class Component : ViewModel, IIDObject, IDisposable
    {
        public Component(IComponentModel componentModel)
        {
            ID = componentModel.ID;

            IsExpanded = false;
            Name = this.GetType().Name;

            Components = new ObservableCollection<Component>();
            Communicator = new ComponentCommunicator(this);
        }

        public ComponentCommunicator Communicator { get; set; }

        public abstract void SetCommunicator(Communicator communicator);
        public abstract void UnsetCommunicator(Communicator communicator);


        public Visibility Visibility { get; set; }
        public ICommand VisibilityCommand { get; set; }
        internal IComponentFactory ComponentFactory { get; set; }


        private Guid _id;
        public Guid ID
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

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
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


        public void AddComponent(Component component)
        {
            component.SetCommunicator(this.Communicator);
            Components.Add(component);
            IsExpanded = true;

            Communicator?.SendMessageAddComponent(component);
        }

        public void RemoveComponent(Component component)
        {
            int index = Components.IndexOf(component);
            component.Dispose();
            component.UnsetCommunicator(this.Communicator);
            Components.Remove(component);

            Communicator?.SendMessageRemoveComponent(index);
        }

        public void RemoveComponentAtIndex(int index)
        {
            Component component = Components.ElementAt(index);
            RemoveComponent(component);
        }

        public void InsertComponent(int index, Component component)
        {
            Components.Insert(index, component);
        }

        public void MoveComponent(int oldIndex, int newIndex)
        {
            Components.Move(oldIndex, newIndex);
        }

        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();

        public void Dispose()
        {
            foreach (var component in Components)
            {
                component.Dispose();
            }
        }
    }
}
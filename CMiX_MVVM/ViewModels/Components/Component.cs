using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.MessageService;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public abstract class Component : ViewModel, IMessageProcessor, IDisposable
    {
        public Component(IMessageTerminal messageTerminal, IComponentModel componentModel)
        {
            MessageDispatcher = new MessageDispatcher(messageTerminal);

            IsExpanded = false;

            Name = $"{this.GetType().Name}{componentModel.ID}";
            ID = componentModel.ID;

            Components = new ObservableCollection<Component>();
        }


        public Visibility Visibility { get; set; }
        public MessageDispatcher MessageDispatcher { get; set; }
        public ICommand VisibilityCommand { get; set; }
        public IMessageTerminal MessageTerminal { get; set; }


        private int _id;
        public int ID
        {
            get => _id;
            set => SetAndNotify(ref _id, value);
        }

        internal void DispatchMessage(IMessage message)
        {
            this.MessageDispatcher.NotifyIn(message);
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

        public string GetAddress() => $"{this.GetType().Name}/{ID}/";



        public void AddComponent(Component component)
        {
            Components.Add(component);
            IsExpanded = true;
            MessageDispatcher.NotifyOut(new MessageAddComponent(this.GetAddress(), component.GetModel() as IComponentModel));
        }

        public void RemoveComponent(Component component)
        {
            int index = Components.IndexOf(component);
            component.Dispose();
            Components.Remove(component);
            MessageDispatcher.NotifyOut(new MessageRemoveComponent(this.GetAddress(), index));
        }

        public void InsertComponent(int index, Component component)
        {
            Components.Insert(index, component);
            MessageDispatcher.NotifyOut(new MessageInsertComponent(GetAddress(), component.GetModel() as IComponentModel, index));
        }

        public void MoveComponent(int oldIndex, int newIndex)
        {
            Components.Move(oldIndex, newIndex);
            MessageDispatcher.NotifyOut(new MessageMoveComponent(this.GetAddress(), oldIndex, newIndex));
        }



        internal IComponentFactory ComponentFactory { get; set; }


        public Component CreateChild()
        {
            return ComponentFactory.CreateComponent();
        }


        public virtual void Dispose()
        {
            foreach (var component in Components)
            {
                component.Dispose();
            }
        }

        public abstract void SetViewModel(IModel model);

        public abstract IModel GetModel();
    }
}
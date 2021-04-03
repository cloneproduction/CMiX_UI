using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public abstract class Component : ViewModel, IComponentMessageProcessor, IDisposable
    {
        public Component(IComponentModel componentModel, IMessageDispatcher messageDispatcher)
        {
            IsExpanded = false;

            Name = $"{this.GetType().Name}";
            ID = componentModel.ID;

            Components = new ObservableCollection<Component>();

            MessageDispatcher = messageDispatcher;
            MessageDispatcher.MessageOutNotification += MessageDispatcher_MessageOutNotification;
        }


        public event Action<IMessage> MessageOutNotification;

        private void MessageDispatcher_MessageOutNotification(IMessage message)
        {
            Console.WriteLine("Component MessageDispatcher_MessageNotification ComponentID " + ((IViewModelMessage)message).ModuleID);
            var handler = MessageOutNotification;
            if (MessageOutNotification != null)
            {
                message.ComponentID = this.ID;
                handler(message);
            }
        }


        public Visibility Visibility { get; set; }
        public IMessageDispatcher MessageDispatcher { get; set; }
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


        public string GetAddress() => $"{this.GetType().Name}/{ID}/";


        public void AddComponent(Component component)
        {
            Components.Add(component);
            IsExpanded = true;
        }


        public void RemoveComponent(Component component)
        {
            int index = Components.IndexOf(component);
            component.Dispose();
            Components.Remove(component);
        }


        public void InsertComponent(int index, Component component)
        {
            Components.Insert(index, component);
        }


        public void MoveComponent(int oldIndex, int newIndex)
        {
            Components.Move(oldIndex, newIndex);
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
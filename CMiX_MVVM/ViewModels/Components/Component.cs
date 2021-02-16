using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Component : ViewModel, ISenderTest, IMessageProcessor, IComponent, IDisposable
    {
        public Component(int id, MessageTerminal MessageTerminal)
        {
            ID = id;
            IsExpanded = false;

            Name = $"{this.GetType().Name}{ID}";

            MessageDispatcher = new MessageDispatcher(MessageTerminal);
            MessageDispatcher.RegisterColleague(this);

            Components = new ObservableCollection<IComponent>();

            SetVisibilityCommand = new RelayCommand(p => SetVisibility());
        }

        public string GetAddress()
        {
            return $"{this.GetType().Name}/{ID}/";
        }


        public ICommand SetVisibilityCommand { get; set; }
        public MessageDispatcher MessageDispatcher { get; set; }
        public IComponentFactory ComponentFactory { get; set; }


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

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }

        private bool _isVisible = false;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetAndNotify(ref _isVisible, value);
                SetVisibility();
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetAndNotify(ref _isExpanded, value);
        }

        private ObservableCollection<IComponent> _components;
        public ObservableCollection<IComponent> Components
        {
            get => _components;
            set => SetAndNotify(ref _components, value);
        }

        public void SetVisibility()
        {
            foreach (var item in Components)
            {
                item.IsVisible = this.IsVisible;
                item.SetVisibility();
            }
        }
        
        public void AddComponent(IComponent component)
        {
            Components.Add(component);
            IsExpanded = true;
            IComponentModel model = component.GetModel() as IComponentModel;
            IMessage message = new MessageAddComponent(this.GetAddress(), model);
            this.Send(message);
        }

        public void RemoveComponent(Component component)
        {
            int index = Components.IndexOf(component);
            component.Dispose();
            Components.Remove(component);
            IMessage message = new MessageRemoveComponent(this.GetAddress(), index);
            this.Send(message);
        }


        public void InsertComponent(int index, Component component)
        {
            Components.Insert(index, component);
            var model = component.GetModel() as IComponentModel;
            IMessage message = new MessageInsertComponent(GetAddress(), model, index);
            this.Send(message);
        }

        public void MoveComponent(int oldIndex, int newIndex)
        {
            Components.Move(oldIndex, newIndex);
            IMessage message = new MessageMoveComponent(this.GetAddress(), oldIndex, newIndex);
            this.Send(message);
        }


        public void Send(IMessage message)
        {
            MessageDispatcher?.Notify(MessageDirection.OUT, message);
        }

        public void Receive(IMessage message)
        {
            message.Process(this);
        }

        public Component CreateAndAddComponent()
        {
            IComponent component = ComponentFactory.CreateComponent(this);
            this.AddComponent(component);
            return (Component)component;
        }

        public virtual void Dispose()
        {
            MessageDispatcher.Dispose();
            foreach (var component in Components)
            {
                component.Dispose();
            }
        }


        public void SetComponents(Component component, IComponentModel componentModel)
        {
            component.Components.Clear();
            foreach (var model in componentModel.ComponentModels)
                component.CreateAndAddComponent().SetViewModel(model);
        }

        public void GetComponents(Component component, IComponentModel componentModel)
        {
            foreach (Component item in component.Components)
                componentModel.ComponentModels.Add(item.GetModel() as IComponentModel);
        }

        public abstract void SetViewModel(IModel model);

        public abstract IModel GetModel();
    }
}
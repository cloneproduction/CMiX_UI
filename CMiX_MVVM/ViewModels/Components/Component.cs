using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Component : ViewModel, IColleague, IComponent, IDisposable
    {
        public Component(int id, MessengerTerminal messengerTerminal)
        {
            ID = id;
            Name = $"{GetType().Name}{ID}";
            IsExpanded = false;
            this.Address = $"{this.GetType().Name}/{ID}/";

            MessageMediator = new MessageMediator(messengerTerminal);
            MessageMediator.RegisterColleague(this);

            Components = new ObservableCollection<Component>();
            Factory = new ComponentFactory(messengerTerminal);

        }

        public string Address { get; set; }
        public MessageMediator MessageMediator { get; set; }
        private ComponentFactory Factory { get; set; }


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

        private void SetVisibility()
        {
            foreach (var item in Components)
            {
                item.ParentIsVisible = IsVisible;
                item.SetVisibility();
            }
        }

        public void AddComponent(Component component)
        {
            Components.Add(component);
            IsExpanded = true;
            var model = component.GetModel();
            Message message = new Message(MessageCommand.ADD_COMPONENT, Address, model);
            this.Send(message);
        }

        public void RemoveComponent(Component component)
        {
            int index = Components.IndexOf(component);
            component.Dispose();
            Components.Remove(component);
            Message message = new Message(MessageCommand.REMOVE_COMPONENT, Address, index);
            this.Send(message);
        }

        public void InsertComponent(int index, Component component)
        {
            Components.Insert(index, component);
            var model = component.GetModel();
            Message message = new Message(MessageCommand.INSERT_COMPONENT, Address, model, index);
            this.Send(message);
        }

        public void MoveComponent(int oldIndex, int newIndex)
        {
            Components.Move(oldIndex, newIndex);
            Message message = new Message(MessageCommand.MOVE_COMPONENT, Address, oldIndex, newIndex);
            this.Send(message);
        }


        public void Send(Message message)
        {
            MessageMediator.Notify(MessageDirection.OUT, message);
        }

        public void Receive(Message message)
        {
            switch (message.Command)
            {
                case MessageCommand.ADD_COMPONENT:
                    {
                        var model = message.Obj as IComponentModel;
                        var component = Factory.CreateComponent(this, model);
                        this.Components.Add(component);
                        break;
                    }
                case MessageCommand.INSERT_COMPONENT:
                    {
                        var model = message.Obj as IComponentModel;
                        int index = (int)message.CommandParameter;
                        var component = Factory.CreateComponent(this, model);
                        this.Components.Insert(index, component);
                        break;
                    }
                case MessageCommand.REMOVE_COMPONENT:
                    {
                        int index = (int)message.Obj;
                        this.Components[index].Dispose();
                        this.Components.RemoveAt(index);
                        break;
                    }
                case MessageCommand.MOVE_COMPONENT:
                    {
                        int oldIndex = (int)message.Obj;
                        int newIndex = (int)message.CommandParameter;
                        this.Components.Move(oldIndex, newIndex);
                        break;
                    }
                case MessageCommand.UPDATE_VIEWMODEL:
                    {
                        var model = message.Obj as IComponentModel;
                        this.SetViewModel(model);
                        break;
                    }
                default: break;
            }
        }

        public Component CreateAndAddComponent()
        {
            Component component = Factory.CreateComponent(this);
            this.AddComponent(component);
            return component;
        }

        public void Dispose()
        {
            MessageMediator.Dispose();
            foreach (var component in Components)
            {
                component.Dispose();
            }
        }
    }
}
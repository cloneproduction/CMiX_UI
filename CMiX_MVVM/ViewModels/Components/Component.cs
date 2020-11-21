using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Component : ViewModel, IComponent, IColleague, IDisposable
    {
        public Component(int id, MessengerTerminal messengerTerminal)
        {
            ID = id;
            Name = $"{GetType().Name}{ID}";
            IsExpanded = false;
            this.Address = $"{this.GetType().Name}/{ID}/";

            MessageMediator = new MessageMediator();
            MessageMediator.SetComponentOwner(this);

            Components = new ObservableCollection<Component>();
            Factory = new ComponentFactory();
            MessengerTerminal = messengerTerminal;
        }

        public string Address { get; set; }
        public MessageMediator MessageMediator { get; set; }
        public MessengerTerminal MessengerTerminal { get; set; }
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
            Send(new Message(MessageDirection.OUT, Address, MessageSerializer.Serializer.Serialize(this.GetModel())));
        }

        public void RemoveComponent(Component component)
        {
            component.Dispose();
            Components.Remove(component);
            Send(new Message(MessageDirection.OUT, Address, MessageSerializer.Serializer.Serialize(this.GetModel())));
        }

        public void InsertComponent(int index, Component component)
        {
            Components.Insert(index, component);
            Send(new Message(MessageDirection.OUT, Address, MessageSerializer.Serializer.Serialize(this.GetModel())));
        }

        public void MoveComponent(int oldIndex, int newIndex)
        {
            Components.Move(oldIndex, newIndex);
            Send(new Message(MessageDirection.OUT, Address, MessageSerializer.Serializer.Serialize(this.GetModel())));
        }

        public void Send(Message message)
        {
            MessengerTerminal.SendComponentUpdate(Address, this.GetModel());
           //MessageMediator.Notify(Address, this, message);
        }

        public void Receive(Message message)
        {
            //if (message.Address == Address)
            //{
            //    var model = MessageSerializer.Serializer.Deserialize<IComponentModel>(message.Data);
            //    this.SetViewModel(model);
            //}
        }

        public Component CreateAndAddComponent()
        {
            Component component = Factory.CreateComponent(this) as Component;
            this.AddComponent(component);
            return component;
        }

        public void Dispose()
        {
            foreach (var component in Components)
            {
                component.Dispose();
            }
        }
    }
}
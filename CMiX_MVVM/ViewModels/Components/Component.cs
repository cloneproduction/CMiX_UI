using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.Studio.ViewModels.MessageService;
using PubSub;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Component : ViewModel, IComponent, IColleague
    {
        public Component(int id)
        {
            ID = id;
            Name = $"{GetType().Name}{ID}";
            
            this.Address = $"{this.GetType().Name}/{ID}/";

            IsExpanded = false;
            Components = new ObservableCollection<Component>();
            Hub = Hub.Default;

            MessageMediator = new MessageMediator();
            MessageMediator.RegisterColleague(Address, this);

            Hub.Subscribe<Message>(this, message =>
            {
                if(message.Direction == MessageDirection.IN)
                {
                    if (message.Address == Address)
                    {
                        var model = MessageSerializer.Serializer.Deserialize<IComponentModel>(message.Data);
                        this.SetViewModel(model);
                    }
                    else if (message.Address.Contains(Address))
                    {
                        MessageMediator.Notify(message.Address, this, message);
                    }
                }
            });
        }

        public Hub Hub { get; set; }
        public string Address { get; set; }
        public MessageMediator MessageMediator { get; set; }
        public MessengerManager MessengerManager { get; set; }



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
            Hub.Unsubscribe(component);
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
            MessageMediator.Notify(Address, this, message);
        }

        public void Receive(Message message)
        {
            //throw new NotImplementedException();
        }
    }
}
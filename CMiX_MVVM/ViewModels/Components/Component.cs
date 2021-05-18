using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.MessageService.Components;
using CMiX.MVVM.ViewModels.Components.Factories;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public abstract class Component : ViewModel, IDisposable
    {
        public Component(IComponentModel componentModel)
        {
            IsExpanded = false;

            Name = $"{this.GetType().Name}";
            ID = componentModel.ID;

            Components = new ObservableCollection<Component>();
        }


        public ComponentMessageEmitter MessageEmitter { get; set; }
        internal IMessageReceiver MessageReceiver { get; set; }


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
            Components.Add(component);
            IsExpanded = true;

            MessageEmitter?.SendMessageAddComponent(component);
        }

        public void RemoveComponent(Component component)
        {
            int index = Components.IndexOf(component);
            component.Dispose();
            Components.Remove(component);
            MessageEmitter?.SendMessageRemoveComponent(index);
        }

        public void RemoveComponentAtIndex(int index)
        {
            Component component = Components.ElementAt(index);
            if(component != null)
            {
                component.Dispose();
                Components.Remove(component);
            }
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



        public virtual void SetReceiver(IMessageReceiver messageReceiver)
        {
            var messageProcessor = new ComponentMessageProcessor(this);
            MessageReceiver = new MessageReceiver(messageProcessor);
            messageReceiver.RegisterMessageReceiver(this.ID, MessageReceiver);
        }


        public virtual IMessageSender SetSender(IMessageSender messageSender)
        {
            var sender = new MessageSender(this.ID);
            sender.SetSender(messageSender);
            MessageEmitter = new ComponentMessageEmitter(sender);

            return sender;
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
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public abstract class Component : ViewModel, IMessageProcessor, IDisposable
    {
        public Component(MessageTerminal messageTerminal, IComponentModel componentModel)
        {
            IsExpanded = false;
            Name = $"{this.GetType().Name}{ID}";
            ID = componentModel.ID;

            MessageTerminal = messageTerminal;
            MessageTerminal.MessageReceived += MessageTerminal_MessageReceived;

            MessageDispatcher = new MessageDispatcher(MessageTerminal);
            Components = new ObservableCollection<Component>();
        }

        private void MessageTerminal_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            if(message is IComponentMessage)
                message.Process(this);
            else
                MessageDispatcher.NotifyIn(message);
        }


        public MessageTerminal MessageTerminal { get; set; }
        public Visibility Visibility { get; set; }
        public MessageDispatcher MessageDispatcher { get; set; }
        public IComponentFactory ComponentFactory { get; set; }
        public ICommand VisibilityCommand { get; set; }


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

        public Component CreateAndAddComponent()
        {
            Component component = ComponentFactory.CreateComponent(this);
            this.AddComponent(component);
            return component;
        }

        public virtual void Dispose()
        {
            MessageTerminal.MessageReceived -= MessageTerminal_MessageReceived;
            //MessageDispatcher.Dispose();
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
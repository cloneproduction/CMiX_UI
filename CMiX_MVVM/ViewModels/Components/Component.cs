﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
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

        public ComponentSender ComponentSender { get; set; }
        public ComponentReceiver ComponentReceiver { get; set; }

        public ModuleReceiver MessageReceiver { get; set; }
        public ModuleMessageSender MessageSender { get; set; }


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

            ComponentSender?.SendMessageAddComponent(ID, component);
        }

        public void RemoveComponent(Component component)
        {
            int index = Components.IndexOf(component);
            component.Dispose();
            Components.Remove(component);
            ComponentSender?.SendMessageRemoveComponent(this.ID, index);
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


        public virtual void SetReceiver(IMessageReceiver<Component> messageReceiver)
        {
            ComponentReceiver = messageReceiver;
            MessageReceiver = new ModuleReceiver();
            messageReceiver.RegisterReceiver(this, ID);
        }

        public virtual void SetSender(ComponentSender messageSender)
        {
            ComponentSender = messageSender;
            MessageSender = new ModuleMessageSender(this.ID);
            MessageSender.SetSender(messageSender);
        }

        public void Dispose()
        {
            //if(MessageDispatcher is IMessageDispatcherReceiver)
            //    ((IMessageDispatcherReceiver)MessageDispatcher).UnregisterMessageReceiver(this);

            foreach (var component in Components)
            {
                component.Dispose();
            }
        }
    }
}
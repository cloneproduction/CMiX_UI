using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.MessageService.MessageSendCOR;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public abstract class Component : ViewModel, IHandler, IComponentMessageProcessor, IDisposable
    {
        public Component(IComponentModel componentModel, IMessageDispatcher messageDispatcher)
        {
            IsExpanded = false;

            Name = $"{this.GetType().Name}";
            ID = componentModel.ID;

            Components = new ObservableCollection<Component>();

            SetNextSender(messageDispatcher as IHandler);
            MessageDispatcher = messageDispatcher;
        }





        private IHandler _nextHandler;
        public IHandler SetNextSender(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }


        public void SendMessage(IMessage message)
        {
            if(_nextHandler != null)
            {
                _nextHandler.SendMessage(message);
            }
            //MessageDispatcher.SendMessage(message);
        }

        public void ProcessMessage(IMessage message)
        {
            if(message is IViewModelMessage)
            {
                var vmMessage = message as IViewModelMessage;
                var module = MessageDispatcher.GetMessageProcessor(vmMessage.ModuleID);
                if(module != null)
                    module.ProcessMessage(vmMessage);
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

        public abstract void SetModuleReceiver(ModuleMessageDispatcher messageDispatcher);


        internal void SetAsSender(IMessageDispatcher messageDispatcher)
        {
            ModuleMessageDispatcher moduleMessageDispatcher = new ModuleMessageDispatcher();
            moduleMessageDispatcher.SetNextSender(messageDispatcher);
            this.SetModuleSender(moduleMessageDispatcher);
        }

        internal void SetAsReceiver(IMessageDispatcher messageDispatcher)
        {
            ModuleMessageDispatcher moduleMessageDispatcher = new ModuleMessageDispatcher();
            this.SetModuleReceiver(moduleMessageDispatcher);
            messageDispatcher.RegisterMessageProcessor(this);
        }


        public abstract void SetModuleSender(ModuleMessageDispatcher messageDispatcher);
    }
}
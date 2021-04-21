using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public abstract class Component : ViewModel, IMessageCommunicator, IDisposable
    {
        public Component(IComponentModel componentModel)
        {
            IsExpanded = false;

            Name = $"{this.GetType().Name}";
            ID = componentModel.ID;

            Components = new ObservableCollection<Component>();
        }


        public IMessageDispatcher MessageDispatcher { get; set; }
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


        public void Dispose()
        {
            //if(MessageDispatcher is IMessageDispatcherReceiver)
            //    ((IMessageDispatcherReceiver)MessageDispatcher).UnregisterMessageReceiver(this);

            foreach (var component in Components)
            {
                component.Dispose();
            }
        }


        public abstract void SetViewModel(IModel model);
        public abstract IModel GetModel();


        public abstract void SetReceiver(IMessageReceiver messageReceiver);
        public abstract void SetSender(IMessageSender messageSender);


        public void SetMessageCommunication(IMessageDispatcher messageDispatcher)
        {
            if (messageDispatcher is ComponentMessageSender)
            {
                this.SetAsSender(messageDispatcher as ComponentMessageSender);
            }
            else if (messageDispatcher is ComponentMessageReceiver)
            {
                this.SetAsReceiver(messageDispatcher as ComponentMessageReceiver);
            }
        }


        private void SetAsSender(ComponentMessageSender componentMessageSender)
        {
            ModuleMessageSender moduleMessageSender = new ModuleMessageSender(this.ID);

            moduleMessageSender.SetSender(componentMessageSender);
            this.SetSender(moduleMessageSender);

            MessageDispatcher = moduleMessageSender;
        }

        private void SetAsReceiver(ComponentMessageReceiver componentMessageReceiver)
        {
            componentMessageReceiver.RegisterReceiver(this);

            ModuleMessageReceiver moduleMessageReceiver = new ModuleMessageReceiver();
            this.SetReceiver(moduleMessageReceiver);

            MessageDispatcher = moduleMessageReceiver;
        }

        public void SendMessage()
        {
            throw new NotImplementedException();
        }

        public void ReceiveMessage(IMessage message)
        {
            var msg = message as IComponentMessage;
            if(msg != null)
            {
                Console.WriteLine("Component received IComponentMessage");
                return;
            }
            MessageDispatcher.ProcessMessage(message);
        }
    }
}
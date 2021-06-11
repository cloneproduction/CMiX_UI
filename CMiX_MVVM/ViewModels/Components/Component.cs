using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.ViewModels.Components.Factories;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public abstract class Component : ViewModel, IIDObject, IDisposable
    {
        public Component(IComponentModel componentModel)
        {
            IsExpanded = false;
            Name = this.GetType().Name;
            ID = componentModel.ID;
            Components = new ObservableCollection<Component>();
            Communicator = new ComponentCommunicator(this);

        }

        public ComponentCommunicator Communicator { get; set; }
        public ComponentMessageProcessor MessageProcessor { get; set; }
        //public IMessageReceiver MessageReceiver { get; set; }
        //public IMessageSender MessageSender { get; set; }


        public abstract void SetCommunicator(ICommunicator communicator);
        //{
        //    Communicator.SetNextCommunicator(communicator);
        //}

        //public virtual void SetReceiver(IMessageReceiver messageReceiver)
        //{
        //    Communicator.SetReceiver(messageReceiver);
        //    //var messageProcessor = new ComponentMessageProcessor(this);
        //    //MessageReceiver = new MessageReceiver(messageProcessor);
        //    //messageReceiver.RegisterReceiver(MessageReceiver);

        //    //Visibility.SetReceiver(MessageReceiver);
        //}

        //public virtual void SetSender(IMessageSender messageSender)
        //{
        //    Communicator.SetSender(messageSender);
        //    //MessageSender = new MessageSender(this);
        //    //MessageSender.SetSender(messageSender);

        //    //Visibility.SetSender(MessageSender);
        //}



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
            Communicator.SendMessageAddComponent(component);
            //MessageSender?.SendMessage(new MessageAddComponent(component));
        }

        public void RemoveComponent(Component component)
        {
            int index = Components.IndexOf(component);
            component.Dispose();
            Components.Remove(component);
            //MessageReceiver?.UnregisterReceiver(component.MessageReceiver);
            Communicator?.SendMessageAddComponent(component);
            //MessageSender?.SendMessage(new MessageRemoveComponent(index));
        }

        public void RemoveComponentAtIndex(int index)
        {
            Component component = Components.ElementAt(index);
            RemoveComponent(component);
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





        public void Dispose()
        {
            foreach (var component in Components)
            {
                component.Dispose();
            }
        }
    }
}
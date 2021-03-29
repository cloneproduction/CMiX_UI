using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public abstract class Component : ViewModel, IComponentMessageProcessor, IDisposable
    {
        public Component(IComponentModel componentModel)
        {
            IsExpanded = false;

            Name = $"{this.GetType().Name}{componentModel.ID}";
            ID = componentModel.ID;

            Components = new ObservableCollection<Component>();
            MessageDispatcher = new MessageDispatcher(componentModel.ID);
            MessageDispatcher.MessageNotification += MessageDispatcher_MessageNotification;
        }

        private void MessageDispatcher_MessageNotification(IViewModelMessage message)
        {
            var handler = MessageNotification;
            if (MessageNotification != null)
            {
                message.ComponentID = this.ID;
                handler(this, message);
                //Console.WriteLine("MessageDispatcher_MessageNotification Raised by " + this.ID);
            }
        }


        public Visibility Visibility { get; set; }
        public MessageDispatcher MessageDispatcher { get; set; }
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


        public event Action<IMessageProcessor, IMessage> MessageNotification;
        //protected void RaiseMessageNotification(IMessage message)
        //{
        //    var handler = MessageNotification;
        //    if (handler != null)
        //    {
        //        handler(this, message);
        //        Console.WriteLine("MessageNotification Raised by " + this.GetAddress());
        //    }
        //}


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

            var handler = MessageNotification;
            if (MessageNotification != null)
            {
                handler(this, new MessageAddComponent(this.ID, component.GetModel() as IComponentModel));
                Console.WriteLine("MessageNotification Raised by " + this.GetType() + " ID is " + ID);
            }
        }


        public void RemoveComponent(Component component)
        {
            int index = Components.IndexOf(component);
            component.Dispose();
            Components.Remove(component);

            var handler = MessageNotification;
            if (MessageNotification != null)
            {
                handler(this, new MessageRemoveComponent(this.ID, index));
            }
        }


        public void InsertComponent(int index, Component component)
        {
            Components.Insert(index, component);
            //MessageTerminal.ProcessMessage(new MessageInsertComponent(GetAddress(), component.GetModel() as IComponentModel, index));
        }

        public void MoveComponent(int oldIndex, int newIndex)
        {
            Components.Move(oldIndex, newIndex);
            //MessageTerminal.ProcessMessage(new MessageMoveComponent(this.GetAddress(), oldIndex, newIndex));
        }


        public Component CreateChild()
        {
            return ComponentFactory.CreateComponent();
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
    }
}
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using CMiX.Studio.ViewModels.MessageService;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public class ComponentManager : ViewModel
    {
        public ComponentManager(ObservableCollection<Component> components)
        {
            Components = components;

            CreateComponentCommand = new RelayCommand(p => CreateComponent(p as Component));
            DuplicateComponentCommand = new RelayCommand(p => DuplicateComponent(p as Component));
            DeleteComponentCommand = new RelayCommand(p => DeleteComponent(p as Component));
            RenameComponentCommand = new RelayCommand(p => RenameComponent(p as Component));
        }


        public void SetMessageCommunication(IMessageDispatcher messageDispatcher)
        {
            if (messageDispatcher is ComponentManagerMessageSender)
            {
                this.SetAsSender(messageDispatcher as ComponentManagerMessageSender);
            }
            else if (messageDispatcher is ComponentManagerMessageReceiver)
            {
                this.SetAsReceiver(messageDispatcher as ComponentManagerMessageReceiver);
            }
        }


        private void SetAsSender(ComponentManagerMessageSender messageSender)
        {
            var componentMessageSender = new ComponentMessageSender();
            componentMessageSender.SetNextSender(messageSender);

            MessageDispatcher = componentMessageSender;
        }


        private void SetAsReceiver(ComponentManagerMessageReceiver messageReceiver)
        {
            var componentMessageReceiver = new ComponentMessageReceiver();

            messageReceiver.RegisterMessageReceiver(componentMessageReceiver);

            MessageDispatcher = componentMessageReceiver;
        }


        public IMessageDispatcher MessageDispatcher { get; set; }


        public ICommand CreateComponentCommand { get; }
        public ICommand DuplicateComponentCommand { get; }
        public ICommand DeleteComponentCommand { get; }
        public ICommand RenameComponentCommand { get; }


        public ObservableCollection<Component> Components { get; set; }


        private Component _selectedComponent;
        public Component SelectedComponent
        {
            get => _selectedComponent;
            set => SetAndNotify(ref _selectedComponent, value);
        }


        public void RenameComponent(Component component) => SelectedComponent.IsRenaming = true;


        public void CreateComponent(Component component)
        {
            var newComponent = component.ComponentFactory.CreateComponent(this.MessageDispatcher);
            component.AddComponent(newComponent);

            SendMessageAddComponent(component, newComponent);
        }


        public event Action<IMessage> MessageOutNotification;


        public void SendMessageAddComponent(Component component, Component newComponent)
        {
            var handler = MessageOutNotification;
            if (handler != null)
            {
                handler(new MessageAddComponent(component.ID, newComponent.GetModel() as IComponentModel));
            }
        }


        public void DeleteComponent(Component component)
        {
            var selectedParent = GetSelectedParent(Components);
            int index = selectedParent.Components.IndexOf(component);
            GetSelectedParent(Components).RemoveComponent(component);
            component.Dispose();
            //this.MessageDispatcher.SendMessage(new MessageRemoveComponent(selectedParent.ID, index));
        }


        public void InsertComponent(int index, Component parentComponent, Component componentToInsert)
        {
            parentComponent.InsertComponent(index, componentToInsert);
        }

        public Component DuplicateComponent(Component component)
        {
            Component result = null;
            // = GetSelectedParent(Components);
            return result;
        }


        public void DeleteSelectedComponent(ObservableCollection<Component> components)
        {
            foreach (Component component in components)
            {
                if (component.IsSelected)
                {
                    components.Remove(component);
                    break;
                }
                else
                    DeleteSelectedComponent(component.Components);
            }
        }


        public Component GetSelectedParent(ObservableCollection<Component> components)
        {
            Component result = null;
            foreach (Component component in components)
            {
                if(component.Components.Any(c => c.IsSelected))
                {
                    result = component;
                    break;
                }
                else
                {
                    result = GetSelectedParent(component.Components);
                }
            }
            return result;
        }
    }
}
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public class ComponentManager : ViewModel, IMessageCommunicator
    {
        public ComponentManager(ObservableCollection<Component> components)
        {
            Components = components;

            CreateComponentCommand = new RelayCommand(p => CreateComponent(p as Component));
            DuplicateComponentCommand = new RelayCommand(p => DuplicateComponent(p as Component));
            DeleteComponentCommand = new RelayCommand(p => DeleteComponent(p as Component));
            RenameComponentCommand = new RelayCommand(p => RenameComponent(p as Component));
        }


        public void SetSender(IMessageSender messageSender)
        {
            MessageSender = new ComponentMessageSender();
            MessageSender.SetSender(messageSender);
        }


        public void SetReceiver(IMessageReceiver messageReceiver)
        {
            MessageReceiver = new ComponentMessageReceiver();
            messageReceiver.RegisterReceiver(this);
        }


        public void ReceiveMessage(IMessage message)
        {
            MessageReceiver.ReceiveMessage(message);
        }


        public ComponentMessageSender MessageSender { get; set; }
        public ComponentMessageReceiver MessageReceiver { get; set; }


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

        public Guid ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void RenameComponent(Component component) => SelectedComponent.IsRenaming = true;


        public void CreateComponent(Component component)
        {
            var newComponent = component.ComponentFactory.CreateComponent();
            component.AddComponent(newComponent);

            if (MessageReceiver != null)
                newComponent.SetReceiver(MessageReceiver);

            if(MessageSender != null)
                newComponent.SetSender(MessageSender);

            MessageSender?.SendMessageAddComponent(component, newComponent);
        }


        public void DeleteComponent(Component component)
        {
            var selectedParent = GetSelectedParent(Components);
            int index = selectedParent.Components.IndexOf(component);
            GetSelectedParent(Components).RemoveComponent(component);
            component.Dispose();

            MessageSender?.SendMessageRemoveComponent(selectedParent, index);
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
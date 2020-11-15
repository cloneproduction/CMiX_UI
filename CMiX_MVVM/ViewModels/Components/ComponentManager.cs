﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
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
            component.CreateAndAddChild();
        }


        public Component DuplicateComponent(Component component)
        {
            Component result = null;
            var parent = GetSelectedParent(Components);

            //if (component is Composition)
            //    result = DuplicateComposition(component as Composition);
            //else if (component is Layer)
            //    result = DuplicateLayer(component as Layer);
            //else if (component is Entity)
            //    result = DuplicateEntity(component as Entity);

            return result;
        }


        public void DeleteComponent(Component component)
        {
            var selectedParent = GetSelectedParent(Components);
            selectedParent.RemoveComponent(component);
            selectedParent.Send(new Message(MessageDirection.OUT, selectedParent.Address, MessageSerializer.Serializer.Serialize(selectedParent.GetModel())));
        }

        public event EventHandler<ComponentEventArgs> ComponentDeletedEvent;

        public void OnComponentDeleted(Component component)
        {
            ComponentDeletedEvent?.Invoke(this, new ComponentEventArgs(component));
        }


        public void DeleteSelectedComponent(ObservableCollection<Component> components)
        {
            foreach (var component in components)
            {
                if (component.IsSelected)
                {
                    components.Remove(component);
                    OnComponentDeleted(component);
                    break;
                }
                else
                    DeleteSelectedComponent(component.Components);
            }
        }



        public Component GetSelectedParent(ObservableCollection<Component> components)
        {
            Component result = null;
            foreach (var component in components)
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


        //private Composition DuplicateComposition(Composition composition)
        //{
        //    var parent = GetSelectedParent(Components);
        //    var newCompo = ComponentFactory.CreateComponent(parent) as Composition;

        //    newCompo.SetViewModel(composition.GetModel());
        //    newCompo.Name += " -Copy";
        //    parent.Components.Insert(parent.Components.IndexOf(composition) + 1, newCompo);
            
        //    return newCompo;
        //}


        //private Component DuplicateLayer(Layer layer)
        //{
        //    var parent = GetSelectedParent(Components);
        //    Component newLayer = ComponentFactory.CreateComponent(parent);
        //    newLayer.SetViewModel(layer.GetModel());
        //    newLayer.Name += " -Copy";
        //    parent.Components.Insert(parent.Components.IndexOf(layer) + 1, newLayer);
        //    return newLayer;
        //}


        //private Entity DuplicateEntity(Entity entity)
        //{
        //    var parent = GetSelectedParent(Components);
        //    var component = ComponentFactory.CreateComponent(parent) as Entity;
        //    component.SetViewModel(entity.GetModel());
        //    component.Name += " -Copy";
        //    parent.Components.Insert(parent.Components.IndexOf(entity) + 1, component);
        //    return component;
        //}
    }
}

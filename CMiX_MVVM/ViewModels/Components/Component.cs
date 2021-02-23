using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Component : ViewModel, IMessageProcessor, IDisposable
    {
        public Component(int id, MessageTerminal MessageTerminal)
        {
            ID = id;
            IsExpanded = false;
            Name = $"{this.GetType().Name}{ID}";
            MessageDispatcher = new MessageDispatcher(MessageTerminal, this);
            Components = new ObservableCollection<Component>();
        }

        public MessageDispatcher MessageDispatcher { get; set; }
        public IComponentFactory ComponentFactory { get; set; }


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






        //public bool IsVisible => LayerIsVisible && Parent?.IsVisible ?? true;


        //bool _LayerIsVisible;
        //public bool LayerIsVisible 
        //{ 
        //    get => _LayerIsVisible;
        //    set => SetIsVisible(value);
        //}

        //void SetIsVisible(bool value)
        //{
        //    if (value == _LayerIsVisible) 
        //        return;

        //    _LayerIsVisible = value;
        //    //PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(LayerIsVisible));
        //    //PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible));
        //}


        public void SetVisibility(bool visibility)
        {
            foreach (var component in this.Components)
            {
                component.ParentIsVisible = visibility;
                component.IsVisible = visibility;

                component.Notify(nameof(IsVisible));
            }
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if(ParentIsVisible)
                    SetAndNotify(ref _isVisible, value);
                SetVisibility(this.IsVisible);
                Console.WriteLine(this.GetAddress() + " IsVisible " + IsVisible);
            }
        }

        private bool _parentIsVisible = true;
        public bool ParentIsVisible
        {
            get { return _parentIsVisible; }
            set
            {
                _parentIsVisible = value;
                if(value == false)
                    SetAndNotify(ref _isVisible, value);
                Console.WriteLine(this.GetAddress() + " ParentIsVisible " + ParentIsVisible);
            }
        }

        //private bool _componentVisibility;

        //public bool ComponentVisibility
        //{
        //    get { return _componentVisibility; }
        //    set 
        //    { 
        //        _componentVisibility = value; 
        //    }
        //}




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
            SetVisibility(this.IsVisible);
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
            MessageDispatcher.Dispose();
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
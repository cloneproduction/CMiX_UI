﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models.Component;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Visibility : Control
    {
        public Visibility(VisibilityModel visibilityModel)
        {
            IsVisible = true;
            ParentIsVisible = true;
            SetVisibilityCommand = new RelayCommand(p => SetVisibility(p as Component));
        }

        public Visibility(Visibility parentVisibility, VisibilityModel visibilityModel)
        {
            IsVisible = true;
            ParentIsVisible = parentVisibility.ParentIsVisible && parentVisibility.IsVisible;
            SetVisibilityCommand = new RelayCommand(p => SetVisibility(p as Component));
        }

        //public override void SetReceiver(IMessageReceiver messageReceiver)
        //{
        //    //messageReceiver?.RegisterReceiver(this, ID);
        //}

        //public Visibility(MessageDispatcher messageDispatcher, Visibility parentVisibility, VisibilityModel visibilityModel) : base (messageDispatcher, visibilityModel)
        //{
        //    IsVisible = visibilityModel.IsVisible;
        //    ParentIsVisible = parentVisibility.ParentIsVisible && parentVisibility.IsVisible;
        //    SetVisibilityCommand = new RelayCommand(p => SetVisibility(p as Component));
        //}

        public ICommand SetVisibilityCommand { get; set; }


        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetAndNotify(ref _isVisible, value);
                MessageSender?.SendMessage(new MessageUpdateViewModel(this.GetModel()));
                System.Console.WriteLine("Visibility Is " + IsVisible);
            } 
        }

        private bool _parentIsVisible;
        public bool ParentIsVisible
        {
            get => _parentIsVisible;
            set => SetAndNotify(ref _parentIsVisible, value);
        }

        public void SetVisibility(Component component)
        {
            if (!IsVisible)
                IsVisible = true;
            else
                IsVisible = false;
            
            if (ParentIsVisible)
                this.SetChildVisibility(component, this.IsVisible);
        }


        public void SetChildVisibility(Component component, bool parentVisibility)
        {
            foreach (var childComponent in component.Components)
            {
                if (childComponent.Visibility.IsVisible)
                {
                    childComponent.Visibility.SetChildVisibility(childComponent, parentVisibility);
                }
                childComponent.Visibility.ParentIsVisible = parentVisibility;
            }
        }

        public override IModel GetModel()
        {
            VisibilityModel visibilityModel = new VisibilityModel();
            visibilityModel.ID = this.ID;
            visibilityModel.IsVisible = this.IsVisible;
            return visibilityModel;
        }

        public override void SetViewModel(IModel model)
        {
            var visibilityModel = model as VisibilityModel;
            this.IsVisible = visibilityModel.IsVisible;
        }
    }
}

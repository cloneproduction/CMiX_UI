using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models.Component;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Visibility : MessageCommunicator, IMessageProcessor
    {
        public Visibility(IMessageProcessor parentSender) : base (parentSender)
        {
            IsVisible = true;
            ParentIsVisible = true;
            SetVisibilityCommand = new RelayCommand(p => SetVisibility(p as Component));
        }

        public Visibility(IMessageProcessor parentSender, Visibility parentVisibility) : base (parentSender)
        {
            IsVisible = true;
            ParentIsVisible = parentVisibility.ParentIsVisible && parentVisibility.IsVisible;
            SetVisibilityCommand = new RelayCommand(p => SetVisibility(p as Component));
        }

        public Visibility(IMessageProcessor parentSender, Visibility parentVisibility, VisibilityModel visibilityModel) : base(parentSender)
        {
            IsVisible = visibilityModel.IsVisible;
            ParentIsVisible = parentVisibility.ParentIsVisible && parentVisibility.IsVisible;
            SetVisibilityCommand = new RelayCommand(p => SetVisibility(p as Component));
        }

        public ICommand SetVisibilityCommand { get; set; }


        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetAndNotify(ref _isVisible, value);
                this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
            } 
        }

        private bool _parentIsVisible;
        public bool ParentIsVisible
        {
            get => _parentIsVisible;
            set
            {
                SetAndNotify(ref _parentIsVisible, value);
                this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
            }
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
            return null;
        }

        public override void SetViewModel(IModel model)
        {
            //throw new NotImplementedException();
        }
    }
}

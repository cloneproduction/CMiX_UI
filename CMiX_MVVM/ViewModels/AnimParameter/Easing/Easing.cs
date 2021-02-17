using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Easing : Sender
    {
        public Easing(string name, IMessageProcessor parentSender) : base (name, parentSender)
        {
            EasingMode = EasingMode.EaseIn;
            EasingFunction = EasingFunction.None;
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                SetAndNotify(ref _isEnabled, value);
                this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
            }
        }

        private EasingFunction _easingFunction;
        public EasingFunction EasingFunction
        {
            get => _easingFunction;
            set
            {
                SetAndNotify(ref _easingFunction, value);
                SetEasing();
            }
        }

        private EasingMode _easingMode;
        public EasingMode EasingMode
        {
            get => _easingMode;
            set
            {
                SetAndNotify(ref _easingMode, value);
                SetEasing();
            }
        }

        private Easings.Functions _selectedEasing;
        public Easings.Functions SelectedEasing
        {
            get => _selectedEasing;
            set => _selectedEasing = value;
        }

        private void SetEasing()
        {
            Easings.Functions myStatus;

            if (EasingFunction == EasingFunction.None)
                Enum.TryParse(EasingFunction.ToString(), out myStatus);
            else
                Enum.TryParse(EasingFunction.ToString() + EasingMode.ToString(), out myStatus);

            SelectedEasing = myStatus;
            this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
        }

        public override void SetViewModel(IModel model)
        {
            EasingModel easingModel = model as EasingModel;
            this.IsEnabled = easingModel.IsEnabled;
            this.EasingFunction = easingModel.EasingFunction;
            this.EasingMode = easingModel.EasingMode;
            this.SelectedEasing = easingModel.SelectedEasing;
        }

        public override IModel GetModel()
        {
            EasingModel model = new EasingModel();
            model.IsEnabled = this.IsEnabled;
            model.EasingFunction = this.EasingFunction;
            model.EasingMode = this.EasingMode;
            model.SelectedEasing = this.SelectedEasing;
            return model;
        }
    }
}
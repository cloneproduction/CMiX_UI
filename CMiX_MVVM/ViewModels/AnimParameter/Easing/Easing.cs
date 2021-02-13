using System;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class Easing : Sender
    {
        public Easing(string name, IMessageProcessor parentSender) : base (name, parentSender)
        {
            EasingMode = EasingMode.EaseIn;
            EasingFunction = EasingFunction.None;
        }

        public override void Receive(IMessage message)
        {
            this.SetViewModel(message.Obj as EasingModel);
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                SetAndNotify(ref _isEnabled, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
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
            this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
        }
    }
}
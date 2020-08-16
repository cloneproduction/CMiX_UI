using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Easing : Sendable
    {
        public Easing()
        {
            EasingFunction = EasingFunction.None;
            EasingMode = EasingMode.EaseIn;
        }

        public Easing(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            
        }

        private EasingFunction _easingFunction;
        public EasingFunction EasingFunction
        {
            get => _easingFunction;
            set => SetAndNotify(ref _easingFunction, value);
        }

        private EasingMode _easingMode;
        public EasingMode EasingMode
        {
            get => _easingMode;
            set => SetAndNotify(ref _easingMode, value);
        }
    }
}

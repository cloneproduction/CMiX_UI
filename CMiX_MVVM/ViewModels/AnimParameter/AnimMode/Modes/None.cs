using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class None : Sendable, IAnimMode
    {
        public None()
        {

        }

        public None(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }
    }
}
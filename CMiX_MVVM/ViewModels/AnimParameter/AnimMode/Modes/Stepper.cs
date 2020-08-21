using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : Sendable, IAnimMode 
    {
        public Stepper()
        {

        }

        public Stepper(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }
    }
}
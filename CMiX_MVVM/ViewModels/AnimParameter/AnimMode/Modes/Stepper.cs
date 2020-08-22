using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : Sendable, IAnimMode 
    {
        public Stepper()
        {

        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }

        public Stepper(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }
    }
}
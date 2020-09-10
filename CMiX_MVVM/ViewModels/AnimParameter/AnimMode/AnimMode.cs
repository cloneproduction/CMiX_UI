using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public abstract class AnimMode : Sendable
    {
        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as IAnimModeModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }


        //public abstract double[] UpdatePeriod(double period);
        public abstract void UpdateOnBeatTick(AnimParameter animParameter, double period);
        public abstract void UpdateParameters(AnimParameter animParameter, double period);
    }
}
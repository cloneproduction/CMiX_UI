using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public abstract void UpdateOnBeatTick(double period);

        public abstract double UpdatePeriod(double period, AnimParameter animParameter);
    }
}

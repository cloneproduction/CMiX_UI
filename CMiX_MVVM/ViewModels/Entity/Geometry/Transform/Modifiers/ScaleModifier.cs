using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class ScaleModifier : Sender, IModifier
    {
        public ScaleModifier(string name, IColleague parentSender) : base(name, parentSender)
        {

        }

        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }
    }
}

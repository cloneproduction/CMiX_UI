using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class TransformModifier : Sender
    {
        public TransformModifier(string name, IColleague parentSender, Transform transform) : base(name, parentSender)
        {

        }

        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }

        private XYZModifier _translateModifier;

        public XYZModifier TranslateModifier
        {
            get => _translateModifier;
            set
            {
                SetAndNotify(ref _translateModifier, value);
                //this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }
    }
}

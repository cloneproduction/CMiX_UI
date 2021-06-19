using CMiX.Core.Presentation.ViewModels;
using System;
using System.Collections.Generic;

namespace CMiX.Core.MessageService
{
    public class MessageRemoveTransformModifier : Message
    {
        public MessageRemoveTransformModifier(int index)
        {
            Index = index;
        }

        public int Index { get; set; }

        public override void Process<T>(T receiver)
        {
            ITransformModifier transformModifier = receiver as ITransformModifier;
        }
    }
}
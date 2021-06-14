﻿using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.MessageService
{
    public class MessageAddTransformModifier : Message, ITransformModifierMessage
    {
        public MessageAddTransformModifier()
        {

        }

        public MessageAddTransformModifier(ITransformModifierModel transformModifierModel)
        {
            TransformModifierModel = transformModifierModel;
        }

        public ITransformModifierModel TransformModifierModel { get; set; }

        public override void Process<T>(T receiver)
        {
            ITransformModifier transformModifier = receiver as ITransformModifier;
        }
    }
}
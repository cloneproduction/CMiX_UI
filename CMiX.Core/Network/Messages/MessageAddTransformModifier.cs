﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Network.Messages
{
    public class MessageAddTransformModifier : Message
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
            Instancer instancer = receiver as Instancer;
            var transformModifier = instancer.Factory.CreateTransformModifier(TransformModifierModel);
            instancer.AddTransformModifier(transformModifier);
        }
    }
}
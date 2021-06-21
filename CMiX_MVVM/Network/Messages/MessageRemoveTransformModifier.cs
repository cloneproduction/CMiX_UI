// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Network.Messages
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
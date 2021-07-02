// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.


using System;
using CMiX.Core.Models;
using MediatR;

namespace CMiX.Core.Presentation.Mediator
{
    public class ReceiveAddNewComponentNotification : INotification
    {
        public ReceiveAddNewComponentNotification(Guid id, IComponentModel componentModel)
        {
            ComponentModel = componentModel;
            ID = id;
        }

        public Guid ID { get; }
        public IComponentModel ComponentModel { get; }
    }
}

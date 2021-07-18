﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.


using System;
using CMiX.Core.Presentation.ViewModels.Components;
using MediatR;

namespace CMiX.Core.Presentation.Mediator
{
    public class AddNewComponentNotification : INotification
    {
        public AddNewComponentNotification(Guid id, Component component)
        {
            Component = component;
            ID = id;
        }

        public Guid ID { get; }
        public Component Component { get; }
    }
}
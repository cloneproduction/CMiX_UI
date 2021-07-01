// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.


using CMiX.Core.Presentation.ViewModels.Components;
using MediatR;

namespace CMiX.Core.Presentation.Mediator
{
    public class AddNewComponentNotification : INotification
    {
        public AddNewComponentNotification(Component component)
        {
            Component = component;
        }

        public Component Component { get; set; }
    }
}

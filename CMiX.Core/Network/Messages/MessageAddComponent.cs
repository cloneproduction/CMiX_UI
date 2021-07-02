// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using CMiX.Core.Models;
using CMiX.Core.Presentation.Mediator;
using CMiX.Core.Presentation.ViewModels.Components;
using MediatR;

namespace CMiX.Core.Network.Messages
{
    public class MessageAddComponent : Message
    {
        public MessageAddComponent()
        {

        }

        public MessageAddComponent(Component component)
        {
            ComponentModel = component.GetModel() as IComponentModel;
            IDs = new List<Guid>();
            //IDs.Add(id);
        }

        public IComponentModel ComponentModel { get; set; }

        public async void ReceiveWithMediator(IMediator mediator)
        {
            await mediator.Publish(new ReceiveAddNewComponentNotification(IDs.First(), ComponentModel));
        }

        public override void Process<T>(T receiver)
        {
            var component = receiver as Component;
            Component newComponent = component.ComponentFactory.CreateComponent(ComponentModel);
            component.AddComponent(newComponent);
            newComponent.SetCommunicator(component.Communicator);
            Console.WriteLine("ReceiveMessageAddComponent Count is " + component.Components.Count);
        }
    }
}

﻿using CMiX.Core.Interfaces;
using CMiX.Core.Presentation.ViewModels.Components;
using System;

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
        }

        public IComponentModel ComponentModel { get; set; } // must be public because of Ceras...

        public override void Process<T>(T receiver)
        {
            var component = receiver as Component;
            Component newComponent = component.ComponentFactory.CreateComponent(ComponentModel);
            component.AddComponent(newComponent);
            Console.WriteLine("ReceiveMessageAddComponent Count is " + component.Components.Count);
        }
    }
}
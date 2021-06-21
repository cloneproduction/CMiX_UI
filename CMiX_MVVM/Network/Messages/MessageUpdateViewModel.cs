﻿using CMiX.Core.Interfaces;
using CMiX.Core.Presentation.ViewModels;
using System;

namespace CMiX.Core.Network.Messages
{
    public class MessageUpdateViewModel : Message
    {
        public MessageUpdateViewModel()
        {

        }

        public MessageUpdateViewModel(IModel model)
        {
            Model = model;
        }

        public MessageUpdateViewModel(IControl control)
        {
            Model = control.GetModel();
        }

        public IModel Model { get; set; }

        public override void Process<T>(T receiver)
        {
            Console.WriteLine("MessageUpdateViewModel ProcessMessage");
            var control = receiver as IControl;
            control.SetViewModel(Model);
        }
    }
}
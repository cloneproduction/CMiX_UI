﻿using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Network.Communicators
{
    public class ControlCommunicator : Communicator
    {
        public ControlCommunicator(IControl iDObject) : base()
        {
            IIDObject = iDObject;
            System.Console.WriteLine("Control COmmunicator Created for " + iDObject.GetType() + " " + iDObject.ID);
        }

        public void SendMessageUpdateViewModel(IControl control)
        {
            var message = new MessageUpdateViewModel(control);
            this.SendMessage(message);
        }
    }
}
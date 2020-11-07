﻿using System;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Project Project = ComponentFactory.CreateComponent() as Project;
            var receiver = new Receiver();
            Project.Receiver = receiver;
            Settings settings = new Settings("Pouet", "Pouet", "192.168.0.192", 2222);
            receiver.SetSettings(settings);
            receiver.StartClient();
            //receiver.DataReceivedEvent += Project.OnParentReceiveChange;
            
            Console.ReadLine();
        }
    }
}

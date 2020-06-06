using System;
using System.ComponentModel;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Project Project = ComponentFactory.CreateProject();
            var receiver = new Receiver();
            Project.Receiver = receiver;
            Settings settings = new Settings("Pouet", "Pouet", "192.168.1.4", 2222);
            receiver.SetSettings(settings);
            receiver.StartClient();
            receiver.DataReceivedEvent += Project.OnParentReceiveChange;
            Console.ReadLine();
        }

    }
}

using System;
using CMiX.Studio.ViewModels;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var project = ComponentFactory.CreateProject();
            var receiver = new Receiver();

            Settings settings = new Settings("Pouet", "Pouet", "192.168.1.4", 2222);
            receiver.SetSettings(settings);
            receiver.StartClient();
            project.Receiver = receiver;

            Console.ReadLine();
        }
    }
}

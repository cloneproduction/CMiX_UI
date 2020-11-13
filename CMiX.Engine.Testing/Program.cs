using System;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Project Project = ComponentFactory.CreateComponent() as Project;

            Settings settings = new Settings("Pouet", "Pouet", "192.168.0.192", 2222);
            Receiver receiver = new Receiver();
            receiver.SetSettings(settings);
            receiver.StartClient();

            Console.ReadLine();
        }
    }
}

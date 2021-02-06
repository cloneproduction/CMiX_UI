using System;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings("Pouet", "Pouet", "192.168.1.5", 2222);
            MessengerTerminal messengerTerminal = new MessengerTerminal();
            messengerTerminal.StartReceiver(settings);
            Project Project = new Project(0, messengerTerminal);
            Console.ReadLine();
        }
    }
}
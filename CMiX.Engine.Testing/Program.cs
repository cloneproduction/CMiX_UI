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
            Settings settings = new Settings("Pouet", "Pouet", "192.168.1.4", 2222);
            MessageTerminal MessageTerminal = new MessageTerminal();
            MessageTerminal.StartReceiver(settings);
            Project Project = new Project(0, MessageTerminal);
            Console.ReadLine();
        }
    }
}
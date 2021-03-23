using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings("Pouet", "Pouet", "192.168.1.4", 2222);
            MessageTerminal MessageTerminal = new MessageTerminal();
            MessageTerminal.StartReceiver(settings);

            var messageDispatcher = new MessageDispatcher(MessageTerminal);
            var projectModel = new ProjectModel(0);

            Project Project = new Project(messageDispatcher, projectModel);
            Console.ReadLine();
        }
    }
}
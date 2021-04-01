using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings("Pouet", "Pouet", "192.168.0.192", 2222);

            MessageDispatcher messageDispatcher = new MessageDispatcher();
            var messageReceiver = new MessageReceiver(messageDispatcher);
            messageReceiver.Start(settings);

            var projectModel = new ProjectModel(Guid.Empty);

            Project Project = new Project(projectModel, messageDispatcher);
            messageReceiver.MessageDispatcher.RegisterMessageProcessor(Project);
            Console.ReadLine();
        }
    }
}
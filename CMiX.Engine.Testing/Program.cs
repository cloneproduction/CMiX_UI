using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Components;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings("Pouet", "Pouet", "192.168.0.192", 2222);

            var messageReceiver = new MessageReceiver();
            messageReceiver.Start(settings);

            var projectModel = new ProjectModel(0);
            
            Project Project = new Project(projectModel);
            messageReceiver.RegisterMessageProcessor(Project);
            Console.ReadLine();
        }
    }
}
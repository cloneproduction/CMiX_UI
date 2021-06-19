using CMiX.Core.MessageService;
using CMiX.Core.Models;
using CMiX.Core.Presentation.ViewModels;
using CMiX.Core.Presentation.ViewModels.Components;
using System;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings("Pouet", "Pouet", "192.168.1.4", 2222);

            Guid g = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
            var projectModel = new ProjectModel(g);
            Project Project = new Project(projectModel);

            var dataReceiver = new DataReceiver();

            dataReceiver.Start(settings);

            var componentCommunicator = new ComponentCommunicator(Project);
            //componentCommunicator.MessageReceiver = messageReceiver;
            Project.SetCommunicator(componentCommunicator);
            dataReceiver.RegisterReceiver(componentCommunicator);

            Console.ReadLine();
        }
    }
}
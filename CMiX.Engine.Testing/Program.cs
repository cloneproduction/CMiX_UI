using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.ViewModels.Components;
using System;
using System.Collections.ObjectModel;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings("Pouet", "Pouet", "192.168.1.4", 2222);

            Guid g = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
            var projectModel = new ProjectModel(g);

            MessageReceiver messageReceiver = new MessageReceiver();

            var dataReceiver = new DataReceiver();
            dataReceiver.RegisterReceiver(messageReceiver);
            dataReceiver.Start(settings);

            Project Project = new Project(projectModel);
            Project.SetReceiver(messageReceiver);

            Console.ReadLine();
        }
    }
}
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using CMiX.Studio.ViewModels.MessageService;
using System;
using System.Collections.ObjectModel;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings("Pouet", "Pouet", "192.168.1.3", 2222);

            var projectModel = new ProjectModel(Guid.Empty);

            ComponentReceiver componentReceiver = new ComponentReceiver();

            var dataReceiver = new DataReceiver();
            dataReceiver.RegisterReceiver(componentReceiver);
            dataReceiver.Start(settings);

            Project Project = new Project(projectModel);
            Project.SetReceiver(componentReceiver);

            var projects = new ObservableCollection<Component>();
            projects.Add(Project);

            Console.ReadLine();
        }
    }
}
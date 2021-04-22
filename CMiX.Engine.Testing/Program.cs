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
            Settings settings = new Settings("Pouet", "Pouet", "192.168.0.192", 2222);

            var projectModel = new ProjectModel(Guid.Empty);

            ComponentManagerMessageReceiver componentManagerMessageReceiver = new ComponentManagerMessageReceiver();
            

            var dataReceiver = new DataReceiver();
            dataReceiver.RegisterReceiver(componentManagerMessageReceiver);
            dataReceiver.Start(settings);

            Project Project = new Project(projectModel);

            var projects = new ObservableCollection<Component>();
            projects.Add(Project);



            ComponentManager componentManager = new ComponentManager(projects);
            componentManager.SetReceiver(componentManagerMessageReceiver);

            componentManagerMessageReceiver.RegisterReceiver(componentManager);

            Project.SetReceiver(componentManager.MessageReceiver);


            Console.ReadLine();
        }
    }
}
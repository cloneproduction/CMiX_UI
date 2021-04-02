using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;
using System;
using System.Collections.ObjectModel;

namespace CMiX.Engine.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings("Pouet", "Pouet", "192.168.1.4", 2222);



            var projectModel = new ProjectModel(Guid.Empty);

            var messageDispatcher = new MessageDispatcher();
            var messageReceiver = new MessageReceiver(messageDispatcher);
            messageReceiver.Start(settings);

            Project Project = new Project(projectModel);
            messageReceiver.MessageDispatcher.RegisterMessageProcessor(Project);
            
            var projects = new ObservableCollection<Component>();
            projects.Add(Project);
            ComponentManager componentManager = new ComponentManager(projects, messageDispatcher);



            Console.ReadLine();
        }
    }
}
using CMiX.Core.MessageService;
using CMiX.Core.Models;
using CMiX.Core.Presentation.Mediator;
using CMiX.Core.Presentation.ViewModels;
using CMiX.Core.Presentation.ViewModels.Components;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CMiX.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var componentDatabase = serviceProvider.GetService<IComponentDatabase>();

            Settings settings = new Settings("Pouet", "Pouet", "192.168.0.192", 2222);

            var projectModel = new ProjectModel();
            Project Project = new Project(projectModel, null);

            componentDatabase.AddComponent(Project);

            var componentCommunicator = new ComponentCommunicator(Project);
            Project.SetCommunicator(componentCommunicator);


            var dataReceiver = new DataReceiver(serviceProvider.GetService<IMediator>());
            dataReceiver.Start(settings);
            dataReceiver.RegisterReceiver(componentCommunicator);

            System.Console.ReadLine();
        }


        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IComponentDatabase, ComponentDatabase>();
            services.AddSingleton<IMediator, Mediator>();
            services.AddMediatR(typeof(AddNewComponentNotification));
        }
    }
}

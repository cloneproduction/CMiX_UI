using CMiX.Core.Presentation.Mediator;
using CMiX.Core.Presentation.ViewModels;
using CMiX.Core.Presentation.ViewModels.Components;
using CMiX.Core.Presentation.ViewModels.Network;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MvvmDialogs;
using System;
using System.Windows;

namespace CMiX
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IComponentDatabase, ComponentDatabase>();
            services.AddSingleton<IMediator, Mediator>();
            services.AddSingleton(typeof(MessengerManager));
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IMessageSender, MessageSender>();
            services.AddTransient<IProject, Project>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();

            services.AddMediatR(typeof(AddNewComponentNotification));

            //services.AddSingleton(typeof(IServiceProvider));

            var provider = services.BuildServiceProvider();
        }
    }
}

using System.Collections.ObjectModel;
using Memento;
using Ceras;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using MvvmDialogs;

namespace CMiX.Studio.ViewModels
{
    public class Project : Component, IGetSet<ProjectModel>
    {
        public Project(IDialogService dialogService)
        {
            DialogService = dialogService;
            MessageAddress = $"{nameof(Project)}/";
            Name = "Project";
            
            Mementor = new Mementor();
            MessageService = new MessageService();
            Beat = new MasterBeat(MessageService);

            Servers = new ObservableCollection<Server>();
            ServerManager = new ServerManager();
            Components = new ObservableCollection<Component>();
            Assets = new ObservableCollection<IAssets>();
            AssetManager = new AssetManager(dialogService, Assets);
        }

        #region PROPERTIES
        private readonly IDialogService DialogService;

        public ObservableCollection<Server> Servers { get; set; }
        public ServerManager ServerManager { get; set; }
        
        public ObservableCollection<IAssets> Assets { get; set; }
        public AssetManager AssetManager { get; set; }
        #endregion


        #region GETSETMODEL
        public ProjectModel GetModel()
        {
            ProjectModel projectModel = new ProjectModel();
            projectModel.ID = ID;
            projectModel.MessageAddress = MessageAddress;
            projectModel.Name = Name;
            projectModel.IsVisible = IsVisible;
            projectModel.AssetManagerModel = AssetManager.GetModel();

            foreach (IGetSet<CompositionModel> item in Components)
            {
                projectModel.ComponentModels.Add(item.GetModel());
            }

            return projectModel;
        }

        public void SetViewModel(ProjectModel projectModel)
        {
            ID = projectModel.ID;
            Name = projectModel.Name;
            IsVisible = projectModel.IsVisible;
            MessageAddress = projectModel.MessageAddress;

            

            Components.Clear();
            foreach (CompositionModel componentModel in projectModel.ComponentModels)
            {
                Composition composition = new Composition(0, this.MessageAddress, this.Beat, new MessageService(), this.Mementor);
                composition.SetViewModel(componentModel);
                this.AddComponent(composition);
            }

            AssetManager.SetViewModel(projectModel.AssetManagerModel);
        }
        #endregion
    }
}
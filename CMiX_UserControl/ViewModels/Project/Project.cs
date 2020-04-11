using System.Collections.ObjectModel;
using Memento;
using Ceras;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using MvvmDialogs;

namespace CMiX.Studio.ViewModels
{
    public class Project : Component, IUndoable
    {
        public Project( int id, string messageAddress, Beat beat, MessageService messageService, Mementor mementor, IDialogService dialogService)
            : base(id, beat, messageAddress, messageService, mementor)
        {
            DialogService = dialogService;
            MessageAddress = $"{nameof(Project)}/";
            Name = "Project";
            
            Mementor = new Mementor();
            MessageService = new MessageService();
            Beat = new MasterBeat(MessageService);

            Servers = new ObservableCollection<Server>();
            ServerManager = new ServerManager(Servers);

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

        public override void SetViewModel(IComponentModel componentModel)
        {
            var projectModel = componentModel as ProjectModel;

            ID = projectModel.ID;
            Name = projectModel.Name;
            IsVisible = projectModel.IsVisible;
            MessageAddress = projectModel.MessageAddress;

            Components.Clear();
            foreach (CompositionModel compositionModel in projectModel.ComponentModels)
            {
                Composition composition = new Composition(0, this.MessageAddress, this.Beat, new MessageService(), this.Mementor);
                composition.SetViewModel(compositionModel);
                this.AddComponent(composition);
            }

            AssetManager.SetViewModel(projectModel.AssetManagerModel);
        }
        #endregion
    }
}
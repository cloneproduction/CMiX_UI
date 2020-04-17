using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Services;
using MvvmDialogs;

namespace CMiX.Studio.ViewModels
{
    public class Project : Component, IUndoable
    {
        public Project(int id, string messageAddress, Beat beat, MessageService messageService, Mementor mementor, IDialogService dialogService)
            : base(id, beat, messageAddress, messageService, mementor)
        {
            ComponentsInEditing = new ObservableCollection<Component>();

            DialogService = dialogService;

            Servers = new ObservableCollection<Server>();
            ServerManager = new ServerManager(Servers);

            Assets = new ObservableCollection<IAssets>();
            //AssetManager = new AssetManager(dialogService, Assets);
        }

        #region PROPERTIES
        private readonly IDialogService DialogService;

        public ObservableCollection<Server> Servers { get; set; }
        public ServerManager ServerManager { get; set; }


        private ObservableCollection<Component> _componentsInEditing;
        public ObservableCollection<Component> ComponentsInEditing
        {
            get => _componentsInEditing;
            set => SetAndNotify(ref _componentsInEditing, value);
        }

        private ObservableCollection<IAssets> _assets;
        public ObservableCollection<IAssets> Assets
        {
            get => _assets;
            set => SetAndNotify(ref _assets, value);
        }

        //private AssetManager _assetManager;
        //public AssetManager AssetManager
        //{
        //    get => _assetManager;
        //    set => SetAndNotify(ref _assetManager, value);
        //}
        #endregion
    }
}
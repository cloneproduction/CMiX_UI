using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Services;
using MvvmDialogs;
using System.Windows.Input;

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
            AssetManager = new AssetManager(dialogService, Assets);
        }


        private readonly IDialogService DialogService;

        public ObservableCollection<Server> Servers { get; set; }
        public ServerManager ServerManager { get; set; }

        public ObservableCollection<Component> ComponentsInEditing { get; set; }

        public ObservableCollection<IAssets> Assets { get; set; }
        public AssetManager AssetManager { get; set; }
    }
}
using System.Collections.ObjectModel;
using MvvmDialogs;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Project : Component
    {
        public Project(int id, IDialogService dialogService) : base(id)
        {
            DialogService = dialogService;
            
            Assets = new ObservableCollection<IAssets>();

            ComponentsInEditing = new ObservableCollection<Component>();
            Messengers = new ObservableCollection<Messenger>();
        }

        public IDialogService DialogService { get; set; }

        private Receiver _receiver;
        public Receiver Receiver
        {
            get => _receiver;
            set => SetAndNotify(ref _receiver, value);
        }


        private ObservableCollection<Messenger> _messengers;
        public ObservableCollection<Messenger> Messengers
        {
            get => _messengers;
            set => SetAndNotify(ref _messengers, value);
        }

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
    }
}
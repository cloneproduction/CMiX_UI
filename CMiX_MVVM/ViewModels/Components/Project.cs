using System.Collections.ObjectModel;
using CMiX.Studio.ViewModels.MessageService;
using MvvmDialogs;

namespace CMiX.MVVM.ViewModels
{
    public class Project : Component
    {
        public Project(int id, MessengerManager messengerManager, IDialogService dialogService) : base(id, messengerManager)
        {
            DialogService = dialogService;    
            Assets = new ObservableCollection<IAssets>();
        }

        public IDialogService DialogService { get; set; }

        private ObservableCollection<IAssets> _assets;
        public ObservableCollection<IAssets> Assets
        {
            get => _assets;
            set => SetAndNotify(ref _assets, value);
        }
    }
}
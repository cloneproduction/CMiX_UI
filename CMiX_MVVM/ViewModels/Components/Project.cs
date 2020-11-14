using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels.Components.Factories;
using MvvmDialogs;

namespace CMiX.MVVM.ViewModels
{
    public class Project : Component
    {
        public Project(int id, IDialogService dialogService) : base(id)
        {
            DialogService = dialogService;    
            Assets = new ObservableCollection<IAssets>();
            Factory = new CompositionFactory();
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
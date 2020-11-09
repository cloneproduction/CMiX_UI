using System.Collections.ObjectModel;
using MvvmDialogs;
using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Project : Component
    {
        public Project(int id, IDialogService dialogService) : base(id)
        {
            DialogService = dialogService;    
            Assets = new ObservableCollection<IAssets>();
            Messengers = new ObservableCollection<Messenger>();
        }

        public IDialogService DialogService { get; set; }

        private ObservableCollection<Messenger> _messengers;
        public ObservableCollection<Messenger> Messengers
        {
            get => _messengers;
            set => SetAndNotify(ref _messengers, value);
        }

        private ObservableCollection<IAssets> _assets;
        public ObservableCollection<IAssets> Assets
        {
            get => _assets;
            set => SetAndNotify(ref _assets, value);
        }
    }
}
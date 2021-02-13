using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;
using MvvmDialogs;

namespace CMiX.MVVM.ViewModels
{
    public class Project : Component
    {
        public Project(int id, MessageTerminal MessageTerminal) : base (id, MessageTerminal)
        {
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());
            Assets = new ObservableCollection<IAssets>();
            ComponentFactory = new CompositionFactory(MessageTerminal);
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
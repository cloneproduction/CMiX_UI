using System.Collections.ObjectModel;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
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

        public override IModel GetModel()
        {
            ProjectModel model = new ProjectModel();

            model.Enabled = this.Enabled;
            model.ID = this.ID;
            model.Name = this.Name;
            model.IsVisible = this.IsVisible;

            GetComponents(this, model);

            foreach (IAssets asset in this.Assets)
                model.AssetModels.Add(asset.GetModel());

            return model;
        }

        public override void SetViewModel(IModel componentModel)
        {
            var projectModel = componentModel as ProjectModel;

            this.Components.Clear();
            foreach (CompositionModel compositionModel in projectModel.ComponentModels)
            {
                this.CreateAndAddComponent().SetViewModel(compositionModel);
            }

            this.Assets.Clear();
            foreach (IAssetModel assetModel in projectModel.AssetModels)
            {
                IAssets asset = null;
                if (assetModel is AssetDirectoryModel)
                    asset = new AssetDirectory();
                else if (assetModel is AssetTextureModel)
                    asset = new AssetTexture();
                else if (assetModel is AssetGeometryModel)
                    asset = new AssetGeometry();

                asset.SetViewModel(assetModel);
                this.Assets.Add(asset);
            }
        }
    }
}
// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Models.Component;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Presentation.ViewModels.Assets;
using CMiX.Core.Presentation.ViewModels.Components.Factories;
using CMiX.Core.Presentation.ViewModels.Scheduler;
using MvvmDialogs;
using System.Collections.ObjectModel;


namespace CMiX.Core.Presentation.ViewModels.Components
{
    public class Project : Component
    {
        public Project(ProjectModel projectModel) : base(projectModel)
        {
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());
            Assets = new ObservableCollection<Asset>();

            Visibility = new Visibility(new VisibilityModel());
            ComponentFactory = new CompositionFactory(this);
            Scheduler = new Scheduler.Scheduler();
        }


        public IDialogService DialogService { get; set; }

        public Scheduler.Scheduler Scheduler { get; set; }


        private ObservableCollection<Asset> _assets;
        public ObservableCollection<Asset> Assets
        {
            get => _assets;
            set => SetAndNotify(ref _assets, value);
        }


        public override void SetCommunicator(Communicator communicator)
        {
            Communicator.SetCommunicator(communicator);
        }

        public override void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public override IModel GetModel()
        {
            ProjectModel model = new ProjectModel(this.ID);

            model.Enabled = this.Enabled;
            model.Name = this.Name;
            //model.IsVisible = this.IsVisible;

            foreach (Component item in this.Components)
                model.ComponentModels.Add(item.GetModel() as IComponentModel);

            foreach (Asset asset in this.Assets)
                model.AssetModels.Add((IAssetModel)asset.GetModel());

            return model;
        }

        public override void SetViewModel(IModel componentModel)
        {
            var projectModel = componentModel as ProjectModel;

            this.Components.Clear();
            foreach (CompositionModel compositionModel in projectModel.ComponentModels)
            {
                var newComponent = this.ComponentFactory.CreateComponent(compositionModel);
                //newComponent.SetReceiver(MessageReceiver);
                //newComponent.SetSender(MessageSender);
                this.AddComponent(newComponent);
            }

            this.Assets.Clear();
            foreach (IAssetModel assetModel in projectModel.AssetModels)
            {
                Asset asset = null;
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

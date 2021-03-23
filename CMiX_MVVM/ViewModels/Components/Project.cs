﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Assets;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.Mediator;
using MvvmDialogs;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Project : Component
    {
        public Project(MessageDispatcher messageDispatcher, ProjectModel projectModel) : base (messageDispatcher, projectModel)
        {
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());
            Assets = new ObservableCollection<Asset>();
            
            
            Visibility = new Visibility(this);
            ComponentFactory = new CompositionFactory();
        }

        public IDialogService DialogService { get; set; }

        private ObservableCollection<Asset> _assets;
        public ObservableCollection<Asset> Assets
        {
            get => _assets;
            set => SetAndNotify(ref _assets, value);
        }

        public override IModel GetModel()
        {
            ProjectModel model = new ProjectModel(this.ID);

            model.Enabled = this.Enabled;
            model.Name = this.Name;
            //model.IsVisible = this.IsVisible;

            GetComponents(this, model);

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
                this.CreateAndAddComponent().SetViewModel(compositionModel);
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
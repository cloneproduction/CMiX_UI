﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Core.Models;
using CMiX.Core.Models.Component;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Presentation.ViewModels.Assets;
using CMiX.Core.Presentation.ViewModels.Components.Factories;
using CMiX.Core.Presentation.ViewModels.Scheduler;
using MvvmDialogs;


namespace CMiX.Core.Presentation.ViewModels.Components
{
    public class Project : Component
    {
        public Project(ProjectModel projectModel) : base(projectModel)
        {
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());
            Assets = new ObservableCollection<Asset>();

            CompositionSchedulers = new ObservableCollection<CompositionScheduler>();

            Visibility = new Visibility(new VisibilityModel());
            ComponentFactory = new CompositionFactory(this);

            CreateSchedulerCommand = new RelayCommand(p => CreateScheduler());
            DeleteSchedulerCommand = new RelayCommand(p => DeleteScheduler());
        }


        public ICommand CreateSchedulerCommand { get; set; }
        public ICommand DeleteSchedulerCommand { get; set; }




        private CompositionScheduler _selectedScheduler;
        public CompositionScheduler SelectedScheduler
        {
            get => _selectedScheduler;
            set => SetAndNotify(ref _selectedScheduler, value);
        }


        public ObservableCollection<CompositionScheduler> CompositionSchedulers { get; set; }


        public IDialogService DialogService { get; set; }

        private ObservableCollection<Asset> _assets;
        public ObservableCollection<Asset> Assets
        {
            get => _assets;
            set => SetAndNotify(ref _assets, value);
        }


        public void CreateScheduler()
        {
            CompositionScheduler compositionScheduler = new CompositionScheduler(this.Components);
            CompositionSchedulers.Add(compositionScheduler);
        }

        public void DeleteScheduler()
        {
            CompositionSchedulers.Remove(SelectedScheduler);
            SelectedScheduler = null;
        }


        public override void SetCommunicator(Communicator communicator)
        {
            Communicator = new ComponentCommunicator(this);
            Communicator.SetCommunicator(communicator);

            //CompositionScheduler.SetCommunicator(Communicator);
        }

        public override void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);

            //CompositionScheduler.UnsetCommunicator(Communicator);
        }





        public override IModel GetModel()
        {
            ProjectModel model = new ProjectModel();

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

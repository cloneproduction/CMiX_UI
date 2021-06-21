﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Presentation.ViewModels.Assets;
using CMiX.Core.Presentation.ViewModels.Beat;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class Geometry : ViewModel, IControl, ITransform
    {
        public Geometry(MasterBeat beat, GeometryModel geometryModel)
        {
            this.ID = geometryModel.ID;
            Instancer = new Instancer(beat, geometryModel.InstancerModel);
            Transform = new Transform(geometryModel.TransformModel);
            GeometryFX = new GeometryFX(geometryModel.GeometryFXModel);
            AssetPathSelector = new AssetPathSelector(new AssetGeometry(), geometryModel.AssetPathSelectorModel);
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }
        public AssetPathSelector AssetPathSelector { get; set; }
        public Transform Transform { get; set; }
        public Instancer Instancer { get; set; }
        public GeometryFX GeometryFX { get; set; }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);

            Instancer.SetCommunicator(Communicator);
            Transform.SetCommunicator(Communicator);
            GeometryFX.SetCommunicator(Communicator);
            AssetPathSelector.SetCommunicator(Communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);

            Instancer.UnsetCommunicator(Communicator);
            Transform.UnsetCommunicator(Communicator);
            GeometryFX.UnsetCommunicator(Communicator);
            AssetPathSelector.UnsetCommunicator(Communicator);
        }


        public IModel GetModel()
        {
            GeometryModel model = new GeometryModel();
            model.ID = this.ID;
            model.TransformModel = (TransformModel)this.Transform.GetModel();
            model.GeometryFXModel = (GeometryFXModel)this.GeometryFX.GetModel();
            model.InstancerModel = (InstancerModel)this.Instancer.GetModel();
            model.AssetPathSelectorModel = (AssetPathSelectorModel)this.AssetPathSelector.GetModel();
            return model;
        }

        public void SetViewModel(IModel model)
        {
            GeometryModel geometryModel = model as GeometryModel;
            this.ID = geometryModel.ID;
            this.Transform.SetViewModel(geometryModel.TransformModel);
            this.GeometryFX.SetViewModel(geometryModel.GeometryFXModel);
            this.Instancer.SetViewModel(geometryModel.InstancerModel);
            this.AssetPathSelector.SetViewModel(geometryModel.AssetPathSelectorModel);
        }
    }
}
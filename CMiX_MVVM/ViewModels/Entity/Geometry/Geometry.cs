﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Assets;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Geometry : MessageCommunicator, ITransform
    {
        public Geometry(IMessageProcessor parentSender, MasterBeat beat) : base (parentSender)
        {
            Instancer = new Instancer(nameof(Instancer), this, beat);
            Transform = new Transform(this);
            GeometryFX = new GeometryFX(this);
            AssetPathSelector = new AssetPathSelector(nameof(AssetPathSelector), this, new AssetGeometry());
        }

        public Geometry(IMessageProcessor parentSender, MasterBeat beat, GeometryModel geometryModel) : base (parentSender)
        {
            Instancer = new Instancer(nameof(Instancer), this, beat);
            Transform = new Transform(this);
            GeometryFX = new GeometryFX(this);
            AssetPathSelector = new AssetPathSelector(nameof(AssetPathSelector), this, new AssetGeometry());
        }

        public AssetPathSelector AssetPathSelector { get; set; }
        public Transform Transform { get; set; }
        public Instancer Instancer { get; set; }
        public GeometryFX GeometryFX { get; set; }


        public override IModel GetModel()
        {
            GeometryModel model = new GeometryModel();
            model.TransformModel = (TransformModel)this.Transform.GetModel();
            model.GeometryFXModel = (GeometryFXModel)this.GeometryFX.GetModel();
            model.InstancerModel = (InstancerModel)this.Instancer.GetModel();
            model.AssetPathSelectorModel = (AssetPathSelectorModel)this.AssetPathSelector.GetModel();
            return model;
        }

        public override void SetViewModel(IModel model)
        {
            GeometryModel geometryModel = model as GeometryModel;
            this.Transform.SetViewModel(geometryModel.TransformModel);
            this.GeometryFX.SetViewModel(geometryModel.GeometryFXModel);
            this.Instancer.SetViewModel(geometryModel.InstancerModel);
            this.AssetPathSelector.SetViewModel(geometryModel.AssetPathSelectorModel);
        }
    }
}
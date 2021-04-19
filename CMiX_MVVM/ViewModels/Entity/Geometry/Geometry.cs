using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Assets;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Geometry : Module, ITransform
    {
        public Geometry(IMessageDispatcher messageDispatcher, MasterBeat beat, GeometryModel geometryModel) 
        {
            Instancer = new Instancer(beat, geometryModel.InstancerModel);
            Transform = new Transform(geometryModel.TransformModel);
            GeometryFX = new GeometryFX(geometryModel.GeometryFXModel);
            AssetPathSelector = new AssetPathSelector(new AssetGeometry(), geometryModel.AssetPathSelectorModel);
        }

        public override void SetModuleReceiver(ModuleMessageReceiver messageDispatcher)
        {
            //messageDispatcher.RegisterMessageProcessor(this);
            Instancer.SetModuleReceiver(messageDispatcher);
            Transform.SetModuleReceiver(messageDispatcher);
            GeometryFX.SetModuleReceiver(messageDispatcher);
            AssetPathSelector.SetModuleReceiver(messageDispatcher);
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
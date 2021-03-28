using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Assets;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Geometry : MessageCommunicator, ITransform
    {
        public Geometry(MessageDispatcher messageDispatcher, MasterBeat beat, GeometryModel geometryModel) 
            : base (messageDispatcher, geometryModel)
        {
            Instancer = new Instancer(messageDispatcher, beat, geometryModel.InstancerModel);
            Transform = new Transform(messageDispatcher, geometryModel.TransformModel);
            GeometryFX = new GeometryFX(messageDispatcher, geometryModel.GeometryFXModel);
            AssetPathSelector = new AssetPathSelector(messageDispatcher, new AssetGeometry(), geometryModel.AssetPathSelectorModel);
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
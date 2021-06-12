using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Assets;
using CMiX.MVVM.ViewModels.Beat;

namespace CMiX.MVVM.ViewModels
{
    public class Geometry : Control, ITransform
    {
        public Geometry(MasterBeat beat, GeometryModel geometryModel)
        {
            this.ID = geometryModel.ID;
            Instancer = new Instancer(beat, geometryModel.InstancerModel);
            Transform = new Transform(geometryModel.TransformModel);
            GeometryFX = new GeometryFX(geometryModel.GeometryFXModel);
            AssetPathSelector = new AssetPathSelector(new AssetGeometry(), geometryModel.AssetPathSelectorModel);
        }


        public AssetPathSelector AssetPathSelector { get; set; }
        public Transform Transform { get; set; }
        public Instancer Instancer { get; set; }
        public GeometryFX GeometryFX { get; set; }


        public override void SetCommunicator(ICommunicator communicator)
        {
            base.SetCommunicator(communicator);

            Instancer.SetCommunicator(Communicator);
            Transform.SetCommunicator(Communicator);
            GeometryFX.SetCommunicator(Communicator);
            AssetPathSelector.SetCommunicator(Communicator);
        }

        public override void UnsetCommunicator(ICommunicator communicator)
        {
            base.UnsetCommunicator(communicator);

            Instancer.UnsetCommunicator(Communicator);
            Transform.UnsetCommunicator(Communicator);
            GeometryFX.UnsetCommunicator(Communicator);
            AssetPathSelector.UnsetCommunicator(Communicator);
        }


        public override IModel GetModel()
        {
            GeometryModel model = new GeometryModel();
            model.ID = this.ID;
            model.TransformModel = (TransformModel)this.Transform.GetModel();
            model.GeometryFXModel = (GeometryFXModel)this.GeometryFX.GetModel();
            model.InstancerModel = (InstancerModel)this.Instancer.GetModel();
            model.AssetPathSelectorModel = (AssetPathSelectorModel)this.AssetPathSelector.GetModel();
            return model;
        }

        public override void SetViewModel(IModel model)
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
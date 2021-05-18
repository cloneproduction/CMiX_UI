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
            Instancer = new Instancer(beat, geometryModel.InstancerModel);
            Transform = new Transform(geometryModel.TransformModel);
            GeometryFX = new GeometryFX(geometryModel.GeometryFXModel);
            AssetPathSelector = new AssetPathSelector(new AssetGeometry(), geometryModel.AssetPathSelectorModel);
        }


        public AssetPathSelector AssetPathSelector { get; set; }
        public Transform Transform { get; set; }
        public Instancer Instancer { get; set; }
        public GeometryFX GeometryFX { get; set; }


        public override void SetReceiver(IMessageReceiver messageReceiver)
        {
            base.SetReceiver(messageReceiver);

            Instancer.SetReceiver(messageReceiver);
            Transform.SetReceiver(messageReceiver);
            GeometryFX.SetReceiver(messageReceiver);
            AssetPathSelector.SetReceiver(messageReceiver);
        }

        public override void SetSender(IMessageSender messageSender)
        {
            base.SetSender(messageSender);

            Instancer.SetSender(messageSender);
            Transform.SetSender(messageSender);
            GeometryFX.SetSender(messageSender);
            AssetPathSelector.SetSender(messageSender);
        }

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
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class GeometryFX : MessageCommunicator
    {
        public GeometryFX(GeometryFXModel geometryFXModel) 
        {
            Explode = new Slider(nameof(Explode), geometryFXModel.Explode);
        }

        public override void SetModuleReceiver(ModuleMessageReceiver messageDispatcher)
        {
            Explode.SetModuleReceiver(messageDispatcher);
        }

        public Slider Explode { get; set; }

        public override void SetViewModel(IModel model)
        {
            GeometryFXModel geometryFXModel = model as GeometryFXModel;
            this.Explode.SetViewModel(geometryFXModel.Explode);
        }

        public override IModel GetModel()
        {
            GeometryFXModel geometryFXModel = new GeometryFXModel();
            geometryFXModel.Explode = (SliderModel)this.Explode.GetModel();
            return geometryFXModel;
        }
    }
}
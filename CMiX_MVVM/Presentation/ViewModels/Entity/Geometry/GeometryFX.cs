using CMiX.Core.Interfaces;
using CMiX.Core.MessageService;
using CMiX.Core.Models;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class GeometryFX : ViewModel, IControl
    {
        public GeometryFX(GeometryFXModel geometryFXModel) 
        {
            this.ID = geometryFXModel.ID;
            Explode = new Slider(nameof(Explode), geometryFXModel.Explode);
        }


        public Guid ID { get; set; }
        public Slider Explode { get; set; }
        public ControlCommunicator Communicator { get; set; }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public void SetViewModel(IModel model)
        {
            GeometryFXModel geometryFXModel = model as GeometryFXModel;
            this.ID = geometryFXModel.ID;
            this.Explode.SetViewModel(geometryFXModel.Explode);
        }

        public IModel GetModel()
        {
            GeometryFXModel geometryFXModel = new GeometryFXModel();
            geometryFXModel.ID = this.ID;
            geometryFXModel.Explode = (SliderModel)this.Explode.GetModel();
            return geometryFXModel;
        }
    }
}
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Models.Beat;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Composition : Component
    {
        public Composition(MessageDispatcher messageDispatcher, Project project, CompositionModel compositionModel) 
            : base (messageDispatcher, compositionModel)
        {
            MasterBeat = new MasterBeat(this);
            Transition = new Slider(nameof(Transition), this, compositionModel.TransitionModel);
            Camera = new Camera(this, MasterBeat, compositionModel.CameraModel);
            
            Visibility = new Visibility(project, project.Visibility);
            ComponentFactory = new LayerFactory();
        }


        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        public MasterBeat MasterBeat { get; set; }


        public override IModel GetModel()
        {
            CompositionModel model = new CompositionModel(this.ID);

            model.Name = this.Name;
            //model.IsVisible = this.IsVisible;
            model.MasterBeatModel = (MasterBeatModel)this.MasterBeat.GetModel();
            model.CameraModel = (CameraModel)this.Camera.GetModel();
            model.TransitionModel = (SliderModel)this.Transition.GetModel();

            GetComponents(this, model);

            return model;
        }

        public override void SetViewModel(IModel model)
        {
            CompositionModel compositionModel = model as CompositionModel;

            this.MasterBeat.SetViewModel(compositionModel.MasterBeatModel);
            this.Camera.SetViewModel(compositionModel.CameraModel);
            this.Transition.SetViewModel(compositionModel.TransitionModel);

            SetComponents(this, compositionModel);
        }
    }
}
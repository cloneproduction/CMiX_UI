using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Models.Beat;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Composition : Component
    {
        public Composition(Project project, CompositionModel compositionModel, IMessageDispatcher messageDispatcher)
            : base(compositionModel, messageDispatcher)
        {

            ModuleMessageDispatcher moduleMessageDispatcher = new ModuleMessageDispatcher(this);

            //MasterBeat = new MasterBeat(moduleMessageDispatcher, new MasterBeatModel());
            Transition = new Slider(nameof(Transition), moduleMessageDispatcher, compositionModel.TransitionModel);
            //Camera = new Camera(moduleMessageDispatcher, MasterBeat, compositionModel.CameraModel);

            //Visibility = new Visibility(moduleMessageDispatcher, project.Visibility, compositionModel.VisibilityModel);
            ComponentFactory = new LayerFactory(this);
        }


        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        public MasterBeat MasterBeat { get; set; }


        public override IModel GetModel()
        {
            CompositionModel model = new CompositionModel(this.ID);

            model.Name = this.Name;
            //model.IsVisible = this.IsVisible;
            //model.MasterBeatModel = (MasterBeatModel)this.MasterBeat.GetModel();
            //model.CameraModel = (CameraModel)this.Camera.GetModel();
            model.TransitionModel = (SliderModel)this.Transition.GetModel();

            foreach (Component item in this.Components)
                model.ComponentModels.Add(item.GetModel() as IComponentModel);

            return model;
        }


        public override void SetViewModel(IModel model)
        {
            CompositionModel compositionModel = model as CompositionModel;

            //this.MasterBeat.SetViewModel(compositionModel.MasterBeatModel);
            //this.Camera.SetViewModel(compositionModel.CameraModel);
            this.Transition.SetViewModel(compositionModel.TransitionModel);

            this.Components.Clear();
            foreach (var componentModel in compositionModel.ComponentModels)
            {
                this.ComponentFactory.CreateComponent(this.MessageDispatcher, componentModel);
            }
        }
    }
}
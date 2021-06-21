﻿using CMiX.Core.Interfaces;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Models;
using CMiX.Core.Models.Beat;
using CMiX.Core.Presentation.ViewModels.Beat;
using CMiX.Core.Presentation.ViewModels.Components.Factories;

namespace CMiX.Core.Presentation.ViewModels.Components
{
    public class Composition : Component
    {
        public Composition(Project project, CompositionModel compositionModel)
            : base(compositionModel)
        {
            Transition = new Slider(nameof(Transition), compositionModel.TransitionModel);

            MasterBeat = new MasterBeat(compositionModel.MasterBeatModel);
            Camera = new Camera(MasterBeat, compositionModel.CameraModel);
            Visibility = new Visibility(project.Visibility, compositionModel.VisibilityModel);
            ComponentFactory = new LayerFactory(this);
        }


        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        public MasterBeat MasterBeat { get; set; }


        public override void SetCommunicator(Communicator communicator)
        {
            Communicator.SetCommunicator(communicator);

            Transition.SetCommunicator(Communicator);
            MasterBeat.SetCommunicator(Communicator);

        }

        public override void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);

            Transition.UnsetCommunicator(Communicator);
            MasterBeat.UnsetCommunicator(Communicator);
        }


        public override IModel GetModel()
        {
            CompositionModel model = new CompositionModel(this.ID);

            model.Name = this.Name;
            //model.IsVisible = this.IsVisible;
            model.MasterBeatModel = (MasterBeatModel)this.MasterBeat.GetModel();
            model.CameraModel = (CameraModel)this.Camera.GetModel();
            model.TransitionModel = (SliderModel)this.Transition.GetModel();

            foreach (Component item in this.Components)
                model.ComponentModels.Add(item.GetModel() as IComponentModel);

            return model;
        }

        public override void SetViewModel(IModel model)
        {
            CompositionModel compositionModel = model as CompositionModel;

            this.MasterBeat.SetViewModel(compositionModel.MasterBeatModel);
            this.Camera.SetViewModel(compositionModel.CameraModel);
            this.Transition.SetViewModel(compositionModel.TransitionModel);

            this.Components.Clear();
            foreach (var componentModel in compositionModel.ComponentModels)
            {
                var newComponent = this.ComponentFactory.CreateComponent(compositionModel);
                //newComponent.SetReceiver(MessageReceiver);
                //newComponent.SetSender(MessageSender);
                this.AddComponent(newComponent);
            }
        }
    }
}
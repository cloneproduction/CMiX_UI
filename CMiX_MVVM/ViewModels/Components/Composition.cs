﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Models.Beat;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Composition : Component
    {
        public Composition(int id, MessageTerminal MessageTerminal, Project project) : base (id, MessageTerminal)
        {
            MasterBeat = new MasterBeat(this);
            Transition = new Slider(nameof(Transition), this);
            Camera = new Camera(this, MasterBeat);
            ComponentFactory = new LayerFactory(MessageTerminal);
            Visibility = new Visibility(project, project.Visibility);
        }


        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        public MasterBeat MasterBeat { get; set; }


        public override IModel GetModel()
        {
            CompositionModel model = new CompositionModel();

            model.Name = this.Name;
            //model.IsVisible = this.IsVisible;
            model.ID = this.ID;
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
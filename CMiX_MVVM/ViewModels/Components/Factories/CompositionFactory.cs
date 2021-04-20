using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class CompositionFactory : IComponentFactory
    {
        public CompositionFactory(Project parentProject)
        {
            ParentProject = parentProject;
        }

        private Project ParentProject { get; set; }
        private static int ID = 0;

        public Component CreateComponent()
        {
            var model = new CompositionModel(Guid.NewGuid());
            var component = new Composition(ParentProject, model);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            var component = new Composition(ParentProject, model as CompositionModel);
            return component;
        }
    }
}
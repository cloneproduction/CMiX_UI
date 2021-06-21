using CMiX.Core.Models;
using System;

namespace CMiX.Core.Presentation.ViewModels.Components.Factories
{
    public class CompositionFactory : IComponentFactory
    {
        public CompositionFactory(Project parentProject)
        {
            ParentProject = parentProject;
        }

        private Project ParentProject { get; set; }

        public Component CreateComponent()
        {
            var model = new CompositionModel(Guid.NewGuid());
            var component = new Composition(ParentProject, model);
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            var component = new Composition(ParentProject, model as CompositionModel);
            return component;
        }
    }
}
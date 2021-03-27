using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;

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
            var model = new CompositionModel(ID);
            var component = new Composition(ParentProject, model);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Composition(ParentProject, model as CompositionModel);
        }
    }
}

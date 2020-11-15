using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class ComponentFactory : IComponentFactory
    {
        public ComponentFactory()
        {

        }

        private static int ID = 0;

        public IComponent CreateComponent(IComponent parentComponent)
        {

            if (parentComponent is Project)
                return CreateComposition();
            else if (parentComponent is Composition)
                return CreateLayer(parentComponent as Composition);
            else if (parentComponent is Layer)
                return CreateScene(parentComponent as Layer);
            else if (parentComponent is Scene)
                return CreateEntity(parentComponent as Scene);
            else
                return null;
        }

        private Composition CreateComposition()
        {
            var component = new Composition(ID, new MasterBeat());
            ID++;
            return component;
        }

        private Layer CreateLayer(Composition parentComponent)
        {
            var component = new Layer(ID, parentComponent.MasterBeat);
            ID++;
            return component;
        }

        private Scene CreateScene(Layer parentComponent)
        {
            var component = new Scene(ID, parentComponent.MasterBeat);
            ID++;
            return component;
        }

        private Entity CreateEntity(Scene parentComponent)
        {
            var component = new Entity(ID, parentComponent.MasterBeat);
            ID++;
            return component;
        }
    }
}

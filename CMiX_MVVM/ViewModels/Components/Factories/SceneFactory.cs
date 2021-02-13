using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public SceneFactory(MessageTerminal MessageTerminal)
        {
            this.MessageTerminal = MessageTerminal;
        }

        private static int ID = 0;

        public MessageTerminal MessageTerminal { get; set; }

        public IComponent CreateComponent(IComponent parentComponent)
        {
            return CreateScene((Layer)parentComponent);
        }

        public IComponent CreateComponent(IComponent parentComponent, IComponentModel model)
        {
            return CreateScene((Layer)parentComponent, model);
        }

        private Scene CreateScene(Layer parentComponent)
        {
            var component = new Scene(ID, MessageTerminal, parentComponent.MasterBeat);
            ID++;
            return component;
        }


        private Scene CreateScene(Layer parentComponent, IComponentModel componentModel)
        {
            var component = new Scene(componentModel.ID, MessageTerminal, parentComponent.MasterBeat);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}

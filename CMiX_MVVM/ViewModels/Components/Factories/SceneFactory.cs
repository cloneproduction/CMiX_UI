using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public SceneFactory(MessengerTerminal messengerTerminal)
        {
            this.MessengerTerminal = messengerTerminal;
        }

        private static int ID = 0;

        public MessengerTerminal MessengerTerminal { get; set; }

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
            var component = new Scene(ID, MessengerTerminal, parentComponent.MasterBeat);
            ID++;
            return component;
        }


        private Scene CreateScene(Layer parentComponent, IComponentModel componentModel)
        {
            var component = new Scene(componentModel.ID, MessengerTerminal, parentComponent.MasterBeat);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}

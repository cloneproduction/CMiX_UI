using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class LayerFactory : IComponentFactory
    {
        public LayerFactory(MessengerTerminal messengerTerminal)
        {
            this.MessengerTerminal = messengerTerminal;
        }

        private static int ID = 0;

        public MessengerTerminal MessengerTerminal { get; set; }

        public IComponent CreateComponent(IComponent parentComponent)
        {
            return CreateLayer((Composition)parentComponent);
        }

        public IComponent CreateComponent(IComponent parentComponent, IComponentModel model)
        {
            return CreateLayer((Composition)parentComponent, model);
        }


        private Layer CreateLayer(Composition parentComponent)
        {
            var component = new Layer(ID, MessengerTerminal, parentComponent.MasterBeat);
            ID++;
            return component;
        }


        private Layer CreateLayer(Composition parentComponent, IComponentModel componentModel)
        {
            var component = new Layer(componentModel.ID, MessengerTerminal, parentComponent.MasterBeat);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}

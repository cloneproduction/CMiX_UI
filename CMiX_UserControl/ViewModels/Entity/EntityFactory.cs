using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class EntityFactory
    {
        public EntityFactory()
        {

        }

        private int EntityID { get; set; } = 0;

        public Entity CreateEntity(BeatModifier beatModifier, string parentMessageAddress, Messenger messenger, Mementor memento)
        {
            Entity entity = new Entity(beatModifier, EntityID, parentMessageAddress, messenger, memento);
            EntityID++;
            return entity;
        }
    }
}
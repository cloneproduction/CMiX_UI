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
        public EntityFactory(Beat beat)
        {
            Beat = beat;
        }

        private int EntityID { get; set; } = 0;
        public Beat Beat { get; set; }
        public Entity CreateEntity(IEntityContext context)
        {
            Entity entity = new Entity(context.Beat, EntityID, context.MessageAddress, context.Sender, context.Mementor);
            EntityID++;
            return entity;
        }
    }
}
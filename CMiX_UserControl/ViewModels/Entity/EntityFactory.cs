using CMiX.MVVM.Models;
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

        public Entity CreateEntity(IEntityContext context)
        {
            Entity entity = new Entity(context.Beat, EntityID, context.MessageAddress, context.Sender, context.Mementor);
            entity.Name = "Entity " + EntityID.ToString();
            context.SelectedEntity = entity;
            context.Entities.Add(entity);
            EntityID++;

            return entity;
        }

        public void DeleteEntity(IEntityContext context)
        {
            if (context.SelectedEntity != null)
            {
                int index = context.Entities.IndexOf(context.SelectedEntity);
                context.Entities.RemoveAt(index);
                if (context.Entities.Count > 0)
                    context.SelectedEntity = context.Entities[context.Entities.Count - 1];
                else
                    context.SelectedEntity = null;
            }
        }

        public void DuplicateEntity(IEntityContext context)
        {
            if (context.SelectedEntity != null)
            {
                EntityModel entityModel = new EntityModel();
                context.SelectedEntity.CopyModel(entityModel);

                Entity entity = new Entity(context.Beat, EntityID, context.MessageAddress, context.Sender, context.Mementor);
                entity.PasteModel(entityModel);
                entity.Name += "- Copy";
                context.SelectedEntity = entity;
                context.Entities.Add(entity);
            }
        }
    }
}
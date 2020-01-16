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
    public class EntityManager
    {
        public EntityManager()
        {

        }

        private int EntityID { get; set; } = 0;

        public Entity CreateEntity(IEntityContext context)
        {
            Entity entity = new Entity(context.Beat, EntityID, context.MessageAddress, context.MessageService, context.Mementor);
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
                EntityModel entityModel = context.SelectedEntity.GetModel();

                Entity entity = new Entity(context.Beat, EntityID, context.MessageAddress, context.MessageService, context.Mementor);
                entity.SetViewModel(entityModel);
                entity.Name += "- Copy";
                context.SelectedEntity = entity;
                context.Entities.Add(entity);
            }
        }

        public void MoveEntity(IEntityContext currentContext, IEntityContext newContext)
        {
            if(currentContext.SelectedEntity != null)
            {
                Entity entity = currentContext.SelectedEntity;
                currentContext.Entities.Remove(entity);
                currentContext.SelectedEntity = null;

                newContext.SelectedEntity = entity;
                newContext.Entities.Add(entity);
            }
        }

        public void CopyEntity(IEntityContext currentContext, IEntityContext newContext)
        {
            if (currentContext.SelectedEntity != null)
            {
                EntityModel currentEntityModel = currentContext.SelectedEntity.GetModel();

                Entity entity = new Entity(newContext.Beat, EntityID, newContext.MessageAddress, newContext.MessageService, newContext.Mementor);
                entity.SetViewModel(currentEntityModel);
                entity.Name = "Entity " + EntityID.ToString();
                newContext.SelectedEntity = entity;
                newContext.Entities.Add(entity);
                EntityID++;
            }
        }
    }
}
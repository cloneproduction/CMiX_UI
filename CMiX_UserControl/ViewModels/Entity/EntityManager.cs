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

        public Entity CreateEntity(Layer layer)
        {
            Entity entity = new Entity(layer.MasterBeat, EntityID, layer.MessageAddress, layer.MessageService, layer.Mementor);
            layer.Entities.Add(entity);
            layer.SelectedEntity = entity;
            EntityID++;
            return entity;
        }



        public void DeleteEntity(Layer layer)
        {
            if (layer.SelectedEntity != null)
            {
                int index = layer.Entities.IndexOf(layer.SelectedEntity);
                layer.Entities.RemoveAt(index);
                if (layer.Entities.Count > 0)
                    layer.SelectedEntity = layer.Entities[layer.Entities.Count - 1];
                else
                    layer.SelectedEntity = null;
            }
        }

        public void DuplicateEntity(Layer layer)
        {
            if (layer.SelectedEntity != null)
            {
                EntityModel entityModel = layer.SelectedEntity.GetModel();

                Entity entity = new Entity(layer.MasterBeat, EntityID, layer.MessageAddress, layer.MessageService, layer.Mementor);
                entity.SetViewModel(entityModel);
                entity.Name += "- Copy";
                layer.SelectedEntity = entity;
                layer.Entities.Add(entity);
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
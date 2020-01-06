using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class EntityEditor : ViewModel
    {
        public EntityEditor()
        {

        }

        #region METHODS
        public void AddEntity(object entityContext)
        {
            if (entityContext != null)
            {
                var ec = entityContext as ISendableEntityContext;
                BeatModifier bm = new BeatModifier(ec.MessageAddress, this.MasterBeat, Sender, Mementor);
                var entity = EntityFactory.CreateEntity(bm, ec.MessageAddress, Sender, Mementor);
                ec.Entities.Add(entity);
                ec.SelectedEntity = entity;

                EntityModel entityModel = new EntityModel();
                entity.CopyModel(entityModel);
                Sender.SendMessages(ec.MessageAddress, MessageCommand.OBJECT_ADD, null, entityModel);
            }
        }

        public void DeleteEntity(object entityContext)
        {
            if (entityContext != null)
            {
                var ec = entityContext as ISendableEntityContext;
                if (ec.SelectedEntity != null)
                {
                    int index = ec.Entities.IndexOf(ec.SelectedEntity);
                    ec.Entities.RemoveAt(index);
                    if (ec.Entities.Count > 0)
                        ec.SelectedEntity = ec.Entities[ec.Entities.Count - 1];
                    else
                        ec.SelectedEntity = null;

                    EntityModel entityModel = new EntityModel();
                    Sender.SendMessages(ec.MessageAddress, MessageCommand.OBJECT_DELETE, null, index);
                }
            }
        }
        #endregion
    }
}

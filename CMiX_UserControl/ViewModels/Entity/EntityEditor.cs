using CMiX.MVVM.Commands;
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
    public class EntityEditor : ViewModel, IEntityEditor
    {
        public EntityEditor(Sender sender, string messageAddress, Beat beat, Assets assets, Mementor mementor)
        {
            EntityFactory = new EntityFactory(beat);
            Entities = new ObservableCollection<Entity>();
            Mementor = mementor;
            Assets = assets;
            Beat = beat;

            MessageAddress = messageAddress;
            Sender = sender;
        }

        public EntityFactory EntityFactory { get; set; }
        public Beat Beat { get; set; }
        public Assets Assets { get; set; }
        public ObservableCollection<Entity> Entities { get; set; }
        public Entity SelectedEntity { get; set; }
        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }


        #region METHODS
        public void AddEntity()
        {
            var entity = EntityFactory.CreateEntity(this);
            Entities.Add(entity);
            SelectedEntity = entity;

            EntityModel entityModel = new EntityModel();
            entity.CopyModel(entityModel);
            Sender.SendMessages(MessageAddress, MessageCommand.ENTITY_ADD, null, entityModel);
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
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
                    Sender.SendMessages(ec.MessageAddress, MessageCommand.ENTITY_DELETE, null, index);
                }
            }
        }

        public void PasteModel(IModel model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

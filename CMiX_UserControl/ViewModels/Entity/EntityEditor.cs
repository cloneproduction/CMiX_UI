using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class EntityEditor : ViewModel, IEntityEditor
    {
        public EntityEditor(MessageService messageService, Beat beat, Assets assets, Mementor mementor)
        {
            MessageService = messageService;
            Assets = assets;
            Beat = beat;
            Mementor = mementor;

            Entities = new ObservableCollection<Entity>();
            EntityManager = new EntityManager();
            EditingEntities = new ObservableCollection<Entity>();

            AddEntityCommand = new RelayCommand(p => AddEntity(p));
            DeleteSelectedEntityCommand = new RelayCommand(p => DeleteEntity(p));
            DuplicateSelectedEntityCommand = new RelayCommand(p => DuplicateEntity(p));
            RenameEntityCommand = new RelayCommand(p => RenameEntity(p));
            MoveEntityToLayerCommand = new RelayCommand(p => MoveEntityToLayer(p));
            EditEntityCommand = new RelayCommand(p => EditEntity(p));
            RemoveEntityFromEditingCommand = new RelayCommand(p => RemoveEntityFromEditing(p));
        }

        public ICommand AddEntityCommand { get; }
        public ICommand DuplicateSelectedEntityCommand { get; }
        public ICommand DeleteSelectedEntityCommand { get; }
        public ICommand RenameEntityCommand { get; }
        public ICommand MoveEntityToLayerCommand { get; }
        public ICommand EditEntityCommand { get; set; }
        public ICommand RemoveEntityFromEditingCommand { get; set; }

        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }

        public EntityManager EntityManager { get; set; }
        public Beat Beat { get; set; }
        public Assets Assets { get; set; }

        public ObservableCollection<Entity> Entities { get; set; }
        public ObservableCollection<Entity> EditingEntities { get; set; }

        private Entity _selectedEntity;
        public Entity SelectedEntity
        {
            get => _selectedEntity;
            set => SetAndNotify(ref _selectedEntity, value);
        }

        public void RemoveEntityFromEditing(object obj)
        {
            if (obj is Entity)
            {
                var entity = obj as Entity;
                EditingEntities.Remove(entity);
            }
        }

        public void EditEntity(object obj)
        {
            if (obj is Entity)
            {
                var entity = obj as Entity;
                if (!EditingEntities.Contains(entity))
                {
                    this.EditingEntities.Add(entity);
                }
                this.SelectedEntity = entity;
            }
        }

        public void RenameEntity(object obj)
        {
            if (obj is Entity)
            {
                var entity = obj as Entity;
                entity.IsRenaming = true;
            }
        }

        public void MoveEntityToLayer(object obj)
        {
            if(obj is Layer)
            {
                var layer = obj as Layer;
                layer.Entities.Add(this.SelectedEntity);
                Entities.Remove(this.SelectedEntity);
                this.SelectedEntity = null;
            }
        }

        public void AddEntity(object obj)
        {
            if(obj is Layer)
            {
                var layer = obj as Layer;
                var entity = EntityManager.CreateEntity(layer);
                MessageService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, layer.GetModel());
                this.EditingEntities.Add(entity);
                this.SelectedEntity = entity;
            }
        }

        public void DeleteEntity(object obj)
        {
            if(obj is Layer)
            {
                var layer = obj as Layer;
                var entityToDelete = layer.SelectedEntity;
                if (EditingEntities.Contains(entityToDelete))
                {
                    EditingEntities.Remove(entityToDelete);
                }
                int deleteIndex = Entities.IndexOf(entityToDelete);
                EntityManager.DeleteEntity(layer);
                
                MessageService.SendMessages(MessageAddress, MessageCommand.ENTITY_DELETE, null, deleteIndex);
            }
        }

        public void DuplicateEntity(object obj)
        {
            if(obj is Layer)
            {
                var layer = obj as Layer;
                EntityManager.DuplicateEntity(layer);
                MessageService.SendMessages(layer.MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, layer.GetModel());
            }
        }

        public EntityEditorModel GetModel()
        {
            EntityEditorModel entityEditorModel = new EntityEditorModel();
            if(SelectedEntity != null)
                entityEditorModel.SelectedEntityModel = SelectedEntity.GetModel();

            foreach (var entity in Entities)
            {
                var entityModel = entity.GetModel();
                entityEditorModel.EntityModels.Add(entityModel);
            }
            return entityEditorModel;
        }

        public void SetViewModel(EntityEditorModel entityEditorModel)
        {
            if (SelectedEntity != null)
                SelectedEntity.SetViewModel(entityEditorModel.SelectedEntityModel);

            Entities.Clear();
            foreach (var entityModel in entityEditorModel.EntityModels)
            {
                //var entity = EntityManager.CreateEntity(this);
                //entity.SetViewModel(entityModel);
            }
        }
    }
}

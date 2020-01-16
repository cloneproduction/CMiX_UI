using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class EntityEditor : ViewModel, IEntityEditor
    {
        public EntityEditor(ObservableCollection<Entity> entities, MessageService messageService, string messageAddress, Beat beat, Assets assets, Mementor mementor)
        {
            EntityManager = new EntityManager(entities);
            Mementor = mementor;
            Assets = assets;
            Beat = beat;
            Entities = new ObservableCollection<Entity>();

            MessageAddress = messageAddress;
            MessageService = messageService;

            AddEntityCommand = new RelayCommand(p => AddEntity());
            DeleteSelectedEntityCommand = new RelayCommand(p => DeleteEntity());
            DuplicateSelectedEntityCommand = new RelayCommand(p => DuplicateEntity());
            RenameSelectedEntityCommand = new RelayCommand(p => RenameEntity());
        }

        public ICommand AddEntityCommand { get; }
        public ICommand DuplicateSelectedEntityCommand { get; }
        public ICommand DeleteSelectedEntityCommand { get; }
        public ICommand RenameSelectedEntityCommand { get; }

        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }

        public EntityManager EntityManager { get; set; }
        public Beat Beat { get; set; }
        public Assets Assets { get; set; }
        public ObservableCollection<Entity> Entities { get; set; }

        private Entity _selectedEntity;
        public Entity SelectedEntity
        {
            get => _selectedEntity;
            set => SetAndNotify(ref _selectedEntity, value);
        }

        public void RenameEntity()
        {
            if(SelectedEntity != null)
                SelectedEntity.IsRenaming = true;
        }

        public void AddEntity()
        {
            var entity = EntityManager.CreateEntity(this);
            MessageService.SendMessages(MessageAddress, MessageCommand.ENTITY_ADD, null, entity.GetModel());
        }

        public void DeleteEntity()
        {
            int deleteIndex = Entities.IndexOf(SelectedEntity);
            EntityManager.DeleteEntity(this);
            MessageService.SendMessages(MessageAddress, MessageCommand.ENTITY_DELETE, null, deleteIndex);
        }

        public void DuplicateEntity()
        {
            EntityManager.DuplicateEntity(this);
            MessageService.SendMessages(SelectedEntity.MessageAddress, MessageCommand.ENTITY_DUPLICATE, null, SelectedEntity.GetModel());
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
                var entity = EntityManager.CreateEntity(this);
                entity.SetViewModel(entityModel);
            }
        }
    }
}

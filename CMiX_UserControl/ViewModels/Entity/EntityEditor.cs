using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class EntityEditor : ViewModel, IEntityEditor
    {
        public EntityEditor(ObservableCollection<Entity> entities, MessageService messageService, string messageAddress, Beat beat, Assets assets, Mementor mementor)
        {
            EntityFactory = new EntityFactory();
            Entities = new ObservableCollection<Entity>();
            Mementor = mementor;
            Assets = assets;
            Beat = beat;
            Entities = entities;

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

        public EntityFactory EntityFactory { get; set; }
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
            var entity = EntityFactory.CreateEntity(this);
            MessageService.SendMessages(MessageAddress, MessageCommand.ENTITY_ADD, null, entity.GetModel());
        }

        public void DeleteEntity()
        {
            if(SelectedEntity != null)
            {
                int deleteIndex = Entities.IndexOf(SelectedEntity);
                EntityFactory.DeleteEntity(this);
                MessageService.SendMessages(MessageAddress, MessageCommand.ENTITY_DELETE, null, deleteIndex);
            }
        }

        public void DuplicateEntity()
        {
            EntityFactory.DuplicateEntity(this);
            MessageService.SendMessages(SelectedEntity.MessageAddress, MessageCommand.COMPOSITION_DUPLICATE, null, SelectedEntity.GetModel());
        }

        public EntityEditorModel GetModel()
        {
            Console.WriteLine("GetModel Entities Count : " + Entities.Count);
            EntityEditorModel entityEditorModel = new EntityEditorModel();
            if(SelectedEntity != null)
                entityEditorModel.SelectedEntityModel = SelectedEntity.GetModel();

            foreach (var entity in Entities)
            {
                Console.WriteLine("ForEach GetEntity");
                var entityModel = entity.GetModel();
                entityEditorModel.EntityModels.Add(entityModel);
            }
            Console.WriteLine("entityEditorModel.EntityModels.Count = " + entityEditorModel.EntityModels.Count);
            return entityEditorModel;
        }

        public void SetViewModel(EntityEditorModel entityEditorModel)
        {
            Console.WriteLine("SetViewModel EntityEditor");
            Console.WriteLine("entityEditorModel.EntityModels.Count" + entityEditorModel.EntityModels.Count);
            if (SelectedEntity != null)
                SelectedEntity.SetViewModel(entityEditorModel.SelectedEntityModel);

            Entities.Clear();
            foreach (var entityModel in entityEditorModel.EntityModels)
            {
                Console.WriteLine("ForEach SetEntity");
                var entity = EntityFactory.CreateEntity(this);
                entity.SetViewModel(entityModel);
            }
        }
    }
}

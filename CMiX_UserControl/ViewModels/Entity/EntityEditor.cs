﻿using CMiX.MVVM.Commands;
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
        public EntityEditor(MessageService messageService, string messageAddress, Beat beat, Assets assets, Mementor mementor)
        {
            EntityFactory = new EntityFactory();
            Entities = new ObservableCollection<Entity>();
            Mementor = mementor;
            Assets = assets;
            Beat = beat;

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
            EntityModel entityModel = new EntityModel();
            entity.CopyModel(entityModel);
            MessageService.SendMessages(MessageAddress, MessageCommand.ENTITY_ADD, null, entityModel);
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
            if(SelectedEntity != null)
            {
                EntityFactory.DuplicateEntity(this);
                EntityModel entityModel = new EntityModel();
                SelectedEntity.CopyModel(entityModel);
                MessageService.SendMessages(SelectedEntity.MessageAddress, MessageCommand.COMPOSITION_DUPLICATE, null, entityModel);
            }
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public void PasteModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Commands;

namespace CMiX.ViewModels
{
    public class Content : SendableViewModel
    {
        #region CONSTRUCTORS
        public Content(Beat masterbeat, string messageAddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor) 
            : base (serverValidations, mementor)
        {
            MessageAddress = String.Format($"{messageAddress}{nameof(Content)}/");
            Enable = true;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, serverValidations, mementor);
            PostFX = new PostFX(MessageAddress, serverValidations, mementor);

            Entities = new ObservableCollection<Entity>();
            CreateEntity();

            AddEntityCommand = new RelayCommand(p => AddEntity());
            DeleteEntityCommand = new RelayCommand(p => DeleteEntity());
            CopyContentCommand = new RelayCommand(p => CopyContent());
            PasteContentCommand = new RelayCommand(p => PasteContent());
            ResetContentCommand = new RelayCommand(p => ResetContent());
        }
        #endregion

        #region METHODS
        private string CreateEntityMessageAddress()
        {
            return "/Entity" + EntityID.ToString() + "/";
        }

        public void UpdateMessageAddress(string messageAddress)
        {
            MessageAddress = messageAddress;
            BeatModifier.UpdateMessageAddress($"{MessageAddress}{nameof(BeatModifier)}/");
            PostFX.UpdateMessageAddress($"{MessageAddress}{nameof(PostFX)}/");

            foreach (Entity obj in Entities)
            {
                obj.UpdateMessageAddress($"{MessageAddress}{obj.Name}/");
            }
        }

        public Entity CreateEntity()
        {
            EntityID++;
            string messageAddress = $"{MessageAddress}Entity{EntityID.ToString()}/";
            Entity obj = new Entity(this.BeatModifier, messageAddress, ServerValidation, Mementor) { ID = EntityID, Name = "Entity" + EntityID };
            Entities.Add(obj);
            SelectedEntity = obj;
            return obj;
        }

        public void AddEntity()
        {
            Mementor.BeginBatch();
            DisabledMessages();

            var entity = CreateEntity();
            Mementor.ElementAdd(Entities, entity);
            EntityModel entityModel = new EntityModel();
            entity.Copy(entityModel);

            EnabledMessages();
            Mementor.EndBatch();

            SendMessages(MessageAddress, MessageCommand.OBJECT_ADD, null, entityModel);
        }

        public void DeleteEntity()
        {
            if (SelectedEntity != null)
            {
                Mementor.BeginBatch();
                DisabledMessages();

                Entity removedObject = SelectedEntity as Entity;
                int removedObjectIndex = Entities.IndexOf(removedObject);
                Mementor.ElementRemove(Entities, removedObject);
                Entities.Remove(removedObject);

                if (Entities.Count > 0)
                {
                    if (removedObjectIndex > 0)
                        SelectedEntity = Entities[removedObjectIndex - 1];
                    else
                        SelectedEntity = Entities[0];
                }
                else
                {
                    EntityID = -1;
                }

                EnabledMessages();
                Mementor.EndBatch();

                SendMessages(MessageAddress, MessageCommand.OBJECT_DELETE, null, removedObjectIndex);
            }
        }
        #endregion

        #region PROPERTIES
        public ICommand AddEntityCommand { get; }
        public ICommand DeleteEntityCommand { get; }
        public ICommand CopyContentCommand { get; }
        public ICommand PasteContentCommand { get; }
        public ICommand ResetContentCommand { get; }

        public ObservableCollection<Entity> Entities { get; set; }

        private Entity _selectedEntity;
        public Entity SelectedEntity
        {
            get => _selectedEntity;
            set => SetAndNotify(ref _selectedEntity, value);
        }

        private int _entityID = -1;
        public int EntityID
        {
            get => _entityID;
            set => _entityID = value;
        }

        public BeatModifier BeatModifier { get; }
        public PostFX PostFX { get; }

        #endregion

        #region COPY/PASTE
        public void Copy(ContentModel contentModel)
        {
            contentModel.MessageAddress = MessageAddress;
            contentModel.Enable = Enable;

            foreach (Entity obj in Entities)
            {
                EntityModel entityModel = new EntityModel();
                obj.Copy(entityModel);
                contentModel.EntityModels.Add(entityModel);
            }

            this.BeatModifier.Copy(contentModel.BeatModifierModel);
            this.PostFX.Copy(contentModel.PostFXModel);
        }

        public void Paste(ContentModel contentModel)
        {
            this.DisabledMessages();

            this.MessageAddress = contentModel.MessageAddress;
            this.Enable = contentModel.Enable;

            Entities.Clear();
            foreach (EntityModel entityModel in contentModel.EntityModels)
            {
                // may have some address problem here
                Entity entity = new Entity(this.BeatModifier, CreateEntityMessageAddress(), ServerValidation, Mementor);
                entity.Paste(entityModel);
                this.Entities.Add(entity);
            }

            this.BeatModifier.Paste(contentModel.BeatModifierModel);
            this.PostFX.Paste(contentModel.PostFXModel);

            this.EnabledMessages();
        }

        public void Reset()
        {
            this.DisabledMessages();

            this.Enable = true;
            this.BeatModifier.Reset();
            this.PostFX.Reset();

            this.EnabledMessages();
        }

        #region COPYPASTE CONTENT
        public void CopyContent()
        {
            ContentModel contentmodel = new ContentModel();
            this.Copy(contentmodel);
            IDataObject data = new DataObject();
            data.SetData("ContentModel", contentmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteContent()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ContentModel"))
            {
                this.Mementor.BeginBatch();
                this.DisabledMessages();

                var contentModel = data.GetData("ContentModel") as ContentModel;
                var contentmessageaddress = MessageAddress;
                this.Paste(contentModel);
                this.UpdateMessageAddress(contentmessageaddress);

                this.EnabledMessages();
                this.Mementor.EndBatch();

                SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, contentModel);
            }
        }

        public void ResetContent()
        {
            ContentModel contentModel = new ContentModel();
            this.Reset();
            this.Copy(contentModel);
            SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, contentModel);
        }
        #endregion

        #endregion
    }
}
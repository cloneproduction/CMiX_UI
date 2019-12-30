using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Commands;
using CMiX.MVVM;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class Content : ViewModel, ISendableEntityContext, IUndoable
    {
        #region CONSTRUCTORS
        public Content(Beat masterbeat, string messageAddress, MessageService messageService, Mementor mementor) 
        {
            MessageAddress = String.Format($"{messageAddress}{nameof(Content)}/");
            Enable = true;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, messageService, mementor);
            PostFX = new PostFX(MessageAddress, messageService, mementor);
            Entities = new ObservableCollection<Entity>();

            CopyContentCommand = new RelayCommand(p => CopyContent());
            PasteContentCommand = new RelayCommand(p => PasteContent());
            ResetContentCommand = new RelayCommand(p => ResetContent());
        }
        #endregion

        #region METHODS


        public void UpdateMessageAddress(string messageAddress)
        {
            MessageAddress = messageAddress;
            BeatModifier.UpdateMessageAddress($"{MessageAddress}{nameof(BeatModifier)}/");
            PostFX.UpdateMessageAddress($"{MessageAddress}{nameof(PostFX)}/");
        }
        #endregion

        #region PROPERTIES
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

        public BeatModifier BeatModifier { get; }
        public PostFX PostFX { get; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        

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
            MessageService.DisabledMessages();

            this.MessageAddress = contentModel.MessageAddress;
            this.Enable = contentModel.Enable;

            this.BeatModifier.Paste(contentModel.BeatModifierModel);
            this.PostFX.Paste(contentModel.PostFXModel);

            MessageService.EnabledMessages();
        }

        public void Reset()
        {
            MessageService.DisabledMessages();

            this.Enable = true;
            this.BeatModifier.Reset();
            this.PostFX.Reset();

            MessageService.EnabledMessages();
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
                MessageService.DisabledMessages();

                var contentModel = data.GetData("ContentModel") as ContentModel;
                var contentmessageaddress = MessageAddress;
                this.Paste(contentModel);
                this.UpdateMessageAddress(contentmessageaddress);

                MessageService.EnabledMessages();
                this.Mementor.EndBatch();

                MessageService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, contentModel);
            }
        }

        public void ResetContent()
        {
            ContentModel contentModel = new ContentModel();
            this.Reset();
            this.Copy(contentModel);
            MessageService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, contentModel);
        }
        #endregion

        #endregion
    }
}
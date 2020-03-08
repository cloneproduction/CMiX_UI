using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Services;
using CMiX.MVVM.Resources;

namespace CMiX.Studio.ViewModels
{
    public class Content : ViewModel, ISendable, IUndoable, IComponent
    {
        #region CONSTRUCTORS
        public Content(Beat beat, string messageAddress, MessageService messageService, Mementor mementor)
        {
            Enabled = true;
            Beat = beat;
            MessageAddress = $"{messageAddress}{nameof(Content)}/";
            MessageService = messageService;
            Components = new ObservableCollection<IComponent>();

            BeatModifier = new BeatModifier(MessageAddress, Beat, messageService, mementor);
            PostFX = new PostFX(MessageAddress, messageService, mementor);

            CopyContentCommand = new RelayCommand(p => CopyContent());
            PasteContentCommand = new RelayCommand(p => PasteContent());
            ResetContentCommand = new RelayCommand(p => ResetContent());
        }
        #endregion

        #region PROPERTIES
        public ICommand DeleteEntityCommand { get; }
        public ICommand CopyContentCommand { get; }
        public ICommand PasteContentCommand { get; }
        public ICommand ResetContentCommand { get; }
        public ICommand RenameCommand { get; }

        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }
        public Assets Assets { get; set; }

        public BeatModifier BeatModifier { get; }
        public PostFX PostFX { get; }
        public Beat Beat { get; set; }

        public ObservableCollection<IComponent> Components { get; set; }

        public Entity SelectedEntity { get; set; }
        public bool IsRenaming { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        


        #endregion

        #region COPY/PASTE
        public ContentModel GetModel()
        {
            ContentModel contentModel = new ContentModel();
            contentModel.Enabled = Enabled;
            contentModel.BeatModifierModel = BeatModifier.GetModel();
            contentModel.PostFXModel = PostFX.GetModel();
            return contentModel;
        }

        public void SetViewModel(ContentModel contentModel)
        {
            MessageService.Disable();

            Enabled = contentModel.Enabled;
            BeatModifier.SetViewModel(contentModel.BeatModifierModel);
            PostFX.SetViewModel(contentModel.PostFXModel);

            if (SelectedEntity != null)
                SelectedEntity.SetViewModel(contentModel.SelectedEntityModel);

            Components.Clear();
            foreach (var entityModel in contentModel.EntityModels)
            {
                //var entity = EntityManager.CreateEntity(this);
                //entity.SetViewModel(entityModel);
            }

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();

            this.Enabled = true;
            this.BeatModifier.Reset();
            this.PostFX.Reset();

            MessageService.Enable();
        }

        #region COPYPASTE CONTENT
        public void CopyContent()
        {
            ContentModel contentmodel = GetModel();
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
                MessageService.Disable();

                var contentModel = data.GetData("ContentModel") as ContentModel;
                var contentmessageaddress = MessageAddress;
                this.SetViewModel(contentModel);

                MessageService.Enable();
                this.Mementor.EndBatch();

                MessageService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, contentModel);
            }
        }

        public void ResetContent()
        {
            this.Reset();
            ContentModel contentModel = GetModel();
            MessageService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, contentModel);
        }

        public void AddComponent(IComponent component)
        {
            
        }

        public void RemoveComponent(IComponent component)
        {
            
        }
        #endregion

        #endregion
    }
}
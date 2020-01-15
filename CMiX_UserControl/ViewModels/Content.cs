using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Content : ViewModel, ISendable
    {
        #region CONSTRUCTORS
        public Content(Beat beat, string messageAddress, MessageService messageService, Mementor mementor)
        {
            Beat = beat;
            MessageAddress = $"{messageAddress}{nameof(Content)}/";
            Enabled = true;
            MessageService = messageService;
            Entities = new ObservableCollection<Entity>();

            BeatModifier = new BeatModifier(MessageAddress, Beat, messageService, mementor);
            PostFX = new PostFX(MessageAddress, messageService, mementor);
            EntityEditor = new EntityEditor(Entities, messageService, MessageAddress, Beat, Assets, mementor);

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

        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }
        public Assets Assets { get; set; }

        public BeatModifier BeatModifier { get; }
        public PostFX PostFX { get; }
        public EntityEditor EntityEditor {get; }
        public ObservableCollection<Entity> Entities { get; set; }
        public Beat Beat { get; set; }
        #endregion

        #region COPY/PASTE
        public ContentModel GetModel()
        {
            ContentModel contentModel = new ContentModel();
            contentModel.Enabled = Enabled;
            contentModel.EntityEditorModel = EntityEditor.GetModel();
            contentModel.BeatModifierModel = BeatModifier.GetModel();
            contentModel.PostFXModel = PostFX.GetModel();
            return contentModel;
        }

        public void SetViewModel(ContentModel contentModel)
        {
            MessageService.Disable();

            Enabled = contentModel.Enabled;
            EntityEditor.SetViewModel(contentModel.EntityEditorModel);
            BeatModifier.SetViewModel(contentModel.BeatModifierModel);
            PostFX.SetViewModel(contentModel.PostFXModel);

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
        #endregion

        #endregion
    }
}
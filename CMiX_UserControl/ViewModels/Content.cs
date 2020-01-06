﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Commands;
using CMiX.MVVM;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Content : ViewModel, ICopyPasteModel, ISendableEntityContext, IUndoable
    {
        #region CONSTRUCTORS
        public Content(Beat masterbeat, string messageAddress, Sender sender, Mementor mementor) 
        {
            MessageAddress = String.Format($"{messageAddress}{nameof(Content)}/");
            Enabled = true;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, sender, mementor);
            PostFX = new PostFX(MessageAddress, sender, mementor);
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

        public BeatModifier BeatModifier { get; }
        public PostFX PostFX { get; }
        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }

        private Entity _selectedEntity;
        public Entity SelectedEntity
        {
            get => _selectedEntity;
            set => SetAndNotify(ref _selectedEntity, value);
        }

        public ObservableCollection<Entity> Entities { get; set; }
        #endregion

        #region COPY/PASTE
        public void CopyModel(IModel model)
        {
            ContentModel contentModel = model as ContentModel;
            contentModel.MessageAddress = MessageAddress;
            contentModel.Enable = Enabled;

            foreach (Entity obj in Entities)
            {
                EntityModel entityModel = new EntityModel();
                obj.CopyModel(entityModel);
                contentModel.EntityModels.Add(entityModel);
            }

            this.BeatModifier.CopyModel(contentModel.BeatModifierModel);
            this.PostFX.CopyModel(contentModel.PostFXModel);
        }

        public void PasteModel(IModel model)
        {
            ContentModel contentModel = model as ContentModel;
            Sender.Disable();

            this.MessageAddress = contentModel.MessageAddress;
            this.Enabled = contentModel.Enable;

            this.BeatModifier.PasteModel(contentModel.BeatModifierModel);
            this.PostFX.PasteModel(contentModel.PostFXModel);

            Sender.Enable();
        }

        public void Reset()
        {
            Sender.Disable();

            this.Enabled = true;
            this.BeatModifier.Reset();
            this.PostFX.Reset();

            Sender.Enable();
        }

        #region COPYPASTE CONTENT
        public void CopyContent()
        {
            ContentModel contentmodel = new ContentModel();
            this.CopyModel(contentmodel);
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
                Sender.Disable();

                var contentModel = data.GetData("ContentModel") as ContentModel;
                var contentmessageaddress = MessageAddress;
                this.PasteModel(contentModel);
                this.UpdateMessageAddress(contentmessageaddress);

                Sender.Enable();
                this.Mementor.EndBatch();

                Sender.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, contentModel);
            }
        }

        public void ResetContent()
        {
            ContentModel contentModel = new ContentModel();
            this.Reset();
            this.CopyModel(contentModel);
            Sender.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, contentModel);
        }
        #endregion

        #endregion
    }
}
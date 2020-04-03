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
    public class Scene : ViewModel, ISendable, IUndoable, IComponent, IGetSet<SceneModel>
    {
        #region CONSTRUCTORS
        public Scene(Beat beat, string messageAddress, MessageService messageService, Assets assets, Mementor mementor)
        {
            Enabled = true;
            Beat = beat;
            Assets = assets;
            Mementor = mementor;
            Name = "Scene";
            MessageAddress = $"{messageAddress}{nameof(Scene)}/";
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

        public bool IsRenaming { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetVisibility();
                SetAndNotify(ref _isVisible, value);
            }
        }

        private bool _parentIsVisible;
        public bool ParentIsVisible
        {
            get => _parentIsVisible;
            set => _parentIsVisible = value;
        }

        public void SetVisibility()
        {
            foreach (var item in this.Components)
                item.ParentIsVisible = IsVisible;
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetAndNotify(ref _isExpanded, value);
        }

        

        #endregion

        #region COPY/PASTE
        public SceneModel GetModel()
        {
            SceneModel sceneModel = new SceneModel();
            sceneModel.Enabled = Enabled;
            sceneModel.BeatModifierModel = BeatModifier.GetModel();
            sceneModel.PostFXModel = PostFX.GetModel();

            foreach (IGetSet<EntityModel> item in Components)
                sceneModel.ComponentModels.Add(item.GetModel());

            return sceneModel;
        }

        public void SetViewModel(SceneModel sceneModel)
        {
            MessageService.Disable();

            Enabled = sceneModel.Enabled;
            BeatModifier.SetViewModel(sceneModel.BeatModifierModel);
            PostFX.SetViewModel(sceneModel.PostFXModel);

            Components.Clear();
            foreach (EntityModel componentModel in sceneModel.ComponentModels)
            {
                Entity entity = new Entity(0, this.Beat, this.MessageAddress, this.MessageService, this.Assets, this.Mementor);
                entity.SetViewModel(componentModel);
                this.AddComponent(entity);
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
            SceneModel contentmodel = GetModel();
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

                var contentModel = data.GetData("ContentModel") as SceneModel;
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
            MessageService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, GetModel());
        }

        public void AddComponent(IComponent component)
        {
            Components.Add(component);
            IsExpanded = true;
        }

        public void RemoveComponent(IComponent component)
        {
            Components.Remove(component);
        }
        #endregion

        #endregion
    }
}
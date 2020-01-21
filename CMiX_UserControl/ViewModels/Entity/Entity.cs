using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class Entity : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Entity(Beat beat, int id, string messageAddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}Entity{id.ToString()}/";
            Mementor = mementor;
            MessageService = messageService;
            Enabled = true;
            ID = id;
            Name = "Entity" + id;
            count++;

            BeatModifier = new BeatModifier(MessageAddress, beat, messageService, mementor);
            Geometry = new Geometry(MessageAddress, messageService, mementor, beat);
            Texture = new Texture(MessageAddress, messageService, mementor);
            Coloration = new Coloration(MessageAddress, messageService, mementor, beat);

            CopyEntityCommand = new RelayCommand(p => CopyEntity());
            PasteEntityCommand = new RelayCommand(p => PasteEntity());
            ResetEntityCommand = new RelayCommand(p => ResetEntity());
        }
        #endregion

        #region PROPERTIES
        private static int count = 0;
        public ICommand CopyEntityCommand { get; }
        public ICommand PasteEntityCommand { get; }
        public ICommand ResetEntityCommand { get; }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }

        private bool _isMask;
        public bool IsMask
        {
            get => _isMask;
            set => SetAndNotify(ref _isMask, value);
        }

        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public Coloration Coloration { get; }

        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE
        public void Reset()
        {
            MessageService.Disable();

            this.Enabled = true;
            this.BeatModifier.Reset();
            this.Geometry.Reset();
            this.Texture.Reset();
            this.Coloration.Reset();
            MessageService.Enable();
        }

        public EntityModel GetModel()
        {
            EntityModel entityModel = new EntityModel();
            entityModel.Enabled = Enabled;
            entityModel.Name = Name;
            entityModel.BeatModifierModel = BeatModifier.GetModel();
            entityModel.TextureModel = Texture.GetModel();
            entityModel.GeometryModel = Geometry.GetModel();
            entityModel.ColorationModel = Coloration.GetModel();
            return entityModel;
        }

        public void SetViewModel(EntityModel entityModel)
        {
            this.MessageService.Disable();

            this.Enabled = entityModel.Enabled;
            this.Name = entityModel.Name;
            this.BeatModifier.SetViewModel(entityModel.BeatModifierModel);
            this.Texture.SetViewModel(entityModel.TextureModel);
            this.Geometry.Paste(entityModel.GeometryModel);
            this.Coloration.SetViewModel(entityModel.ColorationModel);

            this.MessageService.Enable();
        }

        public void CopyEntity()
        {
            IDataObject data = new DataObject();
            data.SetData(nameof(EntityModel), GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteEntity()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(nameof(EntityModel)))
            {
                this.Mementor.BeginBatch();
                this.MessageService.Disable();

                var entityModel = data.GetData(nameof(EntityModel)) as EntityModel;
                this.SetViewModel(entityModel);
                this.MessageService.Enable();
                this.Mementor.EndBatch();
                //SendMessages(nameof(ContentModel), contentmodel);
            }
        }

        public void ResetEntity()
        {
            EntityModel entityModel = GetModel();
            this.Reset();
            //SendMessages(nameof(ContentModel), entityModel);
        }
        #endregion
    }
}
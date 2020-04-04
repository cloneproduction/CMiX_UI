using System.Windows;
using System.Windows.Input;
using Memento;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class Entity : Component, IGetSet<EntityModel>
    {
        #region CONSTRUCTORS
        public Entity(int id, Beat beat, string messageAddress, MessageService messageService, Assets assets, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}Entity{id.ToString()}/";
            Mementor = mementor;
            MessageService = messageService;
            Assets = assets;

            Enabled = true;
            ID = id;
            Name = "Entity" + id;

            BeatModifier = new BeatModifier(MessageAddress, beat, messageService, mementor);
            Geometry = new Geometry(MessageAddress, messageService, mementor, assets, beat);
            Texture = new Texture(MessageAddress, messageService, assets, mementor);
            Coloration = new Coloration(MessageAddress, messageService, mementor, beat);

            Components = new ObservableCollection<Component>();

            RenameCommand = new RelayCommand(p => Rename());
            CopyEntityCommand = new RelayCommand(p => CopyEntity());
            PasteEntityCommand = new RelayCommand(p => PasteEntity());
            ResetEntityCommand = new RelayCommand(p => ResetEntity());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyEntityCommand { get; }
        public ICommand PasteEntityCommand { get; }
        public ICommand ResetEntityCommand { get; }
        public ICommand RenameCommand { get; }

        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public Coloration Coloration { get; }
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
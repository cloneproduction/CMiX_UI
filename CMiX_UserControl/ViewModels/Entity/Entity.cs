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

        private ObservableCollection<Layer> _layers;
        public ObservableCollection<Layer> Layers
        {
            get => _layers;
            set => _layers = value;
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get { return _isRenaming; }
            set { _isRenaming = value; }
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
            get { return _name; }
            set { _name = value; }
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


        public void CopyModel(EntityModel entityModel)
        {
            entityModel.Enable = Enabled;
            entityModel.Name = Name;

            this.BeatModifier.CopyModel(entityModel.BeatModifierModel);
            this.Texture.CopyModel(entityModel.TextureModel);
            this.Geometry.Copy(entityModel.GeometryModel);
            this.Coloration.CopyModel(entityModel.ColorationModel);
        }

        public void PasteModel(EntityModel entityModel)
        {
            this.MessageService.Disable();

            this.Enabled = entityModel.Enable;
            this.Name = entityModel.Name;
            
            this.BeatModifier.PasteModel(entityModel.BeatModifierModel);
            this.Texture.PasteModel(entityModel.TextureModel);
            this.Geometry.Paste(entityModel.GeometryModel);
            this.Coloration.PasteModel(entityModel.ColorationModel);

            this.MessageService.Enable();
        }

        public void CopyEntity()
        {
            EntityModel entityModel = new EntityModel();
            this.CopyModel(entityModel);
            IDataObject data = new DataObject();
            data.SetData("EntityModel", entityModel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteEntity()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("EntityModel"))
            {
                this.Mementor.BeginBatch();
                this.MessageService.Disable();

                var entityModel = data.GetData("EntityModel") as EntityModel;
                this.PasteModel(entityModel);

                this.CopyModel(entityModel);
                this.MessageService.Enable();
                this.Mementor.EndBatch();
                //SendMessages(nameof(ContentModel), contentmodel);
            }
        }

        public void ResetEntity()
        {
            EntityModel entityModel = new EntityModel();
            this.Reset();
            this.CopyModel(entityModel);
            //SendMessages(nameof(ContentModel), contentmodel);
        }
        #endregion
    }
}
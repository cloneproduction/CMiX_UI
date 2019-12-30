using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class Entity : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Entity()
        {

        }

        public Entity(Beat masterbeat, int id, string messageAddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}Entity{id.ToString()}/";

            Enable = true;
            ID = id;
            Name = "Entity" + id;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, messageService, mementor);
            Geometry = new Geometry(MessageAddress, messageService, mementor, masterbeat);
            Texture = new Texture(MessageAddress, messageService, mementor);
            Coloration = new Coloration(MessageAddress, messageService, mementor, masterbeat);

            CopyEntityCommand = new RelayCommand(p => CopyEntity());
            PasteEntityCommand = new RelayCommand(p => PasteEntity());
            ResetEntityCommand = new RelayCommand(p => ResetEntity());
        }
        #endregion

        #region METHODS
        public void SetMessageAddress(string messageAddress)
        {
            MessageAddress = messageAddress;
            BeatModifier.UpdateMessageAddress($"{MessageAddress}{nameof(BeatModifier)}/");
            Geometry.UpdateMessageAddress($"{MessageAddress}{nameof(Geometry)}/");
            Texture.UpdateMessageAddress($"{MessageAddress}{nameof(Texture)}/");
            Coloration.UpdateMessageAddress($"{MessageAddress}{nameof(Coloration)}/");

            Console.WriteLine("Update Entity Message Address " + MessageAddress);
        }

        public void UpdateMessageAddress(string messageaddress)
        {

        }
        #endregion

        #region PROPERTIES
        public ICommand CopyEntityCommand { get; }
        public ICommand PasteEntityCommand { get; }
        public ICommand ResetEntityCommand { get; }

        private ObservableCollection<Layer> _layers;

        public ObservableCollection<Layer> Layers
        {
            get => _layers;
            set => _layers = value;
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
        public string MessageAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MessageService MessageService { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Mementor Mementor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #region COPY/PASTE
        public void Reset()
        {
            MessageService.DisabledMessages();

            this.Enable = true;
            this.BeatModifier.Reset();
            this.Geometry.Reset();
            this.Texture.Reset();
            this.Coloration.Reset();
            MessageService.EnabledMessages();
        }


        public void Copy(EntityModel entityModel)
        {
            entityModel.MessageAddress = MessageAddress;
            entityModel.Enable = Enable;
            entityModel.Name = Name;

            this.BeatModifier.Copy(entityModel.BeatModifierModel);
            this.Texture.Copy(entityModel.TextureModel);
            this.Geometry.Copy(entityModel.GeometryModel);
            this.Coloration.Copy(entityModel.ColorationModel);
        }

        public void Paste(EntityModel entityModel)
        {
            
            this.MessageService.DisabledMessages();

            this.MessageAddress = entityModel.MessageAddress;
            this.Enable = entityModel.Enable;
            this.Name = entityModel.Name;
            
            this.BeatModifier.Paste(entityModel.BeatModifierModel);
            this.Texture.Paste(entityModel.TextureModel);
            this.Geometry.Paste(entityModel.GeometryModel);
            this.Coloration.Paste(entityModel.ColorationModel);

            this.MessageService.EnabledMessages();
        }

        public void CopyEntity()
        {
            EntityModel entityModel = new EntityModel();
            this.Copy(entityModel);
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
                this.MessageService.DisabledMessages();

                var entityModel = data.GetData("EntityModel") as EntityModel;
                var messageAddress = MessageAddress;
                this.Paste(entityModel);
                this.UpdateMessageAddress(messageAddress);

                this.Copy(entityModel);
                this.MessageService.EnabledMessages();
                this.Mementor.EndBatch();
                //SendMessages(nameof(ContentModel), contentmodel);
            }
        }

        public void ResetEntity()
        {
            EntityModel entityModel = new EntityModel();
            this.Reset();
            this.Copy(entityModel);
            //SendMessages(nameof(ContentModel), contentmodel);
        }
        #endregion
    }
}
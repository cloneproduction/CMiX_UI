using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using Memento;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class Entity : SendableViewModel
    {
        #region CONSTRUCTORS
        public Entity()
        {

        }

        public Entity(Beat masterbeat, string messageaddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor)
            : base(serverValidations, mementor)
        {
            MessageAddress = messageaddress;

            Enable = true;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, serverValidations, mementor);
            Geometry = new Geometry(MessageAddress, serverValidations, mementor, masterbeat);
            Texture = new Texture(MessageAddress, serverValidations, mementor);
            Coloration = new Coloration(MessageAddress, serverValidations, mementor, masterbeat);

            CopyEntityCommand = new RelayCommand(p => CopyEntity());
            PasteEntityCommand = new RelayCommand(p => PasteEntity());
            ResetEntityCommand = new RelayCommand(p => ResetEntity());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            BeatModifier.UpdateMessageAddress($"{MessageAddress}{nameof(BeatModifier)}/");
            Geometry.UpdateMessageAddress($"{MessageAddress}{nameof(Geometry)}/");
            Texture.UpdateMessageAddress($"{MessageAddress}{nameof(Texture)}/");
            Coloration.UpdateMessageAddress($"{MessageAddress}{nameof(Coloration)}/");

            Console.WriteLine("Update Entity Message Address " + MessageAddress);
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyEntityCommand { get; }
        public ICommand PasteEntityCommand { get; }
        public ICommand ResetEntityCommand { get; }

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
        #endregion

        #region COPY/PASTE
        public void Reset()
        {
            this.DisabledMessages();

            this.Enable = true;
            this.BeatModifier.Reset();
            this.Geometry.Reset();
            this.Texture.Reset();
            this.Coloration.Reset();
            this.EnabledMessages();
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
            
            this.DisabledMessages();

            this.MessageAddress = entityModel.MessageAddress;
            this.Enable = entityModel.Enable;
            this.Name = entityModel.Name;
            
            this.BeatModifier.Paste(entityModel.BeatModifierModel);
            this.Texture.Paste(entityModel.TextureModel);
            this.Geometry.Paste(entityModel.GeometryModel);
            this.Coloration.Paste(entityModel.ColorationModel);

            this.EnabledMessages();
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
                this.DisabledMessages();

                var entityModel = data.GetData("EntityModel") as EntityModel;
                var messageAddress = MessageAddress;
                this.Paste(entityModel);
                this.UpdateMessageAddress(messageAddress);

                this.Copy(entityModel);
                this.EnabledMessages();
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
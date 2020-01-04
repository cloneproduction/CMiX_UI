using CMiX.MVVM;
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
    public class Entity : ViewModel, ICopyPasteModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Entity()
        {

        }
        //https://stackoverflow.com/questions/731294/generating-the-next-available-unique-name-in-c-sharp
        public Entity(Beat masterbeat, int id, string messageAddress, Messenger messenger, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}Entity{id.ToString()}/";
            Mementor = mementor;

            Enabled = true;
            ID = id;
            Name = "Entity" + id;
            count++;
            Console.WriteLine(count.ToString());
            BeatModifier = new BeatModifier(MessageAddress, masterbeat, messenger, mementor);
            Geometry = new Geometry(MessageAddress, messenger, mementor, masterbeat);
            Texture = new Texture(MessageAddress, messenger, mementor);
            Coloration = new Coloration(MessageAddress, messenger, mementor, masterbeat);

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
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE
        public void Reset()
        {
            Messenger.Disable();

            this.Enabled = true;
            this.BeatModifier.Reset();
            this.Geometry.Reset();
            this.Texture.Reset();
            this.Coloration.Reset();
            Messenger.Enable();
        }


        public void CopyModel(IModel model)
        {
            EntityModel entityModel = model as EntityModel;
            entityModel.MessageAddress = MessageAddress;
            entityModel.Enable = Enabled;
            entityModel.Name = Name;

            this.BeatModifier.CopyModel(entityModel.BeatModifierModel);
            this.Texture.CopyModel(entityModel.TextureModel);
            this.Geometry.Copy(entityModel.GeometryModel);
            this.Coloration.CopyModel(entityModel.ColorationModel);
        }

        public void PasteModel(IModel model)
        {
            this.Messenger.Disable();

            EntityModel entityModel = model as EntityModel;
            this.MessageAddress = entityModel.MessageAddress;
            this.Enabled = entityModel.Enable;
            this.Name = entityModel.Name;
            
            this.BeatModifier.PasteModel(entityModel.BeatModifierModel);
            this.Texture.PasteModel(entityModel.TextureModel);
            this.Geometry.Paste(entityModel.GeometryModel);
            this.Coloration.PasteModel(entityModel.ColorationModel);

            this.Messenger.Enable();
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
                this.Messenger.Disable();

                var entityModel = data.GetData("EntityModel") as EntityModel;
                var messageAddress = MessageAddress;
                this.PasteModel(entityModel);
                this.UpdateMessageAddress(messageAddress);

                this.CopyModel(entityModel);
                this.Messenger.Enable();
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
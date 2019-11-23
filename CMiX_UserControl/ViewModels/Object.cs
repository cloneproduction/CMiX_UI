using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using Memento;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class Object : ViewModel
    {
        #region CONSTRUCTORS
        public Object(Beat masterbeat, string messageaddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor)
            : base(serverValidations, mementor)
        {
            MessageAddress = messageaddress;

            Enable = true;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, serverValidations, mementor);
            Geometry = new Geometry(MessageAddress, serverValidations, mementor, masterbeat);
            Texture = new Texture(MessageAddress, serverValidations, mementor);
            Coloration = new Coloration(MessageAddress, serverValidations, mementor, masterbeat);

            CopyObjectCommand = new RelayCommand(p => CopyObject());
            PasteObjectCommand = new RelayCommand(p => PasteObject());
            ResetObjectCommand = new RelayCommand(p => ResetObject());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            BeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(BeatModifier)));
            Geometry.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Geometry)));
            Texture.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Texture)));
            Coloration.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Coloration)));
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyObjectCommand { get; }
        public ICommand PasteObjectCommand { get; }
        public ICommand ResetObjectCommand { get; }

        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set => SetAndNotify(ref _enable, value);
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


        public void Copy(ObjectModel objectModel)
        {
            objectModel.MessageAddress = MessageAddress;
            objectModel.Enable = Enable;
            objectModel.Name = Name;
            this.BeatModifier.Copy(objectModel.BeatModifierModel);
            this.Texture.Copy(objectModel.TextureModel);
            this.Geometry.Copy(objectModel.GeometryModel);
            this.Coloration.Copy(objectModel.ColorationModel);
        }

        public void Paste(ObjectModel objectModel)
        {
            this.DisabledMessages();

            this.Name = objectModel.Name;
            this.MessageAddress = objectModel.MessageAddress;
            this.Enable = objectModel.Enable;
            this.BeatModifier.Paste(objectModel.BeatModifierModel);
            this.Texture.Paste(objectModel.TextureModel);
            this.Geometry.Paste(objectModel.GeometryModel);
            this.Coloration.Paste(objectModel.ColorationModel);
            this.EnabledMessages();
        }

        public void CopyObject()
        {
            ObjectModel objectModel = new ObjectModel();
            this.Copy(objectModel);
            IDataObject data = new DataObject();
            data.SetData("ObjectModel", objectModel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteObject()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ObjectModel"))
            {
                this.Mementor.BeginBatch();
                this.DisabledMessages();

                var objectModel = data.GetData("ObjectModel") as ObjectModel;
                var messageAddress = MessageAddress;
                this.Paste(objectModel);
                this.UpdateMessageAddress(messageAddress);

                this.Copy(objectModel);
                this.EnabledMessages();
                this.Mementor.EndBatch();
                //SendMessages(nameof(ContentModel), contentmodel);
            }
        }

        public void ResetObject()
        {
            ObjectModel objectModel = new ObjectModel();
            this.Reset();
            this.Copy(objectModel);
            //SendMessages(nameof(ContentModel), contentmodel);
        }
        #endregion
    }
}
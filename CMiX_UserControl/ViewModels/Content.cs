using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Content : ViewModel
    {
        #region CONSTRUCTORS
        public Content(Beat masterbeat, string messageaddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor) 
            : base (serverValidations, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Content));

            Enable = true;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, serverValidations, mementor);
            Geometry = new Geometry(MessageAddress, serverValidations, mementor, masterbeat);
            Texture = new Texture(MessageAddress, serverValidations, mementor);
            PostFX = new PostFX(MessageAddress, serverValidations, mementor);

            CopyContentCommand = new RelayCommand(p => CopyContent());
            PasteContentCommand = new RelayCommand(p => PasteContent());
            ResetContentCommand = new RelayCommand(p => ResetContent());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            BeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(BeatModifier)));
            Geometry.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Geometry)));
            Texture.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Texture)));
            PostFX.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(PostFX)));
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyContentCommand { get; }
        public ICommand PasteContentCommand { get; }
        public ICommand ResetContentCommand { get; }

        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set => SetAndNotify(ref _enable, value);
        }

        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public PostFX PostFX { get; }
        #endregion

        #region COPY/PASTE
        public void Reset()
        {
            this.DisabledMessages();

            this.Enable = true;
            this.BeatModifier.Reset();
            this.Geometry.Reset();
            this.Texture.Reset();
            this.PostFX.Reset();

            this.EnabledMessages();
        }


        public void Copy(ContentModel contentmodel)
        {
            contentmodel.MessageAddress = MessageAddress;
            contentmodel.Enable = Enable;
            this.BeatModifier.Copy(contentmodel.BeatModifierModel);
            this.Texture.Copy(contentmodel.TextureModel);
            this.Geometry.Copy(contentmodel.GeometryModel);
            this.PostFX.Copy(contentmodel.PostFXModel);
        }

        public void Paste(ContentModel contentmodel)
        {
            this.DisabledMessages();
            //MessageAddress = string.Empty;
            this.MessageAddress = contentmodel.MessageAddress;
            this.Enable = contentmodel.Enable;
            this.BeatModifier.Paste(contentmodel.BeatModifierModel);
            this.Texture.Paste(contentmodel.TextureModel);
            this.Geometry.Paste(contentmodel.GeometryModel);
            this.PostFX.Paste(contentmodel.PostFXModel);
            this.EnabledMessages();
        }

        public void CopyContent()
        {
            ContentModel contentmodel = new ContentModel();
            this.Copy(contentmodel);
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
                this.DisabledMessages();

                var contentmodel = data.GetData("ContentModel") as ContentModel;
                var contentmessageaddress = MessageAddress;
                this.Paste(contentmodel);
                this.UpdateMessageAddress(contentmessageaddress);

                this.Copy(contentmodel);
                this.EnabledMessages();
                this.Mementor.EndBatch();
                //SendMessages(nameof(ContentModel), contentmodel);

                //this.QueueObjects(contentmodel);
                //this.SendQueues();
            }
        }

        public void ResetContent()
        {
            ContentModel contentmodel = new ContentModel();
            this.Reset();
            this.Copy(contentmodel);
            //SendMessages(nameof(ContentModel), contentmodel);
            //this.QueueObjects(contentmodel);
            //this.SendQueues();
        }
        #endregion
    }
}
using System;
using System.Windows;
using System.Windows.Input;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Content : ViewModel
    {
        #region CONSTRUCTORS
        public Content(Beat masterbeat, string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) 
            : base (oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Content));

            Enable = true;

            BeatModifier = new BeatModifier(MessageAddress, oscmessengers, masterbeat, mementor);
            Geometry = new Geometry(MessageAddress, oscmessengers, mementor);
            Texture = new Texture(MessageAddress, oscmessengers, mementor);
            PostFX = new PostFX(MessageAddress, oscmessengers, mementor);

            ResetCommand = new RelayCommand(p => Reset());
            CopyTextureCommand = new RelayCommand(p => CopyTexture());
            PasteTextureCommand = new RelayCommand(p => PasteTexture());
            CopyGeometryCommand = new RelayCommand(p => CopyGeometry());
            PasteGeometryCommand = new RelayCommand(p => PasteGeometry());
            CopyPostFXCommand = new RelayCommand(p => CopyPostFX());
            PastePostFXCommand = new RelayCommand(p => PastePostFX());
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
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetCommand { get; }

        public ICommand CopyTextureCommand { get; }
        public ICommand PasteTextureCommand { get; }

        public ICommand CopyGeometryCommand { get; }
        public ICommand PasteGeometryCommand { get; }

        public ICommand CopyPostFXCommand { get; }
        public ICommand PastePostFXCommand { get; }

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
        public void CopyPostFX()
        {
            PostFXModel postfxmodel = new PostFXModel();
            PostFX.Copy(postfxmodel);
            IDataObject data = new DataObject();
            data.SetData("PostFX", postfxmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PastePostFX()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("PostFX"))
            {
                Mementor.BeginBatch();
                var postfxmodel = (PostFXModel)data.GetData("PostFX") as PostFXModel;
                var postfxmessageaddress = PostFX.MessageAddress;

                PostFX.Paste(postfxmodel);
                PostFX.UpdateMessageAddress(postfxmessageaddress);

                PostFX.Copy(postfxmodel);
                Mementor.EndBatch();

                QueueObjects(postfxmodel);
                SendQueues();
            }
        }


        public void CopyGeometry()
        {
            GeometryModel geometrymodel = new GeometryModel();
            Geometry.Copy(geometrymodel);
            IDataObject data = new DataObject();
            data.SetData("Geometry", geometrymodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Geometry"))
            {
                Mementor.BeginBatch();
                var geometrymodel = (GeometryModel)data.GetData("Geometry") as GeometryModel;
                var geometrymessageaddress = Geometry.MessageAddress;

                Geometry.Paste(geometrymodel);
                Geometry.UpdateMessageAddress(geometrymessageaddress);

                Geometry.Copy(geometrymodel);
                Mementor.EndBatch();

                QueueObjects(geometrymodel);
                SendQueues();
            }
        }


        public void CopyTexture()
        {
            TextureModel texturemodel = new TextureModel();
            Texture.Copy(texturemodel);
            IDataObject data = new DataObject();
            data.SetData("Texture", texturemodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteTexture()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Texture"))
            {
                Mementor.BeginBatch();
                var texturemodel = (TextureModel)data.GetData("Texture") as TextureModel;
                var texturemessageaddress = Texture.MessageAddress;

                Texture.Paste(texturemodel);
                Texture.UpdateMessageAddress(texturemessageaddress);

                Texture.Copy(texturemodel);
                Mementor.EndBatch();

                QueueObjects(texturemodel);
                SendQueues();
            }
        }


        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();

            Enable = true;
            BeatModifier.Reset();
            Geometry.Reset();
            Texture.Reset();
            PostFX.Reset();

            EnabledMessages();
            //Mementor.EndBatch();

            ContentModel contentmodel = new ContentModel();
            this.Copy(contentmodel);
            QueueObjects(contentmodel);
            SendQueues();
        }


        public void Copy(ContentModel contentmodel)
        {
            contentmodel.MessageAddress = MessageAddress;
            contentmodel.Enable = Enable;
            BeatModifier.Copy(contentmodel.BeatModifierModel);
            Texture.Copy(contentmodel.TextureModel);
            Geometry.Copy(contentmodel.GeometryModel);
            PostFX.Copy(contentmodel.PostFXModel);
        }

        public void Paste(ContentModel contentmodel)
        {
            DisabledMessages();
            MessageAddress = string.Empty;
            MessageAddress = contentmodel.MessageAddress;
            Enable = contentmodel.Enable;
            BeatModifier.Paste(contentmodel.BeatModifierModel);
            Texture.Paste(contentmodel.TextureModel);
            Geometry.Paste(contentmodel.GeometryModel);
            PostFX.Paste(contentmodel.PostFXModel);
            EnabledMessages();
        }
        #endregion
    }
}
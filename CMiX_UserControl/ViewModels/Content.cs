using System;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Content : ViewModel
    {
        #region CONSTRUCTORS
        public Content(Beat masterbeat, string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) 
            : base (oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Content));

            Enable = true;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, oscvalidation, mementor);
            Geometry = new Geometry(MessageAddress, oscvalidation, mementor);
            Texture = new Texture(MessageAddress, oscvalidation, mementor);
            PostFX = new PostFX(MessageAddress, oscvalidation, mementor);

            //CopyTextureCommand = new RelayCommand(p => CopyTexture());
            //PasteTextureCommand = new RelayCommand(p => PasteTexture());
            //CopyGeometryCommand = new RelayCommand(p => CopyGeometry());
            //PasteGeometryCommand = new RelayCommand(p => PasteGeometry());
            //CopyPostFXCommand = new RelayCommand(p => CopyPostFX());
            //PastePostFXCommand = new RelayCommand(p => PastePostFX());
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
        //public ICommand CopyTextureCommand { get; }
        //public ICommand PasteTextureCommand { get; }
        //public ICommand CopyGeometryCommand { get; }
        //public ICommand PasteGeometryCommand { get; }
        //public ICommand CopyPostFXCommand { get; }
        //public ICommand PastePostFXCommand { get; }
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
        //public void CopyPostFX()
        //{
        //    PostFXModel postfxmodel = new PostFXModel();
        //    PostFX.Copy(postfxmodel);
        //    IDataObject data = new DataObject();
        //    data.SetData("PostFX", postfxmodel, false);
        //    Clipboard.SetDataObject(data);
        //}

        //public void PastePostFX()
        //{
        //    IDataObject data = Clipboard.GetDataObject();
        //    if (data.GetDataPresent("PostFX"))
        //    {
        //        Mementor.BeginBatch();
        //        var postfxmodel = (PostFXModel)data.GetData("PostFX") as PostFXModel;
        //        var postfxmessageaddress = PostFX.MessageAddress;

        //        PostFX.Paste(postfxmodel);
        //        PostFX.UpdateMessageAddress(postfxmessageaddress);

        //        PostFX.Copy(postfxmodel);
        //        Mementor.EndBatch();

        //        QueueObjects(postfxmodel);
        //        SendQueues();
        //    }
        //}


        //public void CopyGeometry()
        //{
        //    GeometryModel geometrymodel = new GeometryModel();
        //    Geometry.Copy(geometrymodel);
        //    IDataObject data = new DataObject();
        //    data.SetData("Geometry", geometrymodel, false);
        //    Clipboard.SetDataObject(data);
            
        //}

        //public void PasteGeometry()
        //{
        //    IDataObject data = Clipboard.GetDataObject();
        //    if (data.GetDataPresent("Geometry"))
        //    {
        //        Mementor.BeginBatch();
        //        var geometrymodel = (GeometryModel)data.GetData("Geometry") as GeometryModel;
        //        var geometrymessageaddress = Geometry.MessageAddress;

        //        Geometry.Paste(geometrymodel);
        //        Geometry.UpdateMessageAddress(geometrymessageaddress);

        //        Geometry.Copy(geometrymodel);
        //        Mementor.EndBatch();

        //        QueueObjects(geometrymodel);
        //        SendQueues();
        //    }
        //}


        //public void CopyTexture()
        //{
        //    TextureModel texturemodel = new TextureModel();
        //    Texture.Copy(texturemodel);
        //    IDataObject data = new DataObject();
        //    data.SetData("Texture", texturemodel, false);
        //    Clipboard.SetDataObject(data);
        //}

        //public void PasteTexture()
        //{
        //    IDataObject data = Clipboard.GetDataObject();
        //    if (data.GetDataPresent("Texture"))
        //    {
        //        Mementor.BeginBatch();
        //        var texturemodel = (TextureModel)data.GetData("Texture") as TextureModel;
        //        var texturemessageaddress = Texture.MessageAddress;

        //        Texture.Paste(texturemodel);
        //        Texture.UpdateMessageAddress(texturemessageaddress);

        //        Texture.Copy(texturemodel);
        //        Mementor.EndBatch();

        //        QueueObjects(texturemodel);
        //        SendQueues();
        //    }
        //}


        public void Reset()
        {
            DisabledMessages();

            Enable = true;
            BeatModifier.Reset();
            Geometry.Reset();
            Texture.Reset();
            PostFX.Reset();

            EnabledMessages();
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
                Mementor.BeginBatch();
                var contentmodel = data.GetData("ContentModel") as ContentModel;
                var contentmessageaddress = MessageAddress;
                this.Paste(contentmodel);
                UpdateMessageAddress(contentmessageaddress);
                Mementor.EndBatch();
            }
        }

        public void ResetContent()
        {
            ContentModel contentmodel = new ContentModel();
            this.Reset();
            this.Copy(contentmodel);
            QueueObjects(contentmodel);
            SendQueues();
        }
        #endregion
    }
}
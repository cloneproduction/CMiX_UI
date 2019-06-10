using System;
using CMiX.Services;
using CMiX.Models;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Mask : ViewModel
    {
        #region CONSTRUCTORS
        public Mask(Beat masterbeat, string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) 
            : base (oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Mask));

            Enable = false;

            BeatModifier = new BeatModifier(MessageAddress, oscmessengers, masterbeat, mementor);
            Geometry = new Geometry(MessageAddress, oscmessengers, mementor);
            Texture = new Texture(MessageAddress, oscmessengers, mementor);
            PostFX = new PostFX(MessageAddress, oscmessengers, mementor);

            //CopySelfCommand = new RelayCommand(p => CopySelf());
            //PasteSelfCommand = new RelayCommand(p => PasteSelf());
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
        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(Enable));
                SetAndNotify(ref _enable, value);
                SendMessages(MessageAddress + nameof(Enable), Enable);
            }
        }

        private bool _keeporiginal;
        public bool KeepOriginal
        {
            get => _keeporiginal;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(KeepOriginal));
                SetAndNotify(ref _keeporiginal, value);
                SendMessages(MessageAddress + nameof(KeepOriginal), KeepOriginal);
            }
        }

        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetCommand { get; }

        public ICommand CopyTextureCommand { get; }
        public ICommand PasteTextureCommand { get; }

        public ICommand CopyGeometryCommand { get; }
        public ICommand PasteGeometryCommand { get; }

        public ICommand CopyPostFXCommand { get; }
        public ICommand PastePostFXCommand { get; }

        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public PostFX PostFX { get; }
        #endregion

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


        #region COPY/PASTE/RESET
        public void Copy(MaskModel maskmodel)
        {
            maskmodel.MessageAddress = MessageAddress;
            maskmodel.Enable = Enable;
            maskmodel.KeepOriginal = KeepOriginal;
            BeatModifier.Copy(maskmodel.BeatModifierModel);
            Texture.Copy(maskmodel.texturemodel);
            Geometry.Copy(maskmodel.GeometryModel);
            PostFX.Copy(maskmodel.PostFXModel);
        }

        public void Paste(MaskModel maskmodel)
        {
            DisabledMessages();
            MessageAddress = maskmodel.MessageAddress;
            Enable = maskmodel.Enable;
            KeepOriginal = maskmodel.KeepOriginal;
            BeatModifier.Paste(maskmodel.BeatModifierModel);
            Texture.Paste(maskmodel.texturemodel);
            Geometry.Paste(maskmodel.GeometryModel);
            PostFX.Paste(maskmodel.PostFXModel);
            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();

            Enable = false;
            BeatModifier.Reset();
            Geometry.Reset();
            Texture.Reset();
            PostFX.Reset();

            EnabledMessages();

            MaskModel maskmodel = new MaskModel();
            this.Copy(maskmodel);
            QueueObjects(maskmodel);
            SendQueues();
        }
        #endregion
    }
}

//public void CopySelf()
//{
//    MaskModel maskmodel = new MaskModel();
//    this.Copy(maskmodel);
//    IDataObject data = new DataObject();
//    data.SetData("Mask", maskmodel, false);
//    Clipboard.SetDataObject(data);
//}

//public void PasteSelf()
//{
//    IDataObject data = Clipboard.GetDataObject();
//    if (data.GetDataPresent("Mask"))
//    {
//        var maskmodel = (MaskModel)data.GetData("Mask") as MaskModel;
//        this.Paste(maskmodel);
//        QueueObjects(maskmodel);
//        SendQueues();
//    }
//}
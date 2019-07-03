using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.Models;
using CMiX.Services;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Layer : ViewModel
    {
        #region CONSTRUCTORS
        public Layer(MasterBeat masterBeat, string layername, ObservableCollection<OSCMessenger> messengers, int index, Mementor mementor) 
            : base (messengers, mementor)
        {
            MessageAddress =  layername;

            LayerName = layername;           
            Index = index;
            Index = 0;
            Enabled = false;
            BlendMode = ((BlendMode)0).ToString();

            Fade = new Slider(MessageAddress + nameof(Fade), messengers, mementor);
            BeatModifier = new BeatModifier(MessageAddress, messengers, masterBeat, mementor);
            Content = new Content(BeatModifier, MessageAddress, messengers, mementor);
            Mask = new Mask(BeatModifier, MessageAddress, messengers, mementor);
            Coloration = new Coloration(MessageAddress, messengers, mementor, BeatModifier);
            PostFX = new PostFX(MessageAddress, messengers, mementor);

            CopyContentCommand = new RelayCommand(p => CopyContent());
            PasteContentCommand = new RelayCommand(p => PasteContent());
            CopyMaskCommand = new RelayCommand(p => CopyMask());
            PasteMaskCommand = new RelayCommand(p => PasteMask());
            CopyColorationCommand = new RelayCommand(p => CopyColoration());
            PasteColorationCommand = new RelayCommand(p => PasteColoration());
            CopyPostFXCommand = new RelayCommand(p => CopyPostFX());
            PastePostFXCommand = new RelayCommand(p => PastePostFX());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Fade.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Fade)));
            BeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(BeatModifier)));
            Content.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Content)));
            Mask.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Mask)));
            Coloration.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Coloration)));
            PostFX.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(PostFX)));
        }
        #endregion

        #region PROPERTIES

        public ICommand CopyContentCommand { get; }
        public ICommand PasteContentCommand { get; }

        public ICommand CopyMaskCommand { get; }
        public ICommand PasteMaskCommand { get; }

        public ICommand CopyColorationCommand { get; }
        public ICommand PasteColorationCommand { get; }

        public ICommand CopyPostFXCommand { get; }
        public ICommand PastePostFXCommand { get; }

        private string _layername;
        public string LayerName
        {
            get => _layername;
            set => SetAndNotify(ref _layername, value);
        }

        private bool _enabled;
        public bool Enabled
        {
            get => _enabled;
            set => SetAndNotify(ref _enabled, value);
        }

        private int _index;
        public int Index
        {
            get => _index;
            set => SetAndNotify(ref _index, value);
        }

        private bool _out;
        public bool Out
        {
            get => _out;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(Out));
                SetAndNotify(ref _out, value);
                if (Out)
                    SendMessages(MessageAddress + nameof(Out), Out);
            }
        }

        private string _blendMode;
        public string BlendMode
        {
            get => _blendMode;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(BlendMode));                  
                SetAndNotify(ref _blendMode, value);
                SendMessages(MessageAddress + nameof(BlendMode), BlendMode);
            }
        }

        public Slider Fade { get; }
        public BeatModifier BeatModifier { get; }
        public Content Content { get; }
        public Mask Mask { get; }
        public Coloration Coloration { get; }
        public PostFX PostFX{ get; }

        #endregion

        #region COPY/PASTE/LOAD
        public void CopyColoration()
        {
            ColorationModel colorationmodel = new ColorationModel();
            Coloration.Copy(colorationmodel);
            IDataObject data = new DataObject();
            data.SetData("ColorationModel", colorationmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteColoration()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ColorationModel"))
            {
                Mementor.BeginBatch();
                var colorationmodel = (ColorationModel)data.GetData("ColorationModel") as ColorationModel;
                var colorationmessageaddress = Coloration.MessageAddress;

                Coloration.Paste(colorationmodel);
                Coloration.UpdateMessageAddress(colorationmessageaddress);

                Coloration.Copy(colorationmodel);
                Mementor.EndBatch();

                QueueObjects(colorationmodel);
                SendQueues();
            }
        }

        public void CopyMask()
        {
            MaskModel maskmodel = new MaskModel();
            Mask.Copy(maskmodel);
            IDataObject data = new DataObject();
            data.SetData("MaskModel", maskmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteMask()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("MaskModel"))
            {
                Mementor.BeginBatch();
                var maskmodel = (MaskModel)data.GetData("MaskModel") as MaskModel;
                var maskmessageaddress = Mask.MessageAddress;

                Mask.Paste(maskmodel);
                Mask.UpdateMessageAddress(maskmessageaddress);

                Mask.Copy(maskmodel);
                Mementor.EndBatch();

                QueueObjects(maskmodel);
                SendQueues();
            }
        }


        public void CopyContent()
        {
            ContentModel contentmodel = new ContentModel();
            Content.Copy(contentmodel);
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
                var contentmodel = (ContentModel)data.GetData("ContentModel") as ContentModel;
                var contentmessageaddress = Content.MessageAddress;

                Content.Paste(contentmodel);
                Content.UpdateMessageAddress(contentmessageaddress);

                Content.Copy(contentmodel);
                Mementor.EndBatch();

                QueueObjects(contentmodel);
                SendQueues();
            }
        }


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


        public void Reset()
        {
            Enabled = false;
            BlendMode = ((BlendMode)0).ToString();

            Fade.Reset();
            BeatModifier.Reset();
            Content.Reset();
            Mask.Reset();
            Coloration.Reset();
            PostFX.Reset();
        }

        public void Copy(LayerModel layermodel)
        {
            layermodel.MessageAddress = MessageAddress;
            layermodel.BlendMode = BlendMode;
            layermodel.Index = Index;
            layermodel.LayerName = LayerName;
            layermodel.Index = Index;
            Fade.Copy(layermodel.Fade);
            BeatModifier.Copy(layermodel.BeatModifierModel);
            Content.Copy(layermodel.ContentModel);
            Mask.Copy(layermodel.maskmodel);
            Coloration.Copy(layermodel.ColorationModel);
            PostFX.Copy(layermodel.PostFXModel);
        }

        public void Paste(LayerModel layermodel)
        {
            DisabledMessages();

            MessageAddress = layermodel.MessageAddress;
            Index = layermodel.Index;
            LayerName = layermodel.LayerName;
            BlendMode = layermodel.BlendMode;
            Fade.Paste(layermodel.Fade);
            Out = layermodel.Out;

            BeatModifier.Paste(layermodel.BeatModifierModel);
            Content.Paste(layermodel.ContentModel);
            Mask.Paste(layermodel.maskmodel);
            Coloration.Paste(layermodel.ColorationModel);
            PostFX.Paste(layermodel.PostFXModel);

            EnabledMessages();
        }

        public void Load(LayerModel layermodel)
        {
            DisabledMessages();

            BlendMode = layermodel.BlendMode;
            LayerName = layermodel.LayerName;
            Index = layermodel.Index;
            Out = layermodel.Out;
            Fade.Paste(layermodel.Fade);
            BeatModifier.Paste(layermodel.BeatModifierModel);
            Content.Paste(layermodel.ContentModel);
            Mask.Paste(layermodel.maskmodel);
            Coloration.Paste(layermodel.ColorationModel);
            PostFX.Paste(layermodel.PostFXModel);

            EnabledMessages();
        }
        #endregion
    }
}
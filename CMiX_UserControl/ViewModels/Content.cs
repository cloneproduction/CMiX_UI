using System;
using System.Windows;
using System.Windows.Input;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Content : ViewModel
    {
        #region CONSTRUCTORS
        public Content(Beat masterbeat, string layername, OSCMessenger messenger, ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            enable: true,
            messageaddress: String.Format("{0}/{1}/", layername, nameof(Content)),
            messenger: messenger,
            beatModifier: new BeatModifier(String.Format("{0}/{1}", layername, nameof(Content)), messenger, masterbeat, actionmanager),
            geometry: new Geometry(String.Format("{0}/{1}", layername, nameof(Content)), messenger, actionmanager),
            texture: new Texture(String.Format("{0}/{1}", layername, nameof(Content)), messenger, actionmanager),
            postFX: new PostFX(String.Format("{0}/{1}", layername, nameof(Content)), messenger, actionmanager)
        )
        {}

        public Content
            (
                ActionManager actionmanager,
                bool enable,
                string messageaddress,
                OSCMessenger messenger,
                BeatModifier beatModifier,
                Geometry geometry,
                Texture texture,
                PostFX postFX
            )
            : base(actionmanager, messenger)
        {
            Enable = enable;
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            PostFX = postFX ?? throw new ArgumentNullException(nameof(postFX));
            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }

        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

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
        public void CopySelf()
        {
            ContentDTO contentdto = new ContentDTO();
            this.Copy(contentdto);
            IDataObject data = new DataObject();
            data.SetData("Content", contentdto, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Content"))
            {
                var contentdto = (ContentDTO)data.GetData("Content") as ContentDTO;
                this.Paste(contentdto);
                Messenger.QueueObject(this);
                Messenger.SendQueue();
            }
        }

        public void ResetSelf()
        {
            ContentDTO contentdto = new ContentDTO();
            this.Paste(contentdto);
        }


        public void Copy(ContentDTO contentdto)
        {
            contentdto.Enable = Enable;
            BeatModifier.Copy(contentdto.BeatModifierDTO);
            Texture.Copy(contentdto.TextureDTO);
            Geometry.Copy(contentdto.GeometryDTO);
            PostFX.Copy(contentdto.PostFXDTO);
        }

        public void Paste(ContentDTO contentdto)
        {
            Messenger.SendEnabled = false;
            Enable = contentdto.Enable;
            BeatModifier.Paste(contentdto.BeatModifierDTO);
            Texture.Paste(contentdto.TextureDTO);
            Geometry.Paste(contentdto.GeometryDTO);
            PostFX.Paste(contentdto.PostFXDTO);
            Messenger.SendEnabled = true;
        }
        #endregion
    }
}
using System;
using CMiX.Services;
using CMiX.Models;
using System.Windows;
using System.Windows.Input;
using MonitoredUndo;

namespace CMiX.ViewModels
{
    public class Mask : ViewModel, IMessengerData
    {
        #region CONSTRUCTORS
        public Mask(Beat masterbeat, string layername, IMessenger messenger)
            : this(
                messenger: messenger,
                messageaddress: String.Format("{0}/{1}/", layername, nameof(Mask)),
                messageEnabled : true,
                enable: false,
                beatModifier: new BeatModifier(String.Format("{0}/{1}", layername, nameof(Mask)), messenger, masterbeat),
                geometry: new Geometry(String.Format("{0}/{1}", layername, nameof(Mask)), messenger),
                texture: new Texture(String.Format("{0}/{1}", layername, nameof(Mask)), messenger),
                postFX: new PostFX(String.Format("{0}/{1}", layername, nameof(Mask)), messenger))
        {}

        public Mask(
            IMessenger messenger,
            string messageaddress,
            bool messageEnabled,
            bool enable, 
            BeatModifier beatModifier, 
            Geometry geometry, 
            Texture texture, 
            PostFX postFX)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Enable = enable;
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
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

        public bool MessageEnabled { get; set; }

        private bool _enable;
        [OSC]
        public bool Enable
        {
            get => _enable;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, nameof(Enable), _enable, value, "Enabled Changed");
                SetAndNotify(ref _enable, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Enable), Enable);
            }
        }

        private bool _keeporiginal;
        [OSC]
        public bool KeepOriginal
        {
            get => _keeporiginal;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, nameof(KeepOriginal), _enable, value, "KeepOriginal Changed");
                SetAndNotify(ref _keeporiginal, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(KeepOriginal), KeepOriginal);
            }
        }

        public IMessenger Messenger { get; }

        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

        public BeatModifier BeatModifier { get; }

        public Geometry Geometry { get; }

        public Texture Texture { get; }

        public PostFX PostFX { get; }
        #endregion

        #region COPY/PASTE
        public void Copy(MaskDTO maskdto)
        {
            maskdto.Enable = Enable;
            maskdto.KeepOriginal = KeepOriginal;

            BeatModifier.Copy(maskdto.BeatModifierDTO);
            Texture.Copy(maskdto.TextureDTO);
            Geometry.Copy(maskdto.GeometryDTO);
            PostFX.Copy(maskdto.PostFXDTO);
        }

        public void Paste(MaskDTO maskdto)
        {
            MessageEnabled = false;

            Enable = maskdto.Enable;
            KeepOriginal = maskdto.KeepOriginal;

            BeatModifier.Paste(maskdto.BeatModifierDTO);
            Texture.Paste(maskdto.TextureDTO);
            Geometry.Paste(maskdto.GeometryDTO);
            PostFX.Paste(maskdto.PostFXDTO);

            MessageEnabled = true;
        }

        public void CopySelf()
        {
            MaskDTO maskdto = new MaskDTO();
            this.Copy(maskdto);
            IDataObject data = new DataObject();
            data.SetData("Mask", maskdto, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Mask"))
            {
                var maskdto = (MaskDTO)data.GetData("Mask") as MaskDTO;
                this.Paste(maskdto);

                Messenger.QueueObject(this);
                Messenger.SendQueue();
            }
        }

        public void ResetSelf()
        {
            MaskDTO maskdto = new MaskDTO();
            this.Paste(maskdto);
        }
        #endregion
    }
}
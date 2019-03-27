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
        public Mask(Beat masterbeat, string layername, ObservableCollection<OSCMessenger> messengers, Mementor mementor)
            : this
            (
                messengers: messengers,
                mementor: mementor,
                messageaddress: String.Format("{0}/{1}/", layername, nameof(Mask)),
                enable: false,
                beatModifier: new BeatModifier(String.Format("{0}/{1}", layername, nameof(Mask)), messengers, masterbeat, mementor),
                geometry: new Geometry(String.Format("{0}/{1}", layername, nameof(Mask)), messengers, mementor),
                texture: new Texture(String.Format("{0}/{1}", layername, nameof(Mask)), messengers, mementor),
                postFX: new PostFX(String.Format("{0}/{1}", layername, nameof(Mask)), messengers, mementor)
            )
        {}

        public Mask
            (
                ObservableCollection<OSCMessenger> messengers,
                Mementor mementor,
                string messageaddress,
                bool enable, 
                BeatModifier beatModifier, 
                Geometry geometry, 
                Texture texture, 
                PostFX postFX
            )
            : base (messengers)
        {  
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
            Enable = enable;
            MessageAddress = messageaddress;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            PostFX = postFX ?? throw new ArgumentNullException(nameof(postFX));
            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
            Mementor = mementor;
        }
        #endregion

        #region PROPERTIES
        private bool _enable;
        [OSC]
        public bool Enable
        {
            get => _enable;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, "Enable");
                SetAndNotify(ref _enable, value);
                SendMessages(MessageAddress + nameof(Enable), Enable);
            }
        }

        private bool _keeporiginal;
        [OSC]
        public bool KeepOriginal
        {
            get => _keeporiginal;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "KeepOriginal");
                SetAndNotify(ref _keeporiginal, value);
                SendMessages(MessageAddress + nameof(KeepOriginal), KeepOriginal);
            }
        }

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
            DisabledMessages();
            Enable = maskdto.Enable;
            KeepOriginal = maskdto.KeepOriginal;
            BeatModifier.Paste(maskdto.BeatModifierDTO);
            Texture.Paste(maskdto.TextureDTO);
            Geometry.Paste(maskdto.GeometryDTO);
            PostFX.Paste(maskdto.PostFXDTO);
            EnabledMessages();
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
                QueueObjects(this);
                SendQueues();
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
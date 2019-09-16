using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Mask : ViewModel
    {
        #region CONSTRUCTORS
        public Mask(Beat masterbeat, string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) 
            : base (oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Mask));
            MaskType = ((MaskType)0).ToString();
            MaskControlType = ((MaskControlType)0).ToString();
            Enable = false;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, oscvalidation, mementor);
            Geometry = new Geometry(MessageAddress, oscvalidation, mementor);
            Texture = new Texture(MessageAddress, oscvalidation, mementor);
            PostFX = new PostFX(MessageAddress, oscvalidation, mementor);

            CopyMaskCommand = new RelayCommand(p => CopyMask());
            PasteMaskCommand = new RelayCommand(p => PasteMask());
            ResetMaskCommand = new RelayCommand(p => ResetMask());
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
        public ICommand CopyMaskCommand { get; }
        public ICommand PasteMaskCommand { get; }
        public ICommand ResetMaskCommand { get; }

        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public PostFX PostFX { get; }

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

        private string _masktype;
        public string MaskType
        {
            get => _masktype;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(MaskType));
                SetAndNotify(ref _masktype, value);
                SendMessages(MessageAddress + nameof(MaskType), MaskType);
            }
        }

        private string _maskcontroltype;
        public string MaskControlType
        {
            get => _maskcontroltype;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(MaskControlType));
                SetAndNotify(ref _maskcontroltype, value);
                SendMessages(MessageAddress + nameof(MaskControlType), MaskControlType);
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(MaskModel maskmodel)
        {
            maskmodel.MessageAddress = MessageAddress;
            maskmodel.Enable = Enable;
            maskmodel.MaskType = MaskType;
            maskmodel.MaskControlType = MaskControlType;
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
            MaskType = maskmodel.MaskType;
            MaskControlType = maskmodel.MaskControlType;
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
        }

        public void CopyMask()
        {
            MaskModel maskmodel = new MaskModel();
            this.Copy(maskmodel);
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
                DisabledMessages();

                var maskmodel = data.GetData("MaskModel") as MaskModel;
                var maskmessageaddress = MessageAddress;
                this.Paste(maskmodel);
                UpdateMessageAddress(maskmessageaddress);

                this.Copy(maskmodel);
                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(maskmodel);
                SendQueues();
            }
        }

        public void ResetMask()
        {
            MaskModel maskmodel = new MaskModel();
            this.Reset();
            this.Copy(maskmodel);
            QueueObjects(maskmodel);
            SendQueues();
        }
        #endregion
    }
}
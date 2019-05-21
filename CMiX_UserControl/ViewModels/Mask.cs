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

        public Mask(Beat masterbeat, string layername, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base (oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}/{1}/", layername, nameof(Mask));
            BeatModifier = new BeatModifier(String.Format("{0}/{1}", layername, nameof(Mask)), oscmessengers, masterbeat, mementor);
            Geometry = new Geometry(String.Format("{0}/{1}", layername, nameof(Mask)), oscmessengers, mementor);
            Texture = new Texture(String.Format("{0}/{1}", layername, nameof(Mask)), oscmessengers, mementor);
            PostFX = new PostFX(String.Format("{0}/{1}", layername, nameof(Mask)), oscmessengers, mementor);
            Enable = false;
            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
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

        public void CopySelf()
        {
            MaskModel maskmodel = new MaskModel();
            this.Copy(maskmodel);
            IDataObject data = new DataObject();
            data.SetData("Mask", maskmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Mask"))
            {
                var maskmodel = (MaskModel)data.GetData("Mask") as MaskModel;
                this.Paste(maskmodel);
                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            MaskModel maskmodel = new MaskModel();
            this.Paste(maskmodel);
        }
        #endregion
    }
}
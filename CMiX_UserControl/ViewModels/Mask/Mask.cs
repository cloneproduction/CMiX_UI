using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class Mask : ViewModel, ISendableEntityContext, IUndoable
    {
        #region CONSTRUCTORS
        public Mask(Beat masterbeat, string messageaddress, Messenger messenger, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Mask));
            MaskType = ((MaskType)2).ToString();
            MaskControlType = ((MaskControlType)1).ToString();
            Enabled = false;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, messenger, mementor);
            Geometry = new Geometry(MessageAddress, messenger, mementor, masterbeat);
            Texture = new Texture(MessageAddress, messenger, mementor);
            PostFX = new PostFX(MessageAddress, messenger, mementor);

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

        private string _masktype;
        public string MaskType
        {
            get => _masktype;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(MaskType));
                SetAndNotify(ref _masktype, value);
                //SendMessages(MessageAddress + nameof(MaskType), MaskType);
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
                //SendMessages(MessageAddress + nameof(MaskControlType), MaskControlType);
            }
        }

        public ObservableCollection<Entity> Entities { get; set; }
        public Entity SelectedEntity { get; set; }
        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(MaskModel maskmodel)
        {
            maskmodel.MessageAddress = MessageAddress;
            maskmodel.Enable = Enabled;
            maskmodel.MaskType = MaskType;
            maskmodel.MaskControlType = MaskControlType;
            BeatModifier.Copy(maskmodel.BeatModifierModel);
            Texture.Copy(maskmodel.texturemodel);
            Geometry.Copy(maskmodel.GeometryModel);
            PostFX.Copy(maskmodel.PostFXModel);
        }

        public void Paste(MaskModel maskmodel)
        {
            Messenger.Disable();;

            MessageAddress = maskmodel.MessageAddress;
            Enabled = maskmodel.Enable;
            MaskType = maskmodel.MaskType;
            MaskControlType = maskmodel.MaskControlType;
            BeatModifier.Paste(maskmodel.BeatModifierModel);
            Texture.Paste(maskmodel.texturemodel);
            Geometry.Paste(maskmodel.GeometryModel);
            PostFX.Paste(maskmodel.PostFXModel);

            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();;

            Enabled = false;
            BeatModifier.Reset();
            Geometry.Reset();
            Texture.Reset();
            PostFX.Reset();

            Messenger.Enable();
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
                Messenger.Disable();;

                var maskmodel = data.GetData("MaskModel") as MaskModel;
                var maskmessageaddress = MessageAddress;
                this.Paste(maskmodel);
                UpdateMessageAddress(maskmessageaddress);

                this.Copy(maskmodel);
                Messenger.Enable();
                Mementor.EndBatch();
                //this.SendMessages(nameof(MaskModel), maskmodel);
            }
        }

        public void ResetMask()
        {
            MaskModel maskmodel = new MaskModel();
            this.Reset();
            this.Copy(maskmodel);
            //this.SendMessages(nameof(MaskModel), maskmodel);
        }
        #endregion
    }
}
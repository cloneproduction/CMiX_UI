using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class Mask : ViewModel, IEntityContext
    {
        #region CONSTRUCTORS
        public Mask(Beat beat, string messageAddress, Sender sender, Mementor mementor) 
        {
            Beat = beat;
            MessageAddress = $"{messageAddress}{nameof(Mask)}/";
            Sender = sender;
            MaskType = ((MaskType)2).ToString();
            MaskControlType = ((MaskControlType)1).ToString();
            Enabled = false;

            BeatModifier = new BeatModifier(MessageAddress, beat, sender, mementor);
            Geometry = new Geometry(MessageAddress, sender, mementor, beat);
            Texture = new Texture(MessageAddress, sender, mementor);
            PostFX = new PostFX(MessageAddress, sender, mementor);

            CopyMaskCommand = new RelayCommand(p => CopyMask());
            PasteMaskCommand = new RelayCommand(p => PasteMask());
            ResetMaskCommand = new RelayCommand(p => ResetMask());
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
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
        public MasterBeat MasterBeat { get; set; }
        public Assets Assets { get; set; }
        public Beat Beat { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyModel(MaskModel maskModel)
        {
            maskModel.Enable = Enabled;
            maskModel.MaskType = MaskType;
            maskModel.MaskControlType = MaskControlType;
            BeatModifier.CopyModel(maskModel.BeatModifierModel);
            Texture.CopyModel(maskModel.TextureModel);
            Geometry.Copy(maskModel.GeometryModel);
            PostFX.CopyModel(maskModel.PostFXModel);
        }

        public void PasteModel(MaskModel maskModel)
        {
            Sender.Disable();

            Enabled = maskModel.Enable;
            MaskType = maskModel.MaskType;
            MaskControlType = maskModel.MaskControlType;
            BeatModifier.PasteModel(maskModel.BeatModifierModel);
            Texture.PasteModel(maskModel.TextureModel);
            Geometry.Paste(maskModel.GeometryModel);
            PostFX.PasteModel(maskModel.PostFXModel);

            Sender.Enable();
        }

        public void Reset()
        {
            Sender.Disable();

            Enabled = false;
            BeatModifier.Reset();
            Geometry.Reset();
            Texture.Reset();
            PostFX.Reset();

            Sender.Enable();
        }

        public void CopyMask()
        {
            MaskModel maskmodel = new MaskModel();
            this.CopyModel(maskmodel);
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
                Sender.Disable();;

                var maskmodel = data.GetData("MaskModel") as MaskModel;
                var maskmessageaddress = MessageAddress;
                this.PasteModel(maskmodel);

                this.CopyModel(maskmodel);
                Sender.Enable();
                Mementor.EndBatch();
                //this.SendMessages(nameof(MaskModel), maskmodel);
            }
        }

        public void ResetMask()
        {
            MaskModel maskmodel = new MaskModel();
            this.Reset();
            this.CopyModel(maskmodel);
            //this.SendMessages(nameof(MaskModel), maskmodel);
        }
        #endregion
    }
}
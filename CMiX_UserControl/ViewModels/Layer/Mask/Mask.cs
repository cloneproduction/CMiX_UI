using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class Mask : Component, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Mask(int id, Beat beat, string messageAddress, MessageService messageService, Mementor mementor)
            : base(id, beat, messageAddress, messageService, mementor)
        {
            MaskType = ((MaskType)2).ToString();
            MaskControlType = ((MaskControlType)1).ToString();
            
            Name = "Mask";

            BeatModifier = new BeatModifier(MessageAddress, beat, messageService, mementor);
            Geometry = new Geometry(MessageAddress, messageService, mementor, beat);
            Texture = new Texture(MessageAddress, messageService, mementor);
            PostFX = new PostFX(MessageAddress, messageService, mementor);

            //CopyMaskCommand = new RelayCommand(p => CopyMask());
            //PasteMaskCommand = new RelayCommand(p => PasteMask());
            //ResetMaskCommand = new RelayCommand(p => ResetMask());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyMaskCommand { get; }
        public ICommand PasteMaskCommand { get; }
        public ICommand ResetMaskCommand { get; }
        public ICommand VisibilityCommand { get; }

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
        #endregion

        #region COPY/PASTE/RESET

        public override void SetViewModel(IComponentModel model)
        {
            var maskModel = model as MaskModel;
            MessageService.Disable();

            Enabled = maskModel.Enable;
            MaskType = maskModel.MaskType;
            MaskControlType = maskModel.MaskControlType;

            BeatModifier.SetViewModel(maskModel.BeatModifierModel);
            Texture.SetViewModel(maskModel.TextureModel);
            Geometry.Paste(maskModel.GeometryModel);
            PostFX.SetViewModel(maskModel.PostFXModel);

            MessageService.Enable();
        }

        //public void Reset()
        //{
        //    MessageService.Disable();

        //    Enabled = false;
        //    BeatModifier.Reset();
        //    Geometry.Reset();
        //    Texture.Reset();
        //    PostFX.Reset();

        //    MessageService.Enable();
        //}

        //public void CopyMask()
        //{
        //    IDataObject data = new DataObject();
        //    data.SetData("MaskModel", GetModel(), false);
        //    Clipboard.SetDataObject(data);
        //}

        //public void PasteMask()
        //{
        //    IDataObject data = Clipboard.GetDataObject();
        //    if (data.GetDataPresent("MaskModel"))
        //    {
        //        Mementor.BeginBatch();
        //        MessageService.Disable();;

        //        var maskmodel = data.GetData("MaskModel") as MaskModel;
        //        this.SetViewModel(maskmodel);
        //        MessageService.Enable();
        //        Mementor.EndBatch();
        //        //this.SendMessages(nameof(MaskModel), GetModel());
        //    }
        //}

        //public void ResetMask()
        //{
        //    this.Reset();
        //    //this.SendMessages(nameof(MaskModel), GetModel());
        //}
        #endregion
    }
}
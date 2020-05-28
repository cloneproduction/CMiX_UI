using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.MVVM.ViewModels
{
    public class Mask : Component
    {
        public Mask(int id, Beat beat)
            : base(id, beat)
        {
            MaskType = ((MaskType)2).ToString();
            MaskControlType = ((MaskControlType)1).ToString();
            
            Name = "Mask";

            BeatModifier = new BeatModifier(beat);
            Geometry = new Geometry(beat);
            Texture = new Texture();
            PostFX = new PostFX();

            //CopyMaskCommand = new RelayCommand(p => CopyMask());
            //PasteMaskCommand = new RelayCommand(p => PasteMask());
            //ResetMaskCommand = new RelayCommand(p => ResetMask());
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (this.GetMessageAddress() == e.MessageAddress)
            {
                this.SetViewModel(e.Model as MaskModel);
                Console.WriteLine("Mask Updated");
            }
        }

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
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, nameof(MaskType));
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
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, nameof(MaskControlType));
                SetAndNotify(ref _maskcontroltype, value);
                //SendMessages(MessageAddress + nameof(MaskControlType), MaskControlType);
            }
        }
        #endregion

        #region COPY/PASTE/RESET

        //public void Reset()
        //{
        //    MessengerService.Disable();

        //    Enabled = false;
        //    BeatModifier.Reset();
        //    Geometry.Reset();
        //    Texture.Reset();
        //    PostFX.Reset();

        //    MessengerService.Enable();
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
        //        MessengerService.Disable();;

        //        var maskmodel = data.GetData("MaskModel") as MaskModel;
        //        this.SetViewModel(maskmodel);
        //        MessengerService.Enable();
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
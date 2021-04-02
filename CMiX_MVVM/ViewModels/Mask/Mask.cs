using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Mask : MessageCommunicator
    {
        public Mask(IMessageDispatcher messageDispatcher, MaskModel maskModel) 
        {
            MaskType = maskModel.MaskType;// ((MaskType)2).ToString();
            MaskControlType = maskModel.MaskControlType;// ((MaskControlType)1).ToString();
            Enabled = maskModel.Enabled;
        }

        private bool _IsMask;
        public bool IsMask
        {
            get => _IsMask;
            set
            {
                SetAndNotify(ref _IsMask, value);
                RaiseMessageNotification();
            }
        }


        private string _masktype;
        public string MaskType
        {
            get => _masktype;
            set
            {
                SetAndNotify(ref _masktype, value);
                RaiseMessageNotification();
            }
        }

        private string _maskcontroltype;
        public string MaskControlType
        {
            get => _maskcontroltype;
            set
            {
                SetAndNotify(ref _maskcontroltype, value);
                RaiseMessageNotification();
            }
        }

        public override void SetViewModel(IModel model)
        {
            MaskModel maskModel = model as MaskModel;
            this.IsMask = maskModel.IsMask;
            this.MaskType = maskModel.MaskType;
            this.MaskControlType = maskModel.MaskControlType;
        }

        public override IModel GetModel()
        {
            MaskModel maskModel = new MaskModel();

            maskModel.IsMask = this.IsMask;
            maskModel.MaskType = this.MaskType;
            maskModel.MaskControlType = this.MaskControlType;

            return maskModel;
        }
    }
}
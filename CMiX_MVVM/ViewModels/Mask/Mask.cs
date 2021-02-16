using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class Mask : Sender
    {
        public Mask(string name, IMessageProcessor parentSender) : base (name, parentSender)
        {
            MaskType = ((MaskType)2).ToString();
            MaskControlType = ((MaskControlType)1).ToString();
            Enabled = false;
        }

        private bool _IsMask;
        public bool IsMask
        {
            get => _IsMask;
            set
            {
                SetAndNotify(ref _IsMask, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
            }
        }


        private string _masktype;
        public string MaskType
        {
            get => _masktype;
            set
            {
                SetAndNotify(ref _masktype, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
            }
        }

        private string _maskcontroltype;
        public string MaskControlType
        {
            get => _maskcontroltype;
            set
            {
                SetAndNotify(ref _maskcontroltype, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
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
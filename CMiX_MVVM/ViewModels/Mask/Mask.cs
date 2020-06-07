using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Mask : Sendable
    {
        public Mask()
        {
            MaskType = ((MaskType)2).ToString();
            MaskControlType = ((MaskControlType)1).ToString();
            Enabled = false;
        }

        public Mask(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as MaskModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        private bool _IsMask;
        public bool IsMask
        {
            get => _IsMask;
            set
            {
                SetAndNotify(ref _IsMask, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }


        private string _masktype;
        public string MaskType
        {
            get => _masktype;
            set
            {
                SetAndNotify(ref _masktype, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private string _maskcontroltype;
        public string MaskControlType
        {
            get => _maskcontroltype;
            set
            {
                SetAndNotify(ref _maskcontroltype, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }
    }
}
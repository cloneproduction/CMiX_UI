using System;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using PubSub;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Sender : ViewModel
    {
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public virtual string GetMessageAddress()
        {
            return $"{this.GetType().Name}/";
        }

        public event EventHandler<ModelEventArgs> ReceiveChangeEvent;
        public void OnReceiveChange(IModel model, string messageAddress, string parentMessageAddress)
        {
            ReceiveChangeEvent?.Invoke(this, new ModelEventArgs(model, messageAddress, parentMessageAddress));
        }

        public virtual void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }


        public event EventHandler<ModelEventArgs> SendChangeEvent;
        public void OnSendChange(IModel model, string messageAddress)
        {
            SendChangeEvent?.Invoke(this, new ModelEventArgs(model, messageAddress, string.Empty));
        }

        public virtual void OnChildPropertyToSendChange(object sender, ModelEventArgs e)
        {
            OnSendChange(e.Model, GetMessageAddress() + e.MessageAddress);
        }

        public void SubscribeToEvent(Sender SenderParent)
        {
            this.SendChangeEvent += SenderParent.OnChildPropertyToSendChange;
            SenderParent.ReceiveChangeEvent += this.OnParentReceiveChange;
        }

        public void UnSubscribeToEvent(Sender SenderParent)
        {
            this.SendChangeEvent -= SenderParent.OnChildPropertyToSendChange;
            SenderParent.ReceiveChangeEvent -= this.OnParentReceiveChange;
        }
    }
}
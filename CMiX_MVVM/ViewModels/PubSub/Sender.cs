using System;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Sender : ViewModel, IPublisher
    {


        public virtual string GetMessageAddress()
        {
            return $"{this.GetType().Name}/";
        }

        ///RECEIVING

        public event EventHandler<ModelEventArgs> ReceiveChangeEvent;
        public void OnReceiveChange(IModel model, string messageAddress, string parentMessageAddress)
        {
            ReceiveChangeEvent?.Invoke(this, new ModelEventArgs(model, messageAddress, parentMessageAddress));
        }

        public virtual void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }




        /// SENDING

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
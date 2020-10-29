using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Sendable : ViewModel
    {
        public virtual string GetMessageAddress()
        {
            return $"{this.GetType().Name}/";
        }

        public event EventHandler<ModelEventArgs> ReceiveChangeEvent;
        public void OnReceiveChange(IModel model, string messageAddress, string parentMessageAddress)
        {
            ReceiveChangeEvent?.Invoke(this, new ModelEventArgs(model, messageAddress, parentMessageAddress));
        }

        public abstract void OnParentReceiveChange(object sender, ModelEventArgs e);




        public event EventHandler<ModelEventArgs> SendChangeEvent;
        public void OnSendChange(IModel model, string messageAddress)
        {
            SendChangeEvent?.Invoke(this, new ModelEventArgs(model, messageAddress, string.Empty));
        }

        public void OnChildPropertyToSendChange(object sender, ModelEventArgs e)
        {
            OnSendChange(e.Model, GetMessageAddress() + e.MessageAddress);
        }

        public void SubscribeToEvent(Sendable sendableParent)
        {
            this.SendChangeEvent += sendableParent.OnChildPropertyToSendChange;
            sendableParent.ReceiveChangeEvent += this.OnParentReceiveChange;
        }

        public void UnSubscribeToEvent(Sendable sendableParent)
        {
            this.SendChangeEvent -= sendableParent.OnChildPropertyToSendChange;
            sendableParent.ReceiveChangeEvent -= this.OnParentReceiveChange;
        }


    }
}
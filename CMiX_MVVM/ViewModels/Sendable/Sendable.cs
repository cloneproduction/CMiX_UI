using System;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Sendable : ViewModel, IHandler
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


        public IHandler NextHandler;
        public IHandler SetNext(IHandler handler)
        {
            this.NextHandler = handler;
            return handler;
        }

        public virtual object Handle(object request)
        {
            if(this.NextHandler != null)
            {
                return this.NextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}
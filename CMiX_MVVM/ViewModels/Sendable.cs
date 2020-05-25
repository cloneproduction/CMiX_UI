﻿using System;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Sendable : ViewModel
    {
        public virtual string GetMessageAddress()
        {
            return $"{this.GetType().Name}/";
        }

        public event EventHandler<ModelEventArgs> ReceiveChangeEvent;
        public void OnReceiveChange(IModel model, string messageAddress)
        {
            ReceiveChangeEvent?.Invoke(this, new ModelEventArgs(model, messageAddress));
        }

        public void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            Console.WriteLine("OnParentReceiveChange" + " Address " + e.MessageAddress + " Data " + e.Model.GetType().Name);
            OnReceiveChange(e.Model, GetMessageAddress() + e.MessageAddress);
        }


        public event EventHandler<ModelEventArgs> SendChangeEvent;
        public void OnSendChange(IModel model, string messageAddress)
        {
            SendChangeEvent?.Invoke(this, new ModelEventArgs(model, messageAddress));
        }

        public void OnChildPropertyToSendChange(object sender, ModelEventArgs e)
        {
            OnSendChange(e.Model, GetMessageAddress() + e.MessageAddress);
        }
    }
}
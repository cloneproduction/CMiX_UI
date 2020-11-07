using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Network
{
    public class ViewModelNetWork : ViewModel, IPublisher, ISubscriber
    {
        public event EventHandler<ModelEventArgs> SendChangeEvent;
        public event EventHandler<ModelEventArgs> ReceiveChangeEvent;

        public string GetMessageAddress()
        {
            throw new NotImplementedException();
        }



        public void OnChildPropertyToSendChange(object sender, ModelEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            throw new NotImplementedException();
        }



        public void OnReceiveChange(IModel model, string messageAddress, string parentMessageAddress)
        {
            throw new NotImplementedException();
        }

        public void OnSendChange(IModel model, string messageAddress)
        {
            throw new NotImplementedException();
        }
    }
}

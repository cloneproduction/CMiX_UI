using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    interface ISubscriber
    {
        event EventHandler<ModelEventArgs> ReceiveChangeEvent;

        void OnReceiveChange(IModel model, string messageAddress, string parentMessageAddress);

        void OnParentReceiveChange(object sender, ModelEventArgs e);
    }
}

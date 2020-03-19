using CMiX.Studio.ViewModels;
using CMiX.ViewModels;
using CMiX.Views;
using MvvmDialogs;
using MvvmDialogs.DialogFactories;
using MvvmDialogs.FrameworkDialogs;
using MvvmDialogs.FrameworkDialogs.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class CustomFrameworkDialogFactory : DefaultFrameworkDialogFactory, IDialogFactory
    {
        public IWindow Create(Type dialogType)
        {
            return new CustomDialog();
        }

        //public override IMessageBox CreateMessageBox(MessageBoxSettings settings)
        //{
        //    return new CustomMessageBox();
        //}
    }
}

using CMiX.ViewModels;
using CMiX.Views;
using MvvmDialogs;
using MvvmDialogs.DialogFactories;
using MvvmDialogs.FrameworkDialogs;
using System;

namespace CMiX.Studio.ViewModels
{
    public class CustomFrameworkDialogFactory : DefaultFrameworkDialogFactory, IDialogFactory
    {
        public IWindow Create(Type dialogType)
        {
            if (dialogType == typeof(MessengerSettingsWindow))
                return new MessageSettingsDialog();
            else
                return new CustomDialog();

        }

        //public override IMessageBox CreateMessageBox(MessageBoxSettings settings)
        //{
        //    return new CustomMessageBox();
        //}
    }
}

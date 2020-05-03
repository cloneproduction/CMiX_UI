using CMiX.Studio.Views;
using CMiX.Views;
using MvvmDialogs.DialogTypeLocators;
using System;
using System.ComponentModel;

namespace CMiX.Studio.ViewModels
{
    public class CustomTypeLocator : IDialogTypeLocator
    {
        public Type Locate(INotifyPropertyChanged viewModel)
        {
            
            //if (viewModel is ModalDialog)
            //    return typeof(CustomWindowDialog);

            if (viewModel is MessengerSettings)
            {
                Console.WriteLine("MessengerSettings");
                return typeof(MessengerSettingsWindow);
            }

            else
                throw new Exception("Dialog type is not defined.");
        }
    }
}

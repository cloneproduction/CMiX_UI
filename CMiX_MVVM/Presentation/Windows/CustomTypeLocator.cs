﻿
using CMiX.Core.Presentation.Views;
using CMiX.Studio.ViewModels;
using MvvmDialogs.DialogTypeLocators;
using System;
using System.ComponentModel;

namespace CMiX.Core.Presentation.ViewModels
{
    public class CustomTypeLocator : IDialogTypeLocator
    {
        public Type Locate(INotifyPropertyChanged viewModel)
        {
            if (viewModel is Messenger)
                return typeof(MessengerSettingsWindow);

            else if (viewModel is ModalDialog)
                return typeof(CustomWindowDialog);

            else
                throw new Exception("Dialog type is not defined.");
        }
    }
}
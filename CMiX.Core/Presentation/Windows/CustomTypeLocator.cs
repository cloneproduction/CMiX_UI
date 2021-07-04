﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.ComponentModel;
using CMiX.Core.Presentation.ViewModels.Network;
using CMiX.Core.Presentation.Views;
using MvvmDialogs.DialogTypeLocators;

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

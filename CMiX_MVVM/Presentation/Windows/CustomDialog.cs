﻿using System.Windows;
using System.Windows.Controls;
using CMiX.Core.Presentation.Views;
using MvvmDialogs;

namespace CMiX.Core.Presentation.ViewModels
{
    public class CustomDialog : IWindow
    {
        private readonly CustomWindowDialog dialog;

        public CustomDialog()
        {
            dialog = new CustomWindowDialog();
        }

        object IWindow.DataContext
        {
            get => dialog.DataContext;
            set => dialog.DataContext = value;
        }

        bool? IWindow.DialogResult
        {
            get => dialog.DialogResult;
            set => dialog.DialogResult = value;
        }

        ContentControl IWindow.Owner
        {
            get => dialog.Owner;
            set => dialog.Owner = (Window)value;
        }

        bool? IWindow.ShowDialog()
        {
            return dialog.ShowDialog();
        }

        void IWindow.Show()
        {
            dialog.Show();
        }
    }
}
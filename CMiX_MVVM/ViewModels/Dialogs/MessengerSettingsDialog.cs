using MvvmDialogs;
using System;
using System.Windows;
using System.Windows.Controls;
using CMiX.MVVM.Views;

namespace CMiX.MVVM.ViewModels
{
    public class MessageSettingsDialog : IWindow
        {
            private readonly MessengerSettingsWindow dialog;

            public MessageSettingsDialog()
            {
                dialog = new MessengerSettingsWindow();
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

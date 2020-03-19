using CMiX.MVVM.ViewModels;
using MvvmDialogs;
using System;
using System.Windows;

namespace CMiX.Studio.ViewModels
{
    public class MessageTextViewModel : ViewModel
    {
        private readonly IDialogService dialogService;

        public MessageTextViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        private void ShowMessageBox()
        {
            dialogService.ShowMessageBox(
                this,
                "This is the text.",
                "This Is The Caption",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Information);
        }
    }
}

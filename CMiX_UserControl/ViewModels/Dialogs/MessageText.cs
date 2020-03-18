using CMiX.MVVM.ViewModels;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CMiX.Studio.ViewModels
{
    public class MessageText : ViewModel, IModalDialogViewModel
    {
        private readonly IDialogService dialogService;

        public MessageText(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public bool? DialogResult => throw new NotImplementedException();

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

using CMiX.MVVM.ViewModels;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class DialogTest : ViewModel
    {
        private readonly IDialogService dialogService;

        public DialogTest(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }


        private void ShowDialog()
        {
            var dialogViewModel = new AddTextDialog();

            bool? success = dialogService.ShowDialog(this, dialogViewModel);
            if (success == true)
            {
                //Texts.Add(dialogViewModel.Text);
            }
        }
    }
}

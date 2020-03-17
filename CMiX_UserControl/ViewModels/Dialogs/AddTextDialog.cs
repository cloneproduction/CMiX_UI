using CMiX.MVVM.ViewModels;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class AddTextDialog : ViewModel, IModalDialogViewModel
    {
        public AddTextDialog()
        {
            OkCommand = new RelayCommand(p => Ok());
        }

        public ICommand OkCommand { get; }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetAndNotify(ref _text, value);
        }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get => _dialogResult;
            set => SetAndNotify(ref _dialogResult, value);
        }

        //public bool? DialogResult
        //{
        //    get => dialogResult;
        //    private set => Set(nameof(DialogResult), ref dialogResult, value);
        //}

        private void Ok()
        {
            if (!string.IsNullOrEmpty(Text))
            {
                DialogResult = true;
            }
        }
    }
}

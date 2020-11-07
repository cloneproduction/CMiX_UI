using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using MvvmDialogs;

namespace CMiX.MVVM.ViewModels
{
    public class ModalDialog : ViewModel, IModalDialogViewModel
    {
        public ModalDialog()
        {
            SaveCommand = new RelayCommand(p => Save(p));
            DontSaveCommand = new RelayCommand(p => DontSave(p));
            CancelCommand = new RelayCommand(p => Cancel(p));
        }

        public bool? DialogResult { get; set; }

        public bool SaveProject { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand DontSaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public void Save(object obj)
        {
            if(obj is Window)
            {
                DialogResult = true;
                SaveProject = true;
                ((Window)obj).Close();
            }
        }

        public void DontSave(object obj)
        {
            if (obj is Window)
            {
                DialogResult = true;
                SaveProject = false;
                ((Window)obj).Close();
            }
        }

        public void Cancel(object obj)
        {
            DialogResult = false;
            ((Window)obj).Close();
        }
    }
}

using CMiX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace CMiX.ViewModels
{
    public class CopyPasteReset : ViewModel
    {
        public CopyPasteReset()
        {

            //CopyCommand = new RelayCommand(p => Copy());
            //PasteCommand = new RelayCommand(p => Paste());
            //ResetCommand = new RelayCommand(p => Reset());
        }

        public ICommand CopyCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand ResetCommand { get; }

    }
}

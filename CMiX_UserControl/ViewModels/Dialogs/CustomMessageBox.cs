using CMiX.MVVM.ViewModels;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CMiX.Studio.ViewModels
{
    public class CustomMessageBox : ViewModel
    {
        public CustomMessageBox()
        {
            Text = "Custom POUET POUET";
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}

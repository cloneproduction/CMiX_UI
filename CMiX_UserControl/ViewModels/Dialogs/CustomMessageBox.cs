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
    public class CustomMessageBox : IWindow
    {
        public CustomMessageBox()
        {

        }

        public object DataContext { get; set; }
        public bool? DialogResult { get; set; }
        public ContentControl Owner { get; set; }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public bool? ShowDialog()
        {
            throw new NotImplementedException();
        }

        //public MessageBoxResult Show(Window owner)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

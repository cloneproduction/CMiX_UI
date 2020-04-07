using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CMiX.MVVM.Themes
{
    public partial class MainWindow : ResourceDictionary
    {
        public MainWindow()
        {
            InitializeComponent();
        }
     
        //private void CloseClick(object sender, RoutedEventArgs e)
        //{
        //    var window = (Window)((FrameworkElement)sender).TemplatedParent;
        //    window.Close();
        //}
    
        //private void MaximizeRestoreClick(object sender, RoutedEventArgs e)
        //{
        //    var window = (Window)((FrameworkElement)sender).TemplatedParent;
        //    if (window.WindowState == System.Windows.WindowState.Normal)
        //        window.WindowState = System.Windows.WindowState.Maximized;
        //    else
        //        window.WindowState = System.Windows.WindowState.Normal;
        //}
    
        //private void MinimizeClick(object sender, RoutedEventArgs e)
        //{
        //    var window = (Window)((FrameworkElement)sender).TemplatedParent;
        //    window.WindowState = System.Windows.WindowState.Minimized;
        //}
    }
}

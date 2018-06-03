using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CMiX
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ChannelEnable : UserControl
    {
        public ChannelEnable()
        {
            InitializeComponent();
        }

        private void Channel_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("UIElement"))
            {
                this.CheckLayer.IsChecked = true;
            }

        }
        private void Channel_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void ChannelLayer_Loaded(object sender, RoutedEventArgs e)
        {
            //Ch1.IsChecked = true;
        }

        private void Ch_Control_Loaded(object sender, RoutedEventArgs e)
        {
            this.CheckLayer.IsChecked = true;
            //EnabledOSC = true;
        }
    }
}

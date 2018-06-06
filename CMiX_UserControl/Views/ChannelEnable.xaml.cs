using System.Windows;
using System.Windows.Controls;

namespace CMiX
{
    public partial class ChannelEnable : UserControl
    {
        public ChannelEnable()
        {
            InitializeComponent();
        }

        private void Channel_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DragOverButton(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("UIElement"))
            {
                CheckLayer.IsChecked = true;
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
            CheckLayer.IsChecked = true;
            //EnabledOSC = true;
        }
    }
}

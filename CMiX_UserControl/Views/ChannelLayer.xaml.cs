using System.Windows.Controls;

namespace CMiX.Views
{
    public partial class ChannelLayer : UserControl
    {
        public ChannelLayer()
        {
            InitializeComponent();
        }

        private void RadioButton_DragOver(object sender, System.Windows.DragEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            rb.IsChecked = true;
        }
    }
}
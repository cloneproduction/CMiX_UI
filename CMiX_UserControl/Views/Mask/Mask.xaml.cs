using System.Windows.Controls;

namespace CMiX.Studio.Views
{
    public partial class Mask : UserControl
    {
        public Mask()
        {
            InitializeComponent();
        }

        private void TabItem_DragOver(object sender, System.Windows.DragEventArgs e)
        {
            TabItem tabitem = sender as TabItem;
            tabitem.IsSelected = true;
        }
    }
}

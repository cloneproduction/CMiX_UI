using System.Windows;
using System.Windows.Controls;

namespace CMiX.Views
{
    public partial class Content : UserControl
    {
        public Content()
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

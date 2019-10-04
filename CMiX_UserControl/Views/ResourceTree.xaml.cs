using CMiX.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace CMiX.Views
{
    public partial class TreeView : UserControl
    {
        public TreeView()
        {
            InitializeComponent();
        }

        private void treeView_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem tvi = sender as TreeViewItem;
            Item cvm = tvi.DataContext as Item;
            if (cvm != null)
            {
                cvm.IsSelected = true;
            }
            //else
            //{
            //    SceneViewModel svm = tvi.DataContext as SceneViewModel;
            //    if (svm != null)
            //        svm.IsSelected = true;
            //}
        }


        //protected override void treeView_PreviewMouseRightButtonDown(MouseButtonEventArgs e)
        //{

        //    e.Handled = true;
        //    base.OnMouseDoubleClick(e);
        //}

        //protected override void OnPreviewMouseUp (MouseButtonEventArgs e)
        //{
        //    base.OnPreviewMouseUp(e);
        //    e.Handled = true;
        //}
    }
}
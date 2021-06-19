using System.Windows.Controls;
using System.Windows.Input;

namespace CMiX.Core.Presentation.Controls
{
    public class TreeViewSelect : TreeView
    {
        public TreeViewSelect()
        {

        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseRightButtonDown(e);
            Keyboard.ClearFocus();
        }
    }
}

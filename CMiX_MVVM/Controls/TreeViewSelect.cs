using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CMiX.MVVM.Controls
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

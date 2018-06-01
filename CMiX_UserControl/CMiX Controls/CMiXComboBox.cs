using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CMiX
{
    class CMiXComboBox : ComboBox
    {
        public CMiXComboBox()
        {
            IsTextSearchEnabled = false;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                if (this.IsDropDownOpen == false)
                {
                    this.IsDropDownOpen = true;
                }
                else if (this.IsDropDownOpen == true)
                {
                    this.IsDropDownOpen = false;
                }
                e.Handled = true;
            }
            if (e.Key == Key.W && this.IsDropDownOpen == true)
            {
                this.SelectedIndex -= 1;
                e.Handled = true;
            }

            if (e.Key == Key.S && this.IsDropDownOpen == true)
            {
                this.SelectedIndex += 1;
                e.Handled = true;
            }

        }
    }

    class CMiXComboBoxItem : ComboBoxItem
    {
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {

            if (this.IsFocused)
            {
                this.IsSelected = true;
            }
            if(e.Key == Key.F && this.IsFocused == true)
            {
                this.IsSelected = true;
            }

            base.OnPreviewKeyDown(e);
            e.Handled = true;
        }   
    }
}

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
                if (IsDropDownOpen == false)
                {
                    IsDropDownOpen = true;
                }
                else if (IsDropDownOpen == true)
                {
                    IsDropDownOpen = false;
                }
                e.Handled = true;
            }
            if (e.Key == Key.W && IsDropDownOpen == true)
            {
                SelectedIndex -= 1;
                e.Handled = true;
            }

            if (e.Key == Key.S && IsDropDownOpen == true)
            {
                SelectedIndex += 1;
                e.Handled = true;
            }

        }
    }

    class CMiXComboBoxItem : ComboBoxItem
    {
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {

            if (IsFocused)
            {
                IsSelected = true;
            }
            if(e.Key == Key.F && IsFocused == true)
            {
                IsSelected = true;
            }

            base.OnPreviewKeyDown(e);
            e.Handled = true;
        }   
    }
}

using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CMiX.MVVM.Controls.ColorPicker
{
    public class ColorHex : TextBox
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Enter)
            {
                BindingExpression bindingExpression = BindingOperations.GetBindingExpression(this, TextProperty);
                if (bindingExpression != null)
                    bindingExpression.UpdateSource();
            }
        }
    }
}
// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CMiX.Core.Presentation.Controls
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
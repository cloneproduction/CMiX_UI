// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Windows;
using System.Windows.Controls.Primitives;

namespace CMiX.Core.Presentation.Controls
{
    public class CMiXToggleButton : ToggleButton
    {
        public static readonly DependencyProperty DockProperty =
        DependencyProperty.Register("Dock", typeof(ToggleDockPosition), typeof(CMiXSlider), new UIPropertyMetadata(ToggleDockPosition.Right));
        public ToggleDockPosition Dock
        {
            get { return (ToggleDockPosition)GetValue(DockProperty); }
            set { SetValue(DockProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption", typeof(string), typeof(CMiXToggleButton), new PropertyMetadata(""));
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }
    }
}

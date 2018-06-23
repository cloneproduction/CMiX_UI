using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CMiX.Views
{
    public partial class Geometry : UserControl
    {
        public Geometry()
        {
            InitializeComponent();
        }

        private void Popup_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Popup popup = sender as Popup;
            if (popup.IsOpen)
            {
                popup.IsOpen = false;
            }
        }

        private void Toggle_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ToggleButton toggle = sender as ToggleButton;
            if (toggle.IsChecked == true)
            {
                toggle.IsChecked = false;
            }
        }
    }
}
